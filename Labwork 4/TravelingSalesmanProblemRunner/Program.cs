namespace TravelingSalesmanProblemRunner;

using System.Diagnostics;
using TravelingSalesmanProblemLogic;

public class Program
{
    public static void Main(string[] args)
    {
        int initialVerticesCount = 50;

        var graph = new GraphOfSites(initialVerticesCount);
        var edgeSelector = new EdgeSelector(graph);
        var antSpawner = new AntSpawner(graph);
        var antMover = new AntMover(edgeSelector, graph);
        var solver = new AntProblemSolver(graph, edgeSelector, antSpawner, antMover);

        // System.Console.WriteLine(graph);
        System.Console.WriteLine($"Minimal solution price - {solver.MinimalSolutionPrice}");

        try
        {
            antSpawner.SpawnAnts();
        }
        catch (InvalidOperationException ex)
        {
            System.Console.WriteLine(ex.Message);
            return;
        }

        var timer = new Stopwatch();
        timer.Start();
        System.Console.WriteLine("Please, wait. Solving traveling salesman problem...");
        var shortestGraphBypass = solver.Solve(100);
        timer.Stop();
        System.Console.WriteLine($"Solving took {timer.Elapsed} time");
        System.Console.WriteLine($"The shortest path length: {shortestGraphBypass.Sum(e => e.Length)}");

        System.Console.WriteLine($"The shortest graph bypass:");
        foreach (AntGraphEdge edge in shortestGraphBypass)
        {
            System.Console.Write($" {edge} |");
        }
    }
}