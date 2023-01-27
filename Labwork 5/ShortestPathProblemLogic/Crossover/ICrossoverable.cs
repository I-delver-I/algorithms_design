namespace ShortestPathProblemLogic.Crossover
{
    public interface ICrossoverable
    {
        public Chromosome MakeCrossover();

        public Chromosome GetCrossoverableParentWithShortestPath();

        public Chromosome GetCrossoverableRandomParent(Chromosome anotherParent);
    }
}