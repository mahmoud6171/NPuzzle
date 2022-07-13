
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPuzzle
{
    internal class AStar
    {
        Puzzle start_node;
        int dim;
        HashSet<string> seen = new HashSet<string>();
        PriorityQueue<Node, int> pq;

        public AStar(Puzzle start_node, int dim)
        {
            this.start_node = start_node;
            this.dim = dim;
        }

        // O(log(L)*N^2)
        public List<Node> Astar_seaech(char way)
        {
            pq = new PriorityQueue<Node, int>();
            Node startNode = new Node(start_node);


            pq.Enqueue(startNode, startNode.AstareScore(way)); //  // O(log(L)*N^2)
            seen.Add(startNode.Step());

            while (pq.Count > 0)
            {

                Node node = pq.Dequeue();  // log(L)

                if (node.solved()) // O(N^2)
                {
                    return node.SolutionPath(); // O(L)
                }

                foreach (Move move in node.Actions()) // O(N^2)
                {

                    Node child = new Node(move.puzzle, node, move.dir);
                    string step = child.Step(); // O(N^2)
                    if (!seen.Contains(step)) // O(1)
                    {
                        pq.Enqueue(child, child.AstareScore(way)); // O(log(L)*N^2)
                        seen.Add(step); // O(1)
                    }

                }
            }
            return null;
        }
    }
}
