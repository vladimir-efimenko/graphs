using System;
using C5;
using Graphs;
using NUnit.Framework;

namespace GraphsUnitTests
{
    [TestFixture]
    public class EdgeTests
    {
        [Test]
        public void MaxValueEdgeIsLessThanPositiveInfinityEdge()
        {
            var edge1 = new WeightedEdge<int>(0, 1, double.MaxValue);
            var edge2 = new WeightedEdge<int>(0, 1, double.PositiveInfinity);

            Assert.AreEqual(-1, edge1.CompareTo(edge2));
        }

        [Test]
        public void PriorityMinEdgeFindsFirst()
        {
            IPriorityQueue<WeightedEdge<int>> queue = new IntervalHeap<WeightedEdge<int>>();

            queue.Add(new WeightedEdge<int>(0, 1, 1));
            queue.Add(new WeightedEdge<int>(1, 2, Double.PositiveInfinity));

            Assert.AreEqual(new WeightedEdge<int>(0, 1, 1), queue.FindMin());
        }

        [Test]
        public void EqualsTest()
        {
            WeightedEdge<int> edge1 = new WeightedEdge<int>(1, 2);
            object edge2 = new WeightedEdge<int>(1, 2);

            Assert.IsTrue(edge1.Equals(edge2));
        }
    }
}
