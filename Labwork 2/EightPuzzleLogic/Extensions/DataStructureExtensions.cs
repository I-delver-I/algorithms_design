namespace EightPuzzleLogic.Extensions;

public static class DataStructureExtensions
{
    /// <exception cref="ArgumentNullException"></exception>
    public static T[] To1Dimension<T>(this T[][] twoDimensionalArray)
    {
        if (twoDimensionalArray is null)
        {
            throw new ArgumentNullException
                (nameof(twoDimensionalArray), "The input array mustn't be null");
        }

        return twoDimensionalArray.SelectMany(subarray => subarray).ToArray();
    }
}