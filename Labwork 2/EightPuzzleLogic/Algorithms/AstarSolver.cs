using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic.Algorithms
{
    public class AstarSolver : IEightPuzzleSolving, ICharacteristicable
    {
        private readonly PriorityQueue<IAstarPuzzleBoard, int> _open = new();
        private readonly HashSet<int[][]> _closed = new();
        private readonly IEightPuzzleValidator _puzzleValidator;

        public int IterationsCount { get; set; }
        public int BlindCornersCount { get; set; }
        public int OverallStatesCount { get; set; }
        public int StatesCountInMemory { get; set; }
        
        public AstarSolver(IEightPuzzleValidator puzzleValidator)
        {
            _puzzleValidator = puzzleValidator;
        }

        /// <exception cref="InsufficientMemoryException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public IPuzzleBoard SolveEightPuzzle(IPuzzleBoard puzzleBoard)
        {
            if (puzzleBoard is null)
            {
                throw new ArgumentNullException(nameof(puzzleBoard));
            }

            _open.Enqueue(puzzleBoard as IAstarPuzzleBoard, 0);

            while (_open.Count != 0)
            {
                StatesCountInMemory--;
                var currentBoard = _open.Dequeue();
                _closed.Add(currentBoard.PuzzleState);

                IterationsCount++;

                if (_puzzleValidator.ValidateGoalStateReaching(currentBoard.PuzzleState))
                {
                    return currentBoard;
                }

                currentBoard.GenerateChildren();

                long bytesCountInGb = (long)Math.Pow(1024, 3);

                if (GC.GetTotalMemory(false) >= bytesCountInGb)
                {
                    throw new InsufficientMemoryException("The available memory has been exceeded");
                }

                foreach (var child in currentBoard.Children.Cast<IAstarPuzzleBoard>())
                {
                    OverallStatesCount++;

                    if (_closed.Contains(child.PuzzleState))
                    {
                        continue;
                    }

                    StatesCountInMemory++;

                    _open.Enqueue(child, child.GetOutlay());
                }
            }

            return null;
        }
    }
}