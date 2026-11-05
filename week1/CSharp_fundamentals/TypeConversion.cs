public class TypeConversion
{
    public static void Run()
    {
        int number = 100;
        double result = number;
        Console.WriteLine(result); // 100

        // int a = 10;
        // long b = a;

        // float c = 10;
        // double d = c;

        // Explicit conversion
        double price = 99.99;
        int convertedPrice = (int)price;
        // Convert.ToInt32() rounds to the nearest integer.
        int b = Convert.ToInt32(price); // 100
        
        Console.WriteLine(convertedPrice); //99 
        Console.WriteLine(convertedPrice); //99 

        string ageInput = "22";
        int age = int.Parse(ageInput);
        Console.WriteLine(age + 5); 

        // Console.Write("Enter your age: ");

        // string? input = Console.ReadLine();
        // // TryParse() returns true if conversion succeeds.
        // bool success = int.TryParse(input, out int parsedAge);

        // if(success)
        // {
        //     Console.WriteLine($"Your age is {parsedAge}");
        // } else
        // {
        //     Console.WriteLine("Invalid age");
            
        // }

        string number1 = "100";
        int result1 = Convert.ToInt32(number1);

        decimal? bonus = null;
        // ?? returns the right-hand value when the left-hand value is null.
        string result2 = bonus?.ToString() ?? "Not assigned";

        int day = 2;

        switch(day)
        {
            case 1:
                Console.WriteLine("monday");
                break;

            case 2:
                Console.WriteLine("monday");
                break;

            default:
                Console.WriteLine("Invalid day");
                break;
        }

        // C# also supports a more concise switch expression.
        string dayName = day switch
        {
            1 => "monday",
            2 => "Tuesday",
            _ => "Weekend or invalid"
        };

        // Difference: A switch statement executes statements; a switch expression produces a value.
        Console.WriteLine(dayName);

        for(int i=0; i<5; i++)
        {
            Console.WriteLine(i);
        }

        string[] names = {"kishan", "jay", "rohan"};

        foreach(string name in names)
        {
            Console.WriteLine(name);
        }

        int k=1;
        while(k <= 5)
        {
            Console.WriteLine(k);
            k++;
        }

        int j = 10;
        do
        {
            Console.WriteLine(j);
            j++;
        } while(j<= 5);







    }
}