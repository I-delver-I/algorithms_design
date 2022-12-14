using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMSlogic
{
    internal class BTreeNode<T> : BNode<T> where T : IComparable
    {
        internal BTreeNode(int maxKeysPerNode, BTreeNode<T> parent)
            : base(maxKeysPerNode)
        {
            Parent = parent;
            Children = new BTreeNode<T>[maxKeysPerNode + 1];
        }

        internal BTreeNode<T> Parent { get; set; }
        internal BTreeNode<T>[] Children { get; set; }

        internal bool IsLeaf => Children[0] == null;

        /// <summary>
        ///     For shared test method accross B and B+ tree
        /// </summary>
        internal override BNode<T> GetParent()
        {
            return Parent;
        }

        /// <summary>
        ///     For shared test method accross B and B+ tree
        /// </summary>
        internal override BNode<T>[] GetChildren()
        {
            return Children;
        }
    }
}