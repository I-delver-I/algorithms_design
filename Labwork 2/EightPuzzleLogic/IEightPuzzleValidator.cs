using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic;

public interface IEightPuzzleValidator
{
    int PuzzlesCount { get; }
    int[][] GoalState { get; }
    int PuzzlesCountInRow { get; }
    int PuzzleValueToMove { get; }
    
    bool ValidatePuzzleMoving(IPuzzleBoard puzzleBoard, MovingDirection movingDirection);
    bool ValidateGoalStateReaching(int[][] fieldState);
    bool ValidateFieldCorrectness(IEnumerable<int[]> fieldToValidate);
}