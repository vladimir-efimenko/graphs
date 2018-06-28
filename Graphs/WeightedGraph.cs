using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Graphs
{
    /// <summary>
    /// Represents weighted oriented graph.
    /// </summary>
    public class WeightedGraph<T> : IEnumerable<WeightedEdge<T>> where T : IComparable<T>
    {
        /// <summary>
        /// Adjacent edges map.
        /// </summary>
        private readonly IDictionary<T, HashSet<WeightedEdge<T>>> _edgeMap = new SortedDictionary<T, HashSet<WeightedEdge<T>>>();

        public void AddEdge(T from, T to, double weight)
        {
            if (!_edgeMap.ContainsKey(from))
            {
                _edgeMap.Add(from, new HashSet<WeightedEdge<T>>());
            }
            _edgeMap[from].Add(new WeightedEdge<T>(from, to, weight));
        }

        public void AddEdge(WeightedEdge<T> edge)
        {
            AddEdge(edge.From, edge.To, edge.Weight);
        }

        public double GetWeight()
        {
            return this.Sum(edge => edge.Weight);
        }

        public ICollection<WeightedEdge<T>> GetAdjacencyList(T vertex)
        {
            if (!_edgeMap.ContainsKey(vertex))
                return new List<WeightedEdge<T>>();
            return _edgeMap[vertex];
        }

        public WeightedEdge<T> GetEdge(T from, T to)
        {
            if (_edgeMap.ContainsKey(from))
            {
                foreach (WeightedEdge<T> edge in _edgeMap[from])
                {
                    if (edge.To.Equals(to))
                        return edge;
                }
            }
            return WeightedEdge<T>.None;
        }
             
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            foreach(T from in _edgeMap.Keys)
            {
                res.Append($"{from}: "); 
                foreach(WeightedEdge<T> edge in _edgeMap[from])
                {
                    res.Append($"{edge.To} - {edge.Weight} ");
                }
                res.AppendLine();
            }
            return res.ToString();
        }

        public IEnumerator<WeightedEdge<T>> GetEnumerator()
        {
            foreach (T from in _edgeMap.Keys)
            {
                foreach (WeightedEdge<T> edge in _edgeMap[from])
                {
                    yield return edge;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
