namespace ShortestPathProblemLogic
{
    public class GeneticProblemSolver
    {
        private GraphOfSites _graph;

        public GeneticProblemSolver(GraphOfSites graph)
        {
            _graph = graph;
        }

        public List<GraphEdge> Solve(int startVertexNumber, int endVertexNumber)
        {
            List<GraphEdge> result = null;

            var chromosomes = GenerateChromosomes();

            

            return result;
        }

        private List<Chromosome> GenerateChromosomes()
        {

        } 
    }
}