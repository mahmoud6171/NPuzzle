namespace NPuzzle
{
    internal class DFSSearch
    {
        HashSet<string> set = new HashSet<string>();
        Queue<Node> queue = new Queue<Node>();
        Node root;
        int N;
        public DFSSearch(Node root, int N)
        {
            this.root = root;
            this.N = N;
            this.root.color = 1;
        }

        public List<Node> DfsSolve()
        {
            this.queue.Enqueue(this.root);
            //this.set.Add(this.root.Step());
            while (this.queue.Count > 0)
            {
                Node u = this.queue.Dequeue();
                if (u.solved())
                {
                    return u.SolutionPath();
                }
                if (this.set.Contains(u.Step()))
                {
                    continue;
                }
                this.set.Add(u.Step());
                List<Node> adj = new List<Node>();
                adj = this.getAdj(u);
                foreach (Node i in adj)
                {
                    if (i.color == 0)
                    {
                        i.color = 1;
                        this.queue.Enqueue(i);
                    }
                }

                u.color = 2;

            }
            return null;
        }

        public List<Node> getAdj(Node node)
        {
            List<Node> adj = new List<Node>();
            foreach (Move move in node.Actions()) // O(N^2)
            {
                Node child = new Node(move.puzzle, node, move.dir);
                //string step = child.Step(); // O(N^2)
                //this.set.Add(step); // O(1)
                adj.Add(child);
            }
            return adj;
        }

    }

}