using System.Diagnostics;

namespace CSharp_Tasks;

public class TaskReturn
{
    
    private static readonly Stopwatch Sw = Stopwatch.StartNew();
    
    public static void M()
    {
        Console.WriteLine("== Version somme avec Task Return ==");
        Console.WriteLine("Main : somme = " + ComputeSum().Result);
        Sw.Reset();
        Console.WriteLine("== Verison somme en Main ==");
        Console.WriteLine("Main : somme = " + ComputeSumMain());
    }

    private static Task<int> ComputeSum()
    {
        Task<int> sum = Task.Run(() => {
        Console.WriteLine("Task : Lancement du comptage");
            Sw.Start();
            int somme = 0;
            for (int i = 1; i < 11; i++)
            {
                somme += i;
                Thread.Sleep(3000);
            }
            Console.WriteLine("Task: Fin du comptage");
            Sw.Stop();
            Console.WriteLine($"Task total = {Sw.ElapsedMilliseconds}ms");
            return somme;
        });
        while (!sum.IsCompleted)
        {
            Console.WriteLine("Main: je continue...");
            Thread.Sleep(5000);
        }
        return sum;
    }

    private static int ComputeSumMain()
    {
        Console.WriteLine("Main: debut du traitement");
        Sw.Start();
        int sum = 0;
        for (int i = 1; i < 11; i++)
        {
            sum += i;
            Thread.Sleep(3000);
        }
        Console.WriteLine("Main: fin du traitement");
        Sw.Stop();
        Console.WriteLine($"Main total = {Sw.ElapsedMilliseconds}ms");
        return sum;
    }
}