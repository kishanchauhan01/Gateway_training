using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public class CollectionLearnings
{
    public static void Run()
    {
        string[] employees = ["kishan", "Jay", "Rahul"];
        // string[] employees1 = {"kishan", "Jay", "Rahul"};
        // employees.Append("Hello");
        Console.WriteLine(employees[2]);
        Console.WriteLine(employees.Length);

        int[] marks = new int[5];
        marks[0] = 1;
        marks[1] = 2;
        marks[2] = 3;

        Console.WriteLine(marks[3]);

        int[] numbers = { 5, 2, 8, 1, 9 };

        Array.Sort(numbers);     // 1, 2, 5, 8, 9
        Array.Reverse(numbers);  // 9, 8, 5, 2, 1

        int index = Array.IndexOf(numbers, 5);
        Console.WriteLine(index);

        List<string> employees2 = new List<string>
        {
            "ksiahn",
            "Rahul",
            "Jay"
        };

        // // shorter syntax
        // List<string> employees = ["Kishan", "Rahul", "Amit"];

        employees2.Add("Amit");
        employees2.AddRange(["Priya", "Neha"]);

        Console.WriteLine(employees2[0]);
        employees2[1] = "Rohan";
        employees2.Remove("Amit"); // Removes by value
        employees2.RemoveAt(0);    // Removes by index
        Console.WriteLine(employees2.Count());

        List<int> salaries = [25000, 40000, 60000, 75000];

        bool exists = salaries.Contains(40000);

        int highSalary = salaries.Find(s => s > 50000); // lambda expression

        List<int> highSalaries = salaries.FindAll(s => s > 50000);

        Dictionary<int, string> employees3 = new Dictionary<int, string>
        {
            {101, "kishan"},
            {102, "Rahul"},
            {458, "Jay"},
        };

        employees3.Add(104, "Amit");
        Console.WriteLine(employees3[101]);
        employees3[102] = "Prit";
        employees3.Remove(458);

        if (employees3.ContainsKey(102))
        {
            Console.WriteLine("employee found");
        }

        if (employees3.TryGetValue(101, out string name))
        {
            Console.WriteLine(name);
        }
        else
        {
            Console.WriteLine("Employee not found");
        }

        // A HashSet<T> stores unique elements only.
        HashSet<string> emails = new HashSet<string>();

        emails.Add("kishan@gmail.com");
        emails.Add("rahul@gmail.com");

        // Adding a duplicate
        emails.Add("kishan@gmail.com");

        // If the element already exists, Add() returns false.
        bool added = emails.Add("jay@gmail.com");
        Console.WriteLine(added);

        Console.WriteLine(emails.Contains("kishan@gmail.com"));

        emails.Remove("rahul@gmail.com");    // Remove element

        emails.Clear();                      // Remove everything

        HashSet<int> teamA = [1, 2, 3, 4];
        HashSet<int> teamB = [3, 4, 5, 6];

        teamA.UnionWith(teamB);
        // teamA: 1, 2, 3, 4, 5, 6

        // elements common to both.
        teamA.IntersectWith(teamB);

        // Elements in the first set but not the second.

        teamA.ExceptWith(teamB);

        for (int i = 0; i < employees.Length; i++)
        {
            Console.WriteLine(employees[i]);
        }

        foreach (string employee in employees2)
        {
            Console.WriteLine(employee);
        }

        foreach (var employee in employees3)
        {
            Console.WriteLine($"ID: {employee.Key}, Name: {employee.Value}");
        }

        foreach (KeyValuePair<int, string> employee in employees3)
        {
            Console.WriteLine($"{employee.Key}: {employee.Value}");
        }

        foreach (string email in emails)
        {
            Console.WriteLine(email);
        }
    }
}