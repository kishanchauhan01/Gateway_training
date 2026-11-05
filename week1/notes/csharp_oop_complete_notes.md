# C# Object-Oriented Programming (OOP) --- Complete Notes

> A practical reference covering the OOP topics studied: classes and
> objects, encapsulation, inheritance, polymorphism, abstraction, static
> members, structs and records, composition, and SOLID.

------------------------------------------------------------------------

## Contents

1.  [OOP fundamentals](#1-oop-fundamentals)
2.  [Classes and objects](#2-classes-and-objects)
3.  [Fields and properties](#3-fields-and-properties)
4.  [Constructors, `this`, and object
    initializers](#4-constructors-this-and-object-initializers)
5.  [Value types and reference
    types](#5-value-types-and-reference-types)
6.  [Encapsulation](#6-encapsulation)
7.  [Inheritance](#7-inheritance)
8.  [Polymorphism](#8-polymorphism)
9.  [Abstraction](#9-abstraction)
10. [Static members](#10-static-members)
11. [Structs and records](#11-structs-and-records)
12. [Composition over inheritance](#12-composition-over-inheritance)
13. [SOLID principles](#13-solid-principles)
14. [Quick revision sheet](#14-quick-revision-sheet)
15. [Practice project](#15-practice-project)

------------------------------------------------------------------------

# 1. OOP fundamentals

**Object-Oriented Programming (OOP)** is a programming approach that
organizes software around objects. An object combines data (state) and
behaviour (methods).

### The four main OOP pillars

  -----------------------------------------------------------------------
  Pillar                  Meaning                 Common C# features
  ----------------------- ----------------------- -----------------------
  Encapsulation           Control access to an    `private`, properties,
                          object's internal state methods

  Inheritance             Build a type from       `:`, `base`, `sealed`
                          another type            

  Polymorphism            Use a common type with  overloading, `virtual`,
                          different behaviours    `override`, interfaces

  Abstraction             Expose essential        abstract classes,
                          behaviour and hide      interfaces
                          implementation details  
  -----------------------------------------------------------------------

These are conceptual pillars. Other topics---such as composition, static
members, structs, records, and SOLID---help design C# applications
effectively.

# 2. Classes and objects

## 2.1 Class

A **class** is a user-defined reference type that describes the data and
behaviour its objects can have.

``` csharp
public class Person
{
    public string Name { get; set; }

    public void Introduce()
    {
        Console.WriteLine($"Hello, I am {Name}");
    }
}
```

## 2.2 Object

An **object** is an instance of a class.

``` csharp
Person person = new Person();

person.Name = "Rahul";
person.Introduce();
```

-   `Person` is the class/type.
-   `person` is a variable holding a reference to an object.
-   `new Person()` creates an object.
-   `Name` is a property.
-   `Introduce()` is a method.

A class is the definition; an object is an instance created from that
definition.

# 3. Fields and properties

## 3.1 Fields

A **field** is a variable declared directly inside a class or struct. It
stores data for an instance or, if static, for the type.

``` csharp
public class Person
{
    private string _name;
    private int _age;
}
```

Instance fields belong to individual objects. Static fields are shared
by the type.

Naming convention: private fields commonly use `_camelCase`.

## 3.2 Properties

A **property** provides controlled access to data. It uses accessors:
`get` and `set`, or `init`.

### Auto-property

``` csharp
public class Person
{
    public string Name { get; set; }
}
```

The compiler provides the backing storage.

### Property with a backing field

Use this when you need validation or custom logic.

``` csharp
public class Person
{
    private int _age;

    public int Age
    {
        get { return _age; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Age cannot be negative.");

            _age = value;
        }
    }
}
```

Inside a setter, `value` represents the value being assigned.

### `get` and `private set`

``` csharp
public class Account
{
    public decimal Balance { get; private set; }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");

        Balance += amount;
    }
}
```

Other classes can read `Balance`, but only code inside `Account` can set
it directly.

### Get-only property

``` csharp
public class Person
{
    public string Id { get; }

    public Person(string id)
    {
        Id = id;
    }
}
```

A get-only auto-property can be assigned in its declaration or in an
instance constructor. It cannot be assigned later through ordinary
external code.

### `init` accessor

``` csharp
public class Person
{
    public string Name { get; init; }
}

Person person = new Person { Name = "Rahul" };
// person.Name = "Amit"; // Not allowed after initialization
```

`init` allows assignment during object initialization, including
construction, but not ordinary reassignment afterward.

### Field vs property

  Field                  Property
  ---------------------- -------------------------------------------
  Stores data directly   Provides access to data
  No accessor logic      Can include `get`, `set`, or `init` logic
  Usually kept private   Often used as the public API for data

Prefer properties for public data access. Keep implementation fields
private unless there is a clear reason otherwise.

# 4. Constructors, `this`, and object initializers

## 4.1 Constructor

A **constructor** initializes a new object.

Rules: - Its name must match the class name. - It has no return
type---not even `void`. - It runs when an object is created. - A
constructor can accept parameters. - If no instance constructor is
declared, C# normally supplies a parameterless one. Once you declare an
instance constructor, that implicit constructor is not supplied.

``` csharp
public class Person
{
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }
}

Person person = new Person("Rahul");
```

## 4.2 Constructor overloading

A class can have multiple constructors with different parameter lists.

``` csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person()
    {
        Name = "Unknown";
        Age = 0;
    }

    public Person(string name)
    {
        Name = name;
        Age = 0;
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

## 4.3 `this` keyword

`this` refers to the current instance of the class.

It is commonly used to distinguish a field or property from a parameter
with the same name.

``` csharp
public class Person
{
    private string _name;

    public Person(string name)
    {
        this._name = name;
    }
}
```

Here, `this._name` refers to the current object's field.

## 4.4 Constructor chaining with `this(...)`

`this(...)` calls another constructor in the same class.

``` csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person() : this("Unknown", 0)
    {
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```

This avoids repeating initialization logic.

## 4.5 Object initializer

An object initializer assigns accessible properties or fields after the
constructor has run.

``` csharp
Person person = new Person("Rahul", 22)
{
    Age = 23
};
```

Conceptually, the order is: 1. The constructor executes. 2. The object
initializer assignments execute.

An object initializer does not replace the constructor.

# 5. Value types and reference types

C# types are broadly divided into **value types** and **reference
types**.

## 5.1 Value types

A value-type variable contains its value. Assigning it to another
variable copies that value.

Examples: - Numeric types such as `int`, `double`, and `decimal` -
`bool`, `char` - `struct` - `enum`

``` csharp
int first = 10;
int second = first;

second = 20;

Console.WriteLine(first);  // 10
Console.WriteLine(second); // 20
```

## 5.2 Reference types

A reference-type variable holds a reference to an object. Assigning it
to another variable copies the reference, so both variables can refer to
the same object.

Examples: - `class` - `string` - Arrays - Interfaces - Delegates

``` csharp
public class Person
{
    public string Name { get; set; }
}

Person first = new Person { Name = "Rahul" };
Person second = first;

second.Name = "Amit";

Console.WriteLine(first.Name);  // Amit
Console.WriteLine(second.Name); // Amit
```

Both variables refer to the same `Person` object.

> **Important:** Value type does not mean "always stored on the stack,"
> and reference type does not mean "the variable is always on the stack
> while the object is always on the heap." Actual storage and
> optimization depend on context and the runtime. Focus on the
> assignment and copying behaviour.

# 6. Encapsulation

**Encapsulation** means keeping data and the operations that work on it
together, while controlling how the object's internal state can be
accessed or changed.

It helps protect an object from invalid state.

## 6.1 Access modifiers

  Modifier      Accessibility
  ------------- -------------------------------------------------------
  `public`      Accessible wherever the containing type is accessible
  `private`     Only within the containing type
  `protected`   Within the containing type and its derived types
  `internal`    Within the same assembly

C# also has combinations such as `protected internal` and
`private protected`.

## 6.2 Encapsulation with validation

``` csharp
public class BankAccount
{
    public string AccountHolder { get; }

    public decimal Balance { get; private set; }

    public BankAccount(string accountHolder)
    {
        AccountHolder = accountHolder;
        Balance = 0;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit must be positive.");

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal must be positive.");

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient balance.");

        Balance -= amount;
    }
}
```

The caller cannot directly assign an arbitrary balance. It must use the
methods that enforce the rules.

### Why encapsulation matters

-   Prevents invalid data changes.
-   Hides internal implementation details.
-   Makes code easier to maintain.
-   Gives a class control over its own state.

# 7. Inheritance

**Inheritance** allows a class to derive from another class and reuse or
specialize its accessible members.

It represents an **IS-A** relationship.

Example: A car is a vehicle.

``` csharp
public class Vehicle
{
    public void Start()
    {
        Console.WriteLine("Vehicle started");
    }
}

public class Car : Vehicle
{
    public void Drive()
    {
        Console.WriteLine("Car is driving");
    }
}

Car car = new Car();
car.Start();
car.Drive();
```

## 7.1 Inheritance types

Common class inheritance structures:

-   **Single:** One derived class inherits from one base class.
-   **Multilevel:** A class derives from a class that is itself derived
    from another class.
-   **Hierarchical:** Multiple classes derive from the same base class.

C# does not support multiple inheritance from classes. A class can
implement multiple interfaces.

## 7.2 `base` keyword

`base` refers to the immediate base class.

It can call a base constructor or access a base implementation.

``` csharp
public class Vehicle
{
    public string Brand { get; }

    public Vehicle(string brand)
    {
        Brand = brand;
    }

    public void Display()
    {
        Console.WriteLine(Brand);
    }
}

public class Car : Vehicle
{
    public Car(string brand) : base(brand)
    {
    }

    public void Show()
    {
        base.Display();
    }
}
```

## 7.3 Constructor chaining in inheritance

The base constructor runs before the derived constructor body.

``` csharp
public class Animal
{
    public Animal(string name)
    {
        Console.WriteLine($"Animal: {name}");
    }
}

public class Dog : Animal
{
    public Dog(string name) : base(name)
    {
        Console.WriteLine("Dog constructor");
    }
}
```

If you do not explicitly use `base(...)`, C# attempts to call the base
class's parameterless constructor. If no accessible parameterless
constructor exists, you must call an appropriate base constructor
explicitly.

Constructors are not inherited.

## 7.4 `sealed`

A sealed class cannot be inherited.

``` csharp
public sealed class SecurityManager
{
}
```

A method override can also be sealed to prevent further overriding down
the inheritance chain.

``` csharp
public class Base
{
    public virtual void Run() { }
}

public class Middle : Base
{
    public sealed override void Run() { }
}
```

# 8. Polymorphism

**Polymorphism** means "many forms." A common method or base type can
represent different behaviours.

## 8.1 Compile-time polymorphism: method overloading

Method overloading means using the same method name with different
parameter lists.

``` csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}
```

The compiler selects the appropriate overload based on the arguments.

**Important:** You cannot overload methods by changing only their return
type.

## 8.2 Runtime polymorphism: `virtual` and `override`

A base class marks a method as `virtual`. A derived class provides a
replacement using `override`.

``` csharp
public class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal sound");
    }
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Bark");
    }
}

Animal animal = new Dog();
animal.MakeSound(); // Bark
```

The actual object's runtime type determines which overridden
implementation runs.

## 8.3 Method hiding with `new`

The `new` modifier hides an inherited member rather than overriding it.

``` csharp
public class Parent
{
    public void Show()
    {
        Console.WriteLine("Parent");
    }
}

public class Child : Parent
{
    public new void Show()
    {
        Console.WriteLine("Child");
    }
}

Parent item = new Child();
item.Show(); // Parent

Child child = new Child();
child.Show(); // Child
```

With method hiding, the member selected depends on the reference's
compile-time type.

`new` here is a member-hiding modifier. It is different from the `new`
operator used to create objects.

### Overriding vs hiding

  -----------------------------------------------------------------------
  `override`                          `new`
  ----------------------------------- -----------------------------------
  Replaces a virtual base             Hides an inherited member
  implementation                      

  Runtime polymorphism                Member selection based on reference
                                      type

  Requires a suitable                 Does not require a virtual base
  `virtual`/`abstract` base member    member
  -----------------------------------------------------------------------

# 9. Abstraction

**Abstraction** exposes what an object can do while hiding unnecessary
implementation details.

C# commonly uses abstract classes and interfaces.

## 9.1 Abstract classes

An abstract class cannot be instantiated directly. It can contain: -
Fields and properties - Constructors - Concrete methods - Abstract
methods

An abstract method has no implementation in the abstract class and must
be implemented by a concrete derived class.

``` csharp
public abstract class Payment
{
    public decimal Amount { get; set; }

    protected Payment(decimal amount)
    {
        Amount = amount;
    }

    public void DisplayAmount()
    {
        Console.WriteLine(Amount);
    }

    public abstract void ProcessPayment();
}

public class CardPayment : Payment
{
    public CardPayment(decimal amount) : base(amount)
    {
    }

    public override void ProcessPayment()
    {
        Console.WriteLine("Processing card payment");
    }
}
```

## 9.2 Interfaces

An interface defines a contract that implementing types agree to
provide.

``` csharp
public interface IPayment
{
    void Pay(decimal amount);
    void Refund(decimal amount);
}

public class UpiPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount} using UPI");
    }

    public void Refund(decimal amount)
    {
        Console.WriteLine($"Refunded {amount} using UPI");
    }
}
```

A class can implement multiple interfaces:

``` csharp
public interface IPrintable
{
    void Print();
}

public interface IScannable
{
    void Scan();
}

public class MultiFunctionPrinter : IPrintable, IScannable
{
    public void Print() { }
    public void Scan() { }
}
```

Ordinary interface members without an implementation are public contract
members. Modern C# interfaces can also provide default implementations
and static members. Interfaces do not have instance fields or
constructors.

## 9.3 Abstract class vs interface

  -----------------------------------------------------------------------
  Abstract class                      Interface
  ----------------------------------- -----------------------------------
  Can contain instance state and      Cannot contain instance fields
  fields                              

  Can have constructors               Cannot have instance constructors

  Can contain implemented and         Defines a contract; can also have
  abstract methods                    default implementations

  A class can inherit from only one   A class can implement multiple
  base class                          interfaces

  Useful for shared base state and    Useful for capabilities and common
  behaviour                           contracts
  -----------------------------------------------------------------------

Use an abstract class when related types share meaningful state or
implementation. Use an interface when different types need to promise
the same capability.

# 10. Static members

The `static` keyword means a member belongs to the **type itself**,
rather than to each object.

## 10.1 Static fields

A static field is shared by all instances of its type.

``` csharp
public class Employee
{
    public string Name { get; set; }

    public static int EmployeeCount = 0;

    public Employee(string name)
    {
        Name = name;
        EmployeeCount++;
    }
}

Employee first = new Employee("Rahul");
Employee second = new Employee("Amit");

Console.WriteLine(Employee.EmployeeCount); // 2
```

`Name` is per object. `EmployeeCount` is shared.

## 10.2 Static methods

A static method can be called using the type name without creating an
object.

``` csharp
public class Calculator
{
    public static int Add(int a, int b)
    {
        return a + b;
    }
}

int result = Calculator.Add(10, 20);
```

A static method cannot directly access instance members because it has
no current instance (`this`). It can access static members and can use
an instance passed to it as an argument.

## 10.3 Static classes

A static class: - Cannot be instantiated. - Cannot be used as a base
class. - Can contain only static members.

``` csharp
public static class MathHelper
{
    public static int Square(int number)
    {
        return number * number;
    }
}

Console.WriteLine(MathHelper.Square(5)); // 25
```

Use static classes for stateless utility functionality, not as a default
place to put every shared method.

## 10.4 Static constructor

A static constructor initializes static state.

``` csharp
public class Database
{
    public static string ConnectionName;

    static Database()
    {
        ConnectionName = "ProductionDB";
        Console.WriteLine("Static constructor executed");
    }
}
```

Rules: - Runs automatically before the type is first used in a way that
requires initialization. - Runs at most once per type in an
application. - Has no parameters. - Cannot have an access modifier. -
Cannot be called directly.

# 11. Structs and records

## 11.1 Struct

A `struct` is a value type. Assigning one struct variable to another
copies its value.

``` csharp
public struct Point
{
    public int X;
    public int Y;
}

Point first = new Point { X = 10, Y = 20 };
Point second = first;

second.X = 100;

Console.WriteLine(first.X);  // 10
Console.WriteLine(second.X); // 100
```

Structs can contain fields, properties, constructors, methods, and
implement interfaces. They cannot inherit from another class or struct,
although they implicitly derive from `System.ValueType`.

Structs are often suitable for small, value-like data. Prefer immutable
structs where practical.

## 11.2 Class vs struct

  -----------------------------------------------------------------------
  Class                               Struct
  ----------------------------------- -----------------------------------
  Reference type                      Value type

  Assignment copies the reference     Assignment copies the value

  Supports class inheritance          Does not support class inheritance

  Can inherit from a base class       Cannot inherit from another class
                                      or struct

  Can implement interfaces            Can implement interfaces
  -----------------------------------------------------------------------

Choose based on the intended semantics and behaviour, not simply because
one seems faster.

## 11.3 Records

Records are types designed for data-centric models. They provide
value-based equality by default.

### Record class

``` csharp
public record Person(string Name, int Age);

Person first = new Person("Rahul", 22);
Person second = new Person("Rahul", 22);

Console.WriteLine(first == second); // True
```

Although these are two separate instances, record equality compares
their values by default.

### Non-destructive mutation with `with`

``` csharp
Person updated = first with { Age = 23 };

Console.WriteLine(first.Age);   // 22
Console.WriteLine(updated.Age); // 23
```

The `with` expression creates a copy with the specified change. It does
not modify the original record.

### Record struct

``` csharp
public record struct Product(string Name, decimal Price);

Product firstProduct = new Product("Keyboard", 1500m);
Product secondProduct = new Product("Keyboard", 1500m);

Console.WriteLine(firstProduct == secondProduct); // True
```

A `record struct` is a value type with record-style value equality.

## 11.4 Class vs struct vs record

  --------------------------------------------------------------------------
  Feature        Class          Struct         Record class   Record struct
  -------------- -------------- -------------- -------------- --------------
  Type category  Reference      Value          Reference      Value

  Value-based    No             No             Yes            Yes
  equality by                                                 
  default                                                     

  Class          Yes            No             Yes, record    No
  inheritance                                  inheritance    

  Typical use    Objects with   Small          Data models    Value-like
                 identity and   value-like     with value     data with
                 behaviour      data           equality       value equality
  --------------------------------------------------------------------------

Records are not automatically deeply immutable. Positional record
properties are init-only by default, but records can still contain
mutable members or reference types.

# 12. Composition over inheritance

Composition and inheritance describe different relationships between
objects and types.

## 12.1 Inheritance: IS-A

A derived type is a specialized kind of its base type.

Example: A car is a vehicle.

``` csharp
public class Vehicle
{
    public void Start()
    {
        Console.WriteLine("Vehicle started");
    }
}

public class Car : Vehicle
{
    public void Drive()
    {
        Console.WriteLine("Car is driving");
    }
}
```

Use inheritance when: - The relationship is genuinely IS-A. - A derived
object can correctly stand in for its base type. - The shared behaviour
belongs naturally in the base class.

## 12.2 Composition: HAS-A

One class contains or uses an object of another type.

Example: A car has an engine.

``` csharp
public class Engine
{
    public void Start()
    {
        Console.WriteLine("Engine started");
    }
}

public class Car
{
    private readonly Engine _engine;

    public Car()
    {
        _engine = new Engine();
    }

    public void StartCar()
    {
        _engine.Start();
        Console.WriteLine("Car started");
    }
}
```

Use composition when: - One object needs another object's
functionality. - You want to separate responsibilities. - You want to
replace components independently. - Inheritance would create an
incorrect relationship or unnecessary coupling.

## 12.3 Composition with interfaces and dependency injection

Instead of constructing a concrete dependency inside a class, accept an
abstraction through the constructor.

``` csharp
public interface IEngine
{
    void Start();
}

public class PetrolEngine : IEngine
{
    public void Start()
    {
        Console.WriteLine("Petrol engine started");
    }
}

public class ElectricEngine : IEngine
{
    public void Start()
    {
        Console.WriteLine("Electric engine started");
    }
}

public class Car
{
    private readonly IEngine _engine;

    public Car(IEngine engine)
    {
        _engine = engine;
    }

    public void Start()
    {
        _engine.Start();
    }
}

Car petrolCar = new Car(new PetrolEngine());
Car electricCar = new Car(new ElectricEngine());

petrolCar.Start();
electricCar.Start();
```

`Car` depends on the `IEngine` abstraction. It does not need to know
which concrete engine it receives.

This is constructor injection, a common form of **Dependency Injection
(DI)**.

## 12.4 Composition vs aggregation

Both are HAS-A relationships, but they express different ownership
strengths.

-   **Composition:** Strong ownership. The contained object is
    conceptually part of the owner. Example: a room as part of a house
    in a simplified domain model.
-   **Aggregation:** Weaker association. The related object can exist
    independently. Example: an employee associated with a department.

### Comparison

  -----------------------------------------------------------------------
  Inheritance                         Composition
  ----------------------------------- -----------------------------------
  IS-A                                HAS-A

  Uses a base class                   Uses another object

  Can create tighter coupling         Often allows looser coupling

  Reuses and specializes a hierarchy  Delegates work to collaborators

  Best for genuine subtype            Best for assembling behaviour from
  relationships                       components
  -----------------------------------------------------------------------

**Rule:** Prefer composition for code reuse when there is no genuine
IS-A relationship. Do not use inheritance just to avoid writing a few
methods.

# 13. SOLID principles

SOLID is a group of five object-oriented design principles that help
make code easier to maintain, extend, test, and reuse.

  -----------------------------------------------------------------------
  Letter                  Principle               Main idea
  ----------------------- ----------------------- -----------------------
  S                       Single Responsibility   One responsibility per
                                                  class

  O                       Open/Closed             Open for extension,
                                                  closed for modification

  L                       Liskov Substitution     Derived types should
                                                  replace base types
                                                  correctly

  I                       Interface Segregation   Prefer small, focused
                                                  interfaces

  D                       Dependency Inversion    Depend on abstractions,
                                                  not concrete
                                                  implementations
  -----------------------------------------------------------------------

## 13.1 S --- Single Responsibility Principle (SRP)

**A class should have one responsibility and one main reason to
change.**

Problem: One class handles unrelated responsibilities.

``` csharp
// Poor separation: this class handles several concerns.
public class Employee
{
    public void CalculateSalary() { }
    public void SaveToDatabase() { }
    public void GenerateReport() { }
}
```

Better: Separate the responsibilities.

``` csharp
public class Employee
{
    public void CalculateSalary() { }
}

public class EmployeeRepository
{
    public void SaveToDatabase(Employee employee) { }
}

public class EmployeeReport
{
    public void GenerateReport(Employee employee) { }
}
```

**Remember:** Avoid classes that do unrelated jobs.

## 13.2 O --- Open/Closed Principle (OCP)

**Software entities should be open for extension but closed for
modification.**

You should be able to add new behaviour without repeatedly changing
stable, existing code.

Problem: Every new discount type requires another condition in the same
method.

``` csharp
public class Discount
{
    public decimal Calculate(string type, decimal price)
    {
        if (type == "Regular")
            return price * 0.10m;

        if (type == "Premium")
            return price * 0.20m;

        return 0;
    }
}
```

Better: Define an abstraction and add implementations.

``` csharp
public interface IDiscount
{
    decimal Calculate(decimal price);
}

public class RegularDiscount : IDiscount
{
    public decimal Calculate(decimal price)
        => price * 0.10m;
}

public class PremiumDiscount : IDiscount
{
    public decimal Calculate(decimal price)
        => price * 0.20m;
}

public class FestivalDiscount : IDiscount
{
    public decimal Calculate(decimal price)
        => price * 0.30m;
}
```

Adding `FestivalDiscount` does not require modifying the existing
discount implementations.

**Remember:** Extend behaviour through new implementations rather than
repeatedly editing stable logic.

## 13.3 L --- Liskov Substitution Principle (LSP)

**A derived class should be usable wherever its base class is expected
without breaking the program's correct behaviour.**

Problem: A base type promises that every bird can fly, but a penguin
cannot.

``` csharp
public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("Flying");
    }
}

public class Penguin : Bird
{
    public override void Fly()
    {
        throw new NotSupportedException();
    }
}
```

Better: Put flying behaviour in a separate capability.

``` csharp
public class Bird
{
    public void Eat()
    {
        Console.WriteLine("Eating");
    }
}

public interface IFlyingBird
{
    void Fly();
}

public class Sparrow : Bird, IFlyingBird
{
    public void Fly()
    {
        Console.WriteLine("Sparrow flying");
    }
}

public class Penguin : Bird
{
}
```

**Remember:** Do not force a subtype to violate the expectations of its
base type.

## 13.4 I --- Interface Segregation Principle (ISP)

**A class should not be forced to implement methods it does not need.**

Problem: A robot can work, but it does not eat.

``` csharp
public interface IWorker
{
    void Work();
    void Eat();
}
```

Better: Split the interface into focused capabilities.

``` csharp
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public class Human : IWorkable, IEatable
{
    public void Work() => Console.WriteLine("Human working");
    public void Eat() => Console.WriteLine("Human eating");
}

public class Robot : IWorkable
{
    public void Work() => Console.WriteLine("Robot working");
}
```

**Remember:** Prefer several small, focused interfaces over one
unnecessarily large interface.

## 13.5 D --- Dependency Inversion Principle (DIP)

**High-level modules should depend on abstractions, not directly on
low-level concrete implementations.**

Problem: `Notification` creates and depends directly on `EmailService`.

``` csharp
public class EmailService
{
    public void Send(string message)
    {
        Console.WriteLine("Email: " + message);
    }
}

public class Notification
{
    private EmailService _email = new EmailService();

    public void Notify()
    {
        _email.Send("Hello!");
    }
}
```

Better: Depend on an interface and inject the implementation.

``` csharp
public interface IMessageService
{
    void Send(string message);
}

public class EmailService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine("Email: " + message);
    }
}

public class SmsService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine("SMS: " + message);
    }
}

public class Notification
{
    private readonly IMessageService _service;

    public Notification(IMessageService service)
    {
        _service = service;
    }

    public void Notify()
    {
        _service.Send("Hello!");
    }
}

Notification emailNotification = new Notification(new EmailService());
Notification smsNotification = new Notification(new SmsService());

emailNotification.Notify();
smsNotification.Notify();
```

`Notification` depends on `IMessageService`, not on a particular
messaging provider.

**Remember:** Depend on abstractions. Dependency Injection is one way to
supply the concrete implementation.

## 13.6 SOLID quick reference

  -----------------------------------------------------------------------
  Principle               Short reminder          Helps prevent
  ----------------------- ----------------------- -----------------------
  SRP                     One responsibility      God classes

  OCP                     Extend without          Risky changes
                          repeatedly modifying    
                          stable code             

  LSP                     Subtypes must honour    Broken inheritance
                          base-type expectations  

  ISP                     Small, focused          Unnecessary methods
                          interfaces              

  DIP                     Depend on abstractions  Tight coupling
  -----------------------------------------------------------------------

SOLID principles are design guidelines, not a requirement to create an
interface for every class. Apply them when they solve a real design
problem; avoid overengineering.

# 14. Quick revision sheet

  Topic                Key point
  -------------------- ---------------------------------------------------------
  Class                A user-defined reference type
  Object               An instance of a class
  Field                A variable declared in a type
  Property             Controlled access to data
  `get`                Reads a property value
  `set`                Assigns a property value
  `init`               Allows assignment during initialization
  Constructor          Initializes a new instance
  `this`               Refers to the current instance
  `this(...)`          Calls another constructor in the same type
  Object initializer   Assigns members after construction
  Encapsulation        Protects and controls internal state
  `private`            Accessible inside the containing type
  `protected`          Accessible in the containing type and derived types
  `internal`           Accessible within the same assembly
  Inheritance          IS-A relationship
  `base`               Refers to the immediate base class
  `sealed`             Prevents inheritance or further overriding
  Overloading          Same method name, different parameter list
  `virtual`            Allows a method to be overridden
  `override`           Replaces a virtual or abstract base implementation
  `new` modifier       Hides an inherited member
  Abstract class       Base type that cannot be instantiated directly
  Interface            Contract/capability a type implements
  Static field         Shared by all instances of the type
  Static method        Called using the type name
  Static class         Cannot be instantiated or inherited
  Struct               Value type
  Record class         Reference type with value-based equality
  Record struct        Value type with value-based equality
  Composition          HAS-A relationship
  Aggregation          Weaker HAS-A relationship
  SOLID                Five principles for maintainable object-oriented design

## Important distinctions to remember

### `set` vs `init`

-   `set`: Can be used to change a property after object creation,
    subject to its accessibility.
-   `init`: Can be used during initialization, but not ordinary
    reassignment afterward.

### `override` vs `new`

-   `override`: Runtime polymorphism; the actual object type determines
    the overridden implementation.
-   `new`: Hides a base member; the reference type determines which
    member is selected.

### Abstract class vs interface

-   Abstract class: Useful for shared state and implementation among
    related types.
-   Interface: Useful for defining capabilities or contracts across
    different types.

### Class vs struct

-   Class: Reference semantics.
-   Struct: Value semantics.

### Inheritance vs composition

-   Inheritance: IS-A.
-   Composition: HAS-A.

