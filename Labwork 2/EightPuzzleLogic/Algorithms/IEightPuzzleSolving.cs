using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic.Algorithms
{
    public interface IEightPuzzleSolving
    {
        public IPuzzleBoard SolveEightPuzzle(IPuzzleBoard puzzleBoard);
    }
}