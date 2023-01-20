namespace TravelingSalesmanProblemRunner;

using System.Text;
using TravelingSalesmanProblemLogic;
using TravelingSalesmanProblemLogic.VerticesTransitionCounter;

public class Program
{
    public static void Main(string[] args)
    {
        int initialVerticesCount = 5;

        var graph = new GraphOfSites(initialVerticesCount);
        var greedyCounter = new GreedyVerticesTransitionCounter();
        var solver = new AntProblemSolver(graph, greedyCounter);

        System.Console.WriteLine(graph);
        System.Console.WriteLine($"Minimal solution price - {solver.MinimalSolutionPrice}");

        var builder = new StringBuilder().Append("\nMinimal solution path:\n");
        solver.MinimalSolutionPath.ForEach(e => builder.Append($" {e} |"));
        System.Console.WriteLine(builder.ToString());
        // var antSpawner = new AntSpawner(graph);
        // antSpawner.SpawnAnts();
    }
}