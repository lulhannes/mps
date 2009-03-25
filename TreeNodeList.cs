// This collection of non-binary tree data structures created by Dan Vanderboom.
// Critical Development blog: http://dvanderboom.wordpress.com
// Original Tree<T> blog article: http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/
// Linked-in: http://www.linkedin.com/profile?viewProfile=&key=13009616&trk=tab_pro

using System;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// Contains a list of TreeNode (or TreeNode-derived) objects, with the capability of linking parents and children in both directions.
    /// </summary>
    public class TreeNodeList<T> : List<TreeNode<T>> where T : TreeNode<T>
    {
        public T Parent;

        public TreeNodeList(TreeNode<T> Parent)
        {
            this.Parent = (T)Parent;
        }

        public T Add(T Node)
        {
            base.Add(Node);
            Node.Parent = Parent;
            return Node;
        }

        public void Remove(T Node)
        {
            if (Node != null)
            {
                Node.Parent = null;
            }
            base.Remove(Node);
        }

        public override string ToString()
        {
            return "Count=" + Count.ToString();
        }
    }
}