namespace TravelingSalesmanProblemLogic
{
    public class GraphSiteVertex
    {
        public int Number { get; }

        private List<Ant> _ants = new List<Ant>();

        public GraphSiteVertex(int number)
        {
            Number = number;
        }

        public void AddAnt(Ant antToAdd)
        {
            _ants.Add(antToAdd);
        }

        public int GetAntsCount()
        {
            return _ants.Count;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}