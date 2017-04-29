using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticTest
{
    class GenericGame<T> where T : GeneticItem, new()
    {
        // Chance for a mutation to occur
        private double p;

        // The amount of individuals allowed to breed
        private int breedCount;

        // The population and its properties
        private List<T> population;
        private readonly int popSize;

        // Statistics
        private int _highestFitness;
        private int _bestIndex;

        public int GetHighestFitness
        {
            get
            {
                if (_highestFitness != -1)    // If the highest fitness has been calculated before, return it
                    return _highestFitness;
                for (int i = 0; i < popSize; i++)    // If it has not, calculate it and return it
                    if (population[i].Fitness > _highestFitness)
                    {
                        _highestFitness = population[i].Fitness;
                        _bestIndex = i;
                    }
                return _highestFitness;
            }
        }

        public T GetBest
        {
            get
            {
                if (_bestIndex != -1)    // If the best has already been calculated
                    return population[_bestIndex];
                for (int i = 0; i < popSize; i++)    // If it has not, calculate it and return it
                    if (population[i].Fitness > _highestFitness)
                    {
                        _highestFitness = population[i].Fitness;
                        _bestIndex = i;
                    }
                return population[_bestIndex];
            }
        }

        // Constructors
        public GenericGame(int populationSize, double mutationChance, int breederCount, params TParams[] parameters)
        {
            // Set these to -1
            _bestIndex = -1;
            _highestFitness = -1;

            // Set parameters
            popSize = populationSize;
            p = mutationChance;
            breedCount = breederCount;

            // Generate the population
            population = new List<T>(populationSize);
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new T());
                population[i].SetValue(parameters);
            }
        }

        public GenericGame(int populationSize, double mutationChance, double breederPercent, params TParams[] parameters) : this(populationSize, mutationChance, (int)(populationSize * breederPercent), parameters)
        {
        }

        public void CompeteTillStagnation(int counts)
        {
            int cnts = 0;
            int prevFitness = 0;

            while (cnts < counts)
            {
                // Set these to -1
                _highestFitness = -1;
                _bestIndex = -1;

                // Get the top n individuals
                var breeders = population.OrderByDescending(g => g.Fitness).ToArray();
                population.Clear();

                for (int j = 0; j < breedCount; j++)
                    population.Add(breeders[j]);

                // Each top individual gets y offspring:
                int y = (popSize - breedCount) / breedCount;

                for (int i = 0; i < breedCount; i++)
                    for (int j = 0; j < y; j++)
                        population.Add(population[i].GenerateOffspring(population[ThreadSafeRandom.Next(breedCount)], p) as T);

                if (GetHighestFitness == prevFitness)
                    cnts++;

                prevFitness = GetHighestFitness;
            }
        }

        public void HoldCompetition(int rounds)
        {
            // Set these to -1
            _bestIndex = -1;
            _highestFitness = -1;

            for (int r = 0; r < rounds; r++)
            {
                // Get the top n individuals
                var breeders = population.OrderByDescending(g => g.Fitness).ToArray();
                population.Clear();

                for (int j = 0; j < breedCount; j++)
                    population.Add(breeders[j]);

                // Each top individual gets y offspring:
                int y = (popSize - breedCount) / breedCount;

                for (int i = 0; i < breedCount; i++)
                    for (int j = 0; j < y; j++)
                        population.Add(population[i].GenerateOffspring(population[ThreadSafeRandom.Next(breedCount)], p) as T);
            }
        }


    }
}