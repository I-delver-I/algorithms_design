namespace TravelingSalesmanProblemLogic
{
    public class AntGraphEdge
    {
        public GraphSiteVertex FirstPlace { get; set; }

        public GraphSiteVertex SecondPlace { get; set; }

        public int Length { get; set; } = 1;

        public double PheromonConcentration { get; set; }

        public AntGraphEdge(GraphSiteVertex firstPlace, GraphSiteVertex secondPlace)
        {
            FirstPlace = firstPlace;
            SecondPlace = secondPlace;

            Random random = new Random();
            PheromonConcentration = random.NextDouble();

            int smallestLength = 1;
            int longestLength = 40;
            Length = random.Next(smallestLength, longestLength + 1);
        }
    }
}