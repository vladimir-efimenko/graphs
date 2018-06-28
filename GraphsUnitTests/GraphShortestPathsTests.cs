﻿using System;
using System.Collections;
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
            WeightedGraph<int> graph = new WeightedGraph<int>();

            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 5);
            graph.AddEdge(0, 3, 7);

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(0, graph);

            IList<WeightedEdge<int>> shortestPath = shortestPaths.GetShortestPath(1).ToList();

            Assert.AreEqual(1, shortestPath.Count, "Shortest path count");
            Assert.AreEqual(new WeightedEdge<int>(0, 1, 10), shortestPath[0]);

            shortestPath = shortestPaths.GetShortestPath(2).ToList();

            Assert.AreEqual(1, shortestPath.Count, "Shortest path count");
            Assert.AreEqual(new WeightedEdge<int>(0, 2, 5), shortestPath[0]);

            shortestPath = shortestPaths.GetShortestPath(3).ToList();

            Assert.AreEqual(1, shortestPath.Count, "Shortest path count");
            Assert.AreEqual(new WeightedEdge<int>(0, 3, 7), shortestPath[0]);
        }

        [TestMethod]
        public void ShortestPathsWithFiveEdges()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>();

            graph.AddEdge(1, 2, 7);
            graph.AddEdge(1, 3, 9);
            graph.AddEdge(2, 4, 15);
            graph.AddEdge(2, 3, 10);
            graph.AddEdge(3, 4, 11);

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(1, graph);

            IList<WeightedEdge<int>> shortestPath = shortestPaths.GetShortestPath(4).ToList();

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
        public void ShortedPathsWithSeveralEdges(int vertexTo, double expectedWeight)
        {
            WeightedGraph<int> graph = new WeightedGraph<int>();

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

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(0, graph);

            IList<WeightedEdge<int>> shortestPath = shortestPaths.GetShortestPath(vertexTo).ToList();
  
            Assert.AreEqual(expectedWeight, Math.Round(shortestPath.Sum(x => x.Weight), 2));
        }

        [TestMethod]
        public void ShortestPathsEmptyInNotConnectedGraph()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>();

            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 10);
            graph.AddEdge(0, 4, 10);

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(1, graph);

            Assert.AreEqual(0, shortestPaths.GetShortestPath(0).Count);
            Assert.AreEqual(0, shortestPaths.GetShortestPath(2).Count);
            Assert.AreEqual(0, shortestPaths.GetShortestPath(4).Count);
        }

        [TestMethod]
        public void ShortestPathDoesnotContainEmptyEdge()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>();

            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 10);
            graph.AddEdge(1, 3, 10);
            graph.AddEdge(0, 4, 10);

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(0, graph);

            Assert.AreEqual(2, shortestPaths.GetShortestPath(3).Count);
            CollectionAssert.DoesNotContain((ICollection) shortestPaths.GetShortestPath(3), WeightedEdge<int>.None);
        }

        [TestMethod]
        public void ShortestPathWithSymbolicLabelsGraph()
        {
            WeightedGraph<char> graph = new WeightedGraph<char>();

            graph.AddEdge('A', 'B', 1);
            graph.AddEdge('B', 'C', 2);
            graph.AddEdge('A', 'C', 4);

            GraphShortestPaths<char> shortestPaths = new GraphShortestPaths<char>('A', graph);

            ICollection<WeightedEdge<char>> shortestPath = shortestPaths.GetShortestPath('C');

            Assert.AreEqual(2, shortestPath.Count, "Count");
            Assert.AreEqual(new WeightedEdge<char>('A', 'B', 1), shortestPath.ElementAt(0));
            Assert.AreEqual(new WeightedEdge<char>('B', 'C', 2), shortestPath.ElementAt(1));
        }
    }
}
