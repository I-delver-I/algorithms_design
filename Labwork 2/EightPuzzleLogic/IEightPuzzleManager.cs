using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic
{
    public interface IEightPuzzleManager
    {
        int[][] GenerateStartState();
        IPuzzleBoard MovePuzzle(IPuzzleBoard puzzleBoard, MovingDirection movingDirection);
    }
}