using System.Collections;

namespace DBMSlogic;

public class BTreeEnumerator<T> : IEnumerator<T> where T : IComparable
{
    private readonly BTreeNode<T> _root;

    private BTreeNode<T> _current;
    private int _index;
    private Stack<BTreeNode<T>> _progress;

    public BTreeEnumerator(BTreeNode<T> root)
    {
        _root = root;
    }

    public bool MoveNext()
    {
        if (_root == null) 
        {
            return false;
        }

        if (_progress == null)
        {
            _current = _root;
            _progress = new Stack<BTreeNode<T>>(_root.Children.Take(_root.KeyCount + 1).Where(x => x != null));

            return _current.KeyCount > 0;
        }

        if ((_current != null) && (_index + 1 < _current.KeyCount))
        {
            _index++;

            return true;
        }

        if (_progress.Count > 0)
        {
            _index = 0;

            _current = _progress.Pop();

            foreach (var child in _current.Children.Take(_current.KeyCount + 1).Where(x => x != null))
            {
                _progress.Push(child);
            }

            return true;
        }

        return false;
    }

    public void Reset()
    {
        _progress = null;
        _current = null;
        _index = 0;
    }

    object IEnumerator.Current => Current;

    public T Current => _current.Keys[_index];

    public void Dispose() => _progress = null;
}