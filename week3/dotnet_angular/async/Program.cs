using System;
using System.Threading.Tasks;

class Program
{

    // static async Task SendEmailAsync(string email)
    // {
    //     Console.WriteLine($"[EmailService] Preparing email to {email}...");
    //     // Task.Delay creates a Task that completes after a delay (simulating I/O)
    //     await Task.Delay(1000);
    //     Console.WriteLine($"[EmailService] Email successfully sent to {email}.");
    // }

    // static async Task<int> GetPendingTicketCountAsync()
    // {
    //     Console.WriteLine("[Database] Querying pending tickets...");
    //     await Task.Delay(800); // Simulating database latency
    //     return 42; // Returns 'int', wrapped inside Task    
    // }

    // // Demonstrates error propagation inside a Task
    // static async Task SimulateFailingOperationAsync()
    // {
    //     await Task.Delay(300);
    //     throw new InvalidOperationException("External service was unavailable!");
    // }
    static async Task Main(string[] args)
    {
        // Console.WriteLine("--- 1. Task without a return value ---");
        // // Creating and awaiting a simple Task
        // Task sendNotificationTask = SendEmailAsync("trainee@gateway.com");

        // // Inspecting task state while it executes
        // Console.WriteLine($"Task status right after start: {sendNotificationTask.Status}");

        // // Awaiting the task to finish
        // await sendNotificationTask;
        // Console.WriteLine($"Task status after completion: {sendNotificationTask.Status}");

        // Console.WriteLine("\n--- 2. Task<T> with reaturn value");

        // // Fetching data asynchronously: Task<int>
        // Task<int> orderCountTask = GetPendingTicketCountAsync();

        // int totalTickets = await orderCountTask;
        // Console.WriteLine($"Result returned by Task<int>: {totalTickets} tickets");


        // Task<long> calculationTask = Task.Run(() =>
        // {
        //     long sum = 0;

        //     for (long i = 1; i <= 50_000_000; i++)
        //     {
        //         sum += i;
        //     }
        //     return sum;
        // });


        // Console.WriteLine("Main thread is free to do other work while calculation runs...");
        // long totalSum = await calculationTask;
        // Console.WriteLine($"Calculated sum: {totalSum}");

        // Console.WriteLine("\n--- 4. How Task handles exceptions ---");
        // try
        // {
        //     Task failingTask = SimulateFailingOperationAsync();
        //     await failingTask;
        // }
        // catch (InvalidOperationException ex)
        // { // The task captured the error inside and re-threw it when awaited 
        //     Console.WriteLine($"Caught error from Task: {ex.Message}");
        // }

        await Async.Run();
    }
}