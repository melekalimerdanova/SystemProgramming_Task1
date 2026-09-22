using System;
using System.Diagnostics;
using System.Threading;

namespace SystemProgramming_Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(() =>
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== Son 20 Proses ===");
                    var allProcesses = Process.GetProcesses();
                    int count = 0;
                    for (int i = allProcesses.Length - 1; i >= 0; i--)
                    {
                        Console.WriteLine($"Id: {allProcesses[i].Id}");
                        Console.WriteLine($"Name: {allProcesses[i].ProcessName}");
                        count++;
                        if (count == 20)
                            break;
                    }
                    Thread.Sleep(500);
                }
            });

            Thread thread2 = new Thread(() =>
            {
                Process.Start("mspaint");
                Thread.Sleep(3000);
                var procs = Process.GetProcessesByName("mspaint");
                foreach (var p in procs)
                {
                    p.Kill();
                }
            });
            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }
    }
}