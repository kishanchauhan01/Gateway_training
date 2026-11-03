public class Methods
{
    static int Add(int a, int b)
    {
        int result = a + b;
        return result;
    }

    static int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    static double Add(double a, double b)
    {
        return a + b;
    }

    // Optional parameters must come after required parameters in the method declaration.
    static void Greet(string name, string message = "Welcome")
    {
        Console.WriteLine($"{message} {name}");
    }

    static void DisplayEmployee(string name, int age, decimal salary)
    {
        Console.WriteLine($"{name} {age} {salary}");
    }

    // Just like pointers and refrence in c/cpp
    static void ChangedValue(ref int number)
    {
        number = 100;
    }

    static void GetEmployee(out int employeeId)
    {
        employeeId = 101;
    }

    // Just a shorthand syntax to write method
    static int Square(int number) => number * number;
    public static void Run()
    {
        Console.WriteLine(Add(10, 20));
        Console.WriteLine(Add(10, 20, 30));
        Console.WriteLine(Add(10.5, 20.5));

        Greet("Kishan");

        Greet("Kishan", "Good morning!");

        // noraml call
        DisplayEmployee("Kishan", 20, 500000.522m);

        // named call
        // Here we can change the order of arguments
        DisplayEmployee(
            age: 50,
            salary: 100.200m,
            name: "kishan"
        );

        int value = 10;
        ChangedValue(ref value);
        Console.WriteLine(value);

        // no need to initialize the variable before passing it.
        // The method must assign a value to the out parameter before returnin
        // You must use out at the declaration and call site.
        GetEmployee(out int id);
        Console.WriteLine(id);


    }
}