using System.Collections.Generic;
using System.Linq;
using Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphsUnitTests
{
    [TestClass]
    public class GraphShortestPathsTests
    {
        [TestMethod]
        public void ShortestPathsWithOneEdgeLength()
        {
            WeightedGraph graph = new WeightedGraph();

            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 5);
            graph.AddEdge(0, 3, 7);

            GraphShortestPaths shortestPaths = new GraphShortestPaths(0, graph);

            IList<WeightedEdge> shortestPath = shortestPaths.GetShortestPath(1).ToList();

            Assert.AreEqual(1, shortestPath.Count, "Shortest path count");
            Assert.AreEqual(new WeightedEdge(0, 1, 10), shortestPath[0]);

            shortestPath = shortestPaths.GetShortestPath(2).ToList();

            Assert.AreEqual(1, shortestPath.Count, "Shortest path count");
            Assert.AreEqual(new WeightedEdge(0, 2, 5), shortestPath[0]);

            shortestPath = shortestPaths.GetShortestPath(3).ToList();

            Assert.AreEqual(1, shortestPath.Count, "Shortest path count");
            Assert.AreEqual(new WeightedEdge(0, 3, 7), shortestPath[0]);
        }

        [TestMethod]
        public void ShortestPathsWithSeveralEdges()
        {
            WeightedGraph graph = new WeightedGraph();

            graph.AddEdge(1, 2, 7);
            graph.AddEdge(1, 3, 9);
            graph.AddEdge(2, 4, 15);
            graph.AddEdge(2, 3, 10);
            graph.AddEdge(3, 4, 11);

            GraphShortestPaths shortestPaths = new GraphShortestPaths(1, graph);

            IList<WeightedEdge> shortestPath = shortestPaths.GetShortestPath(4).ToList();

            Assert.AreEqual(20, shortestPath.Sum(x => x.Weight));
        }
    }
}
