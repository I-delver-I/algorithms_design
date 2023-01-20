namespace TravelingSalesmanProblemLogic
{
    public class AntGraphEdge
    {
        public GraphSiteVertex FirstVertex { get; set; }

        public GraphSiteVertex SecondVertex { get; set; }

        public int Length { get; set; } = 1;

        public double PheromonConcentration { get; set; }

        public AntGraphEdge(GraphSiteVertex firstPlace, GraphSiteVertex secondPlace)
        {
            FirstVertex = firstPlace;
            SecondVertex = secondPlace;

            Random random = new Random();
            PheromonConcentration = random.NextDouble();

            int smallestLength = 1;
            int longestLength = 40;
            Length = random.Next(smallestLength, longestLength + 1);
        }

        public override string ToString()
        {
            return $"{FirstVertex} <--{Length}--> {SecondVertex}";
        }
    }
}