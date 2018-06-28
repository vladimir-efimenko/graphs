using System;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            WeightedGraph<int> graph = new WeightedGraph<int>();

            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 5);
            graph.AddEdge(0, 3, 3);
            graph.AddEdge(1, 2, 7);

            Console.WriteLine(graph);
            Console.WriteLine($"Graph weight: {graph.GetWeight()}");
        }
    }
}
