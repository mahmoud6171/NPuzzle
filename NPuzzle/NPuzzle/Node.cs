using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NPuzzle
{
    internal class Node
    {
        public Puzzle puzzle;
        Node parent;
        public string action;
        int level;
       public int color;

        // O(1) 
        public Node(Puzzle puzzle, Node parent = null, string action = "None",int color=0)
        {
            this.puzzle = puzzle;
            this.parent = parent;
            this.action = action;
            this.color=color;
            if (this.parent != null)
            {
                this.level = parent.level + 1;
            }
            else
                this.level = 0;
        }
        public int AstareScore(char way)
        {
            if (way == 'h')
                return this.level + this.HM();
            else
                return this.level + this.M();
        }

        // O(N^2)
        public int M()
        {
            return this.puzzle.manhattanDistance();  // O(N^2)
        }

        // O(N^2)
        public int HM()
        {
            return this.puzzle.HammingDistance();  // O(N^2)
        }


        // O(L)
        public List<Node> SolutionPath()
        {
            List<Node> path = new List<Node>();
            Node node = this;
            while (node != null)
            {
                path.Add(node);
                node = node.parent;
            }
            path.Reverse();  // O(d)
            return path;
        }

        // O(N^2)
        public bool solved()
        {
            return this.puzzle.Solved(); // O(N^2)
        }

        // O(N^2)
        public List<Move> Actions()
        {
            return this.puzzle.Actions(); // O(N^2)
        }

        // O(N^2)
        public string Step()
        {
            var s = "";
            for (int i = 0; i < this.puzzle.dim; i++)
            {
                for (int j = 0; j < this.puzzle.dim; j++)
                {
                    s += this.puzzle.array[i, j];
                }
            }
            return s;
        }

    }
}