using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPuzzle
{
    class Move
    {
        public Puzzle puzzle;
        public string dir;
    }

    internal class Puzzle
    {
        public int[,] array;
        public int dim;
        public List<Move> moves;
        int [,] goal;
        //List<int> zeroIndex = new List<int> ();

        // O(N^2)
        public Puzzle(int[,] array)
        {
            this.array = array;
            this.dim = array.GetLength(0);  // O(1)
            this.goal=new int[this.dim,this.dim];
            this.goal=this.goalState();  // O(N^2)
            moves = new List<Move>();
        }
        // O(N^2)
        public int[,] goalState()
        {
            int[] arr = new int[this.dim * this.dim];
            for (int i = 0 ,v=1; i < this.dim * this.dim; i++,v++)
            {
                arr[i] = v;
            }
            arr[(this.dim * this.dim) - 1] = 0;

            int[,] output = new int[this.dim, this.dim];

            for (int i = 0; i < this.dim; i++)
            {
                for (int j = 0; j <this.dim; j++)
                {
                    output[i, j] = arr[i * this.dim + j];
                }
            }
            return output;
        }
       
       // O(N^2)
        public int manhattanDistance()
        {
            int distance = 0;
            for (int i = 0; i < this.dim; i++)
            {
                for (int j = 0; j < this.dim; j++)
                {
                    if (this.array[i, j] != 0)
                    {
                        int x = (this.array[i, j] - 1) / this.dim;
                        int y = (this.array[i, j] - 1) % this.dim;
                        distance += Math.Abs(x - i) + Math.Abs(y - j);
                    }
                }
            }
            return distance;
        }

        // O(N^2)
        public int HammingDistance()
        {
            int count=0;
            for (int i = 0; i < this.dim; i++)
            {
                for (int j = 0; j < this.dim; j++)
                {
                    if(this.array[i,j]!=this.goal[i,j]){
                        count++;
                    }
                }
            }
            return count;
        }

        // O(N^2)
        public List<int> getZeroindex(int[,] arr)
        {
            List<int> ls = new List<int>();
            for (int i = 0; i < this.dim; i++)
            {
                for (int j = 0; j < this.dim; j++)
                {
                    if (arr[i, j] == 0)
                    {
                        ls.Add(i);  // O(1)
                        ls.Add(j);  // O(1)
                        return ls;
                    }
                }
            }
            return null;
        }

        // O(N^2)
        public List<Move> Actions()
        {
            List<int> ls = getZeroindex(array); // O(N^2)
            moveRight(array, ls[0], ls[1]);     // O(N^2)
            moveLeft(array, ls[0], ls[1]);      // O(N^2)
            moveUp(array, ls[0], ls[1]);        // O(N^2)
            moveDown(array, ls[0], ls[1]);      // O(N^2)
            return this.moves;

        }
        // O(N^2)
        public void moveLeft(int[,] p, int idx_row, int idx_col)
        {
            Move move = new Move();
            if (idx_row >= 0 && idx_col - 1 >= 0 &&
                idx_row < this.dim && idx_col - 1 < this.dim)
            {
                int[,] pc = (int[,])p.Clone();  // O(N^2)
                int temp = pc[idx_row, idx_col - 1];
                pc[idx_row, idx_col - 1] = pc[idx_row, idx_col];
                pc[idx_row, idx_col] = temp;

                Puzzle puzzle = new Puzzle(pc);
                move.puzzle = puzzle;
                move.dir = "Left";
                this.moves.Add(move);  // O(1)
            }

        }

        // O(N^2)
        public void moveRight(int[,] p, int idx_row, int idx_col)
        {
            Move move = new Move();
            if (idx_row >= 0 && idx_col + 1 >= 0 &&
                idx_row < this.dim && idx_col + 1 < this.dim)
            {
                int[,] pc = (int[,])p.Clone();  // O(N^2)

                int temp = pc[idx_row, idx_col + 1];
                pc[idx_row, idx_col + 1] = pc[idx_row, idx_col];
                pc[idx_row, idx_col] = temp;


                Puzzle puzzle = new Puzzle(pc);

                move.puzzle = puzzle;
                move.dir = "Right";
                this.moves.Add(move);  // O(1)

            }
        }
        
        // O(N^2)
        public void moveDown(int[,] p, int idx_row, int idx_col)
        {
            Move move = new Move();
            if (idx_row + 1 >= 0 && idx_col >= 0 &&
                idx_row + 1 < this.dim && idx_col < this.dim)
            {
                // make a copy

                int[,] pc = (int[,])p.Clone();  // O(N^2)

                int temp = pc[idx_row + 1, idx_col];
                pc[idx_row + 1, idx_col] = pc[idx_row, idx_col];
                pc[idx_row, idx_col] = temp;

                Puzzle puzzle = new Puzzle(pc);
                
                move.puzzle = puzzle;
                move.dir = "Down";
                this.moves.Add(move);  // O(1)

            }
        }

        // O(N^2)
        public void moveUp(int[,] p, int idx_row, int idx_col)
        {
            Move move = new Move();
            if (idx_row - 1 >= 0 && idx_col >= 0 &&
                idx_row - 1 < this.dim && idx_col < this.dim)
            {
                // make a copy

                int[,] pc = (int[,])p.Clone();  // O(N^2)

                int temp = pc[idx_row - 1, idx_col];
                pc[idx_row - 1, idx_col] = pc[idx_row, idx_col];
                pc[idx_row, idx_col] = temp;

                Puzzle puzzle = new Puzzle(pc);

                move.puzzle = puzzle;
                move.dir = "UP";
                this.moves.Add(move);   // O(1)

            }
        }
        public Puzzle indexMove(List<int> at, List<int> to)
        {
            int[,] pc = (int[,])this.array.Clone();
            int i, j, r, c;
            i = at[0];
            j = at[1];
            r = to[0];
            c = to[1];

            int temp = pc[i, j];
            pc[i, j] = pc[r, c];
            pc[r, c] = temp;

            return new Puzzle(pc);

        }
        
        // O(N^2)
        public void PrintPuzzle()
        {
            Console.WriteLine();
            for (int i = 0; i < this.dim; i++)
            {
                for (int j = 0; j < this.dim; j++)
                {
                    Console.Write(this.array[i, j] + " ");
                }
                Console.WriteLine();
            }


        }

        // O(N^2)
        public bool Solved()
        {
            return (this.manhattanDistance() == 0);  // O(N^2)
        }
    }
}
