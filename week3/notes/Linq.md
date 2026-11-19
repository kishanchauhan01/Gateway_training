**LINQ (Language Integrated Query)** is a uniform programming model in .NET that allows you to query and manipulate data from different sources (in-memory collections, SQL databases, XML, JSON) using a consistent C# syntax.

---

## Table of Contents
1. [Setup: Sample Data Model](#1-setup-sample-data-model)
2. [Query Syntax vs. Method Syntax](#2-query-syntax-vs-method-syntax)
3. [Filtering & Projection: `Where` and `Select`](#3-filtering--projection-where-and-select)
4. [Sorting: `OrderBy`](#4-sorting-orderby)
5. [Grouping: `GroupBy`](#5-grouping-groupby)
6. [Joining: `Join`](#6-joining-join)
7. [Quantifiers: `Any` and `All`](#7-quantifiers-any-and-all)
8. [Element Operators: `First` vs. `Single`](#8-element-operators-first-vs-single)
9. [Deferred vs. Immediate Execution](#9-deferred-vs-immediate-execution)
10. [How LINQ Works Under the Hood](#10-how-linq-works-under-the-hood)

---

## 1. Setup: Sample Data Model

All practical examples in these notes use the following models and in-memory collections:

```csharp
public record Department(int Id, string Name);
public record Employee(int Id, string Name, int DepartmentId, decimal Salary, bool IsActive);

var departments = new List<Department>
{
    new(1, "Engineering"),
    new(2, "Marketing"),
    new(3, "Finance")
};

var employees = new List<Employee>
{
    new(101, "Aarav", 1, 95000m, true),
    new(102, "Diya", 1, 110000m, true),
    new(103, "Kabir", 2, 72000m, false),
    new(104, "Meera", 3, 88000m, true),
    new(105, "Rohan", 2, 68000m, true)
};
```

---

## 2. Query Syntax vs. Method Syntax

LINQ offers two ways to write queries. Both compile down to the exact same Intermediate Language (IL) and have identical runtime performance.

### Query Syntax (SQL-like)
Declarative syntax integrated directly into C#. It always starts with `from` and ends with `select` or `group`.

```csharp
var highEarnersQuery =
    from emp in employees
    where emp.Salary > 80000m
    orderby emp.Name
    select emp.Name;
```

### Method Syntax (Fluent API)
Uses standard C# method calls chained together with **Lambda Expressions**. It supports all LINQ operators (some operators like `Count()`, `Any()`, and `First()` do not have query syntax keywords).

```csharp
var highEarnersMethod = employees
    .Where(emp => emp.Salary > 80000m)
    .OrderBy(emp => emp.Name)
    .Select(emp => emp.Name);
```

> **Best Practice:** Method syntax is standard in modern C# development due to its composability and full operator support. Query syntax is occasionally preferred for complex multi-source joins.

---

## 3. Filtering & Projection: `Where` and `Select`

### `Where` (Filtering)
Filters a sequence of values based on a predicate (`Func<T, bool>`).

```csharp
// Get all active employees in Engineering
var activeEngineers = employees
    .Where(e => e.IsActive && e.DepartmentId == 1);
```

### `Select` (Projection)
Transforms each element of a sequence into a new form (`Func<T, TResult>`), often used to shape data into DTOs or anonymous types.

```csharp
// Project into an anonymous object containing only Name and Annual Bonus (10%)
var bonuses = employees
    .Where(e => e.IsActive)
    .Select(e => new 
    { 
        e.Name, 
        Bonus = e.Salary * 0.10m 
    });
```

---

## 4. Sorting: `OrderBy`

Sorts elements in ascending order (`OrderBy`) or descending order (`OrderByDescending`). Use `ThenBy` / `ThenByDescending` for secondary sorting criteria.

```csharp
// Sort by DepartmentId ascending, then by Salary highest-to-lowest
var sortedEmployees = employees
    .OrderBy(e => e.DepartmentId)
    .ThenByDescending(e => e.Salary);
```

> **Pitfall:** Calling `.OrderBy()` twice in a row overwrites the first sort. Always chain secondary sorts with `.ThenBy()`.

---

## 5. Grouping: `GroupBy`

Groups elements that share a common attribute into an `IEnumerable<IGrouping<TKey, TElement>>`. Each group has a `Key` property and acts as a collection of the items in that group.

```csharp
var employeesByDept = employees
    .GroupBy(e => e.DepartmentId);

foreach (var group in employeesByDept)
{
    Console.WriteLine($"Department ID: {group.Key} | Count: {group.Count()}");
    foreach (var emp in group)
    {
        Console.WriteLine($"  - {emp.Name} (${emp.Salary})");
    }
}
```

---

## 6. Joining: `Join`

Correlates elements of two sequences based on matching keys (equivalent to an SQL `INNER JOIN`).

```csharp
var employeeDetails = employees.Join(
    inner: departments,
    outerKeySelector: emp => emp.DepartmentId,
    innerKeySelector: dept => dept.Id,
    resultSelector: (emp, dept) => new
    {
        EmployeeName = emp.Name,
        DepartmentName = dept.Name,
        emp.Salary
    });

// Output:
// Aarav - Engineering ($95000)
// Diya - Engineering ($110000) ...
```

---

## 7. Quantifiers: `Any` and `All`

Quantifiers evaluate elements against a condition and return a single `bool`. Both use **short-circuit evaluation** (they stop iterating the moment the result is guaranteed).

### `Any`
Returns `true` if **at least one** element satisfies the condition (or if the collection is non-empty when called with no arguments).

```csharp
// Are there any inactive employees?
bool hasInactive = employees.Any(e => !e.IsActive); // true (stops at Kabir)
```

### `All`
Returns `true` only if **every** element satisfies the condition.

```csharp
// Do all employees earn more than 50,000?
bool allAbove50k = employees.All(e => e.Salary > 50000m); // true
```

---

## 8. Element Operators: `First` vs. `Single`

Both retrieve a single item from a sequence, but they communicate **different business rules** and behave differently when multiple matches exist.

| Method | 0 Matches | 1 Match | > 1 Matches | Use Case |
| :--- | :--- | :--- | :--- | :--- |
| **`First()`** | Throws `InvalidOperationException` | Returns element | Returns **1st** element | "Get the top/latest record; duplicates are fine." |
| **`FirstOrDefault()`** | Returns `default` (`null` / `0`) | Returns element | Returns **1st** element | "Get the first match if one exists." |
| **`Single()`** | Throws `InvalidOperationException` | Returns element | **Throws `InvalidOperationException`** | "Enforce uniqueness (e.g., lookup by Primary Key)." |
| **`SingleOrDefault()`** | Returns `default` (`null` / `0`) | Returns element | **Throws `InvalidOperationException`** | "Expect 0 or 1 match; multiple is a data corruption bug." |

```csharp
// Good use of Single: Looking up by a unique ID
var meera = employees.Single(e => e.Id == 104);

// Good use of FirstOrDefault: Finding the highest-paid active employee
var topEarner = employees
    .Where(e => e.IsActive)
    .OrderByDescending(e => e.Salary)
    .FirstOrDefault();
```

---

## 9. Deferred vs. Immediate Execution

### Deferred Execution
Most LINQ operators (`Where`, `Select`, `OrderBy`, `Join`, `GroupBy`) **do not execute when defined**. They build a query pipeline (a state machine) that only executes when you enumerate the results (e.g., via `foreach`).

```csharp
var query = employees.Where(e => e.IsActive); // Nothing runs here!

// Modify the underlying list BEFORE enumerating
employees.Add(new Employee(106, "Vikram", 1, 105000m, true));

// Query executes NOW and includes Vikram!
foreach (var emp in query)
{
    Console.WriteLine(emp.Name); 
}
```

### Immediate Execution
Operators that collapse a sequence into a single value or materialize a new collection force immediate execution:
* **Conversions:** `.ToList()`, `.ToArray()`, `.ToDictionary()`
* **Aggregations:** `.Count()`, `.Sum()`, `.Average()`, `.Max()`, `.Min()`
* **Elements & Quantifiers:** `.First()`, `.Single()`, `.Any()`, `.All()`

> **Performance Warning (Multiple Enumeration):** Iterating over an unmaterialized `IEnumerable<T>` multiple times re-runs the entire query pipeline each time. Call `.ToList()` if you need to traverse the results more than once.

---

## 10. How LINQ Works Under the Hood

LINQ is not magic compiler black-box behavior; it is built cleanly on top of four core C# language features:

### 1. Extension Methods (`System.Linq.Enumerable`)
LINQ operators are `static` extension methods targeting `IEnumerable<T>` (or `IQueryable<T>`). When you write `employees.Where(...)`, the compiler translates it into a static method call:

```csharp
Enumerable.Where(employees, e => e.IsActive);
```

### 2. Delegates and Lambda Expressions
Methods like `Where` accept a `Func<TSource, bool>` delegate. The C# compiler converts your lambda expression (`e => e.IsActive`) into a private method and passes a delegate pointing to it.

### 3. Iterator Blocks (`yield return`) & State Machines
In-memory LINQ (`LINQ to Objects`) achieves deferred execution and streaming using C# iterator blocks. A simplified implementation of `Where` looks like this:

```csharp
public static IEnumerable<TSource> CustomWhere<TSource>(
    this IEnumerable<TSource> source, 
    Func<TSource, bool> predicate)
{
    foreach (TSource item in source)
    {
        if (predicate(item))
        {
            yield return item; // Pauses execution and yields control back to caller
        }
    }
}
```
When compiled, `yield return` generates a hidden **state machine class** implementing `IEnumerator<T>`. Calling `MoveNext()` in a `foreach` loop advances the state machine one item at a time—meaning items flow vertically through chained operators (`Where` -> `Select`) one by one without allocating intermediate lists in memory.

### 4. `IEnumerable<T>` vs. `IQueryable<T>` (Expression Trees)
LINQ has two distinct execution engines depending on the interface:

* **`IEnumerable<T>` (LINQ to Objects):** Takes compiled delegates (`Func<T, bool>`). Data is pulled into application memory and filtered using C# code.
* **`IQueryable<T>` (LINQ to SQL / EF Core):** Takes **Expression Trees** (`Expression<Func<T, bool>>`). Instead of compiling your lambda into executable IL code, the compiler turns your code into a **data structure (an Abstract Syntax Tree)** representing the logic. A query provider (like Entity Framework Core) inspects that tree at runtime and translates it into native SQL (`SELECT ... WHERE ...`) to execute directly on the database server.