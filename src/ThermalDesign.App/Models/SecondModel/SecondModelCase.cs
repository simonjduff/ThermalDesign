using System;
using System.Collections.Generic;
using System.Linq;
using ThermalDesign.App.Extensions;
using ThermalDesign.App.Segments;

namespace ThermalDesign.App.Models.SecondModel
{
    public class SecondModelCase : IModelCase
    {
        public IDictionary<string, Segment> Segments { get; } = new Dictionary<string, Segment>(7);

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
                        return genes[6];
                    case "h":
                        return genes[7];
                    case "i":
                        return genes[8];
                    case "j":
                        return genes[9];
                    case "k":
                        return genes[10];
                    case "l":
                        return genes[11];
                    case "m":
                        return genes[12];
                    case "n":
                        return genes[13];
                    default:
                        throw new Exception($"Unknown gene id {id}");
                }
            };
        }

        public SecondModelCase(string[] bypasses, params(double T, int Q)[] inputs)
        {
            Segment SegmentFunc(string id, double area, Func<Func<string, double>, (double T, int Q)>[] i) => new Segment(id, area, i);
            Segment SegmentSpsFunc(string id, double area, Func<Func<string, double>, (double T, int Q)>[] i) => new SegmentSps(id, i);

            BuildSegment(bypasses,
                "a", 0,
                SegmentSpsFunc, 
                uFunc => (inputs[0].T, inputs[0].Q));

            BuildSegment(bypasses,
                "b", 0,
                SegmentSpsFunc,
                uFunc => (inputs[1].T, inputs[1].Q));

            BuildSegment(bypasses,
                "c", 0,
                SegmentSpsFunc,
                uFunc => (inputs[2].T, inputs[2].Q));

            BuildSegment(bypasses,
                "d", 0,
                SegmentSpsFunc,
                uFunc => (inputs[3].T, inputs[3].Q));

            BuildSegment(bypasses,
                "e", 0,
                SegmentSpsFunc,
                uFunc => (inputs[4].T, inputs[4].Q));

            BuildSegment(bypasses,
                "f", 0,
                SegmentSpsFunc,
                uFunc => (inputs[5].T, inputs[5].Q));

            BuildSegment(bypasses, 
                "g", 0,
                SegmentSpsFunc,
                uFunc => Segments["a"].Output(uFunc).TQ,
                uFunc => Segments["b"].Output(uFunc).TQ,
                uFunc => Segments["c"].Output(uFunc).TQ,
                uFunc => Segments["d"].Output(uFunc).TQ,
                uFunc => Segments["e"].Output(uFunc).TQ,
                uFunc => Segments["f"].Output(uFunc).TQ);

            BuildSegment(bypasses,
                "h", Area(7400),
                SegmentFunc, 
                uFunc => Segments["g"].Output(uFunc).TQ);

            BuildSegment(bypasses,
                "i", 0,
                SegmentSpsFunc,
                uFunc => (inputs[6].T, inputs[6].Q));

            BuildSegment(bypasses,
                "j", 0,
                SegmentSpsFunc,
                uFunc => (inputs[7].T, inputs[7].Q));

            BuildSegment(bypasses,
                "k", 0,
                SegmentSpsFunc,
                uFunc => (inputs[8].T, inputs[8].Q));

            BuildSegment(bypasses,
                "l", 0,
                SegmentSpsFunc,
                uFunc => Segments["i"].Output(uFunc).TQ,
                uFunc => Segments["j"].Output(uFunc).TQ,
                uFunc => Segments["k"].Output(uFunc).TQ);

            BuildSegment(bypasses,
                "m", Area(10000),
                SegmentFunc,
                uFunc => Segments["h"].Output(uFunc).TQ,
                uFunc => Segments["l"].Output(uFunc).TQ);

            BuildSegment(bypasses,
                "n", Area(1600),
                SegmentFunc,
                uFunc => Segments["m"].Output(uFunc).TQ);
        }

        private void BuildSegment(string[] bypasses,
            string id,
            double area,
            Func<string, double, Func<Func<string, double>, (double T, int Q)>[], Segment> constructor, 
            params Func<Func<string, double>, (double T, int Q)>[] inputs)
        {
            Segments.Add(id, !bypasses.ContainsSafe(id) ? constructor(id, area, inputs) : new SegmentBypass(id, inputs));
        }

        public IDictionary<string, SegmentOutput> Run(int[] genes)
        {
            return Segments.Values.Select(s => s.Output(UFetcher(genes))).ToDictionary(i => i.Id);
        }

        private double Area(int length)
        {
            return length * Math.PI * 0.359;
        }
    }
}