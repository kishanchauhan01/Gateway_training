using LinqPractice.Data;
using LinqPractice.Models;

using var db = new AppDbContext();

// Create database and table if they don't exist
db.Database.EnsureCreated();

// Insert sample data only once
if (!db.Departments.Any() && !db.Employees.Any())
{
    db.Departments.AddRange(
        new Department { Id = 1, Name = "Engineering" },
        new Department { Id = 2, Name = "Marketing" },
        new Department { Id = 3, Name = "Finance" }
    );

    db.Employees.AddRange(
        new Employee { Id = 1, Name = "Alice Johnson", DepartmentId = 1, Salary = 75000, IsActive = true },
        new Employee { Id = 2, Name = "Bob Smith", DepartmentId = 1, Salary = 82000, IsActive = true },
        new Employee { Id = 3, Name = "Carol Davis", DepartmentId = 2, Salary = 68000, IsActive = true },
        new Employee { Id = 4, Name = "David Wilson", DepartmentId = 2, Salary = 71000, IsActive = false },
        new Employee { Id = 5, Name = "Eva Brown", DepartmentId = 3, Salary = 77000, IsActive = true },
        new Employee { Id = 6, Name = "Frank Miller", DepartmentId = 3, Salary = 69000, IsActive = false }
    );

    db.SaveChanges();
}

// fetch the data
var depratments = db.Departments.ToList();
var employees = db.Employees.ToList();

Console.WriteLine("All departments");

foreach (var department in depratments)
{
    Console.WriteLine($"{department.Id} {department.Name}");
}
Console.WriteLine("---------------\n");

// Query Syntax (SQL-like)
var highEarnersQuery = from emp in employees
                       where emp.Salary > 1000
                       orderby emp.Name
                       select emp.Name;

// Method Syntax (Fluent API)
var highEarnersMethod = employees
                        .Where(emp => emp.Salary > 1000)
                        .OrderBy(emp => emp.Name)
                        .Select(emp => emp.Name);

// Get all active employees in Engineering
var activeEngineers = employees
                        .Where(emp => emp.IsActive && emp.DepartmentId == 1);


// Project into an anonymous object containing only Name and Annual Bonus (10%)
var bonuses = employees
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    e.Name,
                    Bouns = e.Salary * 0.10m
                }).ToList();

foreach (var bonus in bonuses)
{
    Console.WriteLine($"{bonus.Name} {bonus.Bouns}");
}
Console.WriteLine("---------------\n");


// Sort by DepartmentId ascending, then by Salary highest-to-lowest
// Pitfall: Calling.OrderBy() twice in a row overwrites the first sort. Always chain secondary sorts with .ThenBy().
var sortedEmployees = employees
                        .OrderBy(e => e.DepartmentId)
                        .ThenBy(e => e.Salary);

foreach (var sortedEmployee in sortedEmployees)
{
    Console.WriteLine($"{sortedEmployee.DepartmentId} {sortedEmployee.Salary}");
}
Console.WriteLine("---------------\n");

// Groups elements that share a common attribute into an IEnumerable<IGrouping<TKey, TElement>>. Each group has a Key property and acts as a collection of the items in that group.
var employeesByDept = employees
                        .GroupBy(e => e.DepartmentId);

foreach (var group in employeesByDept)
{
    Console.WriteLine($"Departemtn ID: {group.Key} | Count: {group.Count()}");
    foreach (var emp in group)
    {
        Console.WriteLine($" - {emp.Name} (${emp.Salary})");
    }
}
Console.WriteLine("---------------\n");

/* 
this is a vizualize structre of group
[
  {
    "key": 1,
    "count": 2,
    "items": [
      { "name": "Alice Johnson", "salary": 75000 },
      { "name": "Bob Smith", "salary": 82000 }
    ]
  },
  {
    "key": 2,
    "count": 2,
    "items": [
      { "name": "Carol Davis", "salary": 68000 },
      { "name": "David Wilson", "salary": 71000 }
    ]
  }
]
*/


// Correlates elements of two sequences based on matching keys (equivalent to an SQL INNER JOIN).
var employeeDetails = employees.Join(
    inner: depratments,
    outerKeySelector: emp => emp.DepartmentId,
    innerKeySelector: dept => dept.Id,
    resultSelector: (emp, dept) => new
    {
        EmployeeName = emp.Name,
        DepartmentName = dept.Name,
        emp.Salary
    }
);

foreach(var employeeDetail in employeeDetails)
{
    Console.WriteLine($"{employeeDetail.EmployeeName} - {employeeDetail.DepartmentName} ({employeeDetail.Salary})");
}
Console.WriteLine("---------------\n");

// Output:
// Aarav - Engineering ($95000)
// Diya - Engineering ($110000)


// Any - Returns true if at least one element satisfies the condition (or if the collection is non-empty when called with no arguments).
// Are there any inactive employees?
bool hasInactive = employees.Any(emp => !emp.IsActive);
Console.WriteLine(hasInactive);

// All- Returns true only if every element satisfies the condition.
// Do all employees earn more than 50,000?
bool allAbove50k = employees.All(emp => emp.Salary > 50000);
Console.WriteLine(allAbove50k);


