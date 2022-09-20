using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class DoubleLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        DoubleLinkedListNode<T> first;
        DoubleLinkedListNode<T> last;
        public DoubleLinkedListNode<T> Head { get => first; set => first = value; }
        public DoubleLinkedListNode<T> Tail { get => last; }
        public T First { get => first.Value; }
        public T Last { get => last.Value; }
        public int Count { get; private set; }

        public DoubleLinkedList() // O(1)
        {
            first = null;
            last = null;
            Count = 0;
        }
        public DoubleLinkedList(T val) => AddFirst(val); // O(1)
        public DoubleLinkedList(params T[] vals) // O(m)
        {
            for (int i = 0; i < vals.Length; i++) AddLast(vals[i]);
        }
        public DoubleLinkedList(DoubleLinkedList<T> prevList) // O(1)
        {
            first = prevList.first;
            last = prevList.last;
            Count = prevList.Count;
        }

        public void AddFirst(T val) // O(1)
        {
            if (first is null)
            {
                first = new DoubleLinkedListNode<T>(val);
                last = first;
                Count = 1;
            }
            else
            {
                var temp = new DoubleLinkedListNode<T>(val) { Next = first };
                first.Previous = temp;
                first = temp;
                Count++;
            }
        }
        public void AddFirst(DoubleLinkedListNode<T> node)
        {
            if (first is null)
            {
                first = node;
                last = first;
                Count = 1;
            }
            else
            {
                node.Next = first;
                first.Previous = node;
                first = node;
                Count++;
            }
        }
        public void AddLast(T val) // O(1)
        {
            if (last == null) AddFirst(val);
            else
            {
                var newLast = new DoubleLinkedListNode<T>(val);
                last.Next = newLast;
                newLast.Previous = last;
                last = newLast;
                Count++;
            }
        }
        public bool RemoveFirst() // O(1)
        {
            if (first == null) return false;
            if (first.Next == null)
            {
                first = null;
                last = null;
                Count = 0;
                return true;
            }
            first = first.Next;
            first.Previous = null;
            Count--;
            return true;
        }
        public bool RemoveLast() // O(1)
        {
            if (last == null) return false;
            if (last.Previous == null)
            {
                last = null;
                Count--;
                return true;
            }
            last = last.Previous;
            last.Next = null;
            Count--;
            return true;
        }
        public bool Remove(T val) // O(n)
        {
            if (first.Value.CompareTo(val) == 0)
            {
                RemoveFirst();
                return true;
            }
            else if (last.Value.CompareTo(val) == 0)
            {
                RemoveLast();
                return true;
            }
            var temp = first;
            while (temp.Next != null)
            {
                temp = temp.Next;
                if (temp.Value.CompareTo(val) == 0)
                {
                    temp.Previous = temp.Next;
                    //temp.Next.Previous = temp;
                    Count--;
                    return true;
                }
            }
            return false;
        }
        public void Remove(DoubleLinkedListNode<T> val)
        {
            if (val.Value.CompareTo(First) == 0) RemoveFirst();
            else if (val.Value.CompareTo(Last) == 0) RemoveLast();
            else val.Previous = val.Next;
        }

        public void ForEach(Action<T> act) // O(n)
        {
            var temp = first;
            while (temp != null)
            {
                act(temp.Value);
                temp = temp.Next;
            }
        }
        public T[] ToArray() // O(n)
        {
            var array = new T[Count];
            var temp = first;
            for (int i = 0; i < Count; i++)
            {
                array[i] = temp.Value;
                temp = temp.Next;
            }
            return array;
        }
        public LinkedList<T> ToLinkedList() // O(n)
        {
            var list = new LinkedList<T>();
            var temp = first;
            while (temp != null)
            {
                list.AddLast(temp.Value);
                temp = temp.Next;
            }
            return list;
        }
        public DoubleLinkedList<T> Reverse() // O(n)
        {
            var rev = new DoubleLinkedList<T>();
            ForEach(rev.AddFirst);
            return rev;
        }
        public override string ToString() // O(n)
        {
            var sb = new StringBuilder();
            var temp = first;
            while (temp != null)
            {
                sb.Append($"{temp.Value} ");
                temp = temp.Next;
            }
            return sb.ToString();
        }
        public void Clear()
        {
            if (first is null) return;
            first.Next = null;
            first = null;
            last.Previous = null;
            last = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator() // O(n)
        {
            var temp = first;
            while (temp != null)
            {
                yield return temp.Value;
                temp = temp.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
