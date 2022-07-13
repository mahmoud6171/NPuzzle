using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading;
namespace NPuzzle
{
    class Program
    {
        public static List<int[,]> getAllCases(string folderPath)
        {
            string path = folderPath;
            string[] lines;
            int sz;
            int[,] arr;
            int i = 0, j = 0;
            List<int[,]> cases = new List<int[,]>();
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo flInfo in dir.GetFiles())
            {
                String name = flInfo.Name;
                long size = flInfo.Length;
                DateTime creationTime = flInfo.CreationTime;

                lines = System.IO.File.ReadAllLines(path + '/' + name);
                sz = Convert.ToInt32(lines[0]);
                arr = new int[sz, sz];
                i = 0;
                j = 0;
                foreach (string line in lines)
                {
                    if (i > 1)
                    {
                        string[] s = lines[i].Split(' ');
                        foreach (string ele in s)
                        {
                            if (ele != "")
                            {
                                arr[i - 2, j] = int.Parse(ele);
                                j++;
                            }
                        }
                    }
                    i++;
                    j = 0;
                }
                cases.Add(arr);
            }
            return cases;
        }

        public static void Main(string[] args)
        {
            bool loop = true;
            while (loop)
            {
                string folder = "";
                Console.WriteLine("Sample   Test Solvable Puzzles ?                     => 1");
                Console.WriteLine("Sample   Test Unsolvable Puzzles ?                   => 2");
                Console.WriteLine("Complete Test Solvable puzzles Manhattan & Hamming ? => 3");
                Console.WriteLine("Complete Test Solvable puzzles Manhattan Only ?      => 4");
                Console.WriteLine("Complete Test V. Large test case ?                   => 5");
                Console.WriteLine("Complete Test Unsolvable puzzles ?                   => 6");
                Console.Write("Enter The Tests Number                               => ");
                int v = int.Parse(Console.ReadLine());
                char way;

                switch (v)
                {
                    case 1:
                        folder = @"D:\3rd year\2nd term\Algorithm\NPuzzle\NPuzzle\test_cases\Sample Solvable Puzzles";
                        break;
                    case 2:
                        folder = @"D:\3rd year\2nd term\Algorithm\NPuzzle\NPuzzle\test_cases\Sample Unsolvable Puzzles\";
                        break;
                    case 3:
                        folder = @"D:\3rd year\2nd term\Algorithm\NPuzzle\NPuzzle\test_cases\Complete Solvable puzzles\Manhattan & Hamming\";
                        break;
                    case 4:
                        folder = @"D:\3rd year\2nd term\Algorithm\NPuzzle\NPuzzle\test_cases\Complete Solvable puzzles\Manhattan Only\";
                        break;
                    case 5:
                        folder = @"D:\3rd year\2nd term\Algorithm\NPuzzle\NPuzzle\test_cases\Complete V. Large test case\";
                        break;
                    case 6:
                        folder = @"D:\3rd year\2nd term\Algorithm\NPuzzle\NPuzzle\test_cases\Complete Unsolvable puzzles\";
                        break;
                }
                Stopwatch stopwatch = new Stopwatch();
                if (folder != "")
                {
                    List<int[,]> cases = new List<int[,]>();
                    cases = getAllCases(folder);
                    Console.WriteLine("Astar algorithm    ?                       => 1");
                    Console.WriteLine("BFS   algorithm    ?                       => 2");
                    Console.WriteLine("DFS   algorithm    ?                       => 3");
                    Console.Write("Enter The Alog Number                      => ");
                    int algo = int.Parse(Console.ReadLine());

                    Console.WriteLine("We Have " + cases.Count + " Tests");
                    way = 'm';
                    if (algo == 1)
                    {
                        if (v == 1 || v == 3)
                        {
                            Console.Write("which method you want  [M , H] ?");
                            way = char.Parse(Console.ReadLine());
                        }
                    }

                    for (int x = 0; x < cases.Count; x++)
                    {
                        for (int i = 0; i < cases[x].GetLength(0); i++)
                        {
                            for (int j = 0; j < cases[x].GetLength(0); j++)
                            {
                                Console.Write(cases[x][i, j] + " ");
                            }
                            Console.WriteLine();
                        }

                        Puzzle p = new Puzzle(cases[x]);
                        Node n = new Node(p);
                        Solvable solvable = new Solvable(p);
                        AStar aStar = new AStar(p, cases[x].GetLength(0));
                        BFSSearch bFSSearch = new BFSSearch(n, cases[x].GetLength(0));
                        DFSSearch dFSSearch = new DFSSearch(n, cases[x].GetLength(0));
                        if (solvable.isSolvable())
                        {
                            Console.WriteLine("Solveble....... :)");
                            List<Node> ll = new List<Node>();
                            if (algo == 1)
                            {
                                stopwatch.Start();
                                ll = aStar.Astar_seaech(way);
                                stopwatch.Stop();
                            }
                            else if (algo == 2)
                            {
                                stopwatch.Start();
                                ll = bFSSearch.BfsSolve();
                                stopwatch.Stop();
                            }
                            else if (algo == 3)
                            {
                                stopwatch.Start();
                                ll = dFSSearch.DfsSolve();
                                stopwatch.Stop();
                            }
                            // foreach (Node node in ll)
                            // {
                            //     Console.WriteLine(node.action);
                            //     node.puzzle.PrintPuzzle();
                            // }
                            Console.WriteLine("Total number of steps in : " + (ll.Count - 1));
                            Console.WriteLine("Total amount of time in search:  {0}  Second  , {1}  MS", stopwatch.ElapsedMilliseconds / 1000, stopwatch.ElapsedMilliseconds % 1000);
                            Console.WriteLine("---------------------------------------------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("Not solveble....... :(");
                            Console.WriteLine("---------------------------------------------------------------------------");
                        }
                    }
                }
                char k = ' ';
                Console.Write("You Want To Mack Another Operation [y,n] ");
                k = char.Parse(Console.ReadLine());
                if (k == 'y')
                {
                    loop = true;
                }
                else if (k == 'n')
                {
                    loop = false;
                }
            }
        }
    }
}
