public abstract class Animal1
{
    public string Name {get; set;}

    public void Eat()
    {
        Console.WriteLine("Animal is eating");
    }

    // An abstract method is a method declared without an implementation.
    public abstract void MakeSound();
}

// The base class defines what must happen. Derived classes define how it happens.
public class Dog1 : Animal1
{
    public override void MakeSound()
    {
        Console.WriteLine("Dog barks");
    }
}

// An interface defines a contract that implementing types must fulfil.

public interface IPrintable
{
    void Print();
}

// Interface names generally start with I, such as IPrintable, ILogger, IPayment.
public class Printer : IPrintable
{
    public void Print()
    {
        Console.WriteLine("Printing document...");
    }
}

public interface IScannable
{
    void Scan();
}

public class MultiFunctionPrinter : IPrintable, IScannable
{
    public void Print()
    {
        Console.WriteLine("Printing...");
    }

    public void Scan()
    {
        Console.WriteLine("Scanning...");
    }
}