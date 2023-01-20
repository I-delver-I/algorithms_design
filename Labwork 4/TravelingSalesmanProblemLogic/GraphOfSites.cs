using System.Linq;
using System.Text;

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
                for (var i = 1; i < _vertices.Count; i++)
                {
                    for (var j = i + 1; j < _vertices.Count + 1; j++)
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
            _edges.RemoveAll(e => (e.FirstVertex.Number == vertexNumber)
                || (e.SecondVertex.Number == vertexNumber));
            _vertices.Remove(_vertices.Find(v => v.Number == vertexNumber));
        }

        public bool VertexExists(int vertexNumber)
        {
            return _vertices.Exists(v => v.Number == vertexNumber);
        }

        public List<AntGraphEdge> GetEdgesWithSpecifiedVertex(int vertexNumber)
        {
            return _edges.FindAll(e => (e.FirstVertex.Number == vertexNumber) 
                || (e.SecondVertex.Number == vertexNumber)).ToList();
        }

        public List<AntGraphEdge> GetEdgesWithSpecifiedVertexExceptVisitedOnes(int vertexNumber, 
            int[] visitedVerticesNumbers)
        {
            return GetEdgesWithSpecifiedVertex(vertexNumber)
                .Except(GetEdgesWithSpecifiedVertex(vertexNumber)
                    .Where(e => ((e.FirstVertex.Number == vertexNumber) 
                        && (visitedVerticesNumbers.Contains(e.SecondVertex.Number)))
                    || ((e.SecondVertex.Number == vertexNumber) 
                        && (visitedVerticesNumbers.Contains(e.FirstVertex.Number))))).ToList();
        }

        public int GetVerticesCount()
        {
            return _vertices.Count;
        }

        public GraphSiteVertex FindVertexByNumber(int vertexNumber)
        {
            return _vertices.Find(v => v.Number == vertexNumber);
        }

        public bool HasVertexEdge(int vertexNumber)
        {
            return _edges.Exists(e => 
                (e.FirstVertex.Number == vertexNumber) || (e.SecondVertex.Number == vertexNumber));
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
            return _edges.Exists(e => (e.FirstVertex.Number == firstVertexNumber) 
                && (e.SecondVertex.Number == secondVertexNumber));
        }

        public AntGraphEdge GetEdgeByVerticesNumbers(int firstVertexNumber, int secondVertexNumber)
        {
            return _edges.Find(e => 
                ((e.FirstVertex.Number == firstVertexNumber) 
                    && (e.SecondVertex.Number == secondVertexNumber)) 
                || ((e.FirstVertex.Number == secondVertexNumber) 
                    && (e.SecondVertex.Number == firstVertexNumber)));
        }

        public override string ToString()
        {
            var result = new StringBuilder().Append("Edges:\n");

            foreach (AntGraphEdge edge in _edges)
            {
                result.Append($" {edge} |");
            }

            var verticesWithoutEdges = new List<GraphSiteVertex>();
            var existVerticesWithoutEdge = false;
        
            foreach (GraphSiteVertex vertex in _vertices)
            {
                if (!HasVertexEdge(vertex.Number))
                {
                    existVerticesWithoutEdge = true;
                    verticesWithoutEdges.Add(vertex);
                }
            }

            if (existVerticesWithoutEdge)
            {
                result.Append("\n\nVertices without edge:\n");
                verticesWithoutEdges.ForEach(v => result.Append($" {v} |"));
            }

            return result.ToString();
        }
    }
}