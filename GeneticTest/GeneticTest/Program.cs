using System;
using System.Diagnostics;

namespace GeneticTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Boards
            int[,] board2 =
            {
                {0, 7, 1, 0, 3, 0, 0, 0, 5},
                {0, 4, 2, 0, 0, 5, 3, 0, 7},
                {0, 0, 0, 0, 7, 2, 0, 6, 4},

                {0, 0, 8, 0, 0, 7, 0, 4, 3},
                {0, 0, 5, 0, 0, 0, 6, 0, 0},
                {3, 2, 0, 8, 0, 0, 5, 0, 0},

                {8, 5, 0, 1, 4, 0, 0, 0, 0},
                {7, 0, 6, 2, 0, 0, 4, 5, 0},
                {2, 0, 0, 0, 5, 0, 8, 9, 0}
            };

            int[,] board =
            {
                {0, 5, 0, 4, 0, 0, 0, 0, 1},
                {0, 0, 1, 0, 6, 0, 7, 0, 0},
                {0, 0, 8, 7, 3, 1, 9, 0, 0},

                {8, 0, 0, 6, 0, 0, 0, 3, 5},
                {2, 0, 0, 1, 0, 5, 0, 0, 8},
                {1, 7, 0, 0, 0, 4, 0, 0, 6},

                {0, 0, 2, 9, 1, 3, 8, 0, 0},
                {0, 0, 3, 0, 4, 0, 5, 0, 0},
                {7, 0, 0, 0, 0, 6, 0, 2, 0}
            };

            // http://www.websudoku.com/?level=1&set_id=797894922
            int[,] board3 =
            {
                {0, 9, 0, 1, 3, 0, 8, 7, 0},
                {0, 1, 0, 0, 5, 0, 0, 9, 0},
                {7, 3, 0, 0, 0, 0, 5, 6, 0},

                {0, 2, 7, 8, 0, 0, 4, 0, 6},
                {0, 0, 0, 0, 9, 0, 0, 0, 0},
                {6, 0, 1, 0, 0, 7, 9, 2, 0},

                {0, 4, 2, 0, 0, 0, 0, 5, 3},
                {0, 8, 0, 0, 6, 0, 0, 1, 0},
                {0, 6, 8, 0, 2, 1, 0, 4, 0}
            };
            // http://www.websudoku.com/?select=1&level=1&set_id=1
            int[,] board1 =
            {
                {9, 1, 0, 7, 0, 0, 0, 0, 0},
                {0, 3, 2, 6, 0, 9, 0, 8, 0},
                {0, 0, 7, 0, 8, 0, 9, 0, 0},

                {0, 8, 6, 0, 3, 0, 1, 7, 0},
                {3, 0, 0, 0, 0, 0, 0, 0, 6},
                {0, 5, 1, 0, 2, 0, 8, 4, 0},

                {0, 0, 9, 0, 5, 0, 3, 0, 0},
                {0, 2, 0, 3, 0, 1, 4, 9, 0},
                {0, 0, 0, 0, 0, 2, 0, 6, 1}
            };
            int[,] board1s =
            {
                {9, 1, 8, 7, 4, 5, 6, 3, 2},
                {5, 3, 2, 6, 1, 9, 7, 8, 4},
                {6, 4, 7, 2, 8, 3, 9, 1, 5},

                {2, 8, 6, 5, 3, 4, 1, 7, 9},
                {3, 9, 4, 1, 0, 8, 2, 5, 6},
                {7, 5, 1, 9, 2, 6, 8, 4, 3},

                {1, 6, 9, 4, 5, 0, 3, 2, 8},
                {8, 2, 5, 3, 6, 1, 4, 9, 7},
                {4, 7, 3, 8, 9, 2, 0, 6, 1}
            };
            #endregion

            var stp = new Stopwatch();

            // Generically reworked
            for (int i = 0; i < 100; i++)
            {    
                stp.Start();

                var ntm = new GenericGame<GeneticSudoku3X3>(2000, 0.5, 100, new SudokuParams(board1));

                ntm.CompeteTillStagnation(15);

                Console.WriteLine("Best so far: " + ntm.GetHighestFitness + " in " + stp.ElapsedMilliseconds + " milliseconds");
                stp.Reset();
                if (ntm.GetHighestFitness == 3*81)
                    PrintBoard(ntm.GetBest.Board);
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static void PrintBoard(int[,] board)
        {
            Console.WriteLine();
            for (int i = 0; i < board.GetLength(0); ++i)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (j % 3 == 0)
                        Console.Write(" ");
                    Console.Write(board[i, j]);
                }
                if (i % 3 == 2)
                    Console.Write('\n');
                Console.Write('\n');
            }
            Console.WriteLine();
        }
    }
}
