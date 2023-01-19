using System.Linq;

namespace TravelingSalesmanProblemLogic
{
    public class GraphOfSites
    {
        private List<GraphSiteVertex> _vertices = new List<GraphSiteVertex>();
        private List<AntGraphEdge> _edges = new List<AntGraphEdge>();

        public int NextVertexNumber { get; private set; } = 1;

        public GraphOfSites(int initialVerticesCount, 
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
                    TryAddEdge(edgeNumberPair.Item1, edgeNumberPair.Item2);
                }
            }
            else
            {
                for (var i = _vertices.Count; i > 1; i--)
                {
                    for (var j = i - 1; j > 0; j--)
                    {
                        TryAddEdge(i, j);
                    }
                }
            }            
        }

        public void AddVertex()
        {
            _vertices.Add(new GraphSiteVertex(NextVertexNumber++));
        }

        public void RemoveVertex(int vertexNumber)
        {
            _edges.RemoveAll(e => (e.FirstPlace.Number == vertexNumber)
                || (e.SecondPlace.Number == vertexNumber));
            _vertices.Remove(_vertices.Find(v => v.Number == vertexNumber));
        }

        public bool VertexExists(int vertexNumber)
        {
            return _vertices.Exists(v => v.Number == vertexNumber);
        }


        public void TryAddEdge(int firstVertexNumber, int secondVertexNumber)
        {
            if (!EdgeExists(firstVertexNumber, secondVertexNumber))
            {
                var firstVertexOfEdge = _vertices.Find(v => v.Number == firstVertexNumber);
                var secondVertexOfEdge = _vertices.Find(v => v.Number == secondVertexNumber);

                if (firstVertexOfEdge is not null && secondVertexOfEdge is not null)
                {
                    _edges.Add(new AntGraphEdge(firstVertexOfEdge, secondVertexOfEdge));
                }
            }
        }

        public bool EdgeExists(int firstVertexNumber, int secondVertexNumber)
        {
            return _edges.Exists(e => (e.FirstPlace.Number == firstVertexNumber) 
                && (e.SecondPlace.Number == secondVertexNumber));
        }

        public int GetVerticesCount()
        {
            return _vertices.Count;
        }

        public GraphSiteVertex FindVertexByNumber(int vertexNumber)
        {
            return _vertices.Find(v => v.Number == vertexNumber);
        }
    }
}