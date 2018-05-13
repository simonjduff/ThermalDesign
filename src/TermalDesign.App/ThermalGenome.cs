using System;
using System.Collections;
using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

namespace TermalDesign.App
{
    public class ThermalGenome : ChromosomeBase, IEnumerable<(int U, (int Min, int Max) Bounds)>
    {
        private const int NumberOfNodes = 6;
        public readonly (int Min, int Max)[] Bounds = {(1, 50), (1, 50), (1, 50), (3, 10), (1, 50), (3, 10)};

        public ThermalGenome() : base(NumberOfNodes)
        {
            CreateGenes();
        }

        public override Gene GenerateGene(int geneIndex)
        {
            if (NumberOfNodes != Bounds.Length)
            {
                throw new Exception($"Number of nodes {NumberOfNodes} does not match number of Bounds {Bounds.Length}");
            }

            if (geneIndex < 0 || geneIndex > NumberOfNodes - 1)
            {
                throw new Exception($"geneIndex outside of bounds of chromosome. Expected 0-{NumberOfNodes-1} but was {geneIndex}");
            }

            var bound = Bounds[geneIndex];

            var result = RandomizationProvider.Current.GetInt(bound.Min, bound.Max + 1);
            return new Gene(result);
        }

        public override IChromosome CreateNew()
        {
            return new ThermalGenome();
        }

        public IEnumerator<(int U, (int Min, int Max) Bounds)> GetEnumerator()
        {
            for (int i = 0; i < NumberOfNodes; i++)
            {
                var bound = Bounds[i];
                yield return ((int)GetGene(i).Value, bound);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}