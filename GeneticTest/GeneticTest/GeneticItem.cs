namespace GeneticTest
{
    abstract class GeneticItem
    {
        protected int fitness;

        public abstract int Fitness { get; }

        public abstract GeneticItem GenerateOffspring(GeneticItem parent2, double mutationChance);
        protected abstract void calcFitness();

        public abstract void SetValue(TParams[] parameters);
    }

    abstract class TParams
    {

    }

    class SudokuParams : TParams
    {
        public int[,] Board;

        public SudokuParams(int[,] initalBoard)
        {
            Board = initalBoard;
        }
    }
}