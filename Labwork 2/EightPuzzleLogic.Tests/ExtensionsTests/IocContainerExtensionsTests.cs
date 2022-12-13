using Microsoft.Extensions.DependencyInjection;
using EightPuzzleLogic.Algorithms;
using EightPuzzleLogic.Boards;
using EightPuzzleLogic.Extensions;
using Xunit;

namespace EightPuzzleLogic.Tests.ExtensionsTests
{
    public class IocContainerExtensionsTests
    {
        private static readonly int[][] GoalState = 
            { new [] {1, 2, 3}, new [] {4, 5, 6}, new [] {7, 8, 0} };

        public static object GetSolverByName
            (PuzzleSortAlgorithmType algorithmType, IEightPuzzleValidator puzzleValidator)
        {
            switch (algorithmType)
            {
                case PuzzleSortAlgorithmType.Astar:
                    return new AstarSolver(puzzleValidator);
                case PuzzleSortAlgorithmType.LDFS:
                    int depthLimit = 27;

                    return new LdfsSolver(puzzleValidator, depthLimit);
                default:
                    return null;
            }
        }

        public static ServiceProvider GetServiceProvider
            (PuzzleSortAlgorithmType algorithmType = PuzzleSortAlgorithmType.Astar, 
                int puzzleValueToMove = 0)
        {
            var serviceCollection = new ServiceCollection();

            var puzzleValidator = serviceCollection
                .ConfigurePuzzleValidator(GoalState, puzzleValueToMove);
            var puzzleManager = serviceCollection
                .ConfigurePuzzleManager(puzzleValidator);

            var startState = puzzleManager.GenerateStartState();
            var solver = GetSolverByName(algorithmType, puzzleValidator);

            return serviceCollection
                .ConfigureServices(startState, solver)
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