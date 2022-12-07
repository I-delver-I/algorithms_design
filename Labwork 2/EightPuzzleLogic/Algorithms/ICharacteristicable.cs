namespace EightPuzzleLogic.Algorithms
{
    public interface ICharacteristicable
    {
        int IterationsCount { get; set; }
        int BlindCornersCount { get; set; }
        int OverallStatesCount { get; set; }
        int StatesCountInMemory { get; set; }
    }
}