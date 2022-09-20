using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class BST<T> : IEnumerable<T> where T : IComparable<T>
    {
        Node root;
        Path order = Path.InOrder;
        public T Biggest
        {
            get
            {
                if (root == null) return default;
                var temp = root;
                while (temp.Right != null) temp = temp.Right;
                return temp.Value;
            }
        }
        public Path Order { get => order; set { order = value; } }
        public bool IsEmpty => root == null;
        public int Levels { get => GetLevels(root); }

        int GetLevels(Node temp)
        {
            if (temp == null) return 0;
            int lef = GetLevels(temp.Left);
            int rig = GetLevels(temp.Right);
            return Math.Max(lef, rig) + 1;
        }
        public void Add(T val)
        {
            if (IsEmpty)
            {
                root = new Node(val);
                return;
            }
            Node temp = root;
            while (true)
            {
                if (val.CompareTo(temp.Value) < 0)
                {
                    if (temp.Left == null)
                    {
                        temp.Left = new Node(val);
                        break;
                    }
                    temp = temp.Left;
                }
                else
                {
                    if (temp.Right == null)
                    {
                        temp.Right = new Node(val);
                        break;
                    }
                    temp = temp.Right;
                }
            }
        }
        public bool Search(T item, out T foundItem) => Search(root, item, out foundItem);
        bool Search(Node temp, T item, out T foundItem)
        {
            foundItem = default;
            if (temp == null) return false;
            if (item.CompareTo(temp.Value) == 0)
            {
                foundItem = temp.Value;
                return true;
            }
            else if (item.CompareTo(temp.Value) < 0) return Search(temp.Left, item, out foundItem);
            else return Search(temp.Right, item, out foundItem);
        }
        public ref T SearchRef(T item, out bool isFound) // O(LogN)
        {
            Node temp = root;
            Node notFound = new Node(default);
            if (temp == null)
            {
                isFound = false;
                return ref notFound.NotFound;
                // notFound.value;
            }
            while (temp != null)
            {
                if (item.CompareTo(temp.Value) == 0)
                {
                    isFound = true;
                    return ref temp.val;
                }
                if (item.CompareTo(temp.Value) < 0) temp = temp.Left;
                else temp = temp.Right;
            }
            isFound = false;
            return ref notFound.NotFound;
        }
        public bool Remove(T value)
        {
            root = Remove(root, value, out bool isRemoved);
            return isRemoved;
        }
        Node Remove(Node parent, T val, out bool isRemoved)
        {
            isRemoved = false;
            if (parent == null) return parent;

            if (parent.Value.CompareTo(val) > 0) parent.Left = Remove(parent.Left, val, out isRemoved);
            else if (parent.Value.CompareTo(val) < 0)
                parent.Right = Remove(parent.Right, val, out isRemoved);

            else
            {
                if (parent.Left == null)
                {
                    isRemoved = true;
                    return parent.Right;
                }
                else if (parent.Right == null)
                {
                    isRemoved = true;
                    return parent.Left;
                }
                parent.Value = Replacer(parent.Right);
                parent.Right = Remove(parent.Right, parent.Value, out isRemoved);
                //isRemoved = true; //TODO Check if need it
            }
            return parent;
        }
        T Replacer(Node parent)
        {
            if (parent.IsLeaf) return parent.Value;
            Node temp = parent;
            while (temp.Left != null)
            {
                parent = temp;
                temp = temp.Left;
            }
            parent.Left = null;
            return temp.Value;
        }
        public bool Replacer(T item, out T foundItem)
        {
            Node temp = root;
            while (temp != null)
            {
                if (item.CompareTo(temp.Value) == 0)
                {
                    foundItem = temp.Right.Value;
                    return true;
                }
                else if (item.CompareTo(temp.Value) < 0) temp = temp.Left;
                else temp = temp.Right;
            }
            foundItem = default;
            return false;
        }
        public T GetNextValue(T item) // O(LogN)
        {
            T sup = default;
            if (item.CompareTo(Biggest) > 0) return default;
            var temp = root;
            while (temp != null)
            {
                if (temp.Value.CompareTo(item) == 0)
                {
                    if (temp.Right is null) return sup;
                    temp = temp.Right;
                    while (temp.Left != null) temp = temp.Left;
                    return temp.Value;
                }
                else if (temp.Value.CompareTo(item) > 0)
                {
                    sup = temp.Value;
                    temp = temp.Left;
                }
                else temp = temp.Right;
            }
            return sup;
        }

        #region IEnumerable
        IEnumerable<T> GetEnumeratorInOrder(Node tempRoot)
        {
            if (tempRoot == null) yield break;
            if (tempRoot.Left != null)
            {
                foreach (T item in GetEnumeratorInOrder(tempRoot.Left))
                    yield return item;
            }
            yield return tempRoot.Value;
            if (tempRoot.Right != null)
            {
                foreach (T item in GetEnumeratorInOrder(tempRoot.Right))
                    yield return item;
            }
        }
        IEnumerable<T> GetEnumeratorPreOrder(Node tempRoot)
        {
            yield return tempRoot.Value;
            if (tempRoot.Left != null)
            {
                foreach (T item in GetEnumeratorPreOrder(tempRoot.Left))
                    yield return item;
            }
            if (tempRoot.Right != null)
            {
                foreach (T item in GetEnumeratorPreOrder(tempRoot.Right))
                    yield return item;
            }
        }
        IEnumerable<T> GetEnumeratorPostOrder(Node tempRoot)
        {
            if (tempRoot.Left != null)
            {
                foreach (T item in GetEnumeratorPostOrder(tempRoot.Left))
                    yield return item;
            }
            if (tempRoot.Right != null)
            {
                foreach (T item in GetEnumeratorPostOrder(tempRoot.Right))
                    yield return item;
            }
            yield return tempRoot.Value;
        }
        public IEnumerator<T> GetEnumerator()
        {
            switch (order)
            {
                case Path.PreOrder:
                    foreach (var item in GetEnumeratorPreOrder(root))
                        yield return item;
                    break;
                case Path.PostOrder:
                    foreach (var item in GetEnumeratorPostOrder(root))
                        yield return item;
                    break;
                case Path.InOrder:
                default:
                    foreach (var item in GetEnumeratorInOrder(root))
                        yield return item;
                    break;
            }
            yield break;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
        #region Scan
        public void Scan(Action<T> act)
        {
            switch (order)
            {
                case Path.PreOrder:
                    ScanPreOrder(root, act);
                    break;
                case Path.PostOrder:
                    ScanPostOrder(root, act);
                    break;
                case Path.InOrder:
                default:
                    ScanInOrder(root, act);
                    break;
            }
        }
        void ScanInOrder(Node temp, Action<T> act)
        {
            if (temp == null) return;
            ScanInOrder(temp.Left, act);
            act(temp.Value);
            ScanInOrder(temp.Right, act);
        }
        void ScanPreOrder(Node temp, Action<T> act)
        {
            if (temp == null) return;
            act(temp.Value);
            ScanPreOrder(temp.Left, act);
            ScanPreOrder(temp.Right, act);
        }
        void ScanPostOrder(Node temp, Action<T> act)
        {
            if (temp == null) return;
            ScanPostOrder(temp.Left, act);
            ScanPostOrder(temp.Right, act);
            act(temp.Value);
        }
        #endregion
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this)
                sb.AppendLine(item.ToString());
            return sb.ToString();
        }


        class Node
        {
            public T NotFound;
            internal T val;
            public T Value { get => val; set => val = value; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node(T value)
            {
                Value = value;
                Left = Right = null;
            }
            public bool IsLeaf => Left == null && Right == null;
        }
        public enum Path
        {
            InOrder,
            PreOrder,
            PostOrder,
        }
    }
}
