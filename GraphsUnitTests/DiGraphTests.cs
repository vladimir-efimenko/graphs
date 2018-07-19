

using Graphs;
using NUnit.Framework;

namespace GraphsUnitTests
{
    [TestFixture]
    public class DiGraphTests
    {
        [Test]
        public void DiGraphReverseCorrectly()
        {
            WeightedDiGraph<int> graph = new WeightedDiGraph<int>
            {
                new WeightedEdge<int>(1, 2, 1),
                new WeightedEdge<int>(2, 3, 2),
                new WeightedEdge<int>(3, 1, 3)
            };

            WeightedDiGraph<int> reversed = graph.Reverse();

            Assert.AreNotEqual(WeightedEdge<int>.None, reversed.GetEdge(2, 1));
            Assert.AreEqual(WeightedEdge<int>.None, reversed.GetEdge(1, 2));
            Assert.AreNotEqual(WeightedEdge<int>.None, reversed.GetEdge(3, 2));
            Assert.AreEqual(WeightedEdge<int>.None, reversed.GetEdge(2, 3));
            Assert.AreNotEqual(WeightedEdge<int>.None, reversed.GetEdge(1, 3));
            Assert.AreEqual(WeightedEdge<int>.None, reversed.GetEdge(3, 1));
        }
    }
}
