namespace ShortestPathProblemLogic.LocalImprovement
{
    public interface ILocalImprovable
    {
        public void MakeImprovement(Chromosome offspring);
    }
}