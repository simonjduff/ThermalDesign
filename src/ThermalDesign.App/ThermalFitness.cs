using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

namespace ThermalDesign.App
{
    public class ThermalFitness : IFitness
    {
        private readonly ModelCase[] _modelCases;

        public ThermalFitness()
        {
            _modelCases = new[]
            {
                new ModelCase(false, false, (0, 0), (0, 0), (121, 450)), // 1
                new ModelCase(false, false, (0, 0), (125, 350), (125, 110)), // 2
                new ModelCase(false, false, (0, 0), (135, 190), (135, 110)), // 3
                new ModelCase(false, false, (155, 400), (0, 0), (0, 0)), // 4a
                new ModelCase(false, true, (0, 0), (132, 109), (132, 91)), // 4b
                new ModelCase(true, false, (155, 150), (0, 0), (0, 0)),// 5a
                new ModelCase(false, false, (0, 0), (132, 160), (136, 140)), // 5b
                new ModelCase(true, true, (150, 130), (0, 0), (130, 78)), // 6a
                new ModelCase(false, true, (0, 0), (127, 109), (130, 45)), // 6b
                new ModelCase(false, true, (0, 0), (131, 150), (138, 50)) // 7
            };
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
            

            var caseOutputs = _modelCases.Select(c => c.Run(uValues)).ToList();
            var failure = caseOutputs.Sum(c => ModelCase.Failure(c));
            if (failure < 0)
            {
                return failure;
            }

            var outputDiff = caseOutputs.Min(c => c["g"].T);
            return outputDiff;
        }
    }
}