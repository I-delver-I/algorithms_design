

using ShortestPathProblemLogic;

namespace ShortestPathProblemRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int initialVerticesCount = 5;
            var graph = new GraphOfSites(initialVerticesCount);

            System.Console.WriteLine(graph);
        }
    }
}