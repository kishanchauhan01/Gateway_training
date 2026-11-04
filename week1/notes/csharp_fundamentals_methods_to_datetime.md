---
subtitle: Practical study notes with explanations, examples, and
  exercises
title: "C# Fundamentals --- Methods to Dates & Time"
---

# C# Fundamentals --- Methods to Dates & Time

1.  Methods
2.  Strings
3.  Collections
4.  Exception handling
5.  Dates and time

Examples assume a modern .NET console application. Most examples can be
placed inside a method such as `Main` or a `Run` method. Where a
complete method is shown, place it inside a class unless using top-level
statements.

> **Conventions:** C# is case-sensitive. Local variables and parameters
> commonly use `camelCase`; types and methods commonly use `PascalCase`.
> Examples use `Console.WriteLine` for output.

------------------------------------------------------------------------

# 1. Methods

## 1.1 What is a method?

A **method** is a named block of code that performs a particular task.
Methods help split a program into smaller, reusable parts.

For example, instead of writing the same salary calculation in several
places, create one method and call it whenever required.

``` csharp
static decimal CalculateAnnualSalary(decimal monthlySalary)
{
    return monthlySalary * 12;
}
```

Call the method:

``` csharp
decimal annualSalary = CalculateAnnualSalary(50000m);
Console.WriteLine(annualSalary); // 600000
```

## 1.2 Method syntax

``` csharp
accessModifier static returnType MethodName(parameters)
{
    // Statements
    return value;
}
```

  -----------------------------------------------------------------------
  Part                                Meaning
  ----------------------------------- -----------------------------------
  `public` / `private`                Controls where the method can be
                                      accessed

  `static`                            The method belongs to the type, not
                                      an individual object

  `returnType`                        Type of value returned, such as
                                      `int`, `string`, or `void`

  `MethodName`                        Name used to call the method

  `parameters`                        Inputs the method receives

  `return`                            Sends a result back to the caller
  -----------------------------------------------------------------------

Example:

``` csharp
public static int Add(int first, int second)
{
    return first + second;
}
```

A method with no return value uses `void`:

``` csharp
public static void DisplayWelcome()
{
    Console.WriteLine("Welcome!");
}
```

## 1.3 Parameters and arguments

A **parameter** is the variable declared in a method definition. An
**argument** is the actual value passed when calling it.

``` csharp
static void Greet(string name) // name is a parameter
{
    Console.WriteLine($"Hello, {name}!");
}

Greet("Kishan"); // "Kishan" is an argument
```

Methods can accept multiple parameters:

``` csharp
static decimal CalculateTotal(decimal price, int quantity)
{
    return price * quantity;
}

decimal total = CalculateTotal(250m, 3);
```

## 1.4 Return values

A method can return a result to the caller. The returned value must
match the declared return type.

``` csharp
static bool IsAdult(int age)
{
    return age >= 18;
}

bool allowed = IsAdult(20);
Console.WriteLine(allowed); // True
```

A `void` method does not return a value. It can still use `return;` to
exit early:

``` csharp
static void PrintName(string name)
{
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Name is required.");
        return;
    }

    Console.WriteLine(name);
}
```

## 1.5 Static methods vs instance methods

A `static` method belongs to the class/type and can be called using the
type name.

``` csharp
public class MathHelper
{
    public static int Square(int number)
    {
        return number * number;
    }
}

int result = MathHelper.Square(5);
```

An **instance method** belongs to an object. Create an object with `new`
before calling it:

``` csharp
public class Employee
{
    public void Display()
    {
        Console.WriteLine("Employee details");
    }
}

Employee employee = new Employee();
employee.Display();
```

Use `static` when the operation does not need data stored in a
particular object. Use instance methods when the operation works with
that object's state.

## 1.6 Method overloading

**Method overloading** means defining multiple methods with the same
name but different parameter lists.

``` csharp
static int Add(int a, int b)
{
    return a + b;
}

static int Add(int a, int b, int c)
{
    return a + b + c;
}

static double Add(double a, double b)
{
    return a + b;
}
```

The compiler chooses the matching overload based on the arguments.

A method cannot be overloaded by changing only its return type. The
parameter list must differ.

## 1.7 Optional parameters

An optional parameter has a default value. Callers can omit it.

``` csharp
static void Greet(string name, string message = "Welcome")
{
    Console.WriteLine($"{message}, {name}!");
}

Greet("Kishan");                 // Welcome, Kishan!
Greet("Kishan", "Good morning"); // Good morning, Kishan!
```

Optional parameters are generally placed after required parameters.

## 1.8 Named arguments

