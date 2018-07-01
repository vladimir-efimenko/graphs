using System;
using System.Collections;
using SCG = System.Collections.Generic;
using C5;
using System.Text;

namespace Graphs
{
    /// <summary>
    /// Represents weighted undirected graph.
    /// <typeparam name="T">A type of edge label.</typeparam>
    /// </summary>
    public class WeightedGraph<T> : SCG.IEnumerable<WeightedEdge<T>> where T : IComparable<T>
    {
        /// <summary>
        /// Adjacent edges map.
        /// </summary>
        protected readonly IDictionary<T, HashSet<WeightedEdge<T>>> EdgeMap = new HashDictionary<T, HashSet<WeightedEdge<T>>>();

        /// <summary>
        /// Adds a new weighted edge.
        /// </summary>
        public virtual void Add(WeightedEdge<T> edge)
        {
            if(!EdgeMap.Contains(edge.From))
            {
                EdgeMap.Add(edge.From, new HashSet<WeightedEdge<T>>());
            }
            EdgeMap[edge.From].Add(edge);

            if (!EdgeMap.Contains(edge.To))
            {
                EdgeMap.Add(edge.To, new HashSet<WeightedEdge<T>>());
            }
            EdgeMap[edge.To].Add(new WeightedEdge<T>(edge.To, edge.From, -edge.Weight));
        }

        public void Add((T from, T to) edge)
        {
            Add(new WeightedEdge<T>(edge.from, edge.to));
        }

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
            else if (EdgeMap.Contains(to))
            {
                foreach (WeightedEdge<T> edge in EdgeMap[to])
                {
                    if (edge.From.Equals(from))
                        return edge;
                }
            }
            return WeightedEdge<T>.None;
        }

        public SCG.ICollection<T> GetVertices()
        {
            HashSet<T> vertices = new HashSet<T>();
            foreach(WeightedEdge<T> edge in this)
            {
                vertices.Add(edge.From);
                vertices.Add(edge.To);
            }

            return vertices;
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
