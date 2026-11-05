namespace CSharp_fundamentals;

// A struct is a value type, whereas a class is a reference type.
public struct Point
{
    public int x;
    public int y;
}

// Records are designed for data-centric types. They provide value-based equality by default.

public record Person(string Name, int Age);