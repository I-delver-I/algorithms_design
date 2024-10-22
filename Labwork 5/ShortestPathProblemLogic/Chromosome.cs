using System.Text;

namespace ShortestPathProblemLogic
{
    public class Chromosome : ICloneable
    {
        private List<int> _ownedVerticesNumbers = new List<int>();

        public void ReplaceVertexNumber(int index, int numberToReplaceBy)
        {
            _ownedVerticesNumbers[index] = numberToReplaceBy;
        }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void InsertVertexNumberAt(int index, int vertexNumber, GraphOfSites graph)
        {
            if (!graph.ContainsVertexNumber(vertexNumber))
            {
                throw new ArgumentOutOfRangeException(nameof(vertexNumber));
            }

            _ownedVerticesNumbers.Insert(index, vertexNumber);
        }

        public List<int> GetVerticesNumbersExceptEnds()
        {
            return _ownedVerticesNumbers
                .Except(new List<int> { _ownedVerticesNumbers.First(), _ownedVerticesNumbers.Last() }).ToList();
        }

        public int GetLength(GraphOfSites graph)
        {
            return graph.GetEdgesWhichConnectVertices(GetVerticesNumbers())
                .Sum(e => e.Length);
        }

        public int FirstVertexNumber()
        {
            return _ownedVerticesNumbers.FirstOrDefault();
        }

        public void AddVerticesNumbersRange(IEnumerable<int> verticesNumbers)
        {
            _ownedVerticesNumbers.AddRange(verticesNumbers);
        }

        public List<int> GetVerticesNumbers()
        {
            return ((int[])_ownedVerticesNumbers.ToArray().Clone()).ToList();
        }

        public void RemoveVerticesNumbersExceptStart()
        {
            _ownedVerticesNumbers.RemoveRange(1, _ownedVerticesNumbers.Count - 1);
        }

        public void RemoveVertexNumber(int vertexNumber)
        {
            _ownedVerticesNumbers.Remove(vertexNumber);
        }

        public int LastVertexNumber()
        {
            return _ownedVerticesNumbers.LastOrDefault();
        }

        /// <exception cref="ArgumentException"></exception>
        public void AddVertexNumber(int vertexNumber)
        {
            if (ContainsVertexNumber(vertexNumber))
            {
                throw new ArgumentException("Specified vertex number already exists in the list");
            }

            _ownedVerticesNumbers.Add(vertexNumber);
        }

        public bool ContainsVertexNumber(int vertexNumber)
        {
            return _ownedVerticesNumbers.Contains(vertexNumber);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            _ownedVerticesNumbers.ForEach(n => builder.Append($" {n} "));

            return builder.ToString();
        }

        public object Clone()
        {
            var result = new Chromosome();

            result.AddVerticesNumbersRange(GetVerticesNumbers());

            return result;
        }
    }
}