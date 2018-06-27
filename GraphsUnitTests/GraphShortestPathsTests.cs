using System;
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

        [DataRow(0, 0)]
        [DataRow(1, 1.05)]
        [DataRow(2, 0.26)]
        [DataRow(3, 0.99)]
        [DataRow(4, 0.38)]
        [DataRow(5, 0.73)]
        [DataRow(6, 1.51)]
        [DataRow(7, 0.60)]
        [DataTestMethod]
        public void ShortedPathsWithSeveralEdges_2(int vertexTo, double expectedWeight)
        {
            WeightedGraph graph = new WeightedGraph();

            graph.AddEdge(0, 2, 0.26);
            graph.AddEdge(0, 4, 0.38);
            graph.AddEdge(2, 7, 0.34);
            graph.AddEdge(4, 5, 0.35);
            graph.AddEdge(5, 4, 0.35);
            graph.AddEdge(4, 7, 0.37);
            graph.AddEdge(5, 7, 0.28);
            graph.AddEdge(7, 5, 0.28);
            graph.AddEdge(7, 3, 0.39);
            graph.AddEdge(1, 3, 0.29);
            graph.AddEdge(6, 2, 0.40);
            graph.AddEdge(6, 0, 0.58);
            graph.AddEdge(6, 4, 0.93);
            graph.AddEdge(5, 1, 0.32);
            graph.AddEdge(3, 6, 0.52);

            GraphShortestPaths shortestPaths = new GraphShortestPaths(0, graph);

            IList<WeightedEdge> shortestPath = shortestPaths.GetShortestPath(vertexTo).ToList();
            foreach(var edge in shortestPath)
            {
                System.Console.Write($"{edge} ");
            }

            Assert.AreEqual(expectedWeight, Math.Round(shortestPath.Sum(x => x.Weight), 2));
        }
    }
}
