namespace TravelingSalesmanProblemRunner;
using TravelingSalesmanProblemLogic;

public class Program
{
    public static void Main(string[] args)
    {
        var graph = new GraphOfSites(200);
        var antSpawner = new AntSpawner(graph);

        antSpawner.SpawnAnts();
    }
}