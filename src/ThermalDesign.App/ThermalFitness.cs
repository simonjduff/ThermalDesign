using System;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using ThermalDesign.App.Models;

namespace ThermalDesign.App
{
    public class ThermalFitness : IFitness
    {
        private readonly IModel _model;

        public ThermalFitness(IModel model)
        {
            _model = model;
        }

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

            var genes = genome.ToList();

            var uValues = genes.Select(g => g.U).ToArray();
            

            var caseOutputs = _model.Cases.Select(c => c.Run(uValues)).ToList();
            var failure = caseOutputs.Sum(c => _model.Failure(c));
            if (failure < 0)
            {
                return failure;
            }

            var lastKey = caseOutputs.SelectMany(c => c.Keys).Max().First().ToString();

            var outputDiff = caseOutputs.Min(c => c[lastKey].T);
            return outputDiff;// Math.Abs(Math.Abs(46 - outputDiff) * -1);
        }
    }
}