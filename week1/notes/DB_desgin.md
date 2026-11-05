# Database Design Basics: Normalization and Indexes

The two topics fit together. Normalization decides how your data is split across tables so it stays correct. Indexes make the resulting joins and lookups fast.

## Part 1: Normalization

- Why it exists: anomalies

- Imagine one flat table where every row is an order line:

```sql
CREATE TABLE orders_flat (
  order_id       int,
  order_date     date,
  customer_id    int,
  customer_name  text,
  customer_email text,
  customer_city  text,
  product_id     int,
  product_name   text,
  product_price  numeric,
  quantity       int
);
```

- Because facts are repeated across rows, three problems appear:

1. Update anomaly: a customer changes email. You must update every row they appear in. Miss one and the database now holds two contradictory “truths”.
2. Insert anomaly: you can’t record a new product until someone orders it, because there is no row to put it in.
3. Delete anomaly: delete a customer’s only order and you also lose the fact that the customer exists.

- `Normalization is the process of restructuring tables so each fact is stored once, in one place.`

### The key idea: functional dependencies

- Normal forms are all about functional dependencies (FDs). A → B means that if you know A, B is determined. For example:
  - `customer_id → customer_name, customer_email, customer_city`
  - `product_id → product_name, product_price`
  - `order_id → order_date, customer_id`
  - `(order_id, product_id) → quantity`

- Normal forms are rules about which FDs are allowed in a table.

### First Normal Form (1NF): atomic values, no repeating groups

Rules:

1. Each column holds a single atomic value.
2. No repeating groups (no phone1, phone2, phone3 columns, no comma-separated lists).
3. Rows are uniquely identifiable (there is a primary key).

- Violation:

```sql
-- BAD: multi-valued cell
order_id | products
1        | 'pen, notebook, stapler'
```

- You can’t query “which orders contain a pen?” without string-matching hacks, and you can’t enforce a foreign key on a comma list.

- Fix: one row per fact.

```sql
order_id | product
1        | pen
1        | notebook
1        | stapler
```

### Second Normal Form (2NF): no partial dependencies

- Applies only to tables with a composite primary key. Rule: every non-key column must depend on the whole key, not just part of it.

In `orders_flat` (key = `order_id, product_id`):

- `product_name` depends only on `product_id`, which is part of the key. That’s a partial dependency.
- `order_date` depends only on `order_id`. Also partial.
- Only `quantity` depends on both.

- Fix: split by what each part determines.

```sql
CREATE TABLE products (
  product_id   int PRIMARY KEY,
  product_name text NOT NULL,
  price        numeric(10,2) NOT NULL
);

CREATE TABLE orders (
  order_id       int PRIMARY KEY,
  order_date     date NOT NULL,
  customer_id    int NOT NULL,
  customer_name  text,   -- still a problem, see 3NF
  customer_email text,
  customer_city  text
);

CREATE TABLE order_items (
  order_id   int REFERENCES orders(order_id),
  product_id int REFERENCES products(product_id),
  quantity   int NOT NULL CHECK (quantity > 0),
  PRIMARY KEY (order_id, product_id)
);
```

- Now quantity is the only non-key column in order_items, and it depends on the full key.

- Rule: non-key columns must depend on the key, and not on other non-key columns.

Look at `orders` above: `order_id → customer_id`, and `customer_id → customer_name, email, city`. So `customer_name` depends on `order_id` only transitively, through `customer_id`. That’s the source of the update anomaly.

- Fix:

```sql
CREATE TABLE customers (
  customer_id int PRIMARY KEY,
  name        text NOT NULL,
  email       text NOT NULL UNIQUE,
  city        text
);

CREATE TABLE orders (
  order_id    int PRIMARY KEY,
  order_date  date NOT NULL,
  customer_id int NOT NULL REFERENCES customers(customer_id)
);
```

 `A handy mnemonic: every non-key attribute must depend on “the key, the whole key, and nothing but the key.”`


- “The key” gives 1NF.
- “The whole key” gives 2NF.
- “Nothing but the key” gives 3NF.

### BCNF (a brief note)

- NF (a brief note)

Boyce-Codd Normal Form is a slightly stricter 3NF: for every FD `X → Y`, X must be a superkey. The gap only matters when a table has overlapping candidate keys. For example, if `(student, course) → teacher` and `teacher → course`, then 3NF holds but BCNF doesn’t. Most practical schemas that reach 3NF are already BCNF, so it’s good to know the name and the idea.

- Final schema recap

```sql
customers (customer_id PK, name, email, city)
products  (product_id PK, name, price)
orders    (order_id PK, order_date, customer_id FK)
order_items (order_id FK, product_id FK, quantity)  PK(order_id, product_id)
```

