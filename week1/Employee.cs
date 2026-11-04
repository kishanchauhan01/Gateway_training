namespace week1;

public class Employee
{
    public string Name;

    // An init accessor restricts assignment to initialization time.
    // emp.Name = "Rahul"; // Compilation error
    public string LastName { get; init; }

    // These are called auto-implemented properties (auto-properties).
    // Useful when you want simple data access without custom validation.
    public int Age { get; set; }
    private double salary;

    // setter is private here only indside the class it can me modified
    public double AdminSalary { get; private set; }

    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name}");
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Age: {Age}");
    }

    public double Salary
    {
        get { return salary; }
        set
        {
            if (value >= 0)
            {
                salary = value;
            }
        }
    }

    public Employee()
    {
        Name = "Unkown";
        Age = 0;
    }

    public Employee(string name)
    {
        this.Name = name;
        this.Age = 0;
    }

    // This is called constructor chaining.    
    public Employee(string name, int age) : this(name)
    {
        // this refers to the current instance of the class.
        this.Age = age;
    }

    public virtual void Work()
    {
        Console.WriteLine("Employee working");
    }
}

public class Manager : Employee
{
    public void Display()
    {
        Console.WriteLine("Manager details");
    }

    public void DisplayBoth()
    {
        base.DisplayDetails();
        Display();
    }

    public void UpdateName()
    {
        base.Name = "Chauhan";
    }
    
    public string Department { get; set; }

    // The base constructor executes before the derived constructor body.
    public Manager(string name, string department) : base(name) // It calls the parameterized constructor of the parent class.
    {
        Department = department;
        Console.WriteLine("Manager constructor");
    }

    // public Manager() : this("Kishan")
    // {
    // }

    // public Manager(string name) : base(name, 25)
    // {
    // }

    // Now another class can inherit from Manager, but it cannot override Work() again.
    public sealed override void Work()
    {
        Console.WriteLine("Manager managing");
    }
}