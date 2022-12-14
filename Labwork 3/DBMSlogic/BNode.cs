using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMSlogic
{
    /// <summary>
    ///     abstract node shared by both B and B+ tree nodes
    ///     so that we can use this for common tests across B and B+ tree
    /// </summary>
    internal abstract class BNode<T> where T : IComparable
    {
        /// <summary>
        ///     Array Index of this node in parent's Children array
        /// </summary>
        internal int Index;

        internal int KeyCount;

        internal BNode(int maxKeysPerNode)
        {
            Keys = new T[maxKeysPerNode];
        }

        internal T[] Keys { get; set; }

        //for common unit testing across B and B+ tree
        internal abstract BNode<T> GetParent();
        internal abstract BNode<T>[] GetChildren();

        internal int GetMedianIndex()
        {
            return KeyCount / 2 + 1;
        }
    }
}