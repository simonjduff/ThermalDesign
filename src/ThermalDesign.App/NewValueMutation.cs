using System.Runtime.CompilerServices;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;

namespace ThermalDesign.App
{
    public class NewValueMutation : MutationBase
    {
        private IRandomization _random;

        public NewValueMutation()
        {
            _random = RandomizationProvider.Current;
        }

        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            var genome = chromosome as ThermalGenome;

            if (_random.GetDouble() <= probability)
            {
                var index = _random.GetInt(0, chromosome.Length);
                var bound = genome.Bounds[index];
                genome.ReplaceGene(index, new Gene(_random.GetInt(bound.Min, bound.Max + 1)));
            }
        }
    }
}