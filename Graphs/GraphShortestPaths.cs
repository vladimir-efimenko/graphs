using System;
using System.Linq;
using SCG = System.Collections.Generic;
using System.Diagnostics;
using C5;

namespace Graphs
{
    public class GraphShortestPaths<T> where T : IComparable<T>
    {
        private readonly PriorityQueue<T> _priorityQueue;
        private readonly IDictionary<T, WeightedEdge<T>> _parents;
        private readonly IDictionary<T, double> _calcWeights;

        public GraphShortestPaths(T from, WeightedGraph<T> graph)
        {
            From = from;
            Graph = graph;
            _parents = new HashDictionary<T, WeightedEdge<T>>();
            _calcWeights = new HashDictionary<T, double>();
            _priorityQueue = new PriorityQueue<T>();
            InitQueue();
            BuildShortestPaths();
        }

        public T From { get; }

        public WeightedGraph<T> Graph { get; }

        public SCG.ICollection<WeightedEdge<T>> GetShortestPath(T to)
        {
            SCG.Stack<WeightedEdge<T>> stack = new SCG.Stack<WeightedEdge<T>>();

            if (_parents.Contains(to))
            {
                WeightedEdge<T> edge = _parents[to];
                while (edge != WeightedEdge<T>.None)
                {
                    stack.Push(edge);
                    edge = _parents[edge.From];
                }
            }

            return stack.ToList();
        }

        private void InitQueue()
        {
            _calcWeights.Add(From, 0);
            _parents.Add(From, WeightedEdge<T>.None);
            _priorityQueue.Add(From, 0);

            foreach (WeightedEdge<T> edge in Graph)
            {
                if (!_calcWeights.Contains(edge.To))
                    _calcWeights.Add(edge.To, double.PositiveInfinity);
                if (!_calcWeights.Contains(edge.From))
                    _calcWeights.Add(edge.From, double.PositiveInfinity);
            }
        }

        private void BuildShortestPaths()
        {
            while (!_priorityQueue.Empty)
            {
                T vertex = _priorityQueue.DeleteMin();
                Trace.WriteLine($"Vertex {vertex} processing: ");
                foreach (WeightedEdge<T> adj in Graph.GetAdjacencyList(vertex))
                {
                    Debug.Assert(adj.From.Equals(vertex));
                    Trace.WriteLine($"Adjacent edge processing: {adj}");
                    double weight = _calcWeights[adj.To];
                    double newWeight = _calcWeights[adj.From] + adj.Weight;
                    Trace.WriteLine($"Current Weight: {weight}");
                    Trace.WriteLine($"Calculated Weight: {newWeight}");

                    if (weight > newWeight)
                    {
                        _calcWeights[adj.To] = newWeight;
                        _parents[adj.To] = adj;
                        if (_priorityQueue.Contains(adj.To))
                            _priorityQueue.Change(adj.To, newWeight);
                        else
                            _priorityQueue.Add(adj.To, newWeight);
                    }
                }
            }
        }
    }
}
