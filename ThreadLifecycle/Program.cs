using System;
using System.Threading;

class Program
{
    static void Worker()
    {
        Thread.Sleep(200);
    }

    static void Main()
    {
        Thread t = new Thread(Worker);

        Console.WriteLine($"After creation: {t.ThreadState}");
        Console.WriteLine("Conceptual state: New");

        t.Start();

        Console.WriteLine($"Immediately after Start(): {t.ThreadState}");
        Console.WriteLine("Conceptual state: Runnable / Ready or Running");

        Thread.Sleep(50);

        Console.WriteLine($"While worker is sleeping: {t.ThreadState}");
        Console.WriteLine("Conceptual state: Blocked / Waiting");

        t.Join();

        Console.WriteLine($"After Join() completes: {t.ThreadState}");
        Console.WriteLine("Conceptual state: Terminated");
    }
}