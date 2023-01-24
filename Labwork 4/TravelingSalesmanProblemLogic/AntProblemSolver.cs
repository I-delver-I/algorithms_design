namespace TravelingSalesmanProblemLogic
{
    public class AntProblemSolver
    {
        private GraphOfSites _graph;
        private EdgeSelector _edgeSelector;
        private AntSpawner _antSpawner;
        private AntMover _antMover;

        public int MinimalSolutionPrice { get; set; }

        public List<AntGraphEdge> MinimalSolutionPath { get; set; }

        public AntProblemSolver(GraphOfSites graph, EdgeSelector edgeSelector,
            AntSpawner antSpawner, AntMover antMover)
        {
            _graph = graph;
            _edgeSelector = edgeSelector;
            _antSpawner = antSpawner;
            _antMover = antMover;
            MinimalSolutionPath = GetMinimalSolutionPath();
            MinimalSolutionPrice = MinimalSolutionPath
                .Sum(e => e.Length);
        }

        /// <exception cref="InvalidOperationException"></exception>
        public List<AntGraphEdge> Solve(int iterationsCount = 1000)
        {
            if (_antSpawner.GetAntsCount() == 0)
            {
                throw new InvalidOperationException("Ants are not spawned");
            }

            List<AntGraphEdge> shortestGraphBypass = null;
            FileHandler.WriteLine(DateTime.Now.ToString());
            FileHandler.WriteLine($"Default ants count: {_antSpawner.DefaultAntsCount}, "
                + $"elite ants count: {_antSpawner.EliteAntsCount}, iterations count: {iterationsCount}, "
                + $"vertices count: {_graph.GetVerticesCount()}");

            for (var i = 0; i < iterationsCount; i++)
            {
                MoveAntsThroughWholeGraph();
                shortestGraphBypass = GetShortestGraphBypass(shortestGraphBypass);

                foreach (AntGraphEdge edge in _graph)
                {
                    _graph.UpdatePheromonLevel(MinimalSolutionPrice, edge);
                }

                foreach (AntGraphEdge edge in _graph)
                {
                    edge.RemovePassedAnts();
                }

                foreach (Ant ant in _antSpawner)
                {
                    ant.DeleteAllVerticesExceptInitial();
                }

                if ((i + 1) % 20 == 0)
                {
                    FileHandler.WriteLine
                        ($"Iteration {i + 1}, "
                        + $"minimal length {shortestGraphBypass.Sum(e => e.Length)}");
                }
            }

            FileHandler.WriteLine("");

            return shortestGraphBypass;
        }

        private List<AntGraphEdge> GetShortestGraphBypass(List<AntGraphEdge> lastGraphBypass)
        {
            var result = lastGraphBypass;
            int minimalPathLength;

            if (lastGraphBypass is null)
            {
                minimalPathLength = int.MaxValue;
            }
            else
            {
                minimalPathLength = lastGraphBypass.Sum(e => e.Length);
            }

            var antsVisitedVerticesNumbers = _antSpawner.GetAntsVisitedVerticesNumbers();

            foreach (List<int> visitedVerticesNumbers in antsVisitedVerticesNumbers)
            {
                visitedVerticesNumbers.Add(visitedVerticesNumbers.First());
                var path = _graph.GetEdgesWhichConnectVertices(visitedVerticesNumbers.ToArray());
                var pathLength = path.Sum(e => e.Length);

                if (pathLength < minimalPathLength)
                {
                    minimalPathLength = pathLength;
                    result = path;
                }
            }

            return result;
        }

        private void MoveAntsThroughWholeGraph()
        {
            foreach (Ant ant in _antSpawner)
            {
                for (var j = 0; j < _graph.GetVerticesCount() - 1; j++)
                {
                    _antMover.MoveAntToNextVertex(ant);
                }
            }
        }

        public List<AntGraphEdge> GetMinimalSolutionPath()
        {
            int initialVertexNumber = 1;
            var visitedVerticesNumbers = new List<int>() { initialVertexNumber };
            var currentVertexNumber = initialVertexNumber;
            var result = new List<AntGraphEdge>();

            do
            {
                var previousVertexNumber = currentVertexNumber;

                var optimalTransition = _edgeSelector
                    .GreedySelectOptimalEdge(visitedVerticesNumbers.ToArray());
                
                if (optimalTransition.FirstVertex.Number == currentVertexNumber)
                {
                    currentVertexNumber = optimalTransition.SecondVertex.Number;
                }
                else
                {
                    currentVertexNumber = optimalTransition.FirstVertex.Number;
                }

                visitedVerticesNumbers.Add(currentVertexNumber);
                result.Add(_graph
                    .GetEdgeByEndsNumbers(previousVertexNumber, currentVertexNumber));
            } 
            while (visitedVerticesNumbers.Count != _graph.GetVerticesCount());
 
            var edgeOfStartAndEndVertices = _graph.GetEdgeByEndsNumbers
                (visitedVerticesNumbers.Last(), initialVertexNumber);
            result.Add(edgeOfStartAndEndVertices);

            return result;
        } 
    }
}