Named arguments specify parameter names at the call site. They improve
readability and allow arguments to be supplied in a different order.

``` csharp
static void DisplayEmployee(string name, int age, string department)
{
    Console.WriteLine($"{name}, {age}, {department}");
}

DisplayEmployee(
    department: "Engineering",
    name: "Kishan",
    age: 20
);
```

## 1.9 `ref` and `out`

By default, arguments are passed by value. `ref` and `out` allow a
method to assign through a parameter to the caller's variable.

### `ref`

-   The variable must be initialized before the call.
-   The method can read and modify it.
-   The caller must use the `ref` keyword.

``` csharp
static void ApplyIncrement(ref int salary)
{
    salary += 5000;
}

int monthlySalary = 50000;
ApplyIncrement(ref monthlySalary);

Console.WriteLine(monthlySalary); // 55000
```

### `out`

-   The variable does not need to be initialized before the call.
-   The method must assign it before returning.
-   The caller must use the `out` keyword.

``` csharp
static bool TryGetEmployeeId(out int employeeId)
{
    employeeId = 101;
    return true;
}

if (TryGetEmployeeId(out int id))
{
    Console.WriteLine(id);
}
```

A familiar example is `int.TryParse`, which returns whether parsing
succeeded and places the parsed value in an `out` parameter.

## 1.10 Expression-bodied methods

For a method containing a single expression, use `=>` to write it more
concisely.

``` csharp
static int Square(int number) => number * number;

static bool IsEligibleForBonus(decimal salary) => salary >= 50000m;
```

This is shorthand for a method body containing a `return` statement when
the method returns a value.

## 1.11 Good method design

-   Give methods clear names that describe their action.
-   Keep each method focused on one responsibility.
-   Avoid unnecessarily long parameter lists.
-   Validate inputs when the method has specific requirements.
-   Return a result when the caller needs one; use `void` when the
    method performs an action.
-   Avoid duplicating the same logic in multiple places.

### Method practice

Create a class named `EmployeeMethods` and implement:

1.  `CalculateAnnualSalary(decimal monthlySalary)` returning
    `monthlySalary * 12`.
2.  Two overloaded `CalculateBonus` methods: one accepting a salary and
    one accepting a salary plus a bonus percentage.
3.  `DisplayEmployee(string name, string department = "General")`.
4.  `ApplyIncrement(ref decimal salary, decimal increment)`.
5.  `TryGetEmployeeId(out int employeeId)`.

------------------------------------------------------------------------

# 2. Strings

## 2.1 What is a string?

A `string` represents a sequence of Unicode characters. It is used for
text such as names, messages, addresses, and user input.

``` csharp
string employeeName = "Kishan";
string department = "Engineering";
```

Strings are **reference types** and are **immutable**. Immutable means
that a string's contents cannot be changed after that string object is
created. String operations produce a new string rather than modifying
the original.

## 2.2 String interpolation

Use `$` before a string to insert variables and expressions inside `{}`.

``` csharp
string name = "Kishan";
int age = 20;

string message = $"Name: {name}, Age: {age}";
Console.WriteLine(message);
```

You can include expressions:

``` csharp
decimal monthlySalary = 50000m;

Console.WriteLine($"Annual salary: {monthlySalary * 12}");
```

## 2.3 Common string methods

Assume:

``` csharp
string text = "  C# Developer  ";
```

  -----------------------------------------------------------------------------------------
  Method                  Purpose                 Example
  ----------------------- ----------------------- -----------------------------------------
  `Trim()`                Removes whitespace at   `text.Trim()`
                          both ends               

  `TrimStart()`           Removes whitespace at   `text.TrimStart()`
                          the start               

  `TrimEnd()`             Removes whitespace at   `text.TrimEnd()`
                          the end                 

  `ToUpper()`             Returns uppercase text  `text.ToUpper()`

  `ToLower()`             Returns lowercase text  `text.ToLower()`

  `Contains()`            Checks whether text     `text.Contains("Dev")`
                          contains a value        

  `StartsWith()`          Checks the beginning    `text.StartsWith("  C#")`

  `EndsWith()`            Checks the ending       `text.EndsWith("  ")`

  `Replace()`             Replaces matching text  `text.Replace("Developer", "Engineer")`

  `Substring()`           Extracts part of a      `text.Substring(2, 2)`
                          string                  

  `Split()`               Splits text into an     `"A,B,C".Split(',')`
                          array                   

  `IndexOf()`             Finds the first         `text.IndexOf("Developer")`
                          matching position       
  -----------------------------------------------------------------------------------------

Most string methods return a new string. Keep the result if you need it:

