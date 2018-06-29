using Graphs;
using Graphs.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Schema;

namespace GraphsUnitTests.Algorithms
{
    [TestClass]
    public class ConnectedComponentsTests
    {

        [TestMethod]
        public void ConnectedComponentsWhenEveryVertexIsolated()
        {
            WeightedUndirectedGraph<int> graph = new WeightedUndirectedGraph<int>
            {
                new WeightedEdge<int>(1, 1, 0),
                new WeightedEdge<int>(2, 2, 0),
                new WeightedEdge<int>(3, 3, 0)
            };

            ConnectedComponents<int> cc = new ConnectedComponents<int>(graph);

            Assert.AreEqual(3, cc.Count());
        }
    }
}
