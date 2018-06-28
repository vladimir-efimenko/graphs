using C5;
using System;

namespace Graphs
{
    /// <summary>
    /// Convenient wrapper around <see cref="C5.IntervalHeap{T}"/>.
    /// </summary>
    public class VertexPriorityQueue<T> where T : IComparable<T>
    {
        private readonly IPriorityQueue<QueueItem> _priorityQueue = new IntervalHeap<QueueItem>();
        private readonly IDictionary<T, IPriorityQueueHandle<QueueItem>> _handles = new HashDictionary<T, IPriorityQueueHandle<QueueItem>>();

        public void Add(T vertex, double weight)
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

        public T DeleteMin()
        {
            return _priorityQueue.DeleteMin().Vertex;
        }

        public bool Contains(T vertex)
        {
            return _handles.Contains(vertex) && _priorityQueue.Find(_handles[vertex], out _);
        }

        public bool Empty => _priorityQueue.IsEmpty;

        private struct QueueItem : IComparable<QueueItem>
        {
            public QueueItem(T vertex, double weight)
            {
                Vertex = vertex;
                Weight = weight;
            }

            public T Vertex { get; }
            public double Weight { get; }

            public int CompareTo(QueueItem other)
            {
                return Weight.CompareTo(other.Weight);
            }
        }
    }
}