``` csharp
string original = "  Kishan  ";
string cleaned = original.Trim();

Console.WriteLine(original); // Still contains surrounding spaces
Console.WriteLine(cleaned);  // Kishan
```

### Case-insensitive comparisons

`Contains()` is case-sensitive by default. For a case-insensitive check,
specify a comparison rule:

``` csharp
string role = "Software Developer";

bool found = role.Contains(
    "developer",
    StringComparison.OrdinalIgnoreCase
);
```

For equality comparisons:

``` csharp
bool same = string.Equals(
    "admin",
    "ADMIN",
    StringComparison.OrdinalIgnoreCase
);
```

## 2.4 `Split()` and `Substring()`

`Split()` divides a string into an array of parts.

``` csharp
string csv = "Kishan,20,Developer";
string[] parts = csv.Split(',');

Console.WriteLine(parts[0]); // Kishan
Console.WriteLine(parts[1]); // 20
Console.WriteLine(parts[2]); // Developer
```

`Substring(startIndex, length)` extracts a portion of a string. Indexing
is zero-based.

``` csharp
string language = "CSharp";

string firstThree = language.Substring(0, 3);
Console.WriteLine(firstThree); // CSh
```

The start index and requested length must fit within the string, or an
exception is thrown.

## 2.5 String formatting

Numeric values can be formatted inside interpolated strings.

``` csharp
decimal salary = 50000.5m;
double completion = 0.875;

Console.WriteLine($"{salary:F2}");       // 50000.50
Console.WriteLine($"{salary:N2}");       // 50,000.50 (culture-dependent)
Console.WriteLine($"{completion:P0}");   // 88% (rounded)
```

Common format specifiers:

  Specifier   Meaning
  ----------- -------------------------------------------
  `F2`        Fixed-point with 2 decimal places
  `N2`        Number with 2 decimal places and grouping
  `C`         Currency, based on current culture
  `P`         Percentage

Formatting output can vary with the current culture, especially currency
and decimal separators.

## 2.6 String immutability

This code does not modify the original string:

``` csharp
string name = "kishan";
name.ToUpper();

Console.WriteLine(name); // kishan
```

Store the returned string to use the changed value:

``` csharp
name = name.ToUpper();
Console.WriteLine(name); // KISHAN
```

Repeated concatenation can create many intermediate strings. For
frequent modifications or building large text, consider `StringBuilder`.

## 2.7 `StringBuilder`

`StringBuilder` is a mutable text builder in the `System.Text`
namespace.

``` csharp
using System.Text;

StringBuilder report = new StringBuilder();

report.Append("Employee: ");
report.Append("Kishan");
report.AppendLine();
report.Append("Department: Engineering");

string finalReport = report.ToString();
Console.WriteLine(finalReport);
```

Common members:

  Member           Purpose
  ---------------- ------------------------------------
  `Append()`       Adds text
  `AppendLine()`   Adds text followed by a line break
  `Insert()`       Inserts text at an index
  `Replace()`      Replaces matching text
  `Remove()`       Removes a range
  `Clear()`        Removes all content
  `ToString()`     Produces a regular string

Use `StringBuilder` when performing many repeated modifications, such as
building a report in a loop. For simple interpolation or a few
concatenations, regular strings are usually clearer.

### String practice

Given:

``` csharp
string input = "  Kishan Patel,20,Developer,50000  ";
```

1.  Remove surrounding whitespace.
2.  Split the data using commas.
3.  Extract the name, age, role, and salary.
4.  Replace `Developer` with `Software Engineer`.
5.  Check whether the role contains `Engineer`, ignoring case.
6.  Build a formatted employee report using `StringBuilder`.

------------------------------------------------------------------------

# 3. Collections

## 3.1 What are collections?

Collections store and manage multiple values. Different collection types
provide different ways to access, add, remove, and search for data.

The main types covered here are:

-   Arrays
-   `List<T>`
-   `Dictionary<TKey, TValue>`
-   `HashSet<T>`

Choose a collection based on how the data will be used.

## 3.2 Arrays

An array stores a fixed number of elements of the same type. Its indexes
start at zero.

``` csharp
string[] employees = { "Kishan", "Rahul", "Amit" };

Console.WriteLine(employees[0]); // Kishan
Console.WriteLine(employees.Length); // 3
```

Update an element:

``` csharp
employees[1] = "Jay";
```

Create an array with a specified length:

``` csharp
int[] marks = new int[5];

marks[0] = 80;
marks[1] = 90;
```

The remaining `int` elements contain their default value, `0`.

Useful array operations:

