using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public class Async
{
    public static async Task Run()
    {
        Console.WriteLine($"[Main] Application starting on Managed Thread Id: {Thread.CurrentThread.ManagedThreadId} \n");

        Console.WriteLine("=== SCENARIO 1: The Blocking (Synchronous) Way ===");

        SimulateBlockingCall();

        Console.WriteLine("\\n=== SCENARIO 2: The Non-Blocking (Async/Await) Way ===");

        await SimulateAsyncCall();

        Console.WriteLine($"\\n[Main] Application finished on Managed Thread ID: {Thread.CurrentThread.ManagedThreadId}");


    }

    // 1\. BLOCKING: Thread is frozen and wasted
    static void SimulateBlockingCall()
    {
        Console.WriteLine($"[Sync] Starting DB query on Thread ID: {Thread.CurrentThread.ManagedThreadId}");

        // Thread.Sleep forces the operating system thread to freeze
        // It cannot do any other work. It is holding memory and CPU resources hostage.
        Thread.Sleep(2000);


        Console.WriteLine($"[Sync] Finished DB query on Thread ID: {Thread.CurrentThread.ManagedThreadId} (Same thread was held hostage for 2s)");
    }

    // 2. ASYNC: Thread is released back to the pool
    static async Task SimulateAsyncCall()
    {
        Console.WriteLine($"[Async] Starting DB query on Thread ID: {Thread.CurrentThread.ManagedThreadId}");

        // Task.Delay simulates non-blocking I/O (like an EF Core DB call or HttpClient call)
        // At this exact point, the current thread leaves this method entirely!
        await Task.Delay(2000);

        // When the 2 seconds finish, the thread pool assigns an available thread to resume:
        Console.WriteLine($"[Async] Resumed after DB query on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
    }
}

