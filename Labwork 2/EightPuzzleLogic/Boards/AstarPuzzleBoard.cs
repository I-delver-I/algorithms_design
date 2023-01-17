namespace EightPuzzleLogic.Boards;
using System;

public class AstarLdfSpuzzleBoard : LDFSpuzzleBoard, IAstarPuzzleBoard
{
    public AstarLdfSpuzzleBoard(int[][] puzzleState, int depth, IPuzzleBoard parent,
        MovingDirection puzzleMovingDirection, IEightPuzzleManager puzzleManager, IEightPuzzleValidator puzzleValidator)
        : base(puzzleState, depth, parent, puzzleMovingDirection, puzzleManager, puzzleValidator)
    {
    }

    public override IPuzzleBoard Clone()
    {
        var newField = new[] { PuzzleState[0].Clone() as int[], PuzzleState[1].Clone() as int[], 
            PuzzleState[2].Clone() as int[] };

        return new AstarLdfSpuzzleBoard(newField, Depth, Parent, PuzzleLastMovingDirection, 
            PuzzleManager, PuzzleValidator);
    }

    public int GetDistanceToGoal()
    {
        var distanceToGoalPlace = 0;

        for (int i = 0; i < PuzzleValidator.PuzzlesCountInRow; i++)
        {
            for (int j = 0; j < PuzzleValidator.PuzzlesCountInRow; j++)
            {
                if (PuzzleState[i][j] == PuzzleValidator.PuzzleValueToMove)
                {
                    continue;
                }
                
                var (puzzleDiv, puzzleMod) = (
                        (PuzzleState[i][j] - 1) / PuzzleValidator.PuzzlesCountInRow,
                        (PuzzleState[i][j] - 1) % PuzzleValidator.PuzzlesCountInRow
                    );

                distanceToGoalPlace += Math.Abs(puzzleDiv - i) + Math.Abs(puzzleMod - j);
            }
        }

        return distanceToGoalPlace;
    }

    public int GetOutlay()
    {
        return GetDistanceToGoal() + Depth;
    }
}