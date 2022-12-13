namespace EightPuzzleLogic.Boards
{
    public interface IAstarPuzzleBoard : IPuzzleBoard
    {
        public int GetOutlay();
        public int GetDistanceToGoal();
    }
}