using System.Collections;
using System.Text;

namespace TravelingSalesmanProblemLogic
{
    public class GraphOfSites : IEnumerable<AntGraphEdge>
    {
        private List<GraphVertex> _vertices = new List<GraphVertex>();
        private List<AntGraphEdge> _edges = new List<AntGraphEdge>();

        public double PheromonEvaporationRate { get; } = 0.7;

        public int NextVertexNumber { get; private set; } = 1;

        public GraphOfSites(int initialVerticesCount)
        {
            for (var i = 0; i < initialVerticesCount; i++)
            {
                AddVertex();
            }

            CreateEdgesBetweenAllVertices();     
        }

        public void UpdatePheromonLevel(int minimalSolutionPrice, 
            AntGraphEdge edgeToUpdatePheromoneLevelOn)
        {
            var adjustedPheromonSum = 
                GetAdjustedPheromonSum(minimalSolutionPrice, edgeToUpdatePheromoneLevelOn);

            for (int i = 0; i < edgeToUpdatePheromoneLevelOn.GetPassedAntsCount(); i++)
            {
                edgeToUpdatePheromoneLevelOn.PheromonLevel = (1 - PheromonEvaporationRate) 
                    * edgeToUpdatePheromoneLevelOn.PheromonLevel + adjustedPheromonSum;
            }
        }

        private double GetAdjustedPheromonSum(int minimalSolutionPrice, AntGraphEdge edge)
        {
            double result = 0;

            foreach (Ant ant in edge)
            {
                result += (double)minimalSolutionPrice
                    / GetEdgesWhichConnectVertices(ant.GetVerticesNumbers().ToArray())
                    .Sum(e => e.Length);
            }

            return result;
        }

        public void CreateEdgesBetweenAllVertices()
        {
            for (var i = 1; i < _vertices.Count; i++)
            {
                for (var j = i + 1; j < _vertices.Count + 1; j++)
                {
                    TryAddEdge(i, j);
                }
            }
        }

        public List<AntGraphEdge> GetEdgesWhichConnectVertices(int[] verticesNumbers)
        {
            var result = new List<AntGraphEdge>();

            for (var i = 0; i < verticesNumbers.Length - 1; i++)
            {
                result.Add(GetEdgeByEndsNumbers(verticesNumbers[i], verticesNumbers[i + 1]));
            }

            return result;
        }

        public void AddVertex()
        {
            _vertices.Add(new GraphVertex(NextVertexNumber++));
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

        public List<AntGraphEdge> GetEdgesWithSpecifiedVertexExcept(int vertexNumber, 
            int[] exceptionalVerticesNumbers)
        {
            return GetEdgesWithSpecifiedVertex(vertexNumber).Except
                    (GetEdgesWithSpecifiedVertex(vertexNumber)
                    .Where(e => ((e.FirstVertex.Number == vertexNumber) 
                        && (exceptionalVerticesNumbers.Contains(e.SecondVertex.Number)))
                        || ((e.SecondVertex.Number == vertexNumber) 
                        && (exceptionalVerticesNumbers.Contains(e.FirstVertex.Number))))).ToList();
        }

        public int GetVerticesCount()
        {
            return _vertices.Count;
        }

        public GraphVertex FindVertexByNumber(int vertexNumber)
        {
            return _vertices.Find(v => v.Number == vertexNumber);
        }

        public bool ExistsEdgeWithVertex(int vertexNumber)
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

        public AntGraphEdge GetEdgeByEndsNumbers(int firstVertexNumber, int secondVertexNumber)
        {
            return _edges.Find(e => 
                ((e.FirstVertex.Number == firstVertexNumber) 
                    && (e.SecondVertex.Number == secondVertexNumber)) 
                || ((e.FirstVertex.Number == secondVertexNumber) 
                    && (e.SecondVertex.Number == firstVertexNumber)));
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (AntGraphEdge edge in _edges)
            {
                result.Append($" {edge} |");
            }

            return result.ToString();
        }

        public IEnumerator<AntGraphEdge> GetEnumerator()
        {
            foreach (AntGraphEdge edge in _edges)
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