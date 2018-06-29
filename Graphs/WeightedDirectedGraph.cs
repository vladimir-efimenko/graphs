﻿using System;
using C5;
using System.Linq;

namespace Graphs
{
    /// <summary>
    /// Represents weighted oriented graph.
    /// <typeparam name="T">A type of edge label.</typeparam>
    /// </summary>
    public class WeightedDirectedGraph<T> : WeightedGraph<T> where T : IComparable<T>
    {
        /// <summary>
        /// Adds a new weighted edge.
        /// </summary>
        public void Add(T from, T to, double weight)
        {
            if (!EdgeMap.Contains(from))
            {
                EdgeMap.Add(from, new HashSet<WeightedEdge<T>>());
            }
            EdgeMap[from].Add(new WeightedEdge<T>(from, to, weight));
        }

        /// <summary>
        /// Adds a new weighted edge.
        /// </summary>
        public override void Add(WeightedEdge<T> edge)
        {
            Add(edge.From, edge.To, edge.Weight);
        }

        public double GetWeight()
        {
            return this.Sum(edge => edge.Weight);
        }

    }
}