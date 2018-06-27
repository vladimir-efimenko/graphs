using System.Linq;
using SCG = System.Collections.Generic;
using System.Diagnostics;
using C5;

namespace Graphs
{
    public class GraphShortestPaths
    {
        private readonly VertexPriorityQueue _priorityQueue;
        private readonly IDictionary<int, (int from, int to)> _parents;
        private readonly IDictionary<int, double> _calcWeights;

        public GraphShortestPaths(int from, WeightedGraph graph)
        {
            From = from;
            Graph = graph;
            _parents = new HashDictionary<int, (int, int)>();
            _calcWeights = new HashDictionary<int, double>();
            _priorityQueue = new VertexPriorityQueue();
            InitQueue();
            BuildShortestPaths();
        }

        public int From { get; }

        public WeightedGraph Graph { get; }

        public SCG.ICollection<WeightedEdge> GetShortestPath(int to)
        {
            SCG.Stack<WeightedEdge> stack = new SCG.Stack<WeightedEdge>();

            if (_parents.Contains(to))
            {
                (int v, int u) = _parents[to];
                while (v != From || u != From)
                {
                    stack.Push(Graph.GetEdge(v, u));
                    (v, u) = _parents[v];
                }
            }

            return stack.ToList();
        }

        private void InitQueue()
        {
            _calcWeights.Add(From, 0);
            _parents.Add(From, (From, From));
            _priorityQueue.Insert(From, 0);

            foreach (WeightedEdge edge in Graph)
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
                int vertex = _priorityQueue.DelMin();
                Trace.WriteLine($"Vertex {vertex} processing: ");
                foreach (WeightedEdge adj in Graph.GetAdjacencyList(vertex))
                {
                    Debug.Assert(adj.From == vertex);
                    Trace.WriteLine($"Adjacent edge processing: {adj}");
                    double weight = _calcWeights[adj.To];
                    double newWeight = _calcWeights[adj.From] + adj.Weight;
                    Trace.WriteLine($"Current Weight: {weight}");
                    Trace.WriteLine($"Calculated Weight: {newWeight}");

                    if (weight > newWeight)
                    {
                        _calcWeights[adj.To] = newWeight;
                        _parents[adj.To] = (_parents[adj.From].to, adj.To);
                        if (_priorityQueue.Contains(adj.To))
                            _priorityQueue.Change(adj.To, newWeight);
                        else
                            _priorityQueue.Insert(adj.To, newWeight);
                    }
                }
            }
        }
    }
}
