using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;

namespace TermalDesign.App
{
    public class NullMutation : IMutation
    {
        public bool IsOrdered { get; }
        public void Mutate(IChromosome chromosome, float probability)
        {
            
        }
    }
}