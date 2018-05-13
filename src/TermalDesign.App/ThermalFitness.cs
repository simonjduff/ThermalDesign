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

            var genes = genome.ToList();

            var cases = new[]
            {
                new ModelCase(genes.Select(g => g.U).ToList(),
                    false, (0, 0), (0, 0), (121, 450)), // 1
                new ModelCase(genes.Select(g => g.U).ToList(),
                    false, (0, 0), (125, 350), (125, 110)), // 2
                new ModelCase(genes.Select(g => g.U).ToList(),
                    false, (0, 0), (135, 190), (135, 110)), // 3
                new ModelCase(genes.Select(g => g.U).ToList(),
                    false, (155, 400), (0, 0), (0, 0)), // 4a
                new ModelCase(genes.Select(g => g.U).ToList(),
                    false, (0, 0), (132, 109), (132, 91)), // 4b
                new ModelCase(genes.Select(g => g.U).ToList(),
                    true, (155, 150), (0, 0), (0, 0)),// 5a
                new ModelCase(genes.Select(g => g.U).ToList(),
                    false, (0, 0), (132, 160), (136, 140)), // 5b
                new ModelCase(genes.Select(g => g.U).ToList(),
                    true, (150, 130), (0, 0), (130, 78)), // 6a
                new ModelCase(genes.Select(g => g.U).ToList(),
                    false, (0, 0), (127, 109), (130, 45)), // 6b
                new ModelCase(genes.Select(g => g.U).ToList(),
                    false, (0, 0), (131, 150), (138, 50)) // 7
            };

            var failure = cases.Sum(c => c.Failure());
            if (failure < 0)
            {
                return failure;
            }

            var outputDiff = cases.Min(c => c.Segments['g'].OutputTemperature);
            return outputDiff;
        }
    }
}