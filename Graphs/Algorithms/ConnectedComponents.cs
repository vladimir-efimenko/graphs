using System;
using System.Linq;
using C5;

namespace Graphs.Algorithms
{
    public class ConnectedComponents<T> where T : IComparable<T>
    {
        private readonly WeightedGraph<T> _graph;
        private readonly IDictionary<T, HashSet<T>> _components;
        private readonly IDictionary<T, bool> _visited;

        public ConnectedComponents(WeightedGraph<T> graph)
        {
            _graph = graph;
            _components = new HashDictionary<T, HashSet<T>>();
            _visited = new HashDictionary<T, bool>();
            Init();

            foreach (T vertex in _visited.Keys.ToList())
            {
                if (!_visited[vertex])
                {
                    _components.Add(vertex, new HashSet<T>());
                    Visit(vertex, vertex);
                }
            }
        }

        private void Init()
        {
            foreach (WeightedEdge<T> edge in _graph)
            {
                if (!_visited.Contains(edge.From))
                    _visited.Add(edge.From, false);
                if (!_visited.Contains(edge.To))
                    _visited.Add(edge.To, false);
            }
        }

        private void Visit(T vertex, T component)
        {
            _visited[vertex] = true;
            foreach (WeightedEdge<T> edge in _graph.GetAdjacencyList(vertex))
            {
                if (!_visited[edge.To])
                {
                    _components[component].Add(edge.To);
                    Visit(edge.To, component);
                }
            }
        }

        public int Count()
        {
            return _components.Count;
        }
    }
}
