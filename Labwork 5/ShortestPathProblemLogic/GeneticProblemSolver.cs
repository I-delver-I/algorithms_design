using ShortestPathProblemLogic.Crossover;

namespace ShortestPathProblemLogic
{
    public class GeneticProblemSolver
    {
        private GraphOfSites _graph;
        private PopulationGenerator _populationGenerator;

        public ICrossoverable Crossover { get; set; }

        public GeneticProblemSolver(GraphOfSites graph, PopulationGenerator populationGenerator,
            ICrossoverable crossover)
        {
            _graph = graph;
            _populationGenerator = populationGenerator;
            Crossover = crossover;
        }

        public List<GraphEdge> Solve(int chromosomesCount, int iterationsCount = 1000)
        {
            List<GraphEdge> shortestPath = null;

            _populationGenerator.GenerateChromosomes(chromosomesCount);
            var shortestPathLength = GetShortestPathLength();
            var maximalMutationProbabilityLevel = 0.2;

            for (var i = 0; i < iterationsCount; i++)
            {
                var offspring = Crossover.MakeCrossover();

                var random = new Random();
                var mutationProbability = random.NextDouble();

                if (mutationProbability <= maximalMutationProbabilityLevel)
                {
                    
                }
            }

            return shortestPath;
        }

        public int GetShortestPathLength()
        {
            return _populationGenerator.Min(c => _graph
                .GetEdgesWhichConnectVertices(c.GetVerticesNumbers()).Sum(e => e.Length));
        }

        public Chromosome GetShortestPath()
        {
            return _populationGenerator.First(c => _graph
                .GetEdgesWhichConnectVertices(c.GetVerticesNumbers())
                .Sum(e => e.Length) == GetShortestPathLength());
        }
    }
}