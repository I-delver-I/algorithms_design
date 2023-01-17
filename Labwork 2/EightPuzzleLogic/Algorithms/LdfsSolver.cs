using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic.Algorithms
{
    public class LdfsSolver : IEightPuzzleSolving, IDepthLimited, ICharacteristicable
    {
        private readonly IEightPuzzleValidator _puzzleValidator;
        
        private int _depthLimit;
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int DepthLimit { 
            get => _depthLimit; 
            set
            {
                if (value < 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), 
                        "Depth limit mustn't be less than 2");
                }

                _depthLimit = value;
            }
        }

        public int IterationsCount { get; set; }
        public int BlindCornersCount { get; set; }
        public int OverallStatesCount { get; set; }
        public int StatesCountInMemory { get; set; }

        public LdfsSolver(IEightPuzzleValidator puzzleValidator, int depthLimit = 27)
        {
            _puzzleValidator = puzzleValidator;
            DepthLimit = depthLimit;
        }

        /// <exception cref="InsufficientMemoryException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public IPuzzleBoard SolveEightPuzzle(IPuzzleBoard puzzleBoard)
        {
            if (puzzleBoard is null)
            {
                throw new ArgumentNullException(nameof(puzzleBoard));
            }

            IterationsCount++;

            if (_puzzleValidator.ValidateGoalStateReaching(puzzleBoard.PuzzleState))
            {
                return puzzleBoard;
            }
            
            if (puzzleBoard.Depth == DepthLimit)
            {
                BlindCornersCount++;

                return null;
            }
            
            puzzleBoard.GenerateChildren();

            long bytesCountInGb = (long)Math.Pow(1024, 3);

            if (GC.GetTotalMemory(false) >= bytesCountInGb)
            {
                throw new InsufficientMemoryException("The available memory has been exceeded");
            }

            foreach (IPuzzleBoard child in puzzleBoard.Children)
            {
                OverallStatesCount++;
                StatesCountInMemory++;

                var result = SolveEightPuzzle(child);

                if (result is not null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}