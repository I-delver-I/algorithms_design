namespace TravelingSalesmanProblemLogic
{
    public class AntSpawner
    {
        public int DefaultAntsCount { get; } = 35;

        public int EliteAntsCount { get; } = 10;

        private GraphOfSites _graph;

        public AntSpawner(GraphOfSites graph)
        {
            _graph = graph;
        }

        public void SpawnAnts()
        {
            SpawnAnts(_graph.NextVertexNumber, false);
            SpawnAnts(_graph.NextVertexNumber, true);
        }

        private void SpawnAnts(int maximalVertexNumber, bool isElite)
        {
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
                        var antToAdd = new Ant(currentVertexNumber, isElite);
                        var currentVertex = _graph.FindVertexByNumber(currentVertexNumber);
                        
                        if (currentVertex.GetAntsCount() == 0)
                        {
                            currentVertex.AddAnt(antToAdd);
            
                            antIsSpawned = true;
                        }
                    }
                } while (!antIsSpawned);
            }
        }
    }
}