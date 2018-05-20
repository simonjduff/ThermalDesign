using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using TermalDesign.App.Segments;

namespace TermalDesign.App
{
    public class ModelCase
    {
        public IDictionary<char, Segment> Segments = new Dictionary<char, Segment>(7);

        public static Func<string,double> UFetcher(IList<int> genes)
        {
            return id =>
            {

                switch (id)
                {
                    case "a":
                        return genes[0];
                    case "b":
                        return genes[1];
                    case "c":
                        return genes[2];
                    case "d":
                        return genes[3];
                    case "e":
                        return genes[4];
                    case "f":
                        return genes[5];
                    case "g":
                        return 5;
                    default:
                        throw new Exception($"Unknown gene id {id}");
                }
            };
        }

        public ModelCase(bool bypassA = false, bool bypassE = false, params(double T, int Q)[] inputs)
        {
            Segments.Add('a', Bypass(bypassA, () => 
                new SegmentSps("a", uFunc => (inputs[0].T, inputs[0].Q)), "a", uFunc => (inputs[0].T, inputs[0].Q)));
            
            Segments.Add('b', new SegmentSps("b", uFunc => (inputs[1].T, inputs[1].Q)));
            Segments.Add('c', new SegmentSps("c", 
                uFunc => Segments['a'].Output(uFunc).TQ,
                uFunc => Segments['b'].Output(uFunc).TQ
            ));
            Segments.Add('d', new Segment("d", Area(7400), uFunc => Segments['c'].Output(uFunc).TQ));
            Segments.Add('e', Bypass(bypassE, () => 
                new SegmentSps("e", uFunc => (inputs[2].T, inputs[2].Q)), "e", uFunc => (inputs[2].T, inputs[2].Q)));
            Segments.Add('f', new Segment("f", Area(10000),
                uFunc => (Segments['d'].Output(uFunc).TQ),
                uFunc => (Segments['e'].Output(uFunc).TQ)
            ));
            Segments.Add('g', new Segment("g", Area(1600), uFunc => Segments['f'].Output(uFunc).TQ));
        }

        private Segment Bypass(bool bypass, 
            Func<Segment> constructor, 
            string id, 
            params Func<Func<string, double>, (double T, int Q)>[] inputs)
        {
            if (!bypass)
            {
                return constructor();
            }

            return new SegmentBypass(id, inputs);
        }

        public IDictionary<string, SegmentOutput> Run(int[] genes)
        {
            return Segments.Values.Select(s => s.Output(UFetcher(genes))).ToDictionary(i => i.Id);
        }

        public static double Failure(IDictionary<string, SegmentOutput> segments)
        {
            double failure = 0;
            if (segments["d"].InputTemperature > 140)
            {
                failure += segments["d"].InputTemperature - 140;
            }

            if (segments["f"].InputTemperature > 140)
            {
                failure += segments["f"].InputTemperature - 140;
            }

            if (segments["g"].T > 90)
            {
                failure += segments["g"].T - 90;
            }

            return failure * -1;
        }

        private double Area(int length)
        {
            return length * Math.PI * 0.359;
        }
    }
}