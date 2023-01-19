namespace TravelingSalesmanProblemLogic
{
    public class Ant
    {
        public bool IsElite { get; set; }

        private List<int> _visitedVerticesNumbers = new List<int>();

        public Ant(int placementVertexNumber, bool isElite = false)
        {
            _visitedVerticesNumbers.Add(placementVertexNumber);
            IsElite = isElite;
        }

        public void AddVisitedVerticeNumber(int verticeNumber)
        {
            _visitedVerticesNumbers.Add(verticeNumber);
        }

        public bool IsVerticeVisited(int verticeNumber)
        {
            return _visitedVerticesNumbers.Contains(verticeNumber);
        }
    }
}