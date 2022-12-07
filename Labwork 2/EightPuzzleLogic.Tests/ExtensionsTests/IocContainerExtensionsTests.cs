using Microsoft.Extensions.DependencyInjection;
using EightPuzzleLogic.Algorithms;
using EightPuzzleLogic.Boards;
using EightPuzzleLogic.Extensions;
using Xunit;

namespace EightPuzzleLogic.Tests.ExtensionsTests
{
    public class IocContainerExtensionsTests
    {
        private static readonly int[][] GoalState = { new [] {1, 2, 3}, new [] {4, 5, 6}, new [] {7, 8, 0} };

        public static ServiceProvider GetServiceProvider
        (PuzzleSortAlgorithmType algorithmType = PuzzleSortAlgorithmType.Astar, 
            int puzzleValueToMove = 0, bool debug = false)
        {
            var collection = new ServiceCollection();
            return collection
                .ConfigureServices(debug, GoalState, puzzleValueToMove, algorithmType)
                .BuildServiceProvider();
        }

        [Fact]
        public void IocContainer_GetEightPuzzleManagerService_IsNotNull()
        {
            var eightPuzzleManagerService = GetServiceProvider().GetService<IEightPuzzleManager>();

            Assert.NotNull(eightPuzzleManagerService);
        }

        [Fact]
        public void IocContainer_GetEightPuzzleSolvingService_IsNotNull()
        {
            var eightPuzzleSolvingService = GetServiceProvider().GetService<IEightPuzzleSolving>();

            Assert.NotNull(eightPuzzleSolvingService);
        }

        [Fact]
        public void IocContainer_GetPuzzleBoardService_IsNotNull()
        {
            var puzzleBoardService = GetServiceProvider().GetService<IPuzzleBoard>();

            Assert.NotNull(puzzleBoardService);
        }

        [Fact]
        public void IocContainer_GetAstarPuzzleBoardService_IsNotNull()
        {
            var aStarPuzzleBoardService = GetServiceProvider().GetService<IAstarPuzzleBoard>();

            Assert.NotNull(aStarPuzzleBoardService);
        }
        
        [Fact]
        public void IocContainer_GetEightPuzzleValidatorService_IsNotNull()
        {
            var eightPuzzleValidator = GetServiceProvider().GetService<IEightPuzzleValidator>();

            Assert.NotNull(eightPuzzleValidator);
        }
    }
}