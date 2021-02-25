using System;
using System.Collections.Generic;
using System.Text;
using TitanTrustBank.Common.PriorityQueue;

namespace TitanTrustBank.Graph
{
    public class GraphSolution
    {
        public Graph graph { get; set; }
        private PriorityQueue<int> pq;

        public GraphSolution()
        {
            graph = new Graph();
            graph.Vertices = new Dictionary<int, Dictionary<int, int>>();
            pq = new PriorityQueue<int>();
        }
        public void AddVertex(int vertex)
        {
            if (!graph.Vertices.ContainsKey(vertex))
            {
                var edgelist = new Dictionary<int, int>();
                graph.Vertices.Add(vertex, edgelist);
            }
        }


        public void AddEdge(int Vertex, int Edge, int Weight = 0, bool isundirected = false)
        {
            if (!graph.Vertices[Vertex].ContainsKey(Edge))
            {
                graph.Vertices[Vertex].Add(Edge, Weight);
            }

            if (isundirected)
            {
                if (!graph.Vertices[Edge].ContainsKey(Vertex))
                {
                    graph.Vertices[Edge].Add(Vertex, Weight);
                }
            }
        }


        public void FindShortestPath(int start, int destination)
        {
            var isvisited = new HashSet<int>();
            var pathinformation = new Dictionary<int, int>();
            foreach (var item in graph.Vertices)
            {
                if (item.Key == start)
                {
                    pq.Enqueue(item.Key, 0);
                }
                else
                {
                    pq.Enqueue(item.Key, int.MaxValue);
                }
            }


            while (pq.Count() != 0)
            {

                var record = pq.Dequeue();
                var weight = record.Item1;
                var vertex = record.Item2;

                var neighbours = graph.Vertices[vertex];

                foreach (var item in neighbours)
                {
                    if (isvisited.Contains(item.Key))
                    {
                        continue;
                    }

                    isvisited.Add(item.Key);

                    var itempriority = pq.GetPriority(item.Key);

                    var calcweight = weight + item.Value;
                    if (calcweight < itempriority)
                    {
                        pq.UpdatePriority(item.Key, calcweight);

                        if (pathinformation.ContainsKey(item.Key))
                        {
                            pathinformation[item.Key] = vertex;
                        }
                        else
                        {
                            pathinformation.Add(item.Key, vertex);
                        }
                    }
                }

            }
        }

    }


    public class Graph
    {
        public Dictionary<int, Dictionary<int, int>> Vertices { get; set; }
    }
}