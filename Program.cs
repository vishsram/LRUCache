using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    public class LRUCache
    {
        private int capacity;

        LinkedList<int> LL;
        Dictionary<int, LinkedListNode<int>> nodeDict;

        public LRUCache(int c)
        {
            this.capacity = c;
            LL = new LinkedList<int>();
            nodeDict = new Dictionary<int, LinkedListNode<int>>();
            
        }

        public int GetNoOfElements()
        {
            return this.LL.Count;
        }

        public bool TryGetValue(int key, out int value)
        {
            LinkedListNode<int> temp = new LinkedListNode<int>(0);
            value = 0;

            if(nodeDict.TryGetValue(key, out temp))
            {
                value = temp.Value;
                // add it to the front of the list
                LL.AddFirst(value);
                // remove the previous copy of the node 
                LL.Remove(temp);
                return true;
            }
            return false;
        }

        public void Add (int key, int value)
        {
            if (this.nodeDict.ContainsKey(key))
            {
                throw new ArgumentException("Element with the same key already exists in the list.");
            }

            if (GetNoOfElements() == this.capacity)
            {
                // delete an element at the end of the list
                this.LL.RemoveLast();
            }

            // add at the front of the list
            LinkedListNode<int> node = new LinkedListNode<int>(value);
            LL.AddFirst(node);
            nodeDict.Add(key, node);
        }

        public void Clear()
        {
            this.LL.Clear();
            this.nodeDict.Clear();
        }

        public void PrintList()
        {
            if(LL.Count > 0)
            {
                for (int i = 0; i < LL.Count; i++)
                {
                    Console.WriteLine("Value at {0} : {1}", i, LL.ElementAt(i));
                }
            }
        }
        static void Main(string[] args)
        {
            LRUCache lru = new LRUCache(3);
            lru.Add(1, 10);
            lru.Add(2, 20);
            lru.Add(3, 30);
            lru.Add(4, 40);
            lru.PrintList();
            // lru.Add(3, 10);
            int temp = 0;
            lru.TryGetValue(2, out temp);
            Console.WriteLine("Returned value for key 2: {0}", temp);
            lru.PrintList();
        }
    }
}
