using System;
using Graphs;
using Graphs.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphsUnitTests.Algorithms
{
    [TestClass]
    public class DfsTests
    {
        [TestMethod]
        public void CanSumDirectedGraphWeightsCorrectlyWhenTraverseInDfsOrder()
        {
            WeightedDirectedGraph<int> directedGraph = new WeightedDirectedGraph<int>
            {
                new WeightedEdge<int>(2, 3, 9),
                new WeightedEdge<int>(1, 2, 10),
                new WeightedEdge<int>(1, 3, 1),
                new WeightedEdge<int>(3, 4, 8),
                new WeightedEdge<int>(2, 5, 7),
                new WeightedEdge<int>(0, 5, 5)
            };
            double weight = 0;

            Dfs<int> dfs = new Dfs<int>(directedGraph, edge => weight += edge.Weight);
            dfs.Traverse();

            Assert.AreEqual(directedGraph.GetWeight(), weight);
        }

        [TestMethod]
        public void CanSumUndirectedGraphWeightsCorrectlyWhenTraverseInDfsOrder()
        {
            WeightedUndirectedGraph<int> graph = new WeightedUndirectedGraph<int>
            {
                new WeightedEdge<int>(2, 3, 9),
                new WeightedEdge<int>(1, 2, 10),
                new WeightedEdge<int>(1, 3, 1),
                new WeightedEdge<int>(3, 4, 8),
                new WeightedEdge<int>(2, 5, 7),
                new WeightedEdge<int>(0, 5, 5)
            };
            double weight = 0;

            Dfs<int> dfs = new Dfs<int>(graph, edge =>
            {
                weight += edge.Weight;
                Console.WriteLine(edge);
            });
            dfs.Traverse();

            Assert.AreEqual(0, weight);
        }
    }
}
