using System;
using System.Collections.Generic;

namespace TermalDesign.App
{
    public class ModelCase
    {
        public IDictionary<char, Segment> Segments = new Dictionary<char, Segment>(7);

        public ModelCase(IList<int> genes, bool bypassA = false, bool bypassE = false, params(double T, int Q)[] inputs)
        {
            Segments.Add('a', new SegmentSps(genes[0], bypassA, (inputs[0].T, inputs[0].Q)));
            Segments.Add('b', new SegmentSps(genes[1], inputs:(inputs[1].T, inputs[1].Q)));
            Segments.Add('c', new SegmentSps(genes[2], inputs:new[]
            {
                (Segments['a'].OutputTemperature, Segments['a'].Q),
                (Segments['b'].OutputTemperature, Segments['b'].Q),
            }));
            Segments.Add('d', new Segment(genes[3], Area(7400), inputs:(Segments['c'].OutputTemperature, Segments['c'].Q)));
            Segments.Add('e', new SegmentSps(genes[4], bypass: bypassE, inputs:(inputs[2].T, inputs[2].Q)));
            Segments.Add('f', new Segment(genes[5], Area(10000), inputs: new[]
            {
                (Segments['d'].OutputTemperature, Segments['d'].Q),
                (Segments['e'].OutputTemperature, Segments['e'].Q)
            }));
            Segments.Add('g', new Segment(5, Area(1600), inputs:(Segments['f'].OutputTemperature, Segments['f'].Q)));
        }

        public double Failure()
        {
            double failure = 0;
            if (Segments['d'].InputTemperature > 140)
            {
                failure += Segments['d'].InputTemperature - 140;
            }

            if (Segments['f'].InputTemperature > 140)
            {
                failure += Segments['f'].InputTemperature - 140;
            }

            if (Segments['g'].OutputTemperature > 90)
            {
                failure += Segments['g'].OutputTemperature - 90;
            }

            return failure * -1;
        }

        private double Area(int length)
        {
            return length * Math.PI * 0.359;
        }
    }
}