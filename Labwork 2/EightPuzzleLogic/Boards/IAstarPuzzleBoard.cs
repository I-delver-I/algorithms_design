namespace EightPuzzleLogic.Boards
{
    public interface IAstarPuzzleBoard : IPuzzleBoard
    {
        int GetOutlay();
        int GetDistanceToGoal();
    }
}