using System;
using System.Collections.Generic;
using System.Text;

namespace TitanTrustBank.Common.PriorityQueue;
{
    public class PriorityQueue<T>
    {

        private List<int> Prorities;
        private List<T> Nodes;
        private Dictionary<T, int> NodeIndexes;

        public PriorityQueue()
        {
            Prorities = new List<int>();
            Nodes = new List<T>();
            NodeIndexes = new Dictionary<T, int>();
        }
        public void Enqueue(T node, int Priority)
        {
            Prorities.Add(Priority);
            Nodes.Add(node);
            NodeIndexes.Add(node, Prorities.Count - 1);
            Heapify();
        }


        public void UpdatePriority(T node, int Priority)
        {
            var nodeindex = NodeIndexes[node];
            Prorities.RemoveAt(nodeindex);
            Nodes.RemoveAt(nodeindex);
            Prorities.Add(Priority);
            Nodes.Add(node);
            NodeIndexes.Add(node, Prorities.Count - 1);
            Heapify();
        }


        private void Heapify()
        {
            var lastinsertednodeindex = Prorities.Count - 1;
            var index = lastinsertednodeindex;
            var parentindex = GetParentIndex(index);
            while (lastinsertednodeindex != 0 && Prorities[index] < Prorities[parentindex])
            {
                var parentpriority = Prorities[parentindex];
                Prorities[parentindex] = Prorities[index];
                Prorities[index] = parentpriority;

                var parentnode = Nodes[parentindex];
                Nodes[parentindex] = Nodes[index];
                NodeIndexes[Nodes[parentindex]] = index;
                Nodes[index] = parentnode;
                NodeIndexes[Nodes[index]] = parentindex;

            }
        }

        private Tuple<int,T> PeekPriorityAndNode()
        {
            var priority = Prorities[0];
            var node = Nodes[0];
            var response = Tuple.Create(priority, node);
            return response;
        }

        public int Count()
        {
            return Prorities.Count;
        }


        public int GetPriority(T node)
        {
            var nodeindex = NodeIndexes[node];
            var priority = Prorities[nodeindex];
            return priority;
        }


        public Tuple<int, T> Dequeue()
        {
            var priority = Prorities[0];
            var node = Nodes[0];
            Prorities.RemoveAt(0);
            Nodes.RemoveAt(0);
            var response = Tuple.Create(priority, node);
            return response;
        }

        private int GetParentIndex(int index)
        {
            var parentindex = index / 2;
            return parentindex;
        }
    }
}
