namespace CSharp_fundamentals;

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

        Dog1 dog = new Dog1();

        dog.Name = "Tommy";
        dog.Eat();
        dog.MakeSound();

        IPrintable printer = new Printer();

        printer.Print();

        MultiFunctionPrinter device = new MultiFunctionPrinter();

        device.Print();
        device.Scan();

        People p = new People("Kishan");
        People p1 = new People("Rahul");

        Console.WriteLine(People.PeopleCount);

        int result = People.Add(5,6);
        Console.WriteLine(result);

        Console.WriteLine(MathHelper.Square(5)); // 25

        Point p11 = new Point{x=10, y = 20};

        Point p2 = p11;

        p2.x = 100;

        Console.WriteLine(p11.x); // 10
        Console.WriteLine(p2.x); // 100

        Person p3 = new Person("Rahul", 22);
        Person p4 = new Person("Rahul", 22);

        Console.WriteLine(p3 == p4); // True

    }
}