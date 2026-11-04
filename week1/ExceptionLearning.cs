public class ExceptionLearning
{
    static void ValidateAge(int age)
    {
        if(age < 18)
        {
            throw new ArgumentException("Age must be 18 or above");
        }
    }
    public static void Run()
    {
        // // DivideByZeroException
        // int result = 10 / 0;

        try
        {
            int salary = int.Parse("abc");
            Console.WriteLine(salary);
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Please enter a valid number");
        }

        try
        {
            int a = 10;
            int b = 0;

            int result = a / b;

            Console.WriteLine(result);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Cannot divide by zero.", ex.Message);
        }

        try
        {
            int[] numbers = [10, 20, 30];

            Console.Write("Enter an index: ");
            int index = int.Parse(Console.ReadLine()!);

            Console.WriteLine(numbers[index]);
        }
        catch (FormatException)
        {
            Console.WriteLine("Enter a valid integer.");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Inex does not exist.");

        }

        // try
        // {
        //     // Risky code
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"Something went wrong: {ex.Message}"); // // ex.Message gives a description of the error.
        // }

        try
        {
            Console.WriteLine("Trying operation");
            int result = 10 / 2;

        }
        catch(DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero");
        }
        finally
        {
            Console.WriteLine("Operation complete");
        }

        try
        {
            ValidateAge(15);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}