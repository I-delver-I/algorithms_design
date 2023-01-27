using ShortestPathProblemLogic;
using ShortestPathProblemLogic.Crossover;
using ShortestPathProblemLogic.Mutation;
using ShortestPathProblemLogic.LocalImprovement;

namespace ShortestPathProblemRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileHandler.ClearFile();

            var graph = DataCatcher.CatchGraph();
            var populationGenerator = DataCatcher.CatchPopulationGenerator(graph);
            var mutationMaker = new InsertionMutationMaker(graph);
            var localImprover = new ProfitableVertexReplacementLocalImprover(graph);
            var crossover = new SinglePointCrossover(populationGenerator, graph);
            var solver = new GeneticProblemSolver
                (graph, populationGenerator, crossover, mutationMaker, localImprover);

            int chromosomesCount = DataCatcher.CatchChromosomesCount(graph);
            int iterationsCount = DataCatcher.CatchIterationsCount();

            System.Console.WriteLine("Please, wait. Solving the shortest path problem...");
            var shortestPath = solver.Solve(chromosomesCount, iterationsCount);

            System.Console.WriteLine($"\nShortest path: {shortestPath.Sum(e => e.Length)}");

            foreach (GraphEdge edge in shortestPath)
            {
                System.Console.WriteLine($" {edge} |");
            }
        }
    }
}