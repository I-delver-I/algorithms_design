using System;
using System.Collections.Generic;
using EightPuzzleLogic.Boards;
using EightPuzzleLogic.Tests.ExtensionsTests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EightPuzzleLogic.Tests.BoardsTests;

public class PuzzleBoardTests
{
    public static readonly int[][] TestPuzzleState = { new [] {1, 2, 3}, new [] {4, 7, 6}, new [] {5, 0, 8} };

    public static IEnumerable<object[]> GenerateCoordinatesOfPuzzles()
    {
        var result = new List<object[]>()
        {
            new object[]
            {
                0,
                new Tuple<int, int>(2, 1)
            },
            new object[]
            {
                1,
                new Tuple<int, int>(0, 0)
            }
        };

        return result;
    }
        
    public static IEnumerable<object[]> GeneratePuzzleValueWithDirectionAndExpectedBoard()
    {
        var result = new List<object[]>()
        {
            new object[]
            {
                1,
                MovingDirection.Down,
                new [] { new []{ 4, 2, 3 }, new []{ 1, 7, 6 }, new []{ 5, 0, 8 } }
            },
            new object[]
            {
                0,
                MovingDirection.Up,
                new []{ new []{ 1, 2, 3 }, new []{ 4, 0, 6 }, new []{ 5, 7, 8 } }
            }
        };

        return result;
    }

    [Fact]
    public void TryGetChild_ReturnsNullIfChildIsNotFound()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var parentBoard = serviceProvider.GetService<IPuzzleBoard>();
        parentBoard!.PuzzleState = TestPuzzleState;
        
        var actual = parentBoard.TryGetChild(MovingDirection.Down);
        
        Assert.Null(actual);
    }

    [Theory]
    [MemberData(nameof(GeneratePuzzleValueWithDirectionAndExpectedBoard))]
    public void TryGetChild_ReturnsChildCorrectly(int puzzleValueToMove, MovingDirection movingDirection,
        int[][] expected)
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider(puzzleValueToMove: puzzleValueToMove);
        var parentBoard = serviceProvider.GetService<IPuzzleBoard>();
        parentBoard!.PuzzleState = TestPuzzleState;
            
        var actual = parentBoard.TryGetChild(movingDirection);
            
        Assert.Equal(expected, actual.PuzzleState);
    }

    [Theory]
    [MemberData(nameof(GenerateCoordinatesOfPuzzles))]
    public void GetCoordinatesOfPuzzle_CoordinatesAreCorrect(int puzzleValue, Tuple<int, int> expected)
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();

        var puzzleBoard = serviceProvider.GetService<IPuzzleBoard>();
        puzzleBoard!.PuzzleState = TestPuzzleState;
            
        var actual = puzzleBoard!.GetCoordinatesOfPuzzle(puzzleValue);
                
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetCoordinatesOfPuzzle_SearchedPuzzleIsIncorrect()
    {
        var expected = new Tuple<int, int>(-1, -1);

        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();

        var puzzleBoard = serviceProvider.GetService<IPuzzleBoard>();
        puzzleBoard!.PuzzleState = TestPuzzleState;

        const int wrongPuzzleValue = 777;
        var actual = puzzleBoard!.GetCoordinatesOfPuzzle(wrongPuzzleValue);
                
        Assert.Equal(expected, actual);
    }
}