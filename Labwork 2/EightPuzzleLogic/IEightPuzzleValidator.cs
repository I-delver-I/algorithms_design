using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic;

public interface IEightPuzzleValidator
{
    public int PuzzlesCount { get; }
    public int[][] GoalState { get; }
    public int PuzzlesCountInRow { get; }
    public int PuzzleValueToMove { get; }
    
    public bool ValidatePuzzleMoving(IPuzzleBoard puzzleBoard, MovingDirection movingDirection);
    public bool ValidateGoalStateReaching(int[][] fieldState);
    public bool ValidateFieldCorrectness(IEnumerable<int[]> fieldToValidate);
}