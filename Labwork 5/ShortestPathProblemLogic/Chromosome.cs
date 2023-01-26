namespace ShortestPathProblemLogic
{
    public class Chromosome
    {
        private List<int> _graphVerticesNumbers = new List<int>();

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
    }
}