namespace ShortestPathProblemLogic
{
    public class GraphEdge
    {
        public GraphVertex FirstVertex { get; }

        public GraphVertex SecondVertex { get; }

        public int Length { get; }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public GraphEdge(GraphVertex firstVertex, GraphVertex secondVertex)
        {
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
            FirstVertex.Degree++;
            SecondVertex.Degree++;

            var random = new Random();
            Length = random.Next(GraphValidator.MinimalEdgeLength, GraphValidator.MaximalEdgeLength + 1);
        }

        public override string ToString()
        {
            return $"{FirstVertex} <--{Length}--> {SecondVertex}";
        }
    }
}