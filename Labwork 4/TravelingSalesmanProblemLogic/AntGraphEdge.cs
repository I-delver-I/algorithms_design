using System.Collections;

namespace TravelingSalesmanProblemLogic
{
    public class AntGraphEdge : IEnumerable<Ant>
    {
        public GraphVertex FirstVertex { get; set; }

        public GraphVertex SecondVertex { get; set; }

        public int Length { get; set; }

        public double PheromonLevel { get; set; }

        private List<Ant> _antsPassedDuringIteration = new List<Ant>();

        public AntGraphEdge(GraphVertex firstPlace, GraphVertex secondPlace)
        {
            FirstVertex = firstPlace;
            SecondVertex = secondPlace;

            Random random = new Random();
            PheromonLevel = random.NextDouble();

            if (PheromonLevel == 0)
            {
                PheromonLevel = 0.1;
            }

            int smallestLength = 1;
            int longestLength = 40;
            Length = random.Next(smallestLength, longestLength + 1);
        }

        public void RemovePassedAnts()
        {
            _antsPassedDuringIteration.Clear();
        }
        
        public void AddPassedAnt(Ant passedAnt)
        {
            if (_antsPassedDuringIteration.Contains(passedAnt))
            {
                throw new ArgumentException("Specified ant already exists in the list");
            }

            _antsPassedDuringIteration.Add(passedAnt);
        }

        public override string ToString()
        {
            return $"{FirstVertex} <--{Length}--> {SecondVertex}";
        }

        public IEnumerator<Ant> GetEnumerator()
        {
            foreach (Ant ant in _antsPassedDuringIteration)
            {
                yield return ant;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}