using System;
using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;

namespace TermalDesign.App
{
    public class IntegerCrossover : ICrossover
    {
        private static readonly Random Random = new Random();
        public bool IsOrdered => false;
        public IList<IChromosome> Cross(IList<IChromosome> parents)
        {
            int cut1 = Random.Next(1, 3);
            int cut2 = Random.Next(3, 5);

            var child1 = parents[0].Clone();
            var child2 = parents[1].Clone();

            for (int i = cut1; i < cut2; i++)
            {
                child1.ReplaceGene(i, parents[1].GetGene(i));
                child2.ReplaceGene(i, parents[0].GetGene(i));
            }

            return new List<IChromosome>{child1, child2};
        }

        public int ParentsNumber => 2;
        public int ChildrenNumber => 2;
        public int MinChromosomeLength => 6;
    }
}