namespace ShortestPathProblemLogic
{
    public static class GraphValidator
    {
        public static int MaximalVertexDegree { get; } = 10;
        
        public static int MinimalVertexDegree { get; } = 1;
        
        public static int MinimalEdgeLength { get; } = 5;

        public static int MaximalEdgeLength { get; } = 150;

        public static int MinimalVerticesCount { get; } = 2;
    }
}