namespace TravelingSalesmanProblemLogic.VerticesTransitionCounter
{
    public interface IVerticesTransitionCounterable
    {
        public AntGraphEdge GetOptimalTransition(int vertexNumber, int[] visitedVerticesNumbers, 
            GraphOfSites graph);
    }
}