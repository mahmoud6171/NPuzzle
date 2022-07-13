namespace NPuzzle
{
    internal class Solvable
    {
        Puzzle puzzle;
        int dim;
        public Solvable(Puzzle puzzle){
            this.puzzle=puzzle;
            this.dim=this.puzzle.dim;
        }
        public bool isSolvable()
        {
            List<int> ls = puzzle.getZeroindex(puzzle.array);
            List<int> ls2 = new List<int>();

            bool yIsEven = false;

            if (ls[0] % 2 == 0)
                yIsEven = true;
            else
                yIsEven = false;

            int inversion_count = 0;

            // O(N^2)
            for (int i = 0; i < this.dim; i++)
            {
                for (int j = 0; j < this.dim; j++)
                {
                    if (puzzle.array[i, j] != 0)
                        ls2.Add(puzzle.array[i, j]);
                }
            }

            // O(N^2)
            for (int i = 0; i < ls2.Count; i++)
            {
                for (int k = i + 1; k < ls2.Count; k++)
                {
                    if (ls2[i] > ls2[k])
                    {
                        inversion_count += 1;
                    }
                }
            }

            int invCont = inversion_count;
            bool inversEven = false;
            bool widthEven = false;
            bool zeroOdd = false;
            if (invCont % 2 == 0)
            {
                inversEven = true;
            }
            else
                inversEven = false;
            if (this.dim % 2 == 0)
                widthEven = true;
            else
                widthEven = false;

            if (widthEven)
                zeroOdd = !yIsEven;


            return (!widthEven && inversEven) || (widthEven && (zeroOdd == inversEven));

        }

    }
}