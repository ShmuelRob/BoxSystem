using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class LinkedList<T> : IEnumerable<T>, ICollection<T>
    {
        Node first;
        Node last;
        public T First { get => first.value; }
        public T Last { get => last.value; }
        public int Count { get; private set; }
        public bool IsEmpty { get => first == null; }

        public bool IsReadOnly => false;
        
        public LinkedList() // O(1)
        {
            first = null;
            last = null;
        }

        //public LinkedList(T val) => AddFirst(val); // O(1)
        //public LinkedList(params T[] vals) // O(m)
        //{
        //    for (int i = 0; i < vals.Length; i++) AddLast(vals[i]);
        //}
        //public LinkedList(LinkedList<T> prevList) // O(1)
        //{
        //    first = prevList.first;
        //    last = prevList.last;
        //}

        public void AddFirst(T val) // O(1)
        {
            if (last == null)
            {
                first = new Node(val);
                last = first;

            }
            else
            {
                var temp = new Node(val);
                temp.next = first;
                first = temp;
            }
            Count++;
        }
        public void AddLast(T val) // O(1)
        {
            if (last == null) AddFirst(val);
            else
            {
                Node newLast = new Node(val);
                last.next = newLast;
                last = newLast;
                Count++;
            }
        }
        public bool RemoveFirst(out T data) // O(1)
        {
            data = default;
            if (first == null) return false;
            data = first.value;
            first = first.next;
            Count--;
            return true;
        }
        public bool RemoveLast(out T data) // O(n)
        {
            data = default;
            if (first == null) return false;
            if (first == last) return RemoveFirst(out data);
            if (first.next == last)
            {
                data = last.value;
                first.next = null;
                Count--;
                return true;
            }
            Node temp = first;
            while (temp.next != last) temp = temp.next;
            data = temp.next.value;
            temp.next = null;
            Count--;
            return true;
        }

        public void ForEach(Action<T> act) // O(n)
        {
            Node temp = first;
            while (temp != null)
            {
                act(temp.value);
                temp = temp.next;
            }
        }
        public T[] ToArray() // O(n)
        {
            T[] array = new T[Count];
            var temp = first;
            for (int i = 0; i < Count; i++)
            {
                array[i] = temp.value;
                temp = temp.next;
            }
            return array;
        }
        public void Clear()
        {
            first.next = null;
            first = null;
            last = null;
        }

        public override string ToString() // O(n)
        {
            StringBuilder sb = new StringBuilder();
            var temp = first;
            while (temp != null)
            {
                sb.Append($"{temp.value} ");
                temp = temp.next;
            }
            return sb.ToString();
        }
        public LinkedList<T> Reverse() // O(n)
        {
            LinkedList<T> rev = new LinkedList<T>();
            ForEach(rev.AddFirst);
            return rev;
        }

        public IEnumerator<T> GetEnumerator() // O(n)
        {
            Node temp = first;
            while (temp != null)
            {
                yield return temp.value;
                temp = temp.next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T item) => AddLast(item);


        public bool Contains(T item)
        {
            var temp = first;
            while (temp != null)
            {
                if (temp.value.Equals(item)) return true;
                temp = temp.next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            return;
        }

        public bool Remove(T item)
        {
            var temp = first;
            if (temp.value.Equals(item)) RemoveFirst(out _);
            while (temp.next != null)
            {
                if (temp.next.value.Equals(item))
                {
                    temp.next = temp.next.next;
                    return true;
                }
                temp = temp.next;
            }
            return false;
        }

        internal class Node
        {
            internal T value;
            internal Node next;
            public Node(T val) // O(1)
            {
                value = val;
                next = null;
            }
        }
    }
}
