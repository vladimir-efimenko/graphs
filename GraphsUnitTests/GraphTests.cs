using System.Collections.Generic;
using Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphsUnitTests
{
    [TestClass]
    public class GraphTests
    {
        private WeightedDiGraph<int> GetTestGraph()
        {
            WeightedDiGraph<int> directedGraph = new WeightedDiGraph<int>
            {
                new WeightedEdge<int>(0, 1, 10),
                new WeightedEdge<int>(0, 2, 5),
                new WeightedEdge<int>(0, 3, 3),
                new WeightedEdge<int>(1, 2, 7)
            };

            return directedGraph;
        }

        [TestMethod]
        public void GetWeightTest()
        {
            WeightedDiGraph<int> directedGraph = GetTestGraph();

            Assert.AreEqual(25, directedGraph.GetWeight());
        }

        [TestMethod]
        public void GetAdjacencyListTest()
        {
            WeightedDiGraph<int> directedGraph = GetTestGraph();

            ICollection<WeightedEdge<int>> adjacencyList = directedGraph.GetAdjacencyList(0);

            Assert.AreEqual(3, adjacencyList.Count, "Adjacency list count");
            Assert.IsTrue(adjacencyList.Contains(new WeightedEdge<int>(0, 1, 10)));
            Assert.IsTrue(adjacencyList.Contains(new WeightedEdge<int>(0, 2, 5)));
            Assert.IsTrue(adjacencyList.Contains(new WeightedEdge<int>(0, 3, 3)));
        }

        [TestMethod]
        public void GetEdgeTest()
        {
            WeightedDiGraph<int> directedGraph = GetTestGraph();

            Assert.AreEqual(new WeightedEdge<int>(1, 2, 7), directedGraph.GetEdge(1, 2));
        }

        [TestMethod]
        public void WeightedGraphToString()
        {
            WeightedGraph<int> graph = new WeightedDiGraph<int>
            {
                new WeightedEdge<int>(1, 2, 1),
                new WeightedEdge<int>(2, 3, 2),
                new WeightedEdge<int>(3, 1, 3)
            };

            Assert.IsNotNull(graph.ToString());
        }

        [TestMethod]
        public void CanGetNoneWhenEdgeDoesnotExist()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>
            {
                new WeightedEdge<int>(1, 2),
                new WeightedEdge<int>(3, 5),
                new WeightedEdge<int>(2, 6)
            };

            Assert.AreEqual(WeightedEdge<int>.None, graph.GetEdge(1, 3));
        }
    }
}
