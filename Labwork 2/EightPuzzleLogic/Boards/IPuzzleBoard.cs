namespace EightPuzzleLogic.Boards
{
    public interface IPuzzleBoard
    {
        public IPuzzleBoard Parent { get; set; }
        public List<IPuzzleBoard> Children { get; }
        public int Depth { get; set; }
        public int[][] PuzzleState { get; set; }
        public MovingDirection PuzzleLastMovingDirection { get; set; }

        public IPuzzleBoard Clone();
        public void GenerateChildren();
        public Tuple<int, int> GetCoordinatesOfPuzzle(int puzzleValue);
    }
}