using EightPuzzleLogic.Boards;
using EightPuzzleLogic.Tests.ExtensionsTests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EightPuzzleLogic.Tests.BoardsTests;

public class AstarPuzzleBoardTests
{
    [Fact]
    public void GetOutlay_ReturnsCorrectOutlay()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var aStarPuzzleBoard = serviceProvider.GetService<IAstarPuzzleBoard>();
        aStarPuzzleBoard!.PuzzleState = PuzzleBoardTests.TestPuzzleState;
        const int expected = 5;
        
        var actual = aStarPuzzleBoard!.GetOutlay();
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void GetDistanceToGoal_DistanceCalculatesCorrectly()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var aStarPuzzleBoard = serviceProvider.GetService<IAstarPuzzleBoard>();
        aStarPuzzleBoard!.PuzzleState = PuzzleBoardTests.TestPuzzleState;
        const int expected = 5;

        var actual = aStarPuzzleBoard!.GetDistanceToGoal();

        Assert.Equal(expected, actual);
    }
}