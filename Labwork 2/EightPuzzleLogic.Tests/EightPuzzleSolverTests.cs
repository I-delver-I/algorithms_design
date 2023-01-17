using EightPuzzleLogic.Algorithms;
using EightPuzzleLogic.Boards;
using EightPuzzleLogic.Tests.ExtensionsTests;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace EightPuzzleLogic.Tests.AlgorithmsTests;

public class EightPuzzleSolverTests
{
    private static readonly int[][] LdfsSolvableStartState = 
        { new[] { 3, 4, 1 }, new[] { 2, 0, 6 }, new[] { 7, 5, 8 } };
    private static readonly int[][] AstarSolvableStartState = 
        { new[] { 6, 7, 1 }, new[] { 4, 0, 2 }, new[] { 5, 8, 3 } };
    
    private static readonly int[][] AstarUnsolvableStartState = 
        { new []{7, 3, 8}, new []{4, 0, 1}, new []{6, 2, 5} };
    private static readonly int[][] LdfsUnsolvableStartState = 
        { new []{4, 1, 2}, new []{7, 0, 3}, new []{8, 6, 5} };
    
    [Fact]
    public void LdfsSolveEightPuzzle_AvailableMemoryExceeded()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider(PuzzleSortAlgorithmType.LDFS);
        var solver = serviceProvider.GetService<IEightPuzzleSolving>();
        var puzzleBoard = serviceProvider.GetService<IPuzzleBoard>();
        puzzleBoard!.PuzzleState = LdfsUnsolvableStartState;
        
        Assert.Throws<InsufficientMemoryException>(() => solver!.SolveEightPuzzle(puzzleBoard));
    }
    
    [Fact]
    public void AstarSolveEightPuzzle_AvailableMemoryExceeded()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider(PuzzleSortAlgorithmType.Astar);
        var solver = serviceProvider.GetService<IEightPuzzleSolving>();
        var puzzleBoard = serviceProvider.GetService<IAstarPuzzleBoard>();
        puzzleBoard!.PuzzleState = AstarUnsolvableStartState;
        
        Assert.Throws<InsufficientMemoryException>(() => solver!.SolveEightPuzzle(puzzleBoard));
    }
    
    [Fact]
    public void SolveEightPuzzleWithLDFS_SolvesSuccessfully()
    {
        GC.Collect();

        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider(PuzzleSortAlgorithmType.LDFS);
        var solver = serviceProvider.GetService<IEightPuzzleSolving>();
        var puzzleValidator = serviceProvider.GetService<IEightPuzzleValidator>();
        var expected = puzzleValidator!.GoalState;

        var puzzleBoard = serviceProvider.GetService<IPuzzleBoard>();
        puzzleBoard!.PuzzleState = LdfsSolvableStartState;

        var actual = solver!.SolveEightPuzzle(puzzleBoard).PuzzleState;
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void SolveEightPuzzleWithAstar_SolvesSuccessfully()
    {
        var serviceProvider = IocContainerExtensionsTests.GetServiceProvider(PuzzleSortAlgorithmType.Astar);
        var solver = serviceProvider.GetService<IEightPuzzleSolving>();
        var puzzleValidator = serviceProvider.GetService<IEightPuzzleValidator>();
        var expected = puzzleValidator!.GoalState;

        var puzzleBoard = serviceProvider.GetService<IAstarPuzzleBoard>();
        puzzleBoard!.PuzzleState = AstarSolvableStartState;

        var actual = solver!.SolveEightPuzzle(puzzleBoard).PuzzleState;
        
        Assert.Equal(expected, actual);
    }
}