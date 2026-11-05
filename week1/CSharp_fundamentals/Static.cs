namespace CSharp_fundamentals;

// The static keyword means a member belongs to the type itself, not to an individual object.

public class People
{
    public string Name {get; set;}

    public static int PeopleCount = 0;

    public People(string name)
    {
        this.Name = name;
        PeopleCount++;
    }

    // A static method can be called without creating an object.
    public static int Add(int a, int b)
    {
        return a + b;
    }
}

// A static class: Cannot be instantiated Cannot be inherited, Can contain only static members, Is useful for utility/helper functionality.

public static class MathHelper
{
    public static int Square(int number)
    {
        return number * number;
    }
}

public class Database
{
    public static string ConnectionName;

    static Database()
    {
        ConnectionName = "ProductionDb";
        Console.WriteLine("Static constructor executed");
    }
}

