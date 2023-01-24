using System.Collections;

namespace TravelingSalesmanProblemLogic
{
    public class AntSpawner : IEnumerable<Ant>
    {
        public int DefaultAntsCount { get; }

        public int EliteAntsCount { get; }

        private GraphOfSites _graph;
        private List<Ant> _ants = new List<Ant>();

        public AntSpawner(GraphOfSites graph, int defaultAntsCount = 35, int eliteAntsCount = 10)
        {
            _graph = graph;
            DefaultAntsCount = defaultAntsCount;
            EliteAntsCount = eliteAntsCount;
        }

        public List<List<int>> GetAntsVisitedVerticesNumbers()
        {
            var result = new List<List<int>>();

            foreach (Ant ant in _ants)
            {
                result.Add(((int[])ant.GetVerticesNumbers().ToArray().Clone()).ToList());
            }

            return result;
        }

        public int GetAntsCount()
        {
            return _ants.Count;
        }

        /// <exception cref="InvalidOperationException"></exception>
        public void SpawnAnts()
        {
            SpawnAnts(_graph.NextVertexNumber, false);
            SpawnAnts(_graph.NextVertexNumber, true);
        }

        /// <exception cref="InvalidOperationException"></exception>
        private void SpawnAnts(int maximalVertexNumber, bool isElite)
        {
            if (_graph.GetVerticesCount() < DefaultAntsCount + EliteAntsCount)
            {
                throw new InvalidOperationException
                    ("The count of ants is bigger than count of graph vertices");
            }

            Random random = new Random();
            int antsCount;

            if (isElite)
            {
                antsCount = EliteAntsCount;
            }
            else
            {
                antsCount = DefaultAntsCount;
            }

            for (var i = 0; i < antsCount; i++)
            {
                var antIsSpawned = false;

                do
                {
                    var currentVertexNumber = random.Next(1, _graph.GetVerticesCount() + 1);

                    if (_graph.VertexExists(currentVertexNumber))
                    {
                        if (!_ants.Exists(a => a.IsVerticeVisited(currentVertexNumber)))
                        {
                            var antToAdd = new Ant(currentVertexNumber, isElite);
                            _ants.Add(antToAdd);

                            antIsSpawned = true;
                        }
                    }
                } while (!antIsSpawned);
            }
        }

        public IEnumerator<Ant> GetEnumerator()
        {
            foreach (Ant ant in _ants)
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