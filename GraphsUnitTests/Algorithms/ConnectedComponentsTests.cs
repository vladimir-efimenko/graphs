using Graphs;
using Graphs.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphsUnitTests.Algorithms
{
    [TestClass]
    public class ConnectedComponentsTests
    {

        [TestMethod]
        public void ConnectedComponentsWhenEveryVertexIsolated()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>
            {
                new WeightedEdge<int>(1, 1),
                new WeightedEdge<int>(2, 2),
                new WeightedEdge<int>(3, 3)
            };

            ConnectedComponents<int> cc = new ConnectedComponents<int>(graph);

            Assert.AreEqual(3, cc.Count());
        }

        [TestMethod]
        public void ConnectedComponentsTwoVerticesConnected()
        {
            WeightedGraph<int> graph = new WeightedGraph<int>
            {
                new WeightedEdge<int>(1, 2),
                new WeightedEdge<int>(2, 3),
                new WeightedEdge<int>(3, 4)
            };

            ConnectedComponents<int> cc = new ConnectedComponents<int>(graph);

            Assert.IsTrue(cc.Connected(4, 2));
        }

        [DataRow(1, 2)]
        [DataRow(2, 3)]
        [DataRow(3, 1)]
        [DataTestMethod]
        public void ConnectedComponentsWhenEveryVertexIsolatedNoVerticesConnected(int vertex1, int vertex2)
        {
            WeightedGraph<int> graph = new WeightedGraph<int>
            {
                new WeightedEdge<int>(1, 1),
                new WeightedEdge<int>(2, 2),
                new WeightedEdge<int>(3, 3)
            };

            ConnectedComponents<int> cc = new ConnectedComponents<int>(graph);

            Assert.IsFalse(cc.Connected(vertex1, vertex2));
        }
    }
}
