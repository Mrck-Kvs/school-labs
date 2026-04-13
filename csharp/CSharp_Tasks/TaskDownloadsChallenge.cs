using System.Diagnostics;

namespace CSharp_Tasks;

public class TaskDownloadsChallenge
{
    public record DownloadResult(string FileName, int DurationMs, string Status);
    private static readonly Stopwatch Sw = Stopwatch.StartNew();
    public static void M()
    {
        Console.WriteLine("Bienvenue dans le challenge");
        // == Version A :  séquentielle == 
        Console.WriteLine("== Version A :  séquentielle ==");
        Sw.Start();
        DownloadSimulated(@"C:/");
        DownloadSimulated(@"C:/");
        DownloadSimulated(@"C:/");
        Sw.Stop();
        Console.WriteLine($"Sequential total = {Sw.ElapsedMilliseconds}ms");
        
        Sw.Reset();
        
        // == Version B : concurrente == 
        Console.WriteLine("== Version B :  concurrente ==");
        Sw.Start();
        Task[] tasks = new Task[3];
        for (int i = 0; i < 3; i++)
        {
            tasks[i] =  Task.Run(() => DownloadSimulated(@"C:/"));
        }
        
        Task.WaitAll(tasks);
        Sw.Stop();
        Console.WriteLine($"Concurrent total = {Sw.ElapsedMilliseconds}ms");
    }

    static DownloadResult DownloadSimulated(string fileName)
    {
        Random random = new Random();
        int duree = random.Next(1000, 5001);
        Console.WriteLine($"Start {fileName} ({duree} ms)");
        Thread.Sleep(duree);
        Console.WriteLine($"Done {fileName}");
        return new DownloadResult(fileName, duree, "OK");
    }
}