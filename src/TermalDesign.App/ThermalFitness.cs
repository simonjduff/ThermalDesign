using System;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

namespace TermalDesign.App
{
    public class ThermalFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            var genome = chromosome as ThermalGenome;

            foreach (var gene in genome)
            {
                if (gene.U > gene.Bounds.Max
                    || gene.U < gene.Bounds.Min)
                {
                    return -500;
                }
            }

            var genes = Enumerable.Range(0, chromosome.Length).Select(chromosome.GetGene);
            return genes.Sum(g => (int)g.Value);
        }
    }
}