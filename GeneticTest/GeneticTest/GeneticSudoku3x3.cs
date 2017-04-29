using System;
using System.Linq;

namespace GeneticTest
{
    class GeneticSudoku3X3 : GeneticItem
    {
        public int[,] Board { get; private set; }
        private int[,] _initalBoard;

        public override int Fitness
        {
            get
            {
                if (fitness != 0)
                    return fitness;
                // Recalculate fitness
                calcFitness();
                return fitness;
            }
        }

        protected override void calcFitness()
        {
            fitness = 3 * 81;

            // Loop over rows & columns
            for (int i = 0; i < 9; i++)
            {
                var numsH = new int[9];
                var numsV = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    // Get the total count of numbers per row and per column
                    numsH[Board[i, j] - 1]++;
                    numsV[Board[j, i] - 1]++;
                }

                // Loop over the other
                for (int j = 0; j < 9; j++)
                {
                    fitness -= (numsH[j] - 1) * (numsH[j] - 1);
                    fitness -= (numsV[j] - 1) * (numsV[j] - 1);
                }
            }

            // And now each of the squares
            for(int i =0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    var numsS = new int[9];
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 3; l++)
                            numsS[Board[i*3 + k, j*3 + l] - 1]++;

                    // Deduct the squared points
                    for (int k = 0; k < 9; k++)
                        fitness -= (numsS[k] - 1)*(numsS[k] - 1);
                    
                }
        }

        public override void SetValue(TParams[] parameters)
        {
            var pars = parameters[0] as SudokuParams;
            _initalBoard = pars.Board;

            // Generate a random board based on the initial one
            Board = new int[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (pars.Board[i, j] != 0)
                        Board[i, j] = pars.Board[i, j];
                    else
                        Board[i, j] = ThreadSafeRandom.Next(1, 9);

            // And set the fitness
            calcFitness();
            fitness = 0;
        }

        public GeneticSudoku3X3()
        {
            fitness = 0;
        }

        public GeneticSudoku3X3(int[,] initalBoard)
        {
            _initalBoard = initalBoard;
            // Generate a random board based on the initial one
            Board = new int[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (initalBoard[i, j] != 0)
                        Board[i, j] = initalBoard[i, j];
                    else
                        Board[i, j] = ThreadSafeRandom.Next(1, 9);

            // And set the fitness
            calcFitness();
            fitness = 0;
        }

        public override GeneticItem GenerateOffspring(GeneticItem parent2, double mutationChance)
        {
            int[,] board = new int[9, 9];
            var p2 = parent2 as GeneticSudoku3X3;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (_initalBoard[i, j] != 0)
                        board[i, j] = _initalBoard[i, j];
                    else if (ThreadSafeRandom.Nextf() > mutationChance)
                        board[i, j] = p2.Board[i, j];
                    else
                        board[i, j] = board[i, j];


            return new GeneticSudoku3X3(board);
        }
    }
}