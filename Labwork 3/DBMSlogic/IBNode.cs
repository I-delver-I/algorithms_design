namespace DBMSlogic;

public interface IBNode<T> where T : IComparable
{
    /// <summary>
    ///     Array Index of this node in parent's Children array
    /// </summary>
    public int Index { get; set; }
    
    public int KeyCount { get; set; }

    public T[] Keys { get; set; }

    public int GetMedianIndex();
}