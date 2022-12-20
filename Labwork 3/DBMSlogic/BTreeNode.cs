namespace DBMSlogic;

public class BTreeNode<T> : IBNode<T> where T : IComparable
{
    public BTreeNode<T> Parent { get; set; }
    public BTreeNode<T>[] Children { get; set; }
    public int Index { get; set; }
    public int KeyCount { get; set; }
    public T[] Keys { get; set; }

    public bool IsLeaf => Children[0] == null;

    public BTreeNode(int maxKeysCountPerNode, BTreeNode<T> parent)
    {
        Keys = new T[maxKeysCountPerNode];
        Parent = parent;
        Children = new BTreeNode<T>[maxKeysCountPerNode + 1];
    }

    public int GetMedianIndex()
    {
        return KeyCount / 2 + 1;
    }
}