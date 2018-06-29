using System;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            WeightedDirectedGraph<int> directedGraph = new WeightedDirectedGraph<int>();

            directedGraph.Add(0, 1, 10);
            directedGraph.Add(0, 2, 5);
            directedGraph.Add(0, 3, 3);
            directedGraph.Add(1, 2, 7);

            Console.WriteLine(directedGraph);
            Console.WriteLine($"Graph weight: {directedGraph.GetWeight()}");
        }
    }
}
