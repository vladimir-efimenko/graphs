using System;
using SCG = System.Collections.Generic;
using C5;

namespace Graphs.Algorithms
{
    public class DepthFirstPaths<T> where T : IComparable<T>
    {
        private readonly HashSet<T> _visited;
        private readonly IDictionary<T, WeightedEdge<T>> _parents;
        private readonly WeightedGraph<T> _graph;

        public DepthFirstPaths(WeightedGraph<T> graph, T source)
        {
            _visited = new HashSet<T>();
            _parents = new HashDictionary<T, WeightedEdge<T>>();
            _graph = graph;
            Visit(source);
        }

        private void Visit(T v)
        {
            _visited.Add(v);
            foreach(WeightedEdge<T> edge in _graph.GetAdjacencyList(v))
            {
                if (!_visited.Contains(edge.To))
                {
                    _parents.Add(edge.To, edge);
                    Visit(edge.To);
                }
            }
        }

        public SCG.ICollection<WeightedEdge<T>> GetPathTo(T v)
        {
            LinkedList<WeightedEdge<T>> stack = new LinkedList<WeightedEdge<T>>();

            if (_parents.Contains(v))
            {
                WeightedEdge<T> parent = _parents[v];
                while (true)
                {
                    stack.InsertFirst(parent);
                    if (!_parents.Contains(parent.From))
                        break;
                    parent = _parents[parent.From];
                }
            }

            return stack;
        }
    }
}
