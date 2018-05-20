using System.Collections.Generic;
using System.Text;
using ThermalDesign.App.Segments;

namespace ThermalDesign.App.Models.SecondModel
{
    public class SecondModel : IModel
    {
        public IModelCase[] Cases { get; }
        public ThermalGenome Genome { get; }
        public string Output(int[] genes)
        {
            StringBuilder sb = new StringBuilder();

            int i = 1;
            foreach (var c in Cases)
            {
                var result = c.Run(genes);
                sb.Append($"Case {(i++).ToString().PadLeft(2)} - NIn {result["n"].InputTemperature.ToString("0").PadLeft(3)} NOut {result["n"].T.ToString("0").PadLeft(3)}");
                foreach (var key in result.Keys)
                {
                    sb.Append($" {key} {result[key].T.ToString("0").PadLeft(3)}");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public double Failure(IDictionary<string, SegmentOutput> segments)
        {
            double failure = 0;
            if (segments["h"].InputTemperature > 120)
            {
                failure += segments["h"].InputTemperature - 120;
            }

            if (segments["m"].InputTemperature > 140)
            {
                failure += segments["m"].InputTemperature - 120;
            }

            if (segments["n"].InputTemperature > 90)
            {
                failure += (segments["n"].InputTemperature - 90) * 10;
            }

            failure += Failure(segments, "a", 40);
            failure += Failure(segments, "b", 40);
            failure += Failure(segments, "j", 43);
            failure += Failure(segments, "k", 43);

            return failure * -1;
        }

        public double Failure(IDictionary<string, SegmentOutput> segments, string name, int max)
        {
            if (segments[name].Q > 0 && segments[name].T < max)
            {
                return segments[name].T - max;
            }

            return 0d;
        }

        public SecondModel()
        {
            Cases = new[]
            {
                new SecondModelCase(null, (0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(130,100),(130,150),(130,150)), // 1
                new SecondModelCase(null, (0,0),(0,0),(130,96),(130,102),(135,73),(135,70),(0,0),(0,0),(135,110)), // 2
                new SecondModelCase(null, (0,0),(0,0),(130,48),(130,49),(130,47),(130,46),(0,0),(0,0),(130,110)), // 3
                new SecondModelCase(null, (155,201),(155,200),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0)), // 4a
                new SecondModelCase(null, (0,0),(0,0),(130,29),(130,42),(130,38),(0,0),(0,0),(0,0),(130,90)), // 4b
                new SecondModelCase(null, (155,150),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0)), // 5a
                new SecondModelCase(null, (0,0),(0,0),(130,39),(130,41),(130,41),(130,39),(0,0),(135,70),(135,80)), // 5b
                new SecondModelCase(null, (150,80),(150,50),(0,0),(0,0),(0,0),(0,0),(125,38),(0,0),(130,40)), // 6a
                new SecondModelCase(null, (0,0),(0,0),(125,29),(125,25),(125,25),(125,25),(0,0),(130,45),(0,0)), // 6b
                new SecondModelCase(null, (0,0),(0,0),(130,37),(130,37),(130,37),(130,36),(0,0),(135,50),(0,0)), // 7
                new SecondModelCase(null, (150,185),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0)) // 8
            };

            Genome = new ThermalGenome((1, 100), // a
                (1, 100), // b
                (1, 100), // c
                (1, 100), // d
                (1, 100), // e
                (1, 100), // f
                (1, 1), // g
                (3, 10), // h
                (1, 100), // i
                (1, 100), // j
                (1, 100), // k
                (1, 1), // l
                (3, 10), //m
                (6,6)); // n);
        }
    }
}