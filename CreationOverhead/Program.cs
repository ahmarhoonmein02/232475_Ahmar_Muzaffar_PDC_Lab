using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
class Program
{
    const int Iterations = 50;
    static void Main()
    {
        string processLabPath = FindProcessLabExecutable();
        Console.WriteLine("ProcessLab executable:");
        Console.WriteLine(processLabPath);
        Console.WriteLine();

        Stopwatch processStopwatch = Stopwatch.StartNew();
        for (int i = 0; i < Iterations; i++)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = processLabPath,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            startInfo.ArgumentList.Add("--trivial");
            using Process process = Process.Start(startInfo)!;
            process.WaitForExit();
        }
        processStopwatch.Stop();
        Stopwatch threadStopwatch = Stopwatch.StartNew();

        for (int i = 0; i < Iterations; i++)
        {
            Thread t = new Thread(() =>
            {
            });

            t.Start();
            t.Join();
        }
        threadStopwatch.Stop();

        double avgProcessMs =
            processStopwatch.Elapsed.TotalMilliseconds / Iterations;

        double avgThreadMs =
            threadStopwatch.Elapsed.TotalMilliseconds / Iterations;

        double ratio = avgProcessMs / avgThreadMs;

        Console.WriteLine($"Iterations: {Iterations}");
        Console.WriteLine(
            $"Total process time: {processStopwatch.Elapsed.TotalMilliseconds:F3} ms");

        Console.WriteLine(
            $"Total thread time: {threadStopwatch.Elapsed.TotalMilliseconds:F3} ms");

        Console.WriteLine(
            $"Average process creation time: {avgProcessMs:F3} ms");

        Console.WriteLine(
            $"Average thread creation time: {avgThreadMs:F3} ms");

        Console.WriteLine(
            $"Process/Thread ratio: {ratio:F1}x");

        Console.WriteLine(
            $"Process creation was {ratio:F1}x more expensive than thread creation.");
    }
  static string FindProcessLabExecutable()
    {
        string solutionPath = Path.GetFullPath(
            Path.Combine(
                AppContext.BaseDirectory,
                "..",
                "..",
                "..",
                ".."));
        string processLabPath = Path.Combine(
            solutionPath,
            "ProcessLab",
            "bin",
            "Release",
            "net8.0",
            "ProcessLab.exe");
        if (File.Exists(processLabPath))
        {
            return processLabPath;
        }
        throw new FileNotFoundException(
            "ProcessLab.exe was not found. Build ProcessLab in Release mode first.",
            processLabPath);
    }
}