using System;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

namespace TermalDesign.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var selection = new EliteSelection();
            var crossover = new TwoPointCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new ThermalFitness();
            var chromosome = new ThermalGenome();
            var population = new Population(50, 70, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(100);

            Console.WriteLine("GA running...");
            ga.Start();

            Console.WriteLine("Best solution found has {0} fitness.", ga.BestChromosome.Fitness);
        }
    }
}
