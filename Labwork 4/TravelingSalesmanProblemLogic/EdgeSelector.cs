namespace TravelingSalesmanProblemLogic
{
    public class EdgeSelector
    {
        public int AlphaParameter { get; } = 3;

        public int BetaParameter { get; } = 2;

        private GraphOfSites _graph;

        public EdgeSelector(GraphOfSites graph)
        {
            _graph = graph;
        }

        public AntGraphEdge AntSelectOptimalEdge(bool isAntElite, int[] visitedVerticesNumbers)
        {
            var lastVisitedVertexNumber = visitedVerticesNumbers.Last();
            var applicableEdges = 
                _graph.GetEdgesWithSpecifiedVertexExcept(lastVisitedVertexNumber, 
                visitedVerticesNumbers);

            var transitionProbabilities = GetTransitionProbabilities(applicableEdges);

            if (isAntElite)
            {
                return transitionProbabilities.First
                    (p => p.Value == transitionProbabilities.Max(tp => tp.Value)).Key;
            }
            else
            {
                var random = new Random();
                var randomProbabilityNumber = random.NextDouble();

                double probabilitySum = 0;

                foreach (KeyValuePair<AntGraphEdge, double> edgeProbability 
                    in transitionProbabilities)
                {
                    probabilitySum += edgeProbability.Value;

                    if (randomProbabilityNumber <= probabilitySum)
                    {
                        return edgeProbability.Key;
                    }
                }

                return transitionProbabilities.Last().Key;
            }
        }

        private Dictionary<AntGraphEdge, double> GetTransitionProbabilities
            (List<AntGraphEdge> applicableEdges)
        {
            var result = new Dictionary<AntGraphEdge, double>();
            var transitionProbabilityDenominator = GetTransitionProbabilityDenominatorValue(applicableEdges);

            foreach (AntGraphEdge edge in applicableEdges)
            {
                var selectionProbability = (Math.Pow(edge.PheromonLevel, AlphaParameter) 
                    * Math.Pow(1 / (double)edge.Length, BetaParameter)) / transitionProbabilityDenominator;

                result.Add(edge, selectionProbability);
            }

            return result;
        }

        private double GetTransitionProbabilityDenominatorValue(List<AntGraphEdge> edges)
        {
            double denominator = 0;

            foreach (AntGraphEdge edge in edges)
            {
                denominator += 
                    (Math.Pow(edge.PheromonLevel, AlphaParameter) 
                        * Math.Pow(1 / (double)edge.Length, BetaParameter));
            }

            return denominator;
        }

        public AntGraphEdge GreedySelectOptimalEdge(int[] visitedVerticesNumbers)
        {
            var lastVisitedVertexNumber = visitedVerticesNumbers[^1];
            var vertexEdges = _graph
                .GetEdgesWithSpecifiedVertexExcept(lastVisitedVertexNumber, 
                    visitedVerticesNumbers);

            return vertexEdges.First(e => e.Length == vertexEdges.Min(e => e.Length));
        }
    }
}