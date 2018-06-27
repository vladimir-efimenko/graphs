using System;
using C5;
using Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphsUnitTests
{
    [TestClass]
    public class EdgeTests
    {
        [TestMethod]
        public void MaxValueEdgeIsLessThanPositiveInfinityEdge()
        {
            var edge1 = new WeightedEdge(0, 1, double.MaxValue);
            var edge2 = new WeightedEdge(0, 1, double.PositiveInfinity);

            Assert.AreEqual(-1, edge1.CompareTo(edge2));
        }

        [TestMethod]
        public void PriorityMinEdgeFindsFirst()
        {
            IPriorityQueue<WeightedEdge> queue = new IntervalHeap<WeightedEdge>();

            queue.Add(new WeightedEdge(0, 1, 1));
            queue.Add(new WeightedEdge(1, 2, Double.PositiveInfinity));

            Assert.AreEqual(new WeightedEdge(0, 1, 1), queue.FindMin());
        }
    }
}
