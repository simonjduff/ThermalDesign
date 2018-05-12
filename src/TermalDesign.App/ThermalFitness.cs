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
            var genes = Enumerable.Range(0, chromosome.Length).Select(chromosome.GetGene);
            return genes.Sum(g => (int)g.Value);
        }
    }
}