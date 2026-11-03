public class ExceptionLearning
{
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

    }
}