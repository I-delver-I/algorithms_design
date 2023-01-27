using ShortestPathProblemLogic;
using ShortestPathProblemLogic.Crossover;
using ShortestPathProblemLogic.LocalImprovement;
using ShortestPathProblemLogic.Mutation;

namespace ShortestPathProblemRunner
{
    public static class SolvingCases
    {
        public static void UseSolvingCase(GraphOfSites graph, 
            PopulationGenerator populationGenerator, ICrossoverable crossover)
        {
            IMutationMakable mutationMaker = new InsertionMutationMaker(graph);
            ILocalImprovable localImprover = new ProfitableVertexReplacementLocalImprover(graph);
            var solver = new GeneticProblemSolver
                (graph, populationGenerator, crossover, mutationMaker, localImprover);

            int chromosomesCount = DataCatcher.CatchChromosomesCount();
            int iterationsCount = DataCatcher.CatchIterationsCount();

            System.Console.WriteLine("Please, wait. Solving the shortest path problem...");
            var caseShortestPath = solver.Solve(chromosomesCount, iterationsCount);

            System.Console.WriteLine($"\nShortest path: {caseShortestPath.Sum(e => e.Length)}");

            foreach (GraphEdge edge in caseShortestPath)
            {
                System.Console.WriteLine($" {edge} |");
            }
        }
    }
}