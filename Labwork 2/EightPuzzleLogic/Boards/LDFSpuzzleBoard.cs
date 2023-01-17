using EightPuzzleLogic.Extensions;

namespace EightPuzzleLogic.Boards
{
    public class LDFSpuzzleBoard : IPuzzleBoard
    {
        public IPuzzleBoard Parent { get; set; }
        public List<IPuzzleBoard> Children { get; private set; } = new();

        private int _depth;
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Depth { 
            get => _depth;
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Depth mustn't be less than 0");
                }

                _depth = value;
            } 
        }
        
        public int[][] PuzzleState { get; set; }
        public MovingDirection PuzzleLastMovingDirection { get; set; }
        protected readonly IEightPuzzleManager PuzzleManager;
        protected readonly IEightPuzzleValidator PuzzleValidator;

        public LDFSpuzzleBoard
            (int[][] puzzleState, int depth, IPuzzleBoard parent, MovingDirection puzzleMovingDirection,
                IEightPuzzleManager puzzleManager, IEightPuzzleValidator puzzleValidator)
        {
            PuzzleState = puzzleState;
            Depth = depth;
            Parent = parent;
            PuzzleLastMovingDirection = puzzleMovingDirection;
            PuzzleManager = puzzleManager;
            PuzzleValidator = puzzleValidator;
        }

        public virtual IPuzzleBoard Clone()
        {
            var newField = new[] { PuzzleState[0].Clone() as int[], PuzzleState[1].Clone() as int[], 
                PuzzleState[2].Clone() as int[] };

            return new LDFSpuzzleBoard
                (newField, Depth, Parent, PuzzleLastMovingDirection, PuzzleManager, PuzzleValidator);
        }

        public Tuple<int, int> GetCoordinatesOfPuzzle(int puzzleValue)
        {
            for (var x = 0; x < PuzzleValidator.PuzzlesCountInRow; ++x)
            {
                for (var y = 0; y < PuzzleValidator.PuzzlesCountInRow; ++y)
                {
                    if (PuzzleState[x][y] == puzzleValue)
                    {
                        return new Tuple<int, int>(x, y);
                    }
                }
            }

            return new Tuple<int, int>(-1, -1);
        }

        private IPuzzleBoard TryGetChild(MovingDirection movingDirection)
        {
            var child = PuzzleManager.MovePuzzle(this, movingDirection);

            if (child is null)
            {
                return null;
            }

            child.PuzzleLastMovingDirection = movingDirection;
            child.Parent = this;
            child.Depth++;

            return child;
        }

        public void GenerateChildren()
        {
            var children = new List<IPuzzleBoard>
            {
                TryGetChild(MovingDirection.Right),
                TryGetChild(MovingDirection.Left),
                TryGetChild(MovingDirection.Up),
                TryGetChild(MovingDirection.Down)
            };

            Children = children.Where(child => child is not null).ToList();
        }
        
        /// <exception cref="ArgumentNullException"></exception>
        public override string ToString()
        {
            return $"Depth - {Depth}, Puzzle state - {string.Join(" ", PuzzleState.To1Dimension())}, "
                   + $"Star last direction - {PuzzleLastMovingDirection}";
        }
    }
}