using Graphs;
using Graphs.Algorithms;
using SCG = System.Collections.Generic;
using C5;
using System.Linq;
using NUnit.Framework;

namespace GraphsUnitTests.Algorithms
{
    [TestFixture]
    public class DfsTests
    {
        [Test]
        public void CanSumDirectedGraphWeightsCorrectlyWhenTraverseInDfsOrder()
        {
            WeightedDiGraph<int> directedGraph = new WeightedDiGraph<int>
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

            Assert.AreEqual(directedGraph.Weight, weight);
        }

        [Test]
        public void CanSumUndirectedGraphWeightsCorrectlyWhenTraverseInDfsOrder()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>
            {
                new WeightedEdge<int>(2, 3, 9),
                new WeightedEdge<int>(1, 2, 10),
                new WeightedEdge<int>(1, 3, 1),
                new WeightedEdge<int>(3, 4, 8),
                new WeightedEdge<int>(2, 5, 7),
                new WeightedEdge<int>(0, 5, 5)
            };
            double weight = 0;

            Dfs<int> dfs = new Dfs<int>(graph, edge => weight += edge.Weight);

            Assert.AreEqual(0, weight);
        }

        [Test]
        public void TraverseActionCanGetAllVertices()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>
            {
                (1, 2), (2, 3), (3, 4), (1, 4), (5, 2), (7, 2)
            };

            HashSet<int> vertices = new HashSet<int>();

            Dfs<int> dfs = new Dfs<int>(graph, edge => vertices.AddAll(new [] { edge.From, edge.To }));

            Assert.AreEqual(vertices.Count, graph.GetVertices().Count);
        }

        [Test]
        public void Paths()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>
            {
                (1, 2), (2, 3), (3, 4), (4, 5)
            };

            DepthFirstPaths<int> dfPaths = new DepthFirstPaths<int>(graph, 1);

            SCG.ICollection<WeightedEdge<int>> path = dfPaths.GetPathTo(5);

            Assert.AreEqual(4, path.Count, "Path count");

            Assert.AreEqual(new WeightedEdge<int>(4, 5), path.ElementAt(3));
            Assert.AreEqual(new WeightedEdge<int>(3, 4), path.ElementAt(2));
            Assert.AreEqual(new WeightedEdge<int>(2, 3), path.ElementAt(1));
            Assert.AreEqual(new WeightedEdge<int>(1, 2), path.ElementAt(0));
        }
    }
}
