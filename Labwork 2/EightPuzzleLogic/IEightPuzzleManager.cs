using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic
{
    public interface IEightPuzzleManager
    {
        public int[][] GenerateStartState();
        public IPuzzleBoard MovePuzzle(IPuzzleBoard puzzleBoard, MovingDirection movingDirection);
    }
}