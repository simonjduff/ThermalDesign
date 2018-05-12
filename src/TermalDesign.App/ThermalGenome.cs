using System;
using System.Security.Principal;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

namespace TermalDesign.App
{
    public class ThermalGenome : ChromosomeBase
    {
        private const int NumberOfNodes = 7;
        private readonly (int Min, int Max)[] _bounds = {(1, 10), (1, 10), (1, 10), (1, 10), (1, 10), (1, 10), (1, 2)};

        public ThermalGenome() : base(NumberOfNodes)
        {
            CreateGenes();
        }

        public override Gene GenerateGene(int geneIndex)
        {
            if (NumberOfNodes != _bounds.Length)
            {
                throw new Exception($"Number of nodes {NumberOfNodes} does not match number of Bounds {_bounds.Length}");
            }

            if (geneIndex < 0 || geneIndex > NumberOfNodes - 1)
            {
                throw new Exception($"geneIndex outside of bounds of chromosome. Expected 0-{NumberOfNodes-1} but was {geneIndex}");
            }

            var bound = _bounds[geneIndex];

            var result = RandomizationProvider.Current.GetInt(bound.Min, bound.Max + 1);

            return new Gene(result);
        }

        public override IChromosome CreateNew()
        {
            return new ThermalGenome();
        }
    }
}