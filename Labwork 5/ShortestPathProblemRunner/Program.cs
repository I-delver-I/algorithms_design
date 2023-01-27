using ShortestPathProblemLogic;
using ShortestPathProblemLogic.Crossover;

namespace ShortestPathProblemRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int iterationsCount = 1000;
            int initialVerticesCount = 5;
            int startVertexNumber = 1;
            int endVertexNumber = 5;
            int chromosomesCount = 3;

            var graph = new GraphOfSites(initialVerticesCount);
            var populationGenerator = new PopulationGenerator
                (graph, startVertexNumber, endVertexNumber);
            var crossover = new SinglePointCrossover(populationGenerator, graph);
            var solver = new GeneticProblemSolver(graph, populationGenerator, crossover);

            var shortestPath = solver.Solve(chromosomesCount, iterationsCount);

            System.Console.WriteLine("Graph:");
            System.Console.WriteLine(graph);

            System.Console.WriteLine("\nChromosomes:");
            foreach (Chromosome chromosome in populationGenerator)
            {
                System.Console.WriteLine(chromosome);
            }
        }
    }
}