``` csharp
int[] numbers = { 5, 2, 8, 1, 9 };

Array.Sort(numbers);
Array.Reverse(numbers);

int index = Array.IndexOf(numbers, 5);
```

  Member              Purpose
  ------------------- ----------------------------
  `Length`            Number of elements
  `Array.Sort()`      Sorts an array
  `Array.Reverse()`   Reverses element order
  `Array.IndexOf()`   Finds the index of a value

Arrays are useful when the number of elements is known and does not need
to change.

## 3.3 `List<T>`

A `List<T>` is a dynamically sized collection. It can grow or shrink as
elements are added or removed.

``` csharp
List<string> employees = new List<string>
{
    "Kishan",
    "Rahul",
    "Amit"
};
```

The shorter collection expression syntax is also available in modern C#:

``` csharp
List<string> names = ["Kishan", "Rahul", "Amit"];
```

Common operations:

``` csharp
employees.Add("Jay");
employees.AddRange(new[] { "Priya", "Neha" });

employees.Remove("Amit");
employees.RemoveAt(0);

bool exists = employees.Contains("Rahul");
int position = employees.IndexOf("Rahul");

Console.WriteLine(employees.Count);

employees.Sort();
```

  Member         Purpose
  -------------- ----------------------------------
  `Count`        Number of elements
  `Add()`        Adds one element
  `AddRange()`   Adds multiple elements
  `Remove()`     Removes the first matching value
  `RemoveAt()`   Removes the element at an index
  `Contains()`   Checks whether a value exists
  `IndexOf()`    Returns the index of a value
  `Clear()`      Removes all elements
  `Sort()`       Sorts the list
  `Find()`       Finds the first matching element
  `FindAll()`    Finds all matching elements

Example with predicates:

``` csharp
List<int> salaries = [25000, 40000, 60000, 75000];

int firstHighSalary = salaries.Find(salary => salary > 50000);

List<int> highSalaries =
    salaries.FindAll(salary => salary > 50000);
```

The expression `salary => salary > 50000` is a lambda expression. It
describes a condition to test for each element.

Use `List<T>` when you need an ordered, dynamically sized sequence.

## 3.4 `Dictionary<TKey, TValue>`

A dictionary stores data as key-value pairs. Each key must be unique;
values do not have to be unique.

Example: map employee IDs to employee names.

``` csharp
Dictionary<int, string> employees = new Dictionary<int, string>
{
    { 101, "Kishan" },
    { 102, "Rahul" },
    { 103, "Amit" }
};
```

Modern syntax:

``` csharp
Dictionary<int, string> employeeNames = new()
{
    [101] = "Kishan",
    [102] = "Rahul"
};
```

Add an entry:

``` csharp
employees.Add(104, "Jay");
```

Read and update using the indexer:

``` csharp
Console.WriteLine(employees[101]);

employees[102] = "Rohan";
```

Remove by key:

``` csharp
employees.Remove(103);
```

Check for a key:

``` csharp
if (employees.ContainsKey(101))
{
    Console.WriteLine("Employee found.");
}
```

Use `TryGetValue()` for safe lookup:

``` csharp
if (employees.TryGetValue(101, out string name))
{
    Console.WriteLine(name);
}
else
{
    Console.WriteLine("Employee not found.");
}
```

Reading a missing key with `employees[999]` throws a
`KeyNotFoundException`. `TryGetValue()` avoids that exception for an
ordinary missing-key lookup.

Useful members:

  Member                   Purpose
  ------------------------ ------------------------------
  `Add(key, value)`        Adds a new key-value pair
  `dictionary[key]`        Gets or sets a value
  `Remove(key)`            Removes an entry by key
  `ContainsKey(key)`       Checks for a key
  `ContainsValue(value)`   Checks for a value
  `TryGetValue()`          Safely attempts a key lookup
  `Count`                  Number of entries
  `Clear()`                Removes all entries

Dictionary lookup by key is typically O(1) on average. Searching for a
value is O(n).

Use a dictionary when you need to retrieve information using a unique
identifier or key.

## 3.5 `HashSet<T>`

A `HashSet<T>` stores unique elements. Adding a duplicate does not
create another entry.

``` csharp
HashSet<string> emails = new HashSet<string>();

emails.Add("kishan@example.com");
emails.Add("rahul@example.com");
emails.Add("kishan@example.com");

Console.WriteLine(emails.Count); // 2
```

`Add()` returns `true` if the element was added and `false` if it was
already present:

``` csharp
bool added = emails.Add("kishan@example.com");
Console.WriteLine(added); // False
```

Other common operations:

``` csharp
bool exists = emails.Contains("rahul@example.com");

emails.Remove("rahul@example.com");
emails.Clear();
```

