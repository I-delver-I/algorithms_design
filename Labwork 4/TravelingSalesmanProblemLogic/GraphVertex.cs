namespace TravelingSalesmanProblemLogic
{
    public class GraphVertex
    {
        public int Number { get; }

        public GraphVertex(int number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}