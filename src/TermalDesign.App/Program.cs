using System;
using System.Linq;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

namespace TermalDesign.App
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 6)
            {
                var genes = args.Select(a => int.Parse(a)).ToList();

                var cases = new[]
                {
                    new ModelCase(genes, (0, 0), (0, 0), (121, 450)),
                    new ModelCase(genes, (0, 0), (125, 350), (125, 110)),
                    new ModelCase(genes, (0, 0), (135, 190), (135, 110)),
                    new ModelCase(genes, (155, 400), (0, 0), (0, 0)),
                    new ModelCase(genes, (0, 0), (132, 109), (132, 91)),
                    new ModelCase(genes, (155, 150), (0, 0), (0, 0)),
                    new ModelCase(genes, (0, 0), (132, 160), (136, 140)),
                    new ModelCase(genes, (150, 130), (0, 0), (130, 78)),
                    new ModelCase(genes, (0, 0), (127, 109), (130, 45)),
                    new ModelCase(genes, (0, 0), (131, 150), (138, 50))
                };

                for (int i = 0; i < cases.Length; i++)
                {
                    Console.WriteLine($"Case {i + 1} - GOut {cases[i].Segments['g'].OutputTemperature:0.0} DIn {cases[i].Segments['d'].InputTemperature:0.0}");
                }

                return;
            }

            var selection = new EliteSelection();
            var crossover = new IntegerCrossover();
            //var mutation = new NullMutation();
            var mutation = new NewValueMutation();
            var fitness = new ThermalFitness();
            var chromosome = new ThermalGenome();
            var population = new Population(50, 500, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(500);

            Console.WriteLine("GA running...");
            ga.Start();

            var gaBestChromosome = ga.BestChromosome as ThermalGenome;
            Console.WriteLine("Best solution found has {0} fitness.", gaBestChromosome.Fitness);
            Console.WriteLine("Values:");
            Console.WriteLine(string.Join(",", gaBestChromosome.Select(t => t.U.ToString())));
        }
    }
}
