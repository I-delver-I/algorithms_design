using ShortestPathProblemLogic;

namespace ShortestPathProblemRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int initialVerticesCount = 300;
            int startVertexNumber = 1;
            int endVertexNumber = 300;
            int chromosomesCount = 50;

            var graph = new GraphOfSites(initialVerticesCount);
            var populationGenerator = new PopulationGenerator
                (graph, startVertexNumber, endVertexNumber);

            System.Console.WriteLine("Graph:");
            System.Console.WriteLine(graph);

            populationGenerator.GenerateChromosomes(chromosomesCount);
            System.Console.WriteLine("\nChromosomes:");
            
            foreach (Chromosome chromosome in populationGenerator)
            {
                System.Console.WriteLine(chromosome);
            }
        }
    }
}