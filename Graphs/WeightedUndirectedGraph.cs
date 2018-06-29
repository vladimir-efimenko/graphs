using System;
using C5;

namespace Graphs
{
    public class WeightedUndirectedGraph<T> : WeightedGraph<T> where T : IComparable<T>
    {
        public override void Add(WeightedEdge<T> edge)
        {
            Add(edge, -edge.Weight);
        }

        public void Add(WeightedEdge<T> edge, double backwardWeight)
        {
            if (!EdgeMap.Contains(edge.From))
            {
                EdgeMap.Add(edge.From, new HashSet<WeightedEdge<T>>());
            }
            EdgeMap[edge.From].Add(edge);

            if (!EdgeMap.Contains(edge.To))
            {
                EdgeMap.Add(edge.To, new HashSet<WeightedEdge<T>>());
            }
            EdgeMap[edge.To].Add(new WeightedEdge<T>(edge.To, edge.From, backwardWeight));
        }
    }
}
