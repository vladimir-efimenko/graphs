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
    public class WeightedGraph : IEnumerable<WeightedEdge>
    {
        /// <summary>
        /// Adjacent edges map.
        /// </summary>
        private readonly IDictionary<int, HashSet<WeightedEdge>> _edgeMap = new SortedDictionary<int, HashSet<WeightedEdge>>();

        public void AddEdge(int from, int to, double weight)
        {
            if (!_edgeMap.ContainsKey(from))
            {
                _edgeMap.Add(from, new HashSet<WeightedEdge>());
            }
            _edgeMap[from].Add(new WeightedEdge(from, to, weight));
        }

        public void AddEdge(WeightedEdge edge)
        {
            AddEdge(edge.From, edge.To, edge.Weight);
        }

        public double GetWeight()
        {
            return this.Sum(edge => edge.Weight);
        }

        public ICollection<WeightedEdge> GetAdjacencyList(int vertex)
        {
            if (!_edgeMap.ContainsKey(vertex))
                return new List<WeightedEdge>();
            return _edgeMap[vertex];
        }

        public WeightedEdge GetEdge(int from, int to)
        {
            if (_edgeMap.ContainsKey(from))
            {
                foreach (WeightedEdge edge in _edgeMap[from])
                {
                    if (edge.To == to)
                        return edge;
                }
            }
            return WeightedEdge.None;
        }
             
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            foreach(int from in _edgeMap.Keys)
            {
                res.Append($"{from}: "); 
                foreach(WeightedEdge edge in _edgeMap[from])
                {
                    res.Append($"{edge.To} - {edge.Weight} ");
                }
                res.AppendLine();
            }
            return res.ToString();
        }

        public IEnumerator<WeightedEdge> GetEnumerator()
        {
            foreach (int from in _edgeMap.Keys)
            {
                foreach (WeightedEdge edge in _edgeMap[from])
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
