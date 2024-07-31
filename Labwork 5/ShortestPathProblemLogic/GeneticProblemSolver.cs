using ShortestPathProblemLogic.Crossover;
using ShortestPathProblemLogic.Mutation;
using ShortestPathProblemLogic.LocalImprovement;

namespace ShortestPathProblemLogic
{
    public class GeneticProblemSolver
    {
        private GraphOfSites _graph;
        private PopulationGenerator _populationGenerator;

        public ICrossoverable Crossover { get; set; }

        public IMutationMakable MutationMaker { get; set; }

        public ILocalImprovable LocalImprover { get; set; }

        public GeneticProblemSolver(GraphOfSites graph, PopulationGenerator populationGenerator,
            ICrossoverable crossover, IMutationMakable mutationMaker, ILocalImprovable localImprover)
        {
            _graph = graph;
            _populationGenerator = populationGenerator;
            Crossover = crossover;
            MutationMaker = mutationMaker;
            LocalImprover = localImprover;
        }

        public List<GraphEdge> Solve(int chromosomesCount, int iterationsCount)
        {
            _populationGenerator.GenerateChromosomes(chromosomesCount);
            var maximalMutationProbabilityLevel = 0.7;

            FileHandler.WriteLineToFile(DateTime.Now.ToString());

            for (var i = 0; i < iterationsCount; i++)
            {
                if ((i + 1) % 10 == 0)
                {
                    foreach (Chromosome chromosome in _populationGenerator)
                    {
                        FileHandler.WriteLineToFile($"{chromosome}\n");
                    }

                    FileHandler.WriteLineToFile($"The shortest path: {GetShortestPathLength()}\n");

                    foreach (GraphEdge edge in GetShortestPath())
                    {
                        FileHandler.WriteLineToFile($" {edge} |");
                    }
                }

                var offspring = Crossover.MakeCrossover();

                if (offspring is null)
                {
                    continue;
                }

                var random = new Random();
                var mutationProbability = random.NextDouble();

                if (mutationProbability <= maximalMutationProbabilityLevel)
                {
                    MutationMaker.MakeMutation(offspring);
                }

                LocalImprover.MakeImprovement(offspring);

                _populationGenerator.AddChromosome(offspring);
                _populationGenerator.RemoveChromosome(GetLongestChromosome());
            }

            return GetShortestPath();
        }

        public Chromosome GetLongestChromosome()
        {
            return _populationGenerator.MaxBy(c => c.GetLength(_graph));
        }

        public int GetShortestPathLength()
        {
            return _populationGenerator.Min(c => c.GetLength(_graph));
        }

        public List<GraphEdge> GetShortestPath()
        {
            return _graph.GetEdgesWhichConnectVertices(_populationGenerator
                .MinBy(c => c.GetLength(_graph)).GetVerticesNumbers());
        }
    }
}