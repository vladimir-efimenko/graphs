using System;

namespace Graphs
{
    public struct WeightedEdge : IEquatable<WeightedEdge>, IComparable<WeightedEdge>
    {
        public static readonly WeightedEdge None = new WeightedEdge(int.MinValue, int.MinValue, 0);

        public WeightedEdge(int from, int to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public int From { get; }

        public int To { get; }

        public double Weight { get; }

        public int CompareTo(WeightedEdge other)
        {
            return Weight.CompareTo(other.Weight);
        }

        public bool Equals(WeightedEdge other)
        {
            return From == other.From && To == other.To && Math.Abs(Weight - other.Weight) < 1e-6 ;
        }

        public override int GetHashCode()
        {
            var hashCode = 1272664866;
            hashCode = hashCode * -1521134295 + From.GetHashCode();
            hashCode = hashCode * -1521134295 + To.GetHashCode();
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            return hashCode;
        }
    }
}
