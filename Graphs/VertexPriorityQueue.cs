using C5;
using System;

namespace Graphs
{
    public class VertexPriorityQueue<T> where T : IComparable<T>
    {
        private readonly IPriorityQueue<QueueItem> _priorityQueue = new IntervalHeap<QueueItem>();
        private readonly IDictionary<T, IPriorityQueueHandle<QueueItem>> _handles = new HashDictionary<T, IPriorityQueueHandle<QueueItem>>();

        public void Insert(T vertex, double weight)
        {
            IPriorityQueueHandle<QueueItem> h = null;
            _priorityQueue.Add(ref h, new QueueItem(vertex, weight));
            _handles.Add(vertex, h);
        }

        public void Change(T vertex, double weight)
        {
            _priorityQueue.Delete(_handles[vertex]);
            IPriorityQueueHandle<QueueItem> h = null;
            _priorityQueue.Add(ref h, new QueueItem(vertex, weight));
            _handles[vertex] = h;
        }

        public T DelMin()
        {
            return _priorityQueue.DeleteMin().To;
        }

        public bool Contains(T vertex)
        {
            return _priorityQueue.Exists(item => item.To.Equals(vertex));
        }

        public bool Empty => _priorityQueue.IsEmpty;

        struct QueueItem : IComparable<QueueItem>
        {
            public QueueItem(T to, double weight)
            {
                To = to;
                Weight = weight;
            }

            public T To { get; }
            public double Weight { get; }

            public int CompareTo(QueueItem other)
            {
                return Weight.CompareTo(other.Weight);
            }
        }
    }
}
