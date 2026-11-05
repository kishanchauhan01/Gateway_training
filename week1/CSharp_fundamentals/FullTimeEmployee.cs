namespace CSharp_fundamentals;
public class FullTimeEmployee : Employee
{
    public double MonthlySalary {get; set;}

    public void DisplaySalary()
    {
        Console.WriteLine($"Salary: {MonthlySalary}");
    }
}