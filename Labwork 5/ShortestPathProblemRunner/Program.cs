using ShortestPathProblemLogic;
using ShortestPathProblemLogic.Crossover;
using ShortestPathProblemLogic.Mutation;

namespace ShortestPathProblemRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int iterationsCount = 1000;
            int initialVerticesCount = 10;
            int startVertexNumber = 1;
            int endVertexNumber = 10;
            int chromosomesCount = 6;

            var graph = new GraphOfSites(initialVerticesCount);
            var populationGenerator = new PopulationGenerator
                (graph, startVertexNumber, endVertexNumber);
            var crossover = new SinglePointCrossover(populationGenerator, graph);
            var mutationMaker = new InsertionMutationMaker(graph);
            var solver = new GeneticProblemSolver(graph, populationGenerator, crossover, mutationMaker);

            System.Console.WriteLine("Graph:");
            System.Console.WriteLine(graph);

            var shortestPath = solver.Solve(chromosomesCount, iterationsCount);

            System.Console.WriteLine("\nChromosomes:");
            foreach (Chromosome chromosome in populationGenerator)
            {
                System.Console.WriteLine(chromosome);
            }
        }
    }
}