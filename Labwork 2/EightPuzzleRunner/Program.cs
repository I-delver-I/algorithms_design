using System.Diagnostics;
using EightPuzzleLogic;
using EightPuzzleLogic.Algorithms;
using EightPuzzleLogic.Boards;
using EightPuzzleLogic.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace EightPuzzleRunner
{
    public static class Program
    {
        public static void Main()
        {
            const bool debug = false;
            var goalState = new[] { new [] {1, 2, 3}, new [] {4, 5, 6}, new [] {7, 8, 0} };
            const PuzzleSortAlgorithmType sortingAlgorithmType = PuzzleSortAlgorithmType.LDFS;
            const int puzzleValueToMove = 0;

            IServiceProvider serviceContainer;

            try
            {
                serviceContainer = new ServiceCollection()
                    .ConfigureServices(debug, goalState, puzzleValueToMove, sortingAlgorithmType)
                    .BuildServiceProvider();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                System.Console.WriteLine(ex.Message);
                return;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            IPuzzleBoard startingBoard;
            
            if (sortingAlgorithmType is PuzzleSortAlgorithmType.LDFS)
            {
                startingBoard = serviceContainer.GetService<IPuzzleBoard>();
            }
            else
            {
                startingBoard = serviceContainer.GetService<IAstarPuzzleBoard>();
            }

            var outputHandler = new OutputHandler(serviceContainer.GetService<IEightPuzzleValidator>());

            Console.WriteLine("Created puzzle field:");
            outputHandler.PrintField(startingBoard!.PuzzleState);

            var solver = serviceContainer.GetService<IEightPuzzleSolving>();
            
            var timer = new Stopwatch();
            
            timer.Start();
            IPuzzleBoard resultBoard;

            try
            {
                resultBoard = solver!.SolveEightPuzzle(startingBoard);
            }
            catch (ArgumentNullException ex)
            {
                System.Console.WriteLine(ex.Message);
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine(ex.Message);
                return;
            }
            catch (InsufficientMemoryException ex)
            {
                System.Console.WriteLine(ex.Message);
                return;
            }

            timer.Stop();
            
            Console.WriteLine($"Solving of 8-puzzle took {timer.Elapsed} time");
            
            if (resultBoard is null)
            {
                Console.WriteLine("Solution is not found");
            }
            else
            {
                Console.WriteLine("Solution step-by-step:");
                
                try
                {
                    outputHandler.PrintSortingSolutionStepByStep(resultBoard);
                    outputHandler.PrintSolvingCharacteristics(serviceContainer
                                    .GetService<ICharacteristicable>());
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
}