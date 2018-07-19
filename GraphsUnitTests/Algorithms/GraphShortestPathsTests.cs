using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Graphs;
using Graphs.Algorithms;
using NUnit.Framework;

namespace GraphsUnitTests
{
    [TestFixture]
    public class GraphShortestPathsTests
    {
        [Test]
        public void ShortestPathsWithOneEdgeLength()
        {
            WeightedDiGraph<int> directedGraph = new WeightedDiGraph<int>
            {
                new WeightedEdge<int>(0, 1, 10),
                new WeightedEdge<int>(0, 2, 5),
                new WeightedEdge<int>(0, 3, 7)
            };

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(0, directedGraph);

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

        [Test]
        public void ShortestPathsWithFiveEdges()
        {
            WeightedDiGraph<int> directedGraph = new WeightedDiGraph<int>
            {
                new WeightedEdge<int>(1, 2, 7),
                new WeightedEdge<int>(1, 3, 9),
                new WeightedEdge<int>(2, 4, 15),
                new WeightedEdge<int>(2, 3, 10),
                new WeightedEdge<int>(3, 4, 11)
            };

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(1, directedGraph);

            IList<WeightedEdge<int>> shortestPath = shortestPaths.GetShortestPath(4).ToList();

            Assert.AreEqual(20, shortestPath.Sum(x => x.Weight));
        }

        [TestCase(0, 0)]
        [TestCase(1, 1.05)]
        [TestCase(2, 0.26)]
        [TestCase(3, 0.99)]
        [TestCase(4, 0.38)]
        [TestCase(5, 0.73)]
        [TestCase(6, 1.51)]
        [TestCase(7, 0.60)]
        public void ShortedPathsWithSeveralEdges(int vertexTo, double expectedWeight)
        {
            WeightedDiGraph<int> directedGraph = new WeightedDiGraph<int>
            {
                new WeightedEdge<int>(0, 2, 0.26),
                new WeightedEdge<int>(0, 4, 0.38),
                new WeightedEdge<int>(2, 7, 0.34),
                new WeightedEdge<int>(4, 5, 0.35),
                new WeightedEdge<int>(5, 4, 0.35),
                new WeightedEdge<int>(4, 7, 0.37),
                new WeightedEdge<int>(5, 7, 0.28),
                new WeightedEdge<int>(7, 5, 0.28),
                new WeightedEdge<int>(7, 3, 0.39),
                new WeightedEdge<int>(1, 3, 0.29),
                new WeightedEdge<int>(6, 2, 0.40),
                new WeightedEdge<int>(6, 0, 0.58),
                new WeightedEdge<int>(6, 4, 0.93),
                new WeightedEdge<int>(5, 1, 0.32),
                new WeightedEdge<int>(3, 6, 0.52)
            };

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(0, directedGraph);

            IList<WeightedEdge<int>> shortestPath = shortestPaths.GetShortestPath(vertexTo).ToList();
  
            Assert.AreEqual(expectedWeight, Math.Round(shortestPath.Sum(x => x.Weight), 2));
        }

        [Test]
        public void ShortestPathsEmptyInNotConnectedGraph()
        {
            WeightedDiGraph<int> directedGraph = new WeightedDiGraph<int>
            {
                new WeightedEdge<int>(0, 1, 10),
                new WeightedEdge<int>(0, 2, 10),
                new WeightedEdge<int>(0, 4, 10)

            };

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(1, directedGraph);

            Assert.AreEqual(0, shortestPaths.GetShortestPath(0).Count);
            Assert.AreEqual(0, shortestPaths.GetShortestPath(2).Count);
            Assert.AreEqual(0, shortestPaths.GetShortestPath(4).Count);
        }

        [Test]
        public void ShortestPathDoesnotContainEmptyEdge()
        {
            WeightedDiGraph<int> directedGraph = new WeightedDiGraph<int>
            {
                new WeightedEdge<int>(0, 1, 10),
                new WeightedEdge<int>(0, 2, 10),
                new WeightedEdge<int>(1, 3, 10),
                new WeightedEdge<int>(0, 4, 10)
            };

            GraphShortestPaths<int> shortestPaths = new GraphShortestPaths<int>(0, directedGraph);

            Assert.AreEqual(2, shortestPaths.GetShortestPath(3).Count);
            CollectionAssert.DoesNotContain((ICollection) shortestPaths.GetShortestPath(3), WeightedEdge<int>.None);
        }

        [Test]
        public void ShortestPathWithSymbolicLabelsGraph()
        {
            WeightedDiGraph<char> directedGraph = new WeightedDiGraph<char>
            {
                new WeightedEdge<char>('A', 'B', 1),
                new WeightedEdge<char>('B', 'C', 2),
                new WeightedEdge<char>('A', 'C', 4)
            };

            GraphShortestPaths<char> shortestPaths = new GraphShortestPaths<char>('A', directedGraph);

            ICollection<WeightedEdge<char>> shortestPath = shortestPaths.GetShortestPath('C');

            Assert.AreEqual(2, shortestPath.Count, "Count");
            Assert.AreEqual(new WeightedEdge<char>('A', 'B', 1), shortestPath.ElementAt(0));
            Assert.AreEqual(new WeightedEdge<char>('B', 'C', 2), shortestPath.ElementAt(1));
        }
    }
}
