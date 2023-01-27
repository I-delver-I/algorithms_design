namespace ShortestPathProblemLogic.Crossover
{
    public class SinglePointCrossover : ICrossoverable
    {
        private PopulationGenerator _populationGenerator;
        private GraphOfSites _graph;

        public SinglePointCrossover(PopulationGenerator populationGenerator, GraphOfSites graph)
        {
            _populationGenerator = populationGenerator;
            _graph = graph;
        }

        public int FindCommonVertexNumber(Chromosome firstParent, Chromosome secondParent)
        {
            var firstParentVerticesToCompare = firstParent.GetOwnedVerticesExceptEnds();
            var secondParentVerticesToCompare = secondParent.GetOwnedVerticesExceptEnds();

            return firstParentVerticesToCompare.Intersect(secondParentVerticesToCompare).FirstOrDefault();
        }

        public Chromosome GetCrossoverableParentWithShortestPath()
        {
            var populationExceptChromosomeWithTwoVertices = _populationGenerator
                .Where(c => c.GetVerticesNumbers().Count 
                > GraphValidator.MinimalVerticesCountDividedByPointCrossover * 2).ToList();

            return populationExceptChromosomeWithTwoVertices
                .MinBy(c => c.GetLength(_graph));
        }

        public Chromosome GetCrossoverableRandomParent(Chromosome anotherParent)
        {
            Chromosome result = null;
            var commonParentsVertexNumber = 0;
            var chromosomesExceptFirstParent = _populationGenerator
                .Except(new List<Chromosome> { anotherParent }).ToList();

            do
            {
                var random = new Random();
                var randomChromosomeIndex = random.Next(chromosomesExceptFirstParent.Count);
                result = chromosomesExceptFirstParent[randomChromosomeIndex];
                
                commonParentsVertexNumber = FindCommonVertexNumber(anotherParent, result);
            } while (commonParentsVertexNumber == 0);

            return result;
        }

        public Chromosome MakeCrossover()
        {
            var result = new Chromosome();

            var firstParent = GetCrossoverableParentWithShortestPath();
            var secondParent = GetCrossoverableRandomParent(firstParent);
            var commonParentsVertexNumber = FindCommonVertexNumber(firstParent, secondParent);

            var firstParentLeftPart = GetFirstParentLeftPart(firstParent, commonParentsVertexNumber);
            result.AddVerticesNumbersRange(firstParentLeftPart);

            result.AddVertexNumber(commonParentsVertexNumber);

            var secondParentRightPart = GetSecondParentRightPart(secondParent, commonParentsVertexNumber);
            result.AddVerticesNumbersRange(secondParentRightPart);

            return result;
        }

        private List<int> GetSecondParentRightPart(Chromosome secondParent, int commonParentsVertexNumber)
        {
            var secondParentVerticesNumbers = secondParent.GetVerticesNumbers();
            var secondParentCommonVertexIndex = secondParentVerticesNumbers.IndexOf(commonParentsVertexNumber);

            return secondParentVerticesNumbers
                .GetRange(secondParentCommonVertexIndex + 1, 
                secondParentVerticesNumbers.Count - secondParentCommonVertexIndex - 1);
        }

        private List<int> GetFirstParentLeftPart(Chromosome firstParent, int commonParentsVertexNumber)
        {
            return firstParent.GetVerticesNumbers()
                .TakeWhile(num => num != commonParentsVertexNumber).ToList();
        }
    }
}