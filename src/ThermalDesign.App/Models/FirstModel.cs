using System.Text;

namespace ThermalDesign.App.Models
{
    public class FirstModel : IModel
    {
        public ModelCase[] Cases { get; }
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

        public FirstModel()
        {
            Cases = new[]
            {
                new ModelCase(null, (0, 0), (0, 0), (121, 450)), // 1
                new ModelCase(null, (0, 0), (125, 350), (125, 110)), // 2
                new ModelCase(null, (0, 0), (135, 190), (135, 110)), // 3
                new ModelCase(null, (155, 400), (0, 0), (0, 0)), // 4a
                new ModelCase(new []{"e"}, (0, 0), (132, 109), (132, 91)), // 4b
                new ModelCase(new []{"a"}, (155, 150), (0, 0), (0, 0)),// 5a
                new ModelCase(null, (0, 0), (132, 160), (136, 140)), // 5b
                new ModelCase(new []{"a","e"}, (150, 130), (0, 0), (130, 78)), // 6a
                new ModelCase(new []{"e"}, (0, 0), (127, 109), (130, 45)), // 6b
                new ModelCase(new []{"e"}, (0, 0), (131, 150), (138, 50)) // 7
            };

            Genome = new ThermalGenome((1, 100), (1, 50), (1, 50), (3, 10), (1, 50), (3, 10));
        }
    }
}