using System;
using TermalDesign.App;
using Xunit;

namespace ThermalDesign.Tests
{
    public class SegmentTests
    {
        private const double RoundingValue = 0.1;

        [Fact]
        public void SPSMid()
        {
            var segment = new SegmentSps(new[] {(149.7, 200), (132.8, 250)}, 5);

            Assert.True(Math.Abs(136.3 - segment.OutputTemperature) < RoundingValue);
        }

        [Fact]
        public void SPSIn()
        {
            var segment = new SegmentSps(new[] { (160.0, 200)}, 5);

            Assert.True(Math.Abs(149.7 - segment.OutputTemperature) < RoundingValue);
        }

        [Fact]
        public void URF()
        {
            var segment = new Segment(new[] { (120.8, 450), (135.5, 400) }, 5, 0.359 * Math.PI * 10000);

            Assert.True(Math.Abs(117.3 - segment.OutputTemperature) < RoundingValue, $"Diff {117.3 - segment.OutputTemperature}");
        }
    }
}
