using System.Text;

public class StringLearnings
{
    public static void Run()
    {
        // String interpolation
        string name = "Kishan";
        int age = 20;

        string message = $"My name is {name}, I'm {age} years old";

        Console.WriteLine(message);
        Console.WriteLine($"Sum: {5+5}");

        double percentage = 0.856;

        Console.WriteLine($"{percentage:P}");

        string text = "  Hello Gateway Group  ";
        Console.WriteLine(text.Trim());
        Console.WriteLine(text.TrimStart());
        Console.WriteLine(text.TrimEnd());

        string company = "Gateway Group";

        // bool result = company.Contains("Gateway");
        bool result = company.Contains("gateway", StringComparison.OrdinalIgnoreCase);
        Console.WriteLine(result);

        string message1 = "Hello Java";

        string result1 = message1.Replace("Java", "C#");
        Console.WriteLine(result1);

        string number = "123-456-789";
        Console.WriteLine(number.Replace("-", ""));

        string language = "Programming";
        string result2 = language.Substring(0,7);
        Console.WriteLine(result2);
        Console.WriteLine(language.Substring(7));

        string data = "Kishan,20,developer";

        string[] values = data.Split(',');
        
        Console.WriteLine(values[0]);
        foreach(string value in values)
        {
            Console.WriteLine(value);
        }
        
        string names = "Kishan";
        names = "Rahul";

        Console.WriteLine(name);
        names += "Kishan";
        Console.WriteLine(names);

        StringBuilder builder = new StringBuilder();
        builder.Append("Hello");
        builder.Append(" ");
        builder.Append("Kishan\n");

        Console.WriteLine(builder);

        StringBuilder builder1 = new StringBuilder();

        builder1.Append("Hello");
        builder1.AppendLine(" Gateway");
        builder1.Append("Group");

        builder1.Replace("Gateway", "Software");

        string result3 = builder1.ToString();

        Console.WriteLine(result3);
    }
}