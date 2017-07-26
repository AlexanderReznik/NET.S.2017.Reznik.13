using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollections
{
    public sealed class Queue<T> : IEnumerable<T>
    {
        #region Constant

        private const int StartCapacity = 10;

        #endregion

        #region Properties
        /// <summary>
        /// Number of elements in queue
        /// </summary>
        public int Size { get; private set; }

        private T[] Array { get; set; }
        private int Capacity { get; set; }

        private int Head { get; set; }
        private int Tail { get; set; }

        #endregion

        #region C-tors
        /// <summary>
        /// Creates a queue with room for capacity objects
        /// </summary>
        /// <param name="capacity">number of elements</param>
        public Queue(int capacity)
        {
            if(capacity < 1) throw new ArgumentException($"{nameof(capacity)} must be positive.");
            Size = 0;
            Capacity = capacity;
            Array = new T[Capacity];
            Head = 0;
            Tail = 0;
        }

        /// <summary>
        /// Creates a queue with default capacity
        /// </summary>
        public Queue() : this(StartCapacity) { }

        #endregion

        #region Methods
        /// <summary>
        /// Adds element in tail
        /// </summary>
        /// <param name="elem">Neposredstvenno sam element</param>
        public void Enqueue(T elem)
        {
            if (Size == Capacity)
            {
                int newcapacity = Capacity * 2;
                SetCapacity(newcapacity);
            }

            Array[Tail] = elem;
            Tail = (Tail + 1) % Array.Length;
            Size++;
        }

        /// <summary>
        /// Removes element from the head of the queue
        /// </summary>
        /// <returns>removed element</returns>
        public T Dequeue()
        {
            if(Size == 0) throw new InvalidOperationException("Queue is empty.");

            T removed = Array[Head];
            Array[Head] = default(T);
            Head = (Head + 1) % Array.Length;
            Size--;
            return removed;
        }

        /// <summary>
        /// Lets you look at the head element without removing
        /// </summary>
        /// <returns>Element to look</returns>
        public T Peek()
        {
            if(Size == 0) throw new InvalidOperationException("Queue is empty.");
            return Array[Head];
        }

        #endregion

        #region Iterator

        /// <summary>
        /// Getting iterator
        /// </summary>
        /// <returns>iterator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new QueueIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private struct QueueIterator : IEnumerator<T>
        {
            private readonly Queue<T> collection;
            private int currentIndex;

            public QueueIterator(Queue<T> collection)
            {
                this.collection = collection;
                currentIndex = collection.Head - 1;
            }

            public T Current
            {
                get
                {
                    if (collection.Head < collection.Tail)
                    {
                        if (currentIndex < collection.Head || currentIndex >= collection.Tail)
                            throw new InvalidOperationException();
                    }
                    else
                    {
                        if(currentIndex >= collection.Tail && currentIndex < collection.Head)
                            throw new InvalidOperationException();
                    }
                    return collection.Array[currentIndex];
                }
            }

            object System.Collections.IEnumerator.Current => Current;

            public void Reset()
            {
                currentIndex = collection.Head - 1;
            }

            public bool MoveNext()
            {
                currentIndex = (currentIndex + 1) % collection.Array.Length;
                return currentIndex != collection.Tail;
            }

            public void Dispose() { }
        }

        #endregion

        private void SetCapacity(int capacity)
        {
            T[] newarray = new T[capacity];
            if (Size > 0)
            {
                if (Head < Tail)
                {
                    System.Array.Copy(Array, Head, newarray, 0, Size);
                }
                else
                {
                    System.Array.Copy(Array, Head, newarray, 0, Array.Length - Head);
                    System.Array.Copy(Array, 0, newarray, Array.Length - Head, Tail);
                }
            }

            Array = newarray;
            Head = 0;
            Tail = (Size == capacity) ? 0 : Size;
        }
    }
}
