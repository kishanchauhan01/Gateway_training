namespace week1;

internal class Program
{
    private static void Main(string[] args)
    {
        // TypeConversion.Run();
        // MethodLearnings.Run();
        // StringLearnings.Run();
        // CollectionLearnings.Run();
        // ExceptionLearning.Run();
        // DateTimeLearnings.Run();

        // Object initializers can also assign init properties.
        // {} after Employee() is object initializer
        // The constructor executes first, then the object initializer assignments happen.
        Employee emp = new Employee("Kishan", 20)
        {
            LastName = "Chauhan"
        };

        Console.WriteLine(emp.Name);
        emp.Name = "John";
        emp.Age = 22;
        emp.Introduce();

        emp.Salary = 400000;
        Console.WriteLine(emp.Salary);
        Console.WriteLine(emp.LastName);

        BankAccount kishan = new BankAccount("KishanChauhan", 20);
        kishan.HolderAge = 50;
        kishan.Deposite(5000);
        Console.WriteLine(kishan.GetBalance());

        FullTimeEmployee empy = new FullTimeEmployee
        {
            Name = "Kishan",
            Age = 20,
            MonthlySalary = 50000.100
        };

        empy.DisplayDetails();
        empy.DisplaySalary();

        Manager manager = new Manager("Kishan", "IT");

        manager.DisplayBoth();

        Calculator calc = new Calculator();

        // The compiler determines which method to call based on the arguments.
        Console.WriteLine(calc.Add(10, 20));
        Console.WriteLine(calc.Add(10.5, 20.5));
        Console.WriteLine(calc.Add(10, 20, 30));

        Animal animal = new Dog();

        animal.Sound();

        Animal a = new Dog();
        Dog d = new Dog();
        
        a.Sound1();
        d.Sound1();



    }
}