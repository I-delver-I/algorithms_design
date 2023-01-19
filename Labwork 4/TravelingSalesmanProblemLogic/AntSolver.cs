namespace TravelingSalesmanProblemLogic
{
    public class AntSolver
    {
        public int Alpha { get; } = 3;

        public int Beta { get; } = 2;

        public double Rho { get; } = 0.7;

        public double BestCurrentCycleLength { get; } = double.PositiveInfinity;

        
    }
}