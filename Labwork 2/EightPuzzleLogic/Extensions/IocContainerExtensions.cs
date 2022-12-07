using EightPuzzleLogic.Algorithms;
using EightPuzzleLogic.Boards;
using Microsoft.Extensions.DependencyInjection;

namespace EightPuzzleLogic.Extensions
{
    public static class IocContainerExtensions
    {
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public static IServiceCollection ConfigureServices
            (this IServiceCollection services, bool debug, int[][] goalState, int puzzleValueToMove, 
            PuzzleSortAlgorithmType sortingAlgorithmType)
        {
            var puzzleValidator = services
                .AddSingleton<IEightPuzzleValidator>((_) => new EightPuzzleValidator(goalState, puzzleValueToMove))
                .BuildServiceProvider()
                .GetService<IEightPuzzleValidator>();
            
            var puzzleManager = services
                .AddSingleton<IEightPuzzleManager>((_) => new EightPuzzleManager(puzzleValidator))
                .BuildServiceProvider()
                .GetRequiredService<IEightPuzzleManager>();
    
            int[][] generatedPuzzleField;
            
            if (debug)
            {
                generatedPuzzleField = new[] { 
                    new[] { 1, 2, 3 }, new[] { 4, 7, 6 }, new[] { 5, 0, 8 } };

                if (!puzzleValidator!.ValidateFieldCorrectness(generatedPuzzleField))
                {
                    throw new InvalidDataException("The generated puzzle field is wrong");
                }
            }
            else
            {
                generatedPuzzleField = puzzleManager.GenerateStartState();
            }

            switch (sortingAlgorithmType)
            {
                case PuzzleSortAlgorithmType.LDFS:
                    int depthLimit = 27;
                    var ldfsSolver = new LdfsSolver(puzzleValidator, depthLimit);

                    services
                        .AddSingleton<IEightPuzzleSolving>((_) => ldfsSolver)
                        .AddSingleton<ICharacteristicable>((_) => ldfsSolver);
                    break;
                case PuzzleSortAlgorithmType.Astar:
                    var aStarSolver = new AstarSolver(puzzleValidator);

                    services
                        .AddSingleton<IEightPuzzleSolving>((_) => aStarSolver)
                        .AddSingleton<ICharacteristicable>((_) => aStarSolver);
                    break;
                default:
                    throw new ArgumentException("Specified wrong algorithm type");
            }

            services
                .AddSingleton<IPuzzleBoard>((_) => 
                    new LDFSpuzzleBoard(generatedPuzzleField, 0, null, 
                            default, puzzleManager, puzzleValidator))
                .AddSingleton<IAstarPuzzleBoard>((_) => 
                    new AstarLdfSpuzzleBoard
                        (generatedPuzzleField, 0, null, 
                            default, puzzleManager, puzzleValidator));

            return services;
        }
    }
}