using System.Diagnostics;

namespace CSharp_Tasks;

public class TaskIntro
{
    private static readonly Stopwatch Sw = new();
    public static void M()
    {
        // Thread ID
        ThreadTask();
        
        // == Version 1 Traitement dans le Main (bloquant) ===
        Console.WriteLine("== Version 1 : Traitement dans le Main (bloquant) === ");
        TraitementBloquant();
        
        // reset du stop watch
        Sw.Reset();
        
        // == Version 2 : Traitement dans une Task (Main reste actif) ===
        Console.WriteLine("== Version 2 : Traitement dans une Task (Main reste actif) ===");
        Task t = Task.Run(() => TraitementNonBloquant());
        Console.WriteLine("Main: je continue...");
        while (!t.IsCompleted)
        {
            Console.Write(".");
            Thread.Sleep(30);
        }
        
        Console.ReadLine();
    }

    static void ThreadTask()
    {
        Console.WriteLine($"Main: ThreadId = {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine("Main: Lancement de la task...");
        Task.Run(() =>
        {
            Console.WriteLine($"Task: ThreadId = {Thread.CurrentThread.ManagedThreadId}");
            for (int i = 1; i < 4; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Task: etape {i}/3");
            }
            Console.WriteLine($"Main: fin du traitement.");
        });
    }

    static void TraitementBloquant()
    {
        Console.WriteLine("MAIN: début du traitement... ");
        Sw.Start();
        
        Task t = Task.Run(() => Thread.Sleep(3000));
        t.Wait();
        Console.WriteLine("MAIN: fin du traitement.");
        Sw.Stop();
        Console.WriteLine($"Main total = {Sw.ElapsedMilliseconds} ms");
    }

    static void TraitementNonBloquant()
    {
        Console.WriteLine("Task: début du traitement...");
        Sw.Start();
        Thread.Sleep(3000);
        Sw.Stop();
        Console.WriteLine("\nTask: Fin du traitement.");
        Console.WriteLine($"Main total = {Sw.ElapsedMilliseconds} ms");
    }
}