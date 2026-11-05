namespace CSharp_fundamentals;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }
}

public class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Animal makes sound");
    }

    public void Sound1()
    {
        Console.WriteLine("Animal sound");
    }
}

public class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Dog barks");
    }

    public new void Sound1()
    {
        Console.WriteLine("Dog barks");
    }
}