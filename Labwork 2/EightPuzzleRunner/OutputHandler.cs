using EightPuzzleLogic;
using EightPuzzleLogic.Boards;
using EightPuzzleLogic.Algorithms;

namespace EightPuzzleRunner
{
    public class OutputHandler
    {
        private readonly IEightPuzzleValidator _puzzleValidator;

        public OutputHandler(IEightPuzzleValidator puzzleValidator)
        {
            _puzzleValidator = puzzleValidator;
        }

        /// <exception cref="ArgumentNullException"></exception>
        public void PrintField(int[][] puzzleField)
        {
            if (puzzleField is null)
            {
                throw new ArgumentNullException(nameof(puzzleField), "The puzzle field mustn't be null");
            }

            const int dashesCountInRow = 9;
            var currentPuzzlePositionInRow = 0;

            var horizontalLine = new string('-', dashesCountInRow);
            Console.WriteLine(horizontalLine);

            for (var i = 0; i < _puzzleValidator.PuzzlesCountInRow; i++)
            {
                var verticalLine = $"| {puzzleField[i][currentPuzzlePositionInRow++]} "
                    + $"{puzzleField[i][currentPuzzlePositionInRow++]} "
                    + $"{puzzleField[i][currentPuzzlePositionInRow]} |";
                currentPuzzlePositionInRow = 0;

                Console.WriteLine(verticalLine);
            }

            Console.WriteLine(horizontalLine);
        }

        /// <exception cref="ArgumentNullException"></exception>
        public void PrintSortingSolutionStepByStep(IPuzzleBoard resultPuzzleBoard)
        {
            if (resultPuzzleBoard == null)
            {
                throw new ArgumentNullException(nameof(resultPuzzleBoard), "Puzzle board mustn't be null");
            }

            PrintSortingSolutionRecursively(resultPuzzleBoard);
        }

        private void PrintSortingSolutionRecursively(IPuzzleBoard puzzleBoard)
        {
            if (puzzleBoard is null)
            {
                return;
            }
        
            PrintSortingSolutionRecursively(puzzleBoard.Parent);
            Console.WriteLine(puzzleBoard);
            PrintField(puzzleBoard.PuzzleState);
        }

        /// <exception cref="ArgumentNullException"></exception>
        public void PrintSolvingCharacteristics(ICharacteristicable characteristicable)
        {
            if (characteristicable is null)
            {
                throw new ArgumentNullException(nameof(characteristicable));
            }

            System.Console.WriteLine($"Iterations count - {characteristicable.IterationsCount}\n"
                + $"Blind corners count - {characteristicable.BlindCornersCount}\n"
                + $"Overall states count - {characteristicable.OverallStatesCount}\n"
                +$"States count in memory - {characteristicable.StatesCountInMemory}");
        }
    }
}