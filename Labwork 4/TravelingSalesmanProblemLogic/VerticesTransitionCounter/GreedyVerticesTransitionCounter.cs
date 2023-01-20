namespace TravelingSalesmanProblemLogic.VerticesTransitionCounter
{
    public class GreedyVerticesTransitionCounter : IVerticesTransitionCounterable
    {
        public AntGraphEdge GetOptimalTransition(int vertexNumber, int[] visitedVerticesNumbers,
            GraphOfSites graph)
        {
            var vertexEdges = graph
                .GetEdgesWithSpecifiedVertexExceptVisitedOnes(vertexNumber, visitedVerticesNumbers);

            return vertexEdges.First(e => e.Length == vertexEdges.Min(e => e.Length));
        }
    }
}