- `Note`: order_items should usually also store unit_price at the time of purchase. That isn’t a normalization violation. It’s a different fact (historical price) from products.price (current price). Confusing the two is a common design bug.

## Part 2: Indexes
- Taking postgres as leading example

- How Postgres stores a table

- A table is a `heap`: a collection of 8 KB pages, each holding rows (tuples). Each tuple has a physical address called a `ctid` (page number, slot). With no index, `WHERE email = 'a@b.com'`forces a sequential scan: read every page, check every row. That is O(n).

- An index is a separate data structure that maps column values to `ctids`, so Postgres can jump to the right heap page.

#### The B-tree (the default index)

- `CREATE INDEX` builds a B-tree unless you say otherwise.

```
                [ root: 50 | 100 ]
               /        |         \
      [10|20|30]    [60|70|80]    [110|120|130]     <- internal pages
       /  |  \        ...
 [leaf pages: sorted keys + ctids, doubly linked to each other]
```

- It’s a balanced tree. Every leaf is at the same depth.
- Each node is an 8 KB page with hundreds of keys, so the tree is very shallow. Millions of rows usually means 3 to 4 levels.
- Lookup is O(log n): walk root → internal → leaf, then follow the ctid to the heap.
- Leaf pages are sorted and linked, so range queries (`BETWEEN`, `<`, `>`), `ORDER` BY and `MIN/MAX` are efficient.

- That’s why a B-tree supports =, <, <=, >, >=, BETWEEN, IN, IS NULL, and LIKE 'abc%' (a prefix match, under the right collation or operator class).

- Seeing the difference

```sql
CREATE TABLE users (
  id    bigserial PRIMARY KEY,
  email text NOT NULL,
  city  text,
  created_at timestamptz DEFAULT now()
);
-- assume ~1M rows loaded

EXPLAIN ANALYZE SELECT * FROM users WHERE email = 'kishan@example.com';
-- Seq Scan on users  (cost=0.00..20834.00 rows=1 ...)  Execution Time: ~80 ms

CREATE INDEX idx_users_email ON users (email);

EXPLAIN ANALYZE SELECT * FROM users WHERE email = 'kishan@example.com';
-- Index Scan using idx_users_email  (cost=0.42..8.44 rows=1 ...)  Execution Time: ~0.05 ms
```

Scan types you’ll see in EXPLAIN:

- Seq Scan: read the whole table.
- Index Scan: walk the index, fetch each matching row from the heap.
- Index Only Scan: the answer comes entirely from the index, so no heap visit is needed (the visibility map must say the pages are all-visible).
- Bitmap Index Scan + Bitmap Heap Scan: the index builds a bitmap of matching pages, then the heap is read in physical order. Used when many rows match, or when combining several indexes with AND/OR.

- Primary keys and UNIQUE constraints automatically create a unique B-tree index. Foreign keys do not. If you join or delete by orders.customer_id, you almost certainly need to create that index yourself. This is one of the most common Postgres performance mistakes.

### Index types to know

1. Composite (multi-column)

```sql
CREATE INDEX idx_orders_cust_date ON orders (customer_id, order_date);
```

- Entries are sorted by customer_id, then by order_date within each customer. The leftmost-prefix rule applies.

2. Unique

```sql
CREATE UNIQUE INDEX ON users (lower(email));
```

- It enforces a constraint and speeds up lookups.

3. Partial

```sql
CREATE INDEX idx_orders_pending ON orders (created_at) WHERE status = 'pending';
```

- It indexes only the rows you care about, so it is smaller and faster. It is great for hot subsets like “unprocessed jobs” (relevant for queue-style tables).

4. Expression

```sql
CREATE INDEX idx_users_lower_email ON users (lower(email));
-- query must match: WHERE lower(email) = 'x@y.com'
```

5. Covering (INCLUDE)

```sql
CREATE INDEX idx_orders_cust ON orders (customer_id) INCLUDE (order_date, total);
```

- Payload columns are stored in the leaf only, enabling index-only scans without bloating the search keys.

## Part 3: How they connect

- Normalization splits data into many narrow tables, so reads become joins. Joins are only fast if the join columns are indexed (PKs automatically, FKs manually). For example:

```sql
SELECT c.name, o.order_id, p.name, oi.quantity
FROM customers c
JOIN orders o       ON o.customer_id = c.customer_id   -- needs index on orders.customer_id
JOIN order_items oi ON oi.order_id   = o.order_id      -- covered by PK (order_id, product_id)
JOIN products p     ON p.product_id  = oi.product_id   -- products PK
WHERE c.email = 'kishan@example.com';                  -- unique index on customers.email
```

- A well-normalized schema plus well-chosen indexes handles an enormous range of workloads before denormalization is ever needed.