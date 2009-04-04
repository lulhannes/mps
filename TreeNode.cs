// This collection of non-binary tree data structures created by Dan Vanderboom.
// Critical Development blog: http://dvanderboom.wordpress.com
// Original Tree<T> blog article: http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/
// Linked-in: http://www.linkedin.com/profile?viewProfile=&key=13009616&trk=tab_pro

using System;
using System.Text;
using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a node in a Tree structure, with a parent node and zero or more child nodes.
    /// </summary>
    public class TreeNode<T> : IDisposable where T : TreeNode<T>
    {
        private T _Parent;
        public T Parent
        {
            get { return _Parent; }
            set
            {
                if (value == _Parent || value.Parents.Contains((T)this))
                {
                    return;
                }

                if (_Parent != null)
                {
                    _Parent.Children.Remove(this);
                }

                if (value != null && !value.Children.Contains(this))
                {
                    value.Children.Add(this);
                }

                _Parent = value;
            }
        }

        public List<T> Parents
        {
            get
            {
                List<T> parents = new List<T>();
                TreeNode<T> node = this;
                while (node.Parent != null)
                {
                    parents.Add(node.Parent);
                    node = node.Parent;
                }
                return parents;
            }
        }

        public T Root
        {
            get
            {
                //return (Parent == null) ? this : Parent.Root;

                TreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                }
                return (T)node;
            }
        }

        private TreeNodeList<T> _Children;
        public virtual TreeNodeList<T> Children
        {
            get { return _Children; }
            private set { _Children = value; }
        }

        private TreeTraversalDirection _DisposeTraversal = TreeTraversalDirection.BottomUp;
        /// <summary>
        /// Specifies the pattern for traversing the Tree for disposing of resources. Default is BottomUp.
        /// </summary>
        public TreeTraversalDirection DisposeTraversal
        {
            get { return _DisposeTraversal; }
            set { _DisposeTraversal = value; }
        }

        public TreeNode()
        {
            Parent = null;
            Children = new TreeNodeList<T>(this);
        }

        public TreeNode(T Parent)
        {
            this.Parent = Parent;
            Children = new TreeNodeList<T>(this);
        }

        public TreeNode(TreeNodeList<T> Children)
        {
            Parent = null;
            this.Children = Children;
            Children.Parent = (T)this;
        }

        public TreeNode(T Parent, TreeNodeList<T> Children)
        {
            this.Parent = Parent;
            this.Children = Children;
            Children.Parent = (T)this;
        }

        /// <summary>
        /// Reports a depth of nesting in the tree, starting at 0 for the root.
        /// </summary>
        public int Depth
        {
            get
            {
                //return (Parent == null ? -1 : Parent.Depth) + 1;

                int depth = 0;
                TreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                    depth++;
                }
                return depth;
            }
        }

        public List<T> RouteNaar(TreeNode<T> node2)
        {
            if (this.Root != node2.Root)
                return null;

            List<T> nodes1 = this.Parents;
            nodes1.Insert(0, (T)this);
            List<T> nodes2 = node2.Parents;
            nodes2.Insert(0, (T)node2);

            TreeNode<T> sharedNode = nodes1.Intersect(nodes2).ToArray()[0];

            TreeNode<T> node = this;
            List<T> r1 = new List<T>();
            while (node != sharedNode)
            {
                r1.Add((T)node);
                node = node.Parent;
            }
            r1.Add((T)node);

            node = node2;
            List<T> r2 = new List<T>();
            while (node != sharedNode)
            {
                r2.Insert(0, (T)node);
                node = node.Parent;
            }
            r1.AddRange(r2);
            return r1;
        }

        public override string ToString()
        {
            string Description = "Depth=" + Depth.ToString() + ", Children=" + Children.Count.ToString();
            if (this == Root)
            {
                Description += " (Root)";
            }
            return Description;
        }

        #region IDisposable

        private bool _IsDisposed;
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        public virtual void Dispose()
        {
            CheckDisposed();

            // clean up contained objects (in Value property)
            if (DisposeTraversal == TreeTraversalDirection.BottomUp)
            {
                foreach (TreeNode<T> node in Children)
                {
                    node.Dispose();
                }
            }

            OnDisposing();

            if (DisposeTraversal == TreeTraversalDirection.TopDown)
            {
                foreach (TreeNode<T> node in Children)
                {
                    node.Dispose();
                }
            }

            // TODO: clean up the tree itself

            _IsDisposed = true;
        }

        public event EventHandler Disposing;

        protected void OnDisposing()
        {
            if (Disposing != null)
            {
                Disposing(this, EventArgs.Empty);
            }
        }

        protected void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        #endregion
    }

    public enum TreeTraversalType
    {
        DepthFirst,
        BreadthFirst
    }

    public enum TreeTraversalDirection
    {
        TopDown,
        BottomUp
    }
}