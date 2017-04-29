using System;

namespace GeneticTest
{
    public class ThreadSafeRandom
    {
        private static readonly Random rng = new Random();

        public static int Next()
        {
            return rng.Next();
        }

        public static int Next(int maxValue)
        {
            return rng.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return rng.Next(minValue, maxValue);
        }

        public static double Nextf()
        {
            return rng.NextDouble();
        }

    }
}