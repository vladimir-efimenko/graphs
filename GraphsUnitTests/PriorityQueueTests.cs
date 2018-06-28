using Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphsUnitTests
{
    [TestClass]
    public class PriorityQueueTests
    {
        private PriorityQueue<int> _pq;

        [TestInitialize]
        public void Setup()
        {
            _pq = new PriorityQueue<int>();
        }

        [DataRow(1, true)]
        [DataRow(2, false)]
        [DataTestMethod]
        public void ContainsVertexTest(int vertex, bool result)
        {
            _pq.Add(1, 1);

            Assert.AreEqual(result, _pq.Contains(vertex));
        }

        [TestMethod]
        public void CanDeleteMinVertexTest()
        {
            _pq.Add(1, 100);
            _pq.Add(2, double.PositiveInfinity);

            Assert.AreEqual(1, _pq.DeleteMin());
            Assert.IsFalse(_pq.Contains(1));
        }

        [TestMethod]
        public void CanChangePriorityTest()
        {
            _pq.Add(1, 1);
            _pq.Add(2, 100);

            _pq.Change(2, 0);
            Assert.AreEqual(2, _pq.DeleteMin());
        }
    }
}
