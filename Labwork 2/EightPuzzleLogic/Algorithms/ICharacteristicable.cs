namespace EightPuzzleLogic.Algorithms
{
    public interface ICharacteristicable
    {
        public int IterationsCount { get; set; }
        public int BlindCornersCount { get; set; }
        public int OverallStatesCount { get; set; }
        public int StatesCountInMemory { get; set; }
    }
}