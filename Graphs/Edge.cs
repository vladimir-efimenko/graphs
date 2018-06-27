using System;

namespace Graphs
{
    public struct Edge : IEquatable<Edge>
    {
        public Edge(int from, int to)
        {
            From = from;
            To = to;
        }

        public int From { get; private set; }

        public int To { get; private set; }

        public bool Equals(Edge other)
        {
            return From == other.From && To == other.To;
        }
    }
}
