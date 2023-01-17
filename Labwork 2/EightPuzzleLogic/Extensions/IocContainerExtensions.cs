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
            (this IServiceCollection services, int[][] generatedPuzzleField, object solver)
        {           
            var puzzleValidator = services
                .BuildServiceProvider()
                .GetRequiredService<IEightPuzzleValidator>();

            var puzzleManager = services
                .BuildServiceProvider()
                .GetRequiredService<IEightPuzzleManager>();
            
            services
                .AddSingleton<IEightPuzzleSolving>((_) => (IEightPuzzleSolving)solver)
                .AddSingleton<ICharacteristicable>((_) => (ICharacteristicable)solver)
                .AddSingleton<IPuzzleBoard>((_) => 
                    new LDFSpuzzleBoard(generatedPuzzleField, 0, null, default, puzzleManager, 
                        puzzleValidator))
                .AddSingleton<IAstarPuzzleBoard>((_) => 
                    new AstarLdfSpuzzleBoard(generatedPuzzleField, 0, null, default, puzzleManager, 
                        puzzleValidator));

            return services;
        }

        public static IEightPuzzleValidator ConfigurePuzzleValidator
            (this ServiceCollection services, int[][] goalState, int puzzleValueToMove = 0)
        {
            return services
                .AddSingleton<IEightPuzzleValidator>((_) => new EightPuzzleValidator(goalState, puzzleValueToMove))
                .BuildServiceProvider()
                .GetRequiredService<IEightPuzzleValidator>();
        }

        public static IEightPuzzleManager ConfigurePuzzleManager
            (this ServiceCollection services, IEightPuzzleValidator puzzleValidator)
        {
            return services
                .AddSingleton<IEightPuzzleManager>((_) => new EightPuzzleManager(puzzleValidator))
                .BuildServiceProvider()
                .GetRequiredService<IEightPuzzleManager>();
        }
    }
}