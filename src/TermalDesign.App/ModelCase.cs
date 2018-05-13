using System;
using System.Collections.Generic;

namespace TermalDesign.App
{
    public class ModelCase
    {
        public IDictionary<char, Segment> Segments = new Dictionary<char, Segment>(7);

        public ModelCase(IList<int> genes, params (double T, int Q)[] inputs)
        {;
            Segments.Add('a', new SegmentSps((inputs[0].T, inputs[0].Q), genes[0]));
            Segments.Add('b', new SegmentSps((inputs[1].T, inputs[1].Q), genes[1]));
            Segments.Add('c', new SegmentSps(new[]
            {
                (Segments['a'].OutputTemperature, Segments['a'].Q),
                (Segments['b'].OutputTemperature, Segments['b'].Q),
            }, genes[2]));
            Segments.Add('d', new Segment((Segments['c'].OutputTemperature, Segments['c'].Q), genes[3], Area(7400)));
            Segments.Add('e', new SegmentSps((inputs[2].T, inputs[2].Q), genes[4]));
            Segments.Add('f', new Segment(new[]
            {
                (Segments['d'].OutputTemperature, Segments['d'].Q),
                (Segments['e'].OutputTemperature, Segments['e'].Q)
            }, genes[5], Area(10000)));
            Segments.Add('g', new Segment((Segments['f'].OutputTemperature, Segments['f'].Q), 7, Area(1600)));
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