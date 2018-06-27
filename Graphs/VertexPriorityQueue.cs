using C5;
using System;

namespace Graphs
{
    public class VertexPriorityQueue
    {
        private readonly IPriorityQueue<QueueItem> _priorityQueue = new IntervalHeap<QueueItem>();
        private readonly IDictionary<int, IPriorityQueueHandle<QueueItem>> _handles = new HashDictionary<int, IPriorityQueueHandle<QueueItem>>();

        public void Insert(int vertex, double weight)
        {
            IPriorityQueueHandle<QueueItem> h = null;
            _priorityQueue.Add(ref h, new QueueItem(vertex, weight));
            _handles.Add(vertex, h);
        }

        public void Change(int vertex, double weight)
        {
            _priorityQueue.Delete(_handles[vertex]);
            IPriorityQueueHandle<QueueItem> h = null;
            _priorityQueue.Add(ref h, new QueueItem(vertex, weight));
            _handles[vertex] = h;
        }

        public int DelMin()
        {
            return _priorityQueue.DeleteMin().To;
        }

        public bool Contains(int vertex)
        {
            return _priorityQueue.Exists(item => item.To == vertex);
        }

        public bool Empty => _priorityQueue.IsEmpty;

        struct QueueItem : IComparable<QueueItem>
        {
            public QueueItem(int to, double weight)
            {
                To = to;
                Weight = weight;
            }

            public int To { get; }
            public double Weight { get; }

            public int CompareTo(QueueItem other)
            {
                return Weight.CompareTo(other.Weight);
            }
        }
    }
}
