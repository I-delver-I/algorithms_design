namespace TravelingSalesmanProblemLogic
{
    public class AntMover
    {
        private EdgeSelector _edgeSelector;
        private GraphOfSites _graph;

        public AntMover(EdgeSelector edgeSelector, GraphOfSites graph)
        {
            _edgeSelector = edgeSelector;
            _graph = graph;
        }

        public void MoveAntToNextVertex(Ant antToMove)
        {
            if (antToMove.GetVisitedVerticesCount() != _graph.GetVerticesCount())
            {
                var optimalEdge = _edgeSelector
                    .AntSelectOptimalEdge(antToMove.IsElite, antToMove.GetVerticesNumbers().ToArray());
                antToMove.AddVisitedVerticeNumber(optimalEdge);
                optimalEdge.AddPassedAnt(antToMove);
            }
        }
    }
}