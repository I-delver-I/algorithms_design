using System.Collections;
using System.Text;

namespace ShortestPathProblemLogic
{
    public class GraphOfSites : IEnumerable<GraphEdge>
    {
        private List<GraphVertex> _vertices = new List<GraphVertex>();
        private List<GraphEdge> _edges = new List<GraphEdge>();
        private int _nextVertexNumber = 1;

        /// <exception cref="InvalidOperationException"></exception>
        public GraphOfSites(int initialVerticesCount)
        {
            for (var i = 0; i < initialVerticesCount; i++)
            {
                AddVertice();
            }

            GenerateEdgesCovering();
        }

        public GraphVertex GetVertexByNumber(int vertexNumber)
        {
            return _vertices.Find(v => v.Number == vertexNumber);
        }

        public void UpdateEdgeVertexNumber(GraphEdge edgeToUpdate, int numberToStay, int numberToAssign)
        {
            if (edgeToUpdate.FirstVertex.Number == numberToStay)
            {
                edgeToUpdate.SecondVertex = GetVertexByNumber(numberToAssign);
            }
            else
            {
                edgeToUpdate.FirstVertex = GetVertexByNumber(numberToAssign);
            }
        }

        public List<int> GetOtherVerticesOfEdgesWithSpecifiedVertexNumber(int vertexNumber)
        {
            return GetEdgesWithSpecifiedVertex(vertexNumber)
                .Select(e => (e.FirstVertex.Number == vertexNumber) 
                ? e.SecondVertex.Number : e.FirstVertex.Number).ToList();
        }

        public List<GraphEdge> GetEdgesWhichConnectVertices(List<int> verticesNumbers)
        {
            var result = new List<GraphEdge>();

            for (var i = 0; i < verticesNumbers.Count - 1; i++)
            {
                result.Add(FindEdgeByEndsNumbers(verticesNumbers[i], verticesNumbers[i + 1]));
            }

            return result;
        }

        public List<GraphEdge> GetEdgesWithSpecifiedVertex(int vertexNumber)
        {
            return _edges.FindAll(e => (e.FirstVertex.Number == vertexNumber) 
                || (e.SecondVertex.Number == vertexNumber)).ToList();
        }

        public List<GraphEdge> GetEdgesWithSpecifiedVertexExcept(int vertexNumber, 
            List<int> exceptionalVerticesNumbers)
        {
            return GetEdgesWithSpecifiedVertex(vertexNumber).Except
                (GetEdgesWithSpecifiedVertex(vertexNumber)
                .Where(e => ((e.FirstVertex.Number == vertexNumber) 
                    && (exceptionalVerticesNumbers.Contains(e.SecondVertex.Number)))
                    || ((e.SecondVertex.Number == vertexNumber) 
                    && (exceptionalVerticesNumbers.Contains(e.FirstVertex.Number))))).ToList();
        }

        /// <exception cref="InvalidOperationException"></exception>
        private void GenerateEdgesCovering()
        {
            if (GetVerticesCount() < GraphValidator.MinimalVerticesCount)
            {
                throw new InvalidOperationException("The count of vertices should be at least 2");
            }

            GenerateMinimalGraphEdgesCovering();

            foreach (GraphVertex firstVertex in _vertices)
            {
                var random = new Random();
                int randomNeighboursCount = random
                    .Next(0, GraphValidator.MaximalVertexDegree - firstVertex.Degree + 1);

                var otherVertices = _vertices.Except(new List<GraphVertex>() { firstVertex })
                    .Where(v => v.Degree != GraphValidator.MaximalVertexDegree).ToList();

                for (var i = 0; (i < randomNeighboursCount) && (i < GetVerticesCount() - 1); i++)
                {
                    GraphVertex secondVertex;

                    do
                    {
                        var randomVerticeIndex = random.Next(otherVertices.Count);
                        secondVertex = otherVertices[randomVerticeIndex];
                    } while (EdgeExists(firstVertex.Number, secondVertex.Number));

                    AddEdge(firstVertex.Number, secondVertex.Number);
                }
            }
        }

        private void GenerateMinimalGraphEdgesCovering()
        {
            foreach (GraphVertex firstVertex in _vertices)
            {
                var otherVertices = _vertices.Where(vertex => vertex.Number != firstVertex.Number).ToList();
                var secondVertex = otherVertices
                    .Find(v => (v.Degree != GraphValidator.MaximalVertexDegree) 
                    && (v.Degree == otherVertices.Min(v => v.Degree)));

                AddEdge(firstVertex.Number, secondVertex.Number);
            }
        }

        public bool EdgeExists(int firstVertexNumber, int secondVertexNumber)
        {
            return _edges.Contains(FindEdgeByEndsNumbers(firstVertexNumber, secondVertexNumber));
        }

        public GraphEdge FindEdgeByEndsNumbers(int firstVertexNumber, int secondVertexNumber)
        {
            return _edges
                .Find(e => ((e.FirstVertex.Number == firstVertexNumber) 
                && (e.SecondVertex.Number == secondVertexNumber)) 
                || ((e.SecondVertex.Number == firstVertexNumber) 
                && (e.FirstVertex.Number == secondVertexNumber)));
        }

        public int GetVerticesCount()
        {
            return _vertices.Count;
        }

        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AddEdge(int firstVertexNumber, int secondVertexNumber)
        {
            var firstVertex = FindVertexByNumber(firstVertexNumber);
            var secondVertex = FindVertexByNumber(secondVertexNumber);

            if (firstVertex is null)
            {
                throw new ArgumentException
                    ("A vertice with specified number doesn't exist", nameof(firstVertexNumber));
            }

            if (secondVertex is null)
            {
                throw new ArgumentException
                    ("A vertice with specified number doesn't exist", nameof(secondVertexNumber));
            }

            _edges.Add(new GraphEdge(firstVertex, secondVertex));
        }

        public GraphVertex FindVertexByNumber(int vertexNumber)
        {
            return _vertices.Find(v => v.Number == vertexNumber);
        }

        public void AddVertice()
        {
            _vertices.Add(new GraphVertex(_nextVertexNumber++));
        }

        public bool ContainsVertexNumber(int vertexNumber)
        {
            return _vertices.Exists(v => v.Number == vertexNumber);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (GraphEdge edge in _edges)
            {
                builder.Append($" {edge} |");
            }

            return builder.ToString();
        }

        public IEnumerator<GraphEdge> GetEnumerator()
        {
            foreach (GraphEdge edge in _edges)
            {
                yield return edge;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}