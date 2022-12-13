namespace EightPuzzleLogic.Boards
{
    public interface IPuzzleBoard
    {
        IPuzzleBoard Parent { get; set; }
        List<IPuzzleBoard> Children { get; }
        int Depth { get; set; }
        int[][] PuzzleState { get; set; }
        MovingDirection PuzzleLastMovingDirection { get; set; }

        IPuzzleBoard Clone();
        void GenerateChildren();
        Tuple<int, int> GetCoordinatesOfPuzzle(int puzzleValue);
        // IPuzzleBoard TryGetChild(MovingDirection movingDirection);
    }
}