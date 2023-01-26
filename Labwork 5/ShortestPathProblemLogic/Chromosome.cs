using System.Text;

namespace ShortestPathProblemLogic
{
    public class Chromosome
    {
        private List<int> _graphVerticesNumbers = new List<int>();

        public List<int> GetVerticesNumbers()
        {
            return ((int[])_graphVerticesNumbers.ToArray().Clone()).ToList();
        }

        public void RemoveVerticesNumbersExceptStart()
        {
            _graphVerticesNumbers.RemoveRange(1, _graphVerticesNumbers.Count - 1);
        }

        public void RemoveVertexNumber(int vertexNumber)
        {
            _graphVerticesNumbers.Remove(vertexNumber);
        }

        public int LastVertexNumber()
        {
            return _graphVerticesNumbers.Last();
        }

        /// <exception cref="ArgumentException"></exception>
        public void AddVertexNumber(int vertexNumber)
        {
            if (ContainsVertexNumber(vertexNumber))
            {
                throw new ArgumentException("Specified vertex number already exists in the list");
            }

            _graphVerticesNumbers.Add(vertexNumber);
        }

        public bool ContainsVertexNumber(int vertexNumber)
        {
            return _graphVerticesNumbers.Contains(vertexNumber);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            _graphVerticesNumbers.ForEach(n => builder.Append($" {n} "));

            return builder.ToString();
        }
    }
}