### Set operations

``` csharp
HashSet<int> teamA = [1, 2, 3, 4];
HashSet<int> teamB = [3, 4, 5, 6];
```

Union: all elements from either set.

``` csharp
teamA.UnionWith(teamB);
// teamA now contains 1, 2, 3, 4, 5, 6
```

Intersection: elements common to both sets.

``` csharp
teamA.IntersectWith(teamB);
```

Difference: elements in the receiving set that are not in the other set.

``` csharp
teamA.ExceptWith(teamB);
```

The `UnionWith`, `IntersectWith`, and `ExceptWith` methods modify the
receiving set. A `HashSet<T>` does not guarantee insertion order.

Use a hash set when uniqueness and fast membership checks matter.

## 3.6 Looping over collections

### `for` loop

Use a `for` loop when you need the index.

``` csharp
string[] employees = { "Kishan", "Rahul", "Amit" };

for (int i = 0; i < employees.Length; i++)
{
    Console.WriteLine(employees[i]);
}
```

### `foreach` loop

Use `foreach` to process each element without managing an index.

``` csharp
List<string> employees = ["Kishan", "Rahul", "Amit"];

foreach (string employee in employees)
{
    Console.WriteLine(employee);
}
```

### Loop through a dictionary

``` csharp
Dictionary<int, string> employees = new()
{
    [101] = "Kishan",
    [102] = "Rahul"
};

foreach (KeyValuePair<int, string> employee in employees)
{
    Console.WriteLine($"ID: {employee.Key}, Name: {employee.Value}");
}
```

`var` can be used when the compiler can infer the type:

``` csharp
foreach (var employee in employees)
{
    Console.WriteLine($"{employee.Key}: {employee.Value}");
}
```

### Loop through a HashSet

``` csharp
HashSet<string> uniqueEmails = ["a@example.com", "b@example.com"];

foreach (string email in uniqueEmails)
{
    Console.WriteLine(email);
}
```

Avoid adding or removing elements from a collection while directly
iterating over it with `foreach`; this commonly causes an
`InvalidOperationException`.

## 3.7 Choosing the right collection

  Requirement                           Suitable collection
  ------------------------------------- ----------------------------
  Fixed-size sequence                   Array
  Dynamic ordered sequence              `List<T>`
  Lookup by unique key                  `Dictionary<TKey, TValue>`
  Unique values and membership checks   `HashSet<T>`

Typical average-case performance:

  -----------------------------------------------------------------------
  Operation            Array           List     Dictionary        HashSet
  ----------- -------------- -------------- -------------- --------------
  Access by             O(1)           O(1)            ---            ---
  index                                                    

  Search by             O(n)           O(n)           O(n)   O(1) average
  value                                                    

  Lookup by              ---            ---   O(1) average            ---
  key                                                      

  Add element     Fixed size O(1) amortized   O(1) average   O(1) average
  -----------------------------------------------------------------------

Performance depends on the operation and implementation details. For
example, inserting into the middle of a list is O(n), and hash-based
operations can degrade when many collisions occur.

### Collections practice

Create a `CollectionsPractice` class and implement:

1.  An array containing five employee names; print all names.
2.  A list; add two names, remove one, and print the remaining names.
3.  A dictionary mapping employee IDs to names; add, search, update, and
    remove entries.
4.  A hash set of employee email addresses; attempt to add a duplicate
    and inspect the returned `bool`.
5.  Use `foreach` to display the contents of the list, dictionary, and
    hash set.

------------------------------------------------------------------------

# 4. Exception Handling

## 4.1 What is an exception?

An **exception** is an object that represents an error or unexpected
condition during program execution. If it is not handled, it can
interrupt the program.

Common exceptions:

  -----------------------------------------------------------------------
  Exception                           Typical cause
  ----------------------------------- -----------------------------------
  `FormatException`                   Input cannot be converted to the
                                      requested format

  `DivideByZeroException`             Integer division by zero

  `NullReferenceException`            Accessing a member through a null
                                      reference

  `IndexOutOfRangeException`          Accessing an invalid array index

  `KeyNotFoundException`              Looking up a missing dictionary key

  `ArgumentException`                 An argument is invalid for a method
  -----------------------------------------------------------------------

Example:

``` csharp
int number = int.Parse("abc"); // Throws FormatException
```

## 4.2 `try` and `catch`

Place code that may throw an exception inside `try`. Handle a matching
exception in `catch`.

``` csharp
try
{
    int salary = int.Parse("abc");
    Console.WriteLine(salary);
}
catch (FormatException ex)
{
    Console.WriteLine("Please enter a valid number.");
    Console.WriteLine(ex.Message);
}
```

