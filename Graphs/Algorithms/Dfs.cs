using System;
using System.Linq;
using C5;

namespace Graphs.Algorithms
{
    public class Dfs<T> where T : IComparable<T>
    {
        private readonly WeightedGraph<T> _graph;
        private readonly Action<WeightedEdge<T>> _traverseAction;
        private readonly IDictionary<T, bool> _visited;

        public Dfs(WeightedGraph<T> graph, Action<WeightedEdge<T>> traverseAction)
        {
            _graph = graph;
            _traverseAction = traverseAction;
            _visited = new HashDictionary<T, bool>();
            Init();
        }

        private void Init()
        {
            foreach (WeightedEdge<T> edge in _graph)
            {
                if(!_visited.Contains(edge.From))
                    _visited.Add(edge.From, false);
                if(!_visited.Contains(edge.To))
                    _visited.Add(edge.To, false);
            }
        }


        /// <summary>
        /// Traverse a graph in DFS order.
        /// </summary>
        public void Traverse()
        {
            foreach (T vertex in _visited.Keys.ToList())
            {
                if (!_visited[vertex])
                    Visit(vertex);
            }
        }

        private void Visit(T vertex)
        {
            _visited[vertex] = true;
            foreach (WeightedEdge<T> edge in _graph.GetAdjacencyList(vertex))
            {
                _traverseAction(edge);
                if (!_visited[edge.To])
                {
                    Visit(edge.To);
                }
            }
        }
    }
}
