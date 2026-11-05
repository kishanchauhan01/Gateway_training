// Inheritance means one class is a specialized version of another class.
// Example: A car is a vehicle.

public class Vehicle
{
    public void Start()
    {
        Console.WriteLine("Vehicle started");
    }
}

// Here, Car inherits the functionality of Vehicle.
public class Car : Vehicle
{
    public void Drive()
    {
        Console.WriteLine("Car is driving");
    }
}
// Use inheritance when:
// There is a genuine IS - A relationship.
// The derived class can correctly substitute the base class.
// You want to share common behaviour through a meaningful hierarchy.

// Composition means one class contains or uses an object of another class.
// Example: A car has an engine.

public class Engine
{
    public void Start()
    {
        Console.WriteLine("Engine started");
    }
}

public class Car1
{
    private Engine _engine;

    public Car1()
    {
        _engine = new Engine();
    }

    public void StartCar()
    {
        _engine.Start();
        Console.WriteLine("Car started");
    }
}

// Here, Car does not inherit from Engine. It uses an Engine object.
// Use composition when:
//  One object needs another object to perform its work.
//  You want to keep responsibilities separate.
//  You want to replace or change components independently.
//  Inheritance would create unnecessary coupling.
