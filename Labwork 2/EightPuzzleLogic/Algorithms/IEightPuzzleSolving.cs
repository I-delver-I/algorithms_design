using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic.Algorithms
{
    public interface IEightPuzzleSolving
    {
        IPuzzleBoard SolveEightPuzzle(IPuzzleBoard puzzleBoard);
    }
}