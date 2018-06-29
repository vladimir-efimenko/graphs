using System;
using System.Collections;
using SCG = System.Collections.Generic;
using C5;
using System.Text;

namespace Graphs
{
    public abstract class WeightedGraph<T> : SCG.IEnumerable<WeightedEdge<T>> where T : IComparable<T>
    {
        /// <summary>
        /// Adjacent edges map.
        /// </summary>
        protected readonly IDictionary<T, HashSet<WeightedEdge<T>>> EdgeMap = new HashDictionary<T, HashSet<WeightedEdge<T>>>();

        /// <summary>
        /// Adds a new weighted edge.
        /// </summary>
        public abstract void Add(WeightedEdge<T> edge);

        public SCG.ICollection<WeightedEdge<T>> GetAdjacencyList(T vertex)
        {
            if (!EdgeMap.Contains(vertex))
                return new ArrayList<WeightedEdge<T>>();
            return EdgeMap[vertex];
        }

        public WeightedEdge<T> GetEdge(T from, T to)
        {
            if (EdgeMap.Contains(from))
            {
                foreach (WeightedEdge<T> edge in EdgeMap[from])
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
            foreach (T from in EdgeMap.Keys)
            {
                res.Append($"{from}: ");
                foreach (WeightedEdge<T> edge in EdgeMap[from])
                {
                    res.Append($"{edge.To} - {edge.Weight} ");
                }
                res.AppendLine();
            }
            return res.ToString();
        }

        public SCG.IEnumerator<WeightedEdge<T>> GetEnumerator()
        {
            foreach (T from in EdgeMap.Keys)
            {
                foreach (WeightedEdge<T> edge in EdgeMap[from])
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
