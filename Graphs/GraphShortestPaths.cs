using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using C5;

namespace Graphs
{
    public class GraphShortestPaths
    {
        private readonly IPriorityQueue<WeightedEdge> _priorityQueue;
        private readonly C5.IDictionary<(int v, int u), IPriorityQueueHandle<WeightedEdge>> _handleMap;
        private readonly C5.IDictionary<int, (int v, int u)> _parents;
        private readonly C5.IDictionary<int, double> _calcWeights;
        private readonly C5.ICollection<int> _visited;

        public GraphShortestPaths(int from, WeightedGraph graph)
        {
            From = from;
            Graph = graph;
            _parents = new HashDictionary<int, (int, int)>();
            _visited = new C5.HashSet<int>();
            _calcWeights = new HashDictionary<int, double>();
            _priorityQueue = new IntervalHeap<WeightedEdge>();
            _handleMap = new HashDictionary<(int v, int u), IPriorityQueueHandle<WeightedEdge>>();
            InitQueue();
            BuildShortestPaths();
        }

        public int From { get; }

        public WeightedGraph Graph { get; }

        public IEnumerable<WeightedEdge> GetShortestPath(int to)
        {
            if (!_parents.Contains(to))
                yield break;

            (int v, int u) = _parents[to];
            while (true)
            {
                if( v == From && u == From) 
                    yield break;

                yield return Graph.GetEdge(v, u);
                (v, u) = _parents[v];
            }
        }

        private void InitQueue()
        {
            _calcWeights.Add(From, 0);
            _parents.Add(From, (From, From));

            foreach(WeightedEdge edge in Graph)
            {
                WeightedEdge infiniteEdge = new WeightedEdge(edge.From, edge.To, double.PositiveInfinity);
                IPriorityQueueHandle<WeightedEdge> h = null;
                _priorityQueue.Add(ref h, infiniteEdge);
                _handleMap.Add((edge.From, edge.To), h);

                if(!_calcWeights.Contains(edge.To))
                    _calcWeights.Add(edge.To, double.PositiveInfinity);
                if(!_calcWeights.Contains(edge.From))
                    _calcWeights.Add(edge.From, double.PositiveInfinity);
                if(!_parents.Contains(edge.To))
                    _parents.Add(edge.To, (-1, -1));
                if(!_parents.Contains(edge.From))
                    _parents.Add(edge.From, (-1, -1));
            }
        }

        private void BuildShortestPaths()
        {
            while (!_priorityQueue.IsEmpty)
            {
                WeightedEdge minEdge = _priorityQueue.FindMin(out IPriorityQueueHandle<WeightedEdge> h);

                _visited.Add(minEdge.From);

                foreach (WeightedEdge adj in Graph.GetAdjacencyList(minEdge.From))
                {
                    Debug.Assert(adj.From == minEdge.From);

                    if (!_visited.Contains(adj.To))
                    {
                        double weight = _calcWeights[adj.To];
                        double newWeight = _calcWeights[adj.From] + adj.Weight;
                        if (weight > newWeight)
                        {
                            _calcWeights[adj.To] = newWeight;
                            _parents[adj.To] = (_parents[adj.From].u, adj.To);
                            _priorityQueue.Replace(_handleMap[(adj.From, adj.To)], new WeightedEdge(adj.From, adj.To, newWeight));
                        }
                    }
                }

                _priorityQueue.Delete(h);
            }    
        }
    }
}
