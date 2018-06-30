using System;

namespace Graphs
{
    /// <summary>
    /// Represents a directed edge, where from and to vertices can be any comparable type.
    /// </summary>
    /// <typeparam name="T">A type of edge label.</typeparam>
    public struct WeightedEdge<T> : IEquatable<WeightedEdge<T>>, IComparable<WeightedEdge<T>> where T : IComparable<T>
    {
        public static readonly WeightedEdge<T> None = new WeightedEdge<T>(default(T), default(T), 0);

        public WeightedEdge(T from, T to, double weight = 0)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public T From { get; }

        public T To { get; }

        public double Weight { get; }

        public int CompareTo(WeightedEdge<T> other)
        {
            return Weight.CompareTo(other.Weight);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is WeightedEdge<T> edge && Equals(edge);
        }


        public bool Equals(WeightedEdge<T> other)
        {
            return From.CompareTo(other.From) == 0 && To.CompareTo(other.To) == 0 && Math.Abs(Weight - other.Weight) < 1e-6 ;
        }

        public override int GetHashCode()
        {
            var hashCode = 1272664866;
            hashCode = hashCode * -1521134295 + From.GetHashCode();
            hashCode = hashCode * -1521134295 + To.GetHashCode();
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{From}->{To} {Weight}";
        }

        public static bool operator ==(WeightedEdge<T> edge1, WeightedEdge<T> edge2)
        {
            return edge1.Equals(edge2);
        }

        public static bool operator !=(WeightedEdge<T> edge1, WeightedEdge<T> edge2)
        {
            return !(edge1 == edge2);
        }
    }
}
