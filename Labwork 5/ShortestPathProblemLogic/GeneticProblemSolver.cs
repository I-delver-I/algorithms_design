namespace ShortestPathProblemLogic
{
    public class GeneticProblemSolver
    {
        private GraphOfSites _graph;
        private PopulationGenerator _populationGenerator;

        public GeneticProblemSolver(GraphOfSites graph, PopulationGenerator populationGenerator)
        {
            _graph = graph;
            _populationGenerator = populationGenerator;
        }

        public List<GraphEdge> Solve(int chromosomesCount)
        {
            List<GraphEdge> result = null;

            _populationGenerator.GenerateChromosomes(chromosomesCount);
            

            return result;
        }

        
    }
}