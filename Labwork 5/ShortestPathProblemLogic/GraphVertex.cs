namespace ShortestPathProblemLogic
{
    public class GraphVertex
    {
        public int Number { get; }

        private int _degree;
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Degree 
        { 
            get => _degree; 
            set 
            {
                if (value > GraphValidator.MaximalVertexDegree)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Degree mustn't exceed 10");
                }

                _degree = value;
            } 
        }

        public GraphVertex(int number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}