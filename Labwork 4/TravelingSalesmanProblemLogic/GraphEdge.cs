namespace TravelingSalesmanProblemLogic
{
    public class GraphEdge
    {
        public PlaceToAttendVertex FirstPlace { get; set; }

        public PlaceToAttendVertex SecondPlace { get; set; }

        public int Length { get; set; } = 1;

        public double PheromonConcentration { get; set; }

        public GraphEdge(PlaceToAttendVertex firstPlace, PlaceToAttendVertex secondPlace)
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