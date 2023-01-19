namespace TravelingSalesmanProblemLogic
{
    public class GraphOfPlacesToAttend
    {
        private List<PlaceToAttendVertex> _vertices = new List<PlaceToAttendVertex>();
        private List<GraphEdge> _edges = new List<GraphEdge>();
        private int _nextVertexNumber = 1;

        public GraphOfPlacesToAttend(int initialVerticesCount, 
            List<(int, int)> initialEdgesNumberPairs = null)
        {
            for (var i = 0; i < initialVerticesCount; i++)
            {
                AddVertex();
            }

            if (initialEdgesNumberPairs is not null)
            {
                foreach ((int, int) edgeNumberPair in initialEdgesNumberPairs)
                {
                    AddEdge(edgeNumberPair.Item1, edgeNumberPair.Item2);
                }
            }
        }

        public void AddVertex()
        {
            _vertices.Add(new PlaceToAttendVertex(_nextVertexNumber++));
        }

        public void RemoveVertex(int vertexNumber)
        {
            _edges.RemoveAll(e => (e.FirstPlace.Number == vertexNumber)
                || (e.SecondPlace.Number == vertexNumber));
            _vertices.Remove(_vertices.Find(v => v.Number == vertexNumber));
        }

        /// <exception cref="ArgumentException"></exception>
        public void AddEdge(int firstVertexNumber, int secondVertexNumber)
        {
            if (EdgeExists(firstVertexNumber, secondVertexNumber))
            {
                throw new ArgumentException
                    ("At least one of specified numbers of vertices doesn't exist");
            }

            _edges.Add(new GraphEdge(_vertices.Find(v => v.Number == firstVertexNumber),
                _vertices.Find(v => v.Number == secondVertexNumber)));
        }

        public bool EdgeExists(int firstVertexNumber, int secondVertexNumber)
        {
            return _edges.Exists(e => (e.FirstPlace.Number == firstVertexNumber) 
                && (e.SecondPlace.Number == secondVertexNumber));
        }
    }
}