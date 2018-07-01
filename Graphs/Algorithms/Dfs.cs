using System;
using C5;

namespace Graphs.Algorithms
{
    public class Dfs<T> where T : IComparable<T>
    {
        private readonly WeightedGraph<T> _graph;
        private readonly HashSet<T> _visited;

        public Dfs(WeightedGraph<T> graph, Action<WeightedEdge<T>> traverseAction = null)
        {
            _graph = graph;
            TraverseAction = traverseAction;
            _visited = new HashSet<T>();
            Traverse();
        }

        public Action<WeightedEdge<T>> TraverseAction { get; set; }

        /// <summary>
        /// Traverse a graph in DFS order.
        /// </summary>
        public void Traverse()
        {
            foreach (T vertex in _graph.GetVertices())
            {
                if (!_visited.Contains(vertex))
                    Visit(vertex);
            }
        }

        private void Visit(T vertex)
        {
            _visited.Add(vertex);
            foreach (WeightedEdge<T> edge in _graph.GetAdjacencyList(vertex))
            {
                TraverseAction?.Invoke(edge);
                if (!_visited.Contains(edge.To))
                {
                    Visit(edge.To);
                }
            }
        }
    }
}
