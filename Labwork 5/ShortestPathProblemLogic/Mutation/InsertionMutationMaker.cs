namespace ShortestPathProblemLogic.Mutation
{
    public class InsertionMutationMaker : IMutationMakable
    {
        private GraphOfSites _graph;

        public InsertionMutationMaker(GraphOfSites graph)
        {
            _graph = graph;
        }

        public void MakeMutation(Chromosome offspringToMutate)
        {
            int indexToInsertVertexNumberAt;
            int numberToInsert = 0;
            var verticesNumbers = offspringToMutate.GetVerticesNumbers();
            var triedIndices = new List<int>();

            do
            {
                var random = new Random();
                indexToInsertVertexNumberAt = random.Next(1, verticesNumbers.Count);
                triedIndices.Add(indexToInsertVertexNumberAt);

                var leftVertexNumber = verticesNumbers[indexToInsertVertexNumberAt - 1];
                var leftVerticesNumbers = _graph
                    .GetOtherVerticesOfEdgesWithSpecifiedVertexNumber(leftVertexNumber);

                var rightVertexNumber = verticesNumbers[indexToInsertVertexNumberAt];
                var rightVerticesNumbers = _graph
                    .GetOtherVerticesOfEdgesWithSpecifiedVertexNumber(rightVertexNumber);

                var commonVerticesNumbers = leftVerticesNumbers.Intersect(rightVerticesNumbers)
                    .Except(verticesNumbers).ToList();
                
                if (commonVerticesNumbers.Count != 0)
                {
                    var randomIndexOfCommonVertexNumber = random.Next(commonVerticesNumbers.Count);
                    numberToInsert = commonVerticesNumbers[randomIndexOfCommonVertexNumber];
                }
            } while ((numberToInsert == 0) && (triedIndices.Count != verticesNumbers.Count - 1));

            if (numberToInsert != 0)
            {
                offspringToMutate.InsertVertexNumberAt(indexToInsertVertexNumberAt, numberToInsert, _graph);
            }
        }
    }
}