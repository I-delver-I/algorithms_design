using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DBMSlogic;

internal class BTreeEnumerator<T> : IEnumerator<T> where T : IComparable
{
    private readonly BTreeNode<T> root;

    private BTreeNode<T> current;
    private int index;
    private Stack<BTreeNode<T>> progress;

    internal BTreeEnumerator(BTreeNode<T> root)
    {
        this.root = root;
    }

    public bool MoveNext()
    {
        if (root == null) return false;

        if (progress == null)
        {
            current = root;
            progress = new Stack<BTreeNode<T>>(root.Children.Take(root.KeyCount + 1).Where(x => x != null));
            return current.KeyCount > 0;
        }

        if (current != null && index + 1 < current.KeyCount)
        {
            index++;
            return true;
        }

        if (progress.Count > 0)
        {
            index = 0;

            current = progress.Pop();

            foreach (var child in current.Children.Take(current.KeyCount + 1).Where(x => x != null))
                progress.Push(child);

            return true;
        }

        return false;
    }

    public void Reset()
    {
        progress = null;
        current = null;
        index = 0;
    }

    object IEnumerator.Current => Current;

    public T Current => current.Keys[index];

    public void Dispose()
    {
        progress = null;
    }
}