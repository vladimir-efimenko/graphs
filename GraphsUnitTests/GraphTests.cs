using System.Collections.Generic;
using Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphsUnitTests
{
    [TestClass]
    public class GraphTests
    {
        private WeightedGraph<int> GetTestGraph()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>();

            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 5);
            graph.AddEdge(0, 3, 3);
            graph.AddEdge(1, 2, 7);

            return graph;
        }

        [TestMethod]
        public void GetWeightTest()
        {
            WeightedGraph<int> graph = GetTestGraph();

            Assert.AreEqual(25, graph.GetWeight());
        }

        [TestMethod]
        public void GetAdjacencyListTest()
        {
            WeightedGraph<int> graph = GetTestGraph();

            ICollection<WeightedEdge<int>> adjacencyList = graph.GetAdjacencyList(0);

            Assert.AreEqual(3, adjacencyList.Count, "Adjacency list count");
            Assert.IsTrue(adjacencyList.Contains(new WeightedEdge<int>(0, 1, 10)));
            Assert.IsTrue(adjacencyList.Contains(new WeightedEdge<int>(0, 2, 5)));
            Assert.IsTrue(adjacencyList.Contains(new WeightedEdge<int>(0, 3, 3)));
        }

        [TestMethod]
        public void GetEdgeTest()
        {
            WeightedGraph<int> graph = GetTestGraph();

            Assert.AreEqual(new WeightedEdge<int>(1, 2, 7), graph.GetEdge(1, 2));
        }
    }
}
