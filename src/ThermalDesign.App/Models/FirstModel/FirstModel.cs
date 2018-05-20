using System.Collections.Generic;
using System.Text;
using ThermalDesign.App.Segments;

namespace ThermalDesign.App.Models.FirstModel
{
    public class FirstModel : IModel
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
                sb.AppendLine($"Case {i++} - GIn {result["g"].InputTemperature:0} GOut {result["g"].T:0} DIn {result["d"].InputTemperature:0} FIn {result["f"].InputTemperature:0}");
            }

            return sb.ToString();
        }

        public double Failure(IDictionary<string, SegmentOutput> segments)
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

        public FirstModel()
        {
            Cases = new[]
            {
                new FirstModelCase(null, (0, 0), (0, 0), (121, 450)), // 1
                new FirstModelCase(null, (0, 0), (125, 350), (125, 110)), // 2
                new FirstModelCase(null, (0, 0), (135, 190), (135, 110)), // 3
                new FirstModelCase(null, (155, 400), (0, 0), (0, 0)), // 4a
                new FirstModelCase(new []{"e"}, (0, 0), (132, 109), (132, 91)), // 4b
                new FirstModelCase(new []{"a"}, (155, 150), (0, 0), (0, 0)),// 5a
                new FirstModelCase(null, (0, 0), (132, 160), (136, 140)), // 5b
                new FirstModelCase(new []{"a","e"}, (150, 130), (0, 0), (130, 78)), // 6a
                new FirstModelCase(new []{"e"}, (0, 0), (127, 109), (130, 45)), // 6b
                new FirstModelCase(new []{"e"}, (0, 0), (131, 150), (138, 50)) // 7
            };

            Genome = new ThermalGenome((1, 100), (1, 50), (1, 50), (3, 10), (1, 50), (3, 10));
        }
    }
}