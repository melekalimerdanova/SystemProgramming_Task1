using System.Diagnostics;

namespace SystemProgramming_Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1.Task managerde yaranmish son 20 olan processlerin
            //Ve onlar real olaraq gorunsun yeni her 0.5 saniyeden bir console refresh eden bir thread istifade edin

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=== Son 20 Proses ===");

                    var allProcesses = Process.GetProcesses();

                    int count = 0;
                    for (int i = allProcesses.Length - 1; i >= 0; i--)
                    {
                        Console.WriteLine($"Id: {allProcesses[i].Id} | Name: {allProcesses[i].ProcessName}");
                        count++;

                        if (count == 20)
                            break;
                    }

                    Console.WriteLine("\nKomanda daxil edin (mes: start mspaint / kill mspaint): ");
                    Thread.Sleep(500);
                }
            });
            thread.Start();

            //2. 1 ci ile eyni anda ekrandan kommandalar qebul edib icra eden bir thread yaradin
            while (true)
            {
                string input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    string[] parts = input.Split(' ');

                    if (parts.Length == 2)
                    {
                        string command = parts[0];
                        string processName = parts[1];

                        if (processName.EndsWith(".exe"))
                        {
                            processName = processName.Replace(".exe", "");
                        }

                        if (command == "start")
                        {
                            Process.Start(processName);
                        }
                        else if (command == "kill")
                        {
                            var allProcesses = Process.GetProcesses();
                            foreach (var p in allProcesses)
                            {
                                if (p.ProcessName.Contains(processName))
                                {
                                    p.Kill();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}