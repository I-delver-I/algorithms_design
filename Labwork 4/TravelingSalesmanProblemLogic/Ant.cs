using System.Text;

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

        public void DeleteAllVerticesExceptInitial()
        {
            _visitedVerticesNumbers.RemoveRange(1, _visitedVerticesNumbers.Count - 1);
        }

        public int GetVisitedVerticesCount()
        {
            return _visitedVerticesNumbers.Count;
        }

        public List<int> GetVerticesNumbers()
        {
            return _visitedVerticesNumbers;
        }

        /// <exception cref="ArgumentException"></exception>
        public void AddVisitedVerticeNumber(int verticeNumberToAdd)
        {
            if (IsVerticeVisited(verticeNumberToAdd))
            {
                throw new ArgumentException("Specified vertice is already visited");
            }

            _visitedVerticesNumbers.Add(verticeNumberToAdd);
        }

        /// <exception cref="ArgumentException"></exception>
        public void AddVisitedVerticeNumber(AntGraphEdge visitedEdge)
        {
            if (IsVerticeVisited(visitedEdge.FirstVertex.Number) 
                && IsVerticeVisited(visitedEdge.SecondVertex.Number))
            {
                throw new ArgumentException("Specified edge is already visited");
            }

            if (IsVerticeVisited(visitedEdge.FirstVertex.Number))
            {
                _visitedVerticesNumbers.Add(visitedEdge.SecondVertex.Number);
            }
            else
            {
                _visitedVerticesNumbers.Add(visitedEdge.FirstVertex.Number);
            }
        }

        public bool IsVerticeVisited(int verticeNumber)
        {
            return _visitedVerticesNumbers.Contains(verticeNumber);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            _visitedVerticesNumbers.ForEach(v => builder.Append($" {v} "));

            return $"Is elite: {IsElite}\n"
                + $"Visited vertices' numbers: {builder.ToString()}\n\n";
        }
    }
}