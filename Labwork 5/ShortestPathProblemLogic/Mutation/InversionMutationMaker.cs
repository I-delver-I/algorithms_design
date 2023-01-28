namespace ShortestPathProblemLogic.Mutation
{
    public class InversionMutationMaker : IMutationMakable
    {
        private GraphOfSites _graph;

        public InversionMutationMaker(GraphOfSites graph)
        {
            _graph = graph;
        }

        public void MakeMutation(Chromosome offspring)
        {
            var (firstVertexNumberPosition, secondVertexNumberPosition) = GetBorderVerticesPositions
                (offspring.GetVerticesNumbers());

            MakeVerticesSubsetInversion
                (offspring.GetVerticesNumbers(), firstVertexNumberPosition, secondVertexNumberPosition);
        }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void MakeVerticesSubsetInversion(List<int> verticesNumbers, int firstBorderVertexNumberPosition,
            int secondBorderVertexNumberPosition)
        {
            bool firstPositionIsLessThanSecond = (firstBorderVertexNumberPosition < secondBorderVertexNumberPosition); 
            int leftIndex;
            int rightIndex;

            if (firstPositionIsLessThanSecond)
            {
                leftIndex = firstBorderVertexNumberPosition;
                rightIndex = secondBorderVertexNumberPosition;
            }
            else
            {
                leftIndex = secondBorderVertexNumberPosition;
                rightIndex = firstBorderVertexNumberPosition;
            }

            var leftBorderNumber = verticesNumbers[leftIndex];
            var rightBorderNumber = verticesNumbers[rightIndex];

            var firstEdgeToUpdate = _graph
                .FindEdgeByEndsNumbers(leftBorderNumber, verticesNumbers[leftIndex + 1]);
            var secondEdgeToUpdate = _graph
                .FindEdgeByEndsNumbers(rightBorderNumber, verticesNumbers[rightIndex - 1]);
            
            verticesNumbers.Reverse(leftIndex + 1, rightIndex - leftIndex - 1);
            _graph.UpdateEdgeVertexNumber(firstEdgeToUpdate, leftBorderNumber, verticesNumbers[leftIndex + 1]);
            _graph.UpdateEdgeVertexNumber(secondEdgeToUpdate, rightBorderNumber, verticesNumbers[rightIndex - 1]);
        }

        /// <exception cref="ArgumentException"></exception>
        private (int, int) GetBorderVerticesPositions(List<int> verticesNumbersToCreateSubsetIn)
        {
            int minimalVerticesNumbersCountToGetPositions = 4;

            if (verticesNumbersToCreateSubsetIn.Count < minimalVerticesNumbersCountToGetPositions)
            {
                throw new ArgumentException("The count of vertices numbers mustn't be less than 3");
            }
            
            var random = new Random();

            var firstPosition = random.Next(verticesNumbersToCreateSubsetIn.Count);
            int secondPosition;

            do
            {
                secondPosition = random.Next(verticesNumbersToCreateSubsetIn.Count);
            } while (Math.Abs(firstPosition - secondPosition) < 3);

            return (firstPosition, secondPosition);
        }
    }
}