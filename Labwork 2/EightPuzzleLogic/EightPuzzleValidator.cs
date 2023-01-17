using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic;

public class EightPuzzleValidator : IEightPuzzleValidator
{
    public int PuzzlesCountInRow { get; } = 3;
    public int PuzzlesCount { get; } = 9;

    private int[][] _goalState;
    /// <exception cref="ArgumentException"></exception>
    public int[][] GoalState { 
        get => _goalState;
        set
        {
            if (value.Length != PuzzlesCountInRow)
            {
                throw new ArgumentException("The state has incorrent count of puzzles");
            }

            for (var i = 0; i < value.Length; i++)
            {
                if (value[i].Length != PuzzlesCountInRow)
                {
                    throw new ArgumentException("The state has incorrent count of puzzles");
                }
            }

            _goalState = value;
        } 
    }

    private int _puzzleValueToMove;
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public int PuzzleValueToMove { 
        get => _puzzleValueToMove;
        set 
        {
            if ((value < 0) || (value > 8))
            {
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Specified value mustn't be less than 0 and bigger then 8");
            }

            _puzzleValueToMove = value;
        }
    }

    public EightPuzzleValidator(int[][] goalState, int puzzleValueToMove)
    {
        GoalState = goalState;
        PuzzleValueToMove = puzzleValueToMove;
    }
    
    /// <exception cref="ArgumentNullException"></exception>
    public bool ValidatePuzzleMoving(IPuzzleBoard puzzleBoard, MovingDirection movingDirection)
    {
        if (puzzleBoard is null)
        {
            throw new ArgumentNullException(nameof(puzzleBoard));
        }

        var (xPuzzleCoordinate, yPuzzleCoordinate) = puzzleBoard.GetCoordinatesOfPuzzle(PuzzleValueToMove);

        switch (movingDirection)
        {
            case MovingDirection.Left:
                if ((yPuzzleCoordinate == 0) 
                    || (puzzleBoard.PuzzleLastMovingDirection == MovingDirection.Right))
                {
                    return false;
                }
                break;
            case MovingDirection.Right:
                if ((yPuzzleCoordinate == (PuzzlesCountInRow - 1))
                    || (puzzleBoard.PuzzleLastMovingDirection == MovingDirection.Left))
                {
                    return false;
                }
                break;
            case MovingDirection.Up:
                if ((xPuzzleCoordinate == 0) 
                    || (puzzleBoard.PuzzleLastMovingDirection == MovingDirection.Down))
                {
                    return false;
                }
                break;
            case MovingDirection.Down:
                if ((xPuzzleCoordinate == (PuzzlesCountInRow - 1))
                    || (puzzleBoard.PuzzleLastMovingDirection == MovingDirection.Up))
                {
                    return false;
                }
                break;
            default:
                return false;
        }

        return true;
    }

    /// <exception cref="ArgumentException"></exception>
    public bool ValidateGoalStateReaching(int[][] fieldState)
    {
        if (fieldState.Length != PuzzlesCountInRow)
        {
            throw new ArgumentException("The state has incorrent count of puzzles");
        }

        for (var i = 0; i < fieldState.Length; i++)
        {
            if (fieldState[i].Length != PuzzlesCountInRow)
            {
                throw new ArgumentException("The state has incorrent count of puzzles");
            }
        }

        for (int i = 0; i < PuzzlesCountInRow; i++)
        {
            for (int j = 0; j < PuzzlesCountInRow; j++)
            {
                if (fieldState[i][j] != GoalState[i][j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <exception cref="ArgumentNullException"></exception>
    public bool ValidateFieldCorrectness(IEnumerable<int[]> fieldToValidate)
    {
        if (fieldToValidate is null)
        {
            throw new ArgumentNullException(nameof(fieldToValidate));
        }

        if ((fieldToValidate.Count() != PuzzlesCountInRow) || (fieldToValidate.Any(row => row.Length != 3)))
        {
            return false;
        }

        var sortedPuzzleState = fieldToValidate
            .SelectMany(row => row).ToList()
            .OrderBy(num => num).ToList();

        sortedPuzzleState.Remove(0);
        sortedPuzzleState.Add(0);

        var slicedSortedPuzzleState = new int[3][] {new int[PuzzlesCountInRow], 
            new int[PuzzlesCountInRow], new int[PuzzlesCountInRow]};
        var currentPositionInList = 0;

        for (var i = 0; i < PuzzlesCountInRow; i++)
        {
            for (var j = 0; j < PuzzlesCountInRow; j++)
            {
                slicedSortedPuzzleState[i][j] = sortedPuzzleState[currentPositionInList++];
            }
        }
        
        return ValidateGoalStateReaching(slicedSortedPuzzleState);
    }
}