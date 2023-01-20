using TravelingSalesmanProblemLogic.VerticesTransitionCounter;

namespace TravelingSalesmanProblemLogic
{
    public class AntProblemSolver
    {
        public int Alpha { get; } = 3;

        public int Beta { get; } = 2;

        public double Rho { get; } = 0.7;

        public double BestCurrentCycleLength { get; } = double.PositiveInfinity;

        public int MinimalSolutionPrice { get; private set; }
        public List<AntGraphEdge> MinimalSolutionPath { get; private set; } = new();

        private GraphOfSites _graph;
        private GreedyVerticesTransitionCounter _greedyCounter;

        public AntProblemSolver(GraphOfSites graph, GreedyVerticesTransitionCounter greedyCounter)
        {
            _graph = graph;
            _greedyCounter = greedyCounter;
            MinimalSolutionPrice = GetMinimalSolutionPrice();
        }

        private int GetMinimalSolutionPrice()
        {
            int initialVertexNumber = 1;
            var visitedVerticesNumbers = new List<int>() { initialVertexNumber };
            var currentVertexNumber = initialVertexNumber;
            var result = 0;

            do
            {
                var previousVertexNumber = currentVertexNumber;

                var optimalTransition = _greedyCounter
                    .GetOptimalTransition(currentVertexNumber, visitedVerticesNumbers.ToArray(), _graph);
                
                if (optimalTransition.FirstVertex.Number == currentVertexNumber)
                {
                    currentVertexNumber = optimalTransition.SecondVertex.Number;
                }
                else
                {
                    currentVertexNumber = optimalTransition.FirstVertex.Number;
                }

                result += optimalTransition.Length;
                visitedVerticesNumbers.Add(currentVertexNumber);
                MinimalSolutionPath
                    .Add(_graph.GetEdgeByVerticesNumbers(previousVertexNumber, currentVertexNumber));
            } 
            while (visitedVerticesNumbers.Count != _graph.GetVerticesCount());
 
            var edgeOfStartAndEndVertices = _graph.GetEdgeByVerticesNumbers
                (visitedVerticesNumbers.Last(), initialVertexNumber);
            MinimalSolutionPath.Add(edgeOfStartAndEndVertices);

            result += edgeOfStartAndEndVertices.Length;

            return result;
        } 
    }
}