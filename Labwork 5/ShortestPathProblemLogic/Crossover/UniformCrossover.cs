namespace ShortestPathProblemLogic.Crossover
{
    public class UniformCrossover : ICrossoverable
    {
        private PopulationGenerator _populationGenerator;
        private GraphOfSites _graph;

        public UniformCrossover(PopulationGenerator populationGenerator, GraphOfSites graph)
        {
            _populationGenerator = populationGenerator;
            _graph = graph;
        }

        public Chromosome MakeCrossover()
        {
            var result = new Chromosome();

            var firstParent = GetCrossoverableRandomParent(null);
            var secondParent = GetCrossoverableRandomParent(firstParent);
            result.AddVertexNumber(firstParent.FirstVertexNumber());

            var firstParentVerticesNumbers = firstParent.GetVerticesNumbersExceptEnds();
            var secondParentVerticesNumbers = secondParent.GetVerticesNumbersExceptEnds();

            var isFirstParentBiggerThanSecond = firstParentVerticesNumbers.Count 
                > secondParentVerticesNumbers.Count;

            List<int> tailOfBiggerParentVerticesNumbers = null;
            int lessParentVerticesNumbersCount;

            if (isFirstParentBiggerThanSecond)
            {
                lessParentVerticesNumbersCount = secondParentVerticesNumbers.Count;
                tailOfBiggerParentVerticesNumbers = 
                    firstParentVerticesNumbers.Skip(secondParentVerticesNumbers.Count).ToList();
            }
            else
            {
                lessParentVerticesNumbersCount = firstParentVerticesNumbers.Count;
                tailOfBiggerParentVerticesNumbers = 
                    secondParentVerticesNumbers.Skip(firstParentVerticesNumbers.Count).ToList();
            }
            
            for (var i = 0; i < lessParentVerticesNumbersCount; i++)
            {
                var random = new Random();
                var indicatorOnWhichVertexNumberGoesToOffspring = random.Next(2);

                if (indicatorOnWhichVertexNumberGoesToOffspring == 0)
                {
                    if (!result.ContainsVertexNumber(firstParentVerticesNumbers[i]))
                    {
                        result.AddVertexNumber(firstParentVerticesNumbers[i]);
                    }
                }
                else
                {
                    if (!result.ContainsVertexNumber(secondParentVerticesNumbers[i]))
                    {
                        result.AddVertexNumber(secondParentVerticesNumbers[i]);
                    }
                }
            }

            result.AddVerticesNumbersRange(tailOfBiggerParentVerticesNumbers
                .Where(num => !result.ContainsVertexNumber(num)));
            result.AddVertexNumber(firstParent.LastVertexNumber());

            if (_graph.GetEdgesWhichConnectVertices(result.GetVerticesNumbers()).Contains(null))
            {
                return null;
            }

            return result;
        }

        private int FindCommonVertexNumber(Chromosome firstParent, Chromosome secondParent)
        {
            var firstParentVerticesToCompare = firstParent.GetVerticesNumbersExceptEnds();
            var secondParentVerticesToCompare = secondParent.GetVerticesNumbersExceptEnds();

            return firstParentVerticesToCompare.Intersect(secondParentVerticesToCompare).FirstOrDefault();
        }

        private Chromosome GetCrossoverableRandomParent(Chromosome anotherParent)
        {
            Chromosome result = null;
            var chromosomesExceptFirstParent = _populationGenerator
                .Except(new List<Chromosome> { anotherParent }).ToList();
                
            var random = new Random();
            var randomChromosomeIndex = random.Next(chromosomesExceptFirstParent.Count);
            result = chromosomesExceptFirstParent[randomChromosomeIndex];
            
            return result;
        }
    }
}