using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using ThermalDesign.App.Models;
using ThermalDesign.App.Models.FirstModel;
using ThermalDesign.App.Models.SecondModel;

namespace ThermalDesign.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //var model = new FirstModel();
            var model = new SecondModel();

            var selection = new EliteSelection();
            var crossover = new IntegerCrossover();
            var mutation = new NewValueMutation();
            var fitness = new ThermalFitness(model);
            var chromosome = model.Genome;
            var population = new Population(500, 5000, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(100);
            ga.MutationProbability = 0.5f;
            //ga.CrossoverProbability = 0.9f;

            ga.GenerationRan += (sender, eventargs) =>
            {
                var geneticAlgorithm = sender as GeneticAlgorithm;

                if (geneticAlgorithm.GenerationsNumber % 3 == 0)
                {
                    return;}

                var genome = geneticAlgorithm.BestChromosome as ThermalGenome;
                Console.WriteLine(genome.Fitness);
            };

            Console.WriteLine("GA running...");
            ga.Start();

            var gaBestChromosome = ga.BestChromosome as ThermalGenome;
            Console.WriteLine("Best solution found has {0} fitness.", gaBestChromosome.Fitness);
            Console.WriteLine("Values:");
            Console.WriteLine(string.Join(",", gaBestChromosome.Select(t => t.U.ToString())));

            Console.WriteLine(model.Output(gaBestChromosome.Select(t => t.U).ToArray()));
        }
    }
}
