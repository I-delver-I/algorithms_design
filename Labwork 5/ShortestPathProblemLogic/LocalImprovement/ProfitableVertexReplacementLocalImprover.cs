namespace ShortestPathProblemLogic.LocalImprovement
{
    public class ProfitableVertexReplacementLocalImprover : ILocalImprovable
    {
        private GraphOfSites _graph;

        public ProfitableVertexReplacementLocalImprover(GraphOfSites graph)
        {
            _graph = graph;
        }

        public void MakeImprovement(Chromosome offspringToImprove)
        {
            int indexOfVertexNumberToReplace;
            var verticesNumbers = offspringToImprove.GetVerticesNumbers();
            var triedIndices = new List<int>();
            var replacementIsDone = false;

            do
            {
                var random = new Random();
                indexOfVertexNumberToReplace = random.Next(1, verticesNumbers.Count - 1);
                
                if (!triedIndices.Contains(indexOfVertexNumberToReplace))
                {
                    triedIndices.Add(indexOfVertexNumberToReplace);
                }

                var leftVertexNumber = verticesNumbers[indexOfVertexNumberToReplace - 1];
                var leftVerticesNumbers = _graph
                    .GetOtherVerticesOfEdgesWithSpecifiedVertexNumber(leftVertexNumber);

                var rightVertexNumber = verticesNumbers[indexOfVertexNumberToReplace + 1];
                var rightVerticesNumbers = _graph
                    .GetOtherVerticesOfEdgesWithSpecifiedVertexNumber(rightVertexNumber);

                var commonVerticesNumbers = leftVerticesNumbers.Intersect(rightVerticesNumbers)
                    .Except(verticesNumbers).ToList();
                
                if (commonVerticesNumbers.Count != 0)
                {
                    var currentPathLength = offspringToImprove.GetLength(_graph);

                    foreach (int vertexNumber in commonVerticesNumbers)
                    {
                        var offstringClone = (Chromosome)offspringToImprove.Clone();

                        offstringClone.ReplaceVertexNumber(indexOfVertexNumberToReplace, vertexNumber);

                        if ((offstringClone.GetLength(_graph) < currentPathLength))
                        {
                            offspringToImprove.ReplaceVertexNumber(indexOfVertexNumberToReplace, 
                                vertexNumber);
                            currentPathLength = offspringToImprove.GetLength(_graph);
                            replacementIsDone = true;
                        }
                    }
                }
            } while (!replacementIsDone && (triedIndices.Count != verticesNumbers.Count - 2));
        }
    }
}