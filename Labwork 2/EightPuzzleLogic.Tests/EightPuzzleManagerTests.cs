using System.Collections.Generic;
using EightPuzzleLogic.Boards;
using EightPuzzleLogic.Tests.BoardsTests;
using EightPuzzleLogic.Tests.ExtensionsTests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EightPuzzleLogic.Tests;

public class EightPuzzleManagerTests
{
    public static IEnumerable<object[]> GeneratePuzzleMovingResults()
    {
        var result = new List<object[]>()
        {
            new object[]
            {
                MovingDirection.Left,
                new []{ new []{1, 2, 3}, new  []{4, 7, 6}, new []{0, 5, 8}}
            },
            new object[]
            {
                MovingDirection.Right,
                new []{ new []{1, 2, 3}, new  []{4, 7, 6}, new []{5, 8, 0}}
            } 
        };

        return result;
    }

    [Fact]
    public void GenerateStartState_GeneratesCorrectState()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var puzzleManager = serviceProvider.GetService<IEightPuzzleManager>();
        var puzzleValidator = serviceProvider.GetService<IEightPuzzleValidator>();
        
        Assert.True(puzzleValidator!.ValidateFieldCorrectness(puzzleManager!.GenerateStartState()));
    }

    [Theory]
    [MemberData(nameof(GeneratePuzzleMovingResults))]
    public void MovePuzzle_MovesPuzzleCorrectly(MovingDirection movingDirection, int[][] expected)
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var puzzleManager = serviceProvider.GetService<IEightPuzzleManager>();
        var puzzleBoard = serviceProvider.GetService<IPuzzleBoard>();
        puzzleBoard!.PuzzleState = PuzzleBoardTests.TestPuzzleState;
        
        var actual = puzzleManager!.MovePuzzle(puzzleBoard, movingDirection);
        
        Assert.Equal(expected, actual.PuzzleState);
    }

    [Fact]
    public void MovePuzzle_ReturnsNullIfCannotMovePuzzle()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var puzzleManager = serviceProvider.GetService<IEightPuzzleManager>();
        var puzzleBoard = serviceProvider.GetService<IPuzzleBoard>();
        puzzleBoard!.PuzzleState = PuzzleBoardTests.TestPuzzleState;
        
        var childBoard = puzzleManager!.MovePuzzle(puzzleBoard, MovingDirection.Down);
        
        Assert.Null(childBoard);
    }
}