using Graphs;
using NUnit.Framework;

namespace GraphsUnitTests
{
    [TestFixture]
    public class PriorityQueueTests
    {
        private PriorityQueue<int> _pq;

        [SetUp]
        public void Setup()
        {
            _pq = new PriorityQueue<int>();
        }

        [TestCase(1, true)]
        [TestCase(2, false)]
        public void ContainsVertexTest(int vertex, bool result)
        {
            _pq.Add(1, 1);

            Assert.AreEqual(result, _pq.Contains(vertex));
        }

        [Test]
        public void CanDeleteMinVertexTest()
        {
            _pq.Add(1, 100);
            _pq.Add(2, double.PositiveInfinity);

            Assert.AreEqual(1, _pq.DeleteMin());
            Assert.IsFalse(_pq.Contains(1));
        }

        [Test]
        public void CanChangePriorityTest()
        {
            _pq.Add(1, 1);
            _pq.Add(2, 100);

            _pq.Change(2, 0);
            Assert.AreEqual(2, _pq.DeleteMin());
        }
    }
}
