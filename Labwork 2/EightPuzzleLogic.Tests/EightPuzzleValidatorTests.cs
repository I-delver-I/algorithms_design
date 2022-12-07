using EightPuzzleLogic.Boards;
using EightPuzzleLogic.Tests.BoardsTests;
using EightPuzzleLogic.Tests.ExtensionsTests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EightPuzzleLogic.Tests;

public class EightPuzzleValidatorTests
{
    [Fact]
    public void ValidateFieldCorrectness_FieldIsIncorrect()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var puzzleValidator = serviceProvider.GetService<IEightPuzzleValidator>();
        var actual = new[] { new[] { 1, 2, 3 } };
        
        Assert.False(puzzleValidator!.ValidateFieldCorrectness(actual));
    }
    
    [Fact]
    public void ValidateFieldCorrectness_FieldIsCorrect()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var puzzleValidator = serviceProvider.GetService<IEightPuzzleValidator>();
        
        Assert.True(puzzleValidator!.ValidateFieldCorrectness(PuzzleBoardTests.TestPuzzleState));
    }
    
    [Fact]
    public void ValidateGoalStateReaching_GoalIsNotReached()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var puzzleValidator = serviceProvider.GetService<IEightPuzzleValidator>();
        var actual = PuzzleBoardTests.TestPuzzleState;

        Assert.False(puzzleValidator!.ValidateGoalStateReaching(actual));
    }
    
    [Fact]
    public void ValidateGoalStateReaching_GoalIsReached()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var puzzleValidator = serviceProvider.GetService<IEightPuzzleValidator>();
        var actual = new[] { new[] { 1, 2, 3 }, new[] { 4, 5, 6 }, new[] { 7, 8, 0 } };

        Assert.True(puzzleValidator!.ValidateGoalStateReaching(actual));
    }
    
    [Theory]
    [InlineData(MovingDirection.Left)]
    [InlineData(MovingDirection.Up)]
    [InlineData(MovingDirection.Right)]
    public void ValidatePuzzleMoving_PuzzleMovingCanBeDone(MovingDirection movingDirection)
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var puzzleValidator = serviceProvider.GetService<IEightPuzzleValidator>();
        var puzzleBoard = serviceProvider.GetService<IPuzzleBoard>();
        
        puzzleBoard!.PuzzleState = PuzzleBoardTests.TestPuzzleState;
        
        Assert.True(puzzleValidator!.ValidatePuzzleMoving(puzzleBoard, movingDirection));
    }

    [Fact]
    public void ValidatePuzzleMoving_PuzzleMovingCanNotBeDone()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider();
        var puzzleValidator = serviceProvider.GetService<IEightPuzzleValidator>();
        var puzzleBoard = serviceProvider.GetService<IPuzzleBoard>();
        
        puzzleBoard!.PuzzleState = PuzzleBoardTests.TestPuzzleState;
        const MovingDirection movingDirection = MovingDirection.Down;
        
        Assert.False(puzzleValidator!.ValidatePuzzleMoving(puzzleBoard, movingDirection));
    }
}