When an exception is thrown in the `try` block, normal execution of that
block stops and the runtime looks for a matching handler.

## 4.3 Multiple `catch` blocks

Use separate handlers when different exceptions need different
responses.

``` csharp
try
{
    int[] numbers = { 10, 20, 30 };

    Console.Write("Enter an index: ");
    int index = int.Parse(Console.ReadLine()!);

    Console.WriteLine(numbers[index]);
}
catch (FormatException)
{
    Console.WriteLine("Enter a valid integer.");
}
catch (IndexOutOfRangeException)
{
    Console.WriteLine("That index does not exist.");
}
```

Put more specific exception types before more general types. A general
handler placed first would prevent later, more specific handlers from
being reached.

## 4.4 Catching `Exception`

`Exception` is the base type for most exceptions thrown by .NET APIs and
applications.

``` csharp
try
{
    // Operation that may fail
}
catch (Exception ex)
{
    Console.WriteLine($"Something went wrong: {ex.Message}");
}
```

Use broad handlers only when you have a meaningful recovery or logging
strategy. Avoid silently swallowing exceptions or using a broad catch as
a substitute for understanding expected failure cases.

Useful diagnostic properties include:

-   `ex.Message`: a description of the exception.
-   `ex.GetType().Name`: the exception type name.
-   `ex.StackTrace`: information about where the exception occurred.

## 4.5 The `finally` block

A `finally` block runs after the `try` and any matching `catch`, whether
an exception occurred or not in normal control flow.

``` csharp
try
{
    Console.WriteLine("Starting operation.");
    int result = 10 / 2;
}
catch (DivideByZeroException)
{
    Console.WriteLine("Cannot divide by zero.");
}
finally
{
    Console.WriteLine("Cleanup or completion step.");
}
```

`finally` is useful for cleanup. For disposable resources such as files
and streams, C# commonly uses `using` statements or declarations to
ensure disposal.

``` csharp
using (var reader = new StreamReader("data.txt"))
{
    string content = reader.ReadToEnd();
}
```

The `using` statement disposes the resource even if an exception occurs
while inside the block.

## 4.6 The `throw` keyword

Use `throw` to raise an exception when a method detects an invalid
condition.

``` csharp
static void ValidateAge(int age)
{
    if (age < 18)
    {
        throw new ArgumentException("Age must be 18 or above.");
    }

    Console.WriteLine("Age accepted.");
}
```

Handle it at the call site if the caller can respond:

``` csharp
try
{
    ValidateAge(15);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
```

### Rethrowing an exception

Inside a catch block, use `throw;` to rethrow the current exception
while preserving its original stack trace:

``` csharp
try
{
    // Operation
}
catch (Exception)
{
    // Log or perform relevant work
    throw;
}
```

Avoid `throw ex;` when rethrowing the same exception because it resets
the stack trace starting point.

## 4.7 Validation vs exception handling

Not every invalid input should be handled by throwing an exception. For
expected user-input errors, validation methods such as `TryParse` are
usually better.

Less suitable for ordinary user input:

``` csharp
int age = int.Parse(Console.ReadLine()!);
```

Safer validation:

``` csharp
Console.Write("Enter age: ");

if (int.TryParse(Console.ReadLine(), out int age))
{
    Console.WriteLine($"Age: {age}");
}
else
{
    Console.WriteLine("Please enter a valid whole number.");
}
```

`TryParse` returns `true` when conversion succeeds and `false` when it
fails. It does not throw a `FormatException` for ordinary invalid input.

Use exceptions for exceptional failures and `TryParse` or other
validation for expected input problems.

### Exception-handling practice

Create an employee salary validator:

1.  Read an employee name.
2.  Read a monthly salary.
3.  Parse the salary safely.
4.  Reject negative salary values.
5.  Calculate annual salary.
6.  Display a useful message for invalid input.
7.  Use `finally` for a completion message if appropriate.

------------------------------------------------------------------------

# 5. Dates and Time

Date and time handling is important for attendance systems, audit
records, scheduling, APIs, and applications used across time zones.

The three main types in this section are:

-   `DateTime`: a date and time.
-   `DateTimeOffset`: a date and time with a UTC offset.
-   `TimeSpan`: a duration or difference.

## 5.1 `DateTime`

`DateTime` represents a date and time.

``` csharp
DateTime now = DateTime.Now;
DateTime utcNow = DateTime.UtcNow;
DateTime today = DateTime.Today;

Console.WriteLine(now);
Console.WriteLine(utcNow);
Console.WriteLine(today);
```

  Member                Meaning
  --------------------- --------------------------------
  `DateTime.Now`        Current local date and time
  `DateTime.UtcNow`     Current UTC date and time
  `DateTime.Today`      Current local date at midnight
  `DateTime.MinValue`   Minimum supported value
  `DateTime.MaxValue`   Maximum supported value

Create a specific date:

``` csharp
DateTime joiningDate = new DateTime(2026, 11, 2);
DateTime meeting = new DateTime(2026, 11, 4, 14, 30, 0);
```

The second example represents 4 November 2026 at 14:30:00.

## 5.2 Formatting a `DateTime`

Use `ToString()` with a format string:

``` csharp
DateTime now = DateTime.Now;

Console.WriteLine(now.ToString("dd-MM-yyyy"));
Console.WriteLine(now.ToString("dd MMM yyyy"));
Console.WriteLine(now.ToString("hh:mm tt"));
Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss"));
```

  Format          Example
  --------------- ---------------
  `dd-MM-yyyy`    `04-11-2026`
  `dd MMM yyyy`   `04 Nov 2026`
  `hh:mm tt`      `10:30 AM`
  `HH:mm:ss`      `10:30:00`

`hh` is a 12-hour clock; `HH` is a 24-hour clock. The actual output can
depend on culture settings.

## 5.3 The `DateTime.Kind` property

A `DateTime` has a `Kind` property:

-   `DateTimeKind.Local`: represents local system time.
-   `DateTimeKind.Utc`: represents UTC.
-   `DateTimeKind.Unspecified`: does not identify a time zone.

``` csharp
DateTime local = DateTime.Now;
DateTime utc = DateTime.UtcNow;
DateTime unspecified = new DateTime(2026, 11, 4);

Console.WriteLine(local.Kind);       // Local
Console.WriteLine(utc.Kind);         // Utc
Console.WriteLine(unspecified.Kind); // Unspecified
```

A `DateTime` with `Unspecified` kind does not tell you which time zone
the clock reading belongs to. Be careful when converting or comparing
date/time values with different kinds.

## 5.4 `DateTimeOffset`

`DateTimeOffset` represents a date and time together with an offset from
UTC.

``` csharp
DateTimeOffset now = DateTimeOffset.Now;

Console.WriteLine(now);
Console.WriteLine(now.Offset);
```

For example, an offset of `+05:30` means the represented local clock
time is five hours and thirty minutes ahead of UTC.

Create an explicit offset:

``` csharp
DateTimeOffset indiaTime =
    new DateTimeOffset(2026, 11, 4, 10, 30, 0, TimeSpan.FromHours(5.5));
```

The offset makes it possible to identify the corresponding instant in
UTC.

### `DateTime` vs `DateTimeOffset`

  -----------------------------------------------------------------------
  `DateTime`                          `DateTimeOffset`
  ----------------------------------- -----------------------------------
  Represents a date and time          Represents a date and time with a
                                      UTC offset

  Has `Kind`                          Has `Offset`

  May be `Local`, `Utc`, or           Carries an explicit offset
  `Unspecified`                       

  Can be ambiguous if its time-zone   Identifies an instant relative to
  context is unclear                  UTC
  -----------------------------------------------------------------------

A `DateTimeOffset` stores an offset, not a named time zone. An offset
such as `+05:30` does not by itself identify a region or its historical
and future time-zone rules.

Practical guidance:

-   Use `DateTime` for simple date/time values when the time-zone
    meaning is clear.
-   Use `DateTimeOffset` for event timestamps, audit records, and values
    exchanged between systems.
-   If an application must schedule an event according to a region's
    local clock, also retain the relevant named time zone.

## 5.5 UTC vs local time

UTC (Coordinated Universal Time) is a shared time reference. Local time
is the clock time used in a particular time zone.

India Standard Time (IST) is UTC+05:30. Therefore, when UTC is 05:00,
the corresponding Indian local time is 10:30.

Get current UTC and local time:

``` csharp
DateTime local = DateTime.Now;
DateTime utc = DateTime.UtcNow;

Console.WriteLine($"Local: {local}");
Console.WriteLine($"UTC: {utc}");
```

Convert UTC `DateTime` to the machine's local time:

``` csharp
DateTime utc = DateTime.UtcNow;
DateTime local = utc.ToLocalTime();
```

This conversion uses the system's local time zone.

### Convert using `DateTimeOffset`

``` csharp
DateTimeOffset utc = DateTimeOffset.UtcNow;

DateTimeOffset indiaTime =
    utc.ToOffset(TimeSpan.FromHours(5.5));

Console.WriteLine(indiaTime);
```

### Convert using a named time zone

``` csharp
TimeZoneInfo indiaZone =
    TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");

DateTime indiaTime = TimeZoneInfo.ConvertTimeFromUtc(
    DateTime.UtcNow,
    indiaZone
);

Console.WriteLine(indiaTime);
```

`Asia/Kolkata` is an IANA time-zone identifier commonly available on
Linux systems. Time-zone identifiers can differ by platform and runtime
configuration; check the target environment when writing portable
software.

### Practical time-zone guidance

-   Store event timestamps as UTC or `DateTimeOffset`.
-   Convert timestamps to the user's local time for display.
-   Do not assume every location has the same UTC offset throughout the
    year; some regions observe daylight-saving time.
-   For future schedules tied to a city's local clock, retain the named
    time zone, not only the current offset.

## 5.6 `TimeSpan`

`TimeSpan` represents a duration or difference between two date/time
values.

Create a duration:

``` csharp
TimeSpan duration = new TimeSpan(2, 30, 0);

Console.WriteLine(duration); // 02:30:00
```

The constructor arguments above are hours, minutes, and seconds.

Other constructors:

``` csharp
TimeSpan twoAndHalfHours = TimeSpan.FromHours(2.5);
TimeSpan fortyFiveMinutes = TimeSpan.FromMinutes(45);
TimeSpan thirtySeconds = TimeSpan.FromSeconds(30);
```

## 5.7 Calculate the difference between two times

Subtracting one `DateTime` from another produces a `TimeSpan`.

``` csharp
DateTime start = new DateTime(2026, 11, 4, 9, 0, 0);
DateTime end = new DateTime(2026, 11, 4, 17, 30, 0);

TimeSpan workingDuration = end - start;

Console.WriteLine(workingDuration);           // 08:30:00
Console.WriteLine(workingDuration.TotalHours); // 8.5
```

For timestamps that represent real-world instants, `DateTimeOffset`
subtraction is also useful:

``` csharp
DateTimeOffset checkIn = DateTimeOffset.Now;
DateTimeOffset checkOut = checkIn.AddHours(8);

TimeSpan duration = checkOut - checkIn;

Console.WriteLine(duration.TotalHours);
```

## 5.8 `TimeSpan` properties

  Property         Meaning
  ---------------- --------------------------------------
  `Days`           Whole-day component
  `Hours`          Hour component after whole days
  `Minutes`        Minute component
  `Seconds`        Second component
  `TotalDays`      Entire duration expressed in days
  `TotalHours`     Entire duration expressed in hours
  `TotalMinutes`   Entire duration expressed in minutes
  `TotalSeconds`   Entire duration expressed in seconds

Example:

``` csharp
TimeSpan duration = TimeSpan.FromHours(30);

Console.WriteLine(duration.Days);       // 1
Console.WriteLine(duration.Hours);      // 6
Console.WriteLine(duration.TotalHours); // 30
```

`Hours` is only the hour component. `TotalHours` represents the full
duration.

## 5.9 Adding and subtracting time

`DateTime` and `DateTimeOffset` provide methods for date/time
arithmetic:

``` csharp
DateTime now = DateTime.Now;

DateTime tomorrow = now.AddDays(1);
DateTime nextWeek = now.AddDays(7);
DateTime afterTwoHours = now.AddHours(2);
DateTime yesterday = now.AddDays(-1);
```

You can also add or subtract a `TimeSpan`:

``` csharp
TimeSpan duration = TimeSpan.FromMinutes(90);

DateTime start = DateTime.Now;
DateTime end = start + duration;
```

## 5.10 Attendance example

This example combines `DateTimeOffset` and `TimeSpan`:

``` csharp
DateTimeOffset checkIn = DateTimeOffset.Now;

// Example: employee checks out eight hours later.
DateTimeOffset checkOut = checkIn.AddHours(8);

TimeSpan workingDuration = checkOut - checkIn;

Console.WriteLine($"Check-in: {checkIn}");
Console.WriteLine($"Check-out: {checkOut}");
Console.WriteLine($"Working hours: {workingDuration.TotalHours}");
```

For a real attendance application, read and validate actual timestamps
instead of assuming a fixed checkout time.

### Date and time practice

Create a `DateTimePractice` class:

1.  Read an employee's name.
2.  Read check-in and check-out timestamps.
3.  Parse them as `DateTimeOffset`.
4.  Calculate the elapsed duration using `TimeSpan`.
5.  Display total working hours.
6.  Display the current UTC time.
7.  Validate input and handle invalid values.

For predictable input formats, consider `DateTimeOffset.TryParseExact()`
with a specified format and culture.

