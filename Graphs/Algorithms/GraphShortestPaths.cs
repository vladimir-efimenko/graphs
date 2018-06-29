using System;
using System.Diagnostics;
using C5;
using SCG = System.Collections.Generic;

namespace Graphs.Algorithms
{
    public class GraphShortestPaths<T> where T : IComparable<T>
    {
        private readonly PriorityQueue<T> _priorityQueue;
        private readonly IDictionary<T, WeightedEdge<T>> _parents;
        private readonly IDictionary<T, double> _calcWeights;

        public GraphShortestPaths(T from, WeightedDirectedGraph<T> directedGraph)
        {
            From = from;
            DirectedGraph = directedGraph;
            _parents = new HashDictionary<T, WeightedEdge<T>>();
            _calcWeights = new HashDictionary<T, double>();
            _priorityQueue = new PriorityQueue<T>();
            InitQueue();
            BuildShortestPaths();
        }

        public T From { get; }

        public WeightedDirectedGraph<T> DirectedGraph { get; }

        public SCG.ICollection<WeightedEdge<T>> GetShortestPath(T to)
        {
            LinkedList<WeightedEdge<T>> stack = new LinkedList<WeightedEdge<T>>();

            if (_parents.Contains(to))
            {
                WeightedEdge<T> edge = _parents[to];
                while (edge != WeightedEdge<T>.None)
                {
                    stack.InsertFirst(edge);
                    edge = _parents[edge.From];
                }
            }

            return stack;
        }

        private void InitQueue()
        {
            _calcWeights.Add(From, 0);
            _parents.Add(From, WeightedEdge<T>.None);
            _priorityQueue.Add(From, 0);

            foreach (WeightedEdge<T> edge in DirectedGraph)
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
                foreach (WeightedEdge<T> adj in DirectedGraph.GetAdjacencyList(vertex))
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
