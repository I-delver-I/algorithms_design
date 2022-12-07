using EightPuzzleLogic.Boards;

namespace EightPuzzleLogic
{
    public class EightPuzzleManager : IEightPuzzleManager
    {
        private readonly IEightPuzzleValidator _puzzleValidator;
        
        public EightPuzzleManager(IEightPuzzleValidator puzzleValidator)
        {
            _puzzleValidator = puzzleValidator;
        }

        public IPuzzleBoard MovePuzzle(IPuzzleBoard puzzleBoard, MovingDirection movingDirection)
        {
            if (!_puzzleValidator.ValidatePuzzleMoving(puzzleBoard, movingDirection))
            {
                return null;
            }

            var child = puzzleBoard.Clone();
            int tempPuzzle;
            var (xPuzzleCoordinate, yPuzzleCoordinate) = puzzleBoard
                .GetCoordinatesOfPuzzle(_puzzleValidator.PuzzleValueToMove);

            switch (movingDirection)
            {
                case MovingDirection.Left:
                    tempPuzzle = child.PuzzleState[xPuzzleCoordinate][yPuzzleCoordinate - 1];
                    child.PuzzleState[xPuzzleCoordinate][yPuzzleCoordinate - 1] = _puzzleValidator.PuzzleValueToMove;
                    child.PuzzleState[xPuzzleCoordinate][yPuzzleCoordinate] = tempPuzzle;
                    break;
                case MovingDirection.Right:
                    tempPuzzle = child.PuzzleState[xPuzzleCoordinate][yPuzzleCoordinate + 1];
                    child.PuzzleState[xPuzzleCoordinate][yPuzzleCoordinate + 1] = _puzzleValidator.PuzzleValueToMove;
                    child.PuzzleState[xPuzzleCoordinate][yPuzzleCoordinate] = tempPuzzle;
                    break;
                case MovingDirection.Up:
                    tempPuzzle = child.PuzzleState[xPuzzleCoordinate - 1][yPuzzleCoordinate];
                    child.PuzzleState[xPuzzleCoordinate - 1][yPuzzleCoordinate] = _puzzleValidator.PuzzleValueToMove;
                    child.PuzzleState[xPuzzleCoordinate][yPuzzleCoordinate] = tempPuzzle;
                    break;
                case MovingDirection.Down:
                    tempPuzzle = child.PuzzleState[xPuzzleCoordinate + 1][yPuzzleCoordinate];
                    child.PuzzleState[xPuzzleCoordinate + 1][yPuzzleCoordinate] = _puzzleValidator.PuzzleValueToMove;
                    child.PuzzleState[xPuzzleCoordinate][yPuzzleCoordinate] = tempPuzzle;
                    break;
                default:
                    return null;
            }

            return child;
        }

        public int[][] GenerateStartState()
        {
            var result = new[] { new int[_puzzleValidator.PuzzlesCountInRow], 
                new int[_puzzleValidator.PuzzlesCountInRow], new int[_puzzleValidator.PuzzlesCountInRow] };
            var rd = new Random();
            var puzzlePositionInRow = 0;
            var zeroExists = false;

            for (var i = 0; i < _puzzleValidator.PuzzlesCountInRow; i++)
            {
                while (puzzlePositionInRow < _puzzleValidator.PuzzlesCountInRow)
                {
                    var generatedPuzzle = rd.Next(0, _puzzleValidator.PuzzlesCount);

                    if (generatedPuzzle == _puzzleValidator.PuzzleValueToMove)
                    {
                        continue;
                    }
                    
                    if ((generatedPuzzle == 0) && !zeroExists)
                    {
                        puzzlePositionInRow++;
                        zeroExists = true;
                    }

                    if (Array.Exists(result, row => row.Contains(generatedPuzzle)))
                    {
                        continue;
                    }

                    if ((puzzlePositionInRow != 1) || (i != 1))
                    {
                        result[i][puzzlePositionInRow] = generatedPuzzle;
                    }

                    puzzlePositionInRow++;
                }

                puzzlePositionInRow = 0;
            }

            result[^2][_puzzleValidator.PuzzlesCountInRow - 2] = _puzzleValidator.PuzzleValueToMove;

            return result;
        }
    }
}