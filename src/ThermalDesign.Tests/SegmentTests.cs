using System;
using TermalDesign.App.Segments;
using Xunit;

namespace ThermalDesign.Tests
{
    public class SegmentTests
    {
        private const double RoundingValue = 0.1;

        [Fact]
        public void SPSMid()
        {
            var segment = new SegmentSps("id", a => (149.7, 200), a => (132.8, 250));

            Assert.True(Math.Abs(136.3 - segment.Output(i => 5).T) < RoundingValue);
        }

        [Fact]
        public void SPSIn()
        {
            var segment = new SegmentSps("id", a => (160.0, 200));
        
            Assert.True(Math.Abs(149.7 - segment.Output(i => 5).T) < RoundingValue);
        }

        [Fact]
        public void URF()
        {
            var segment = new Segment("id", 0.359 * Math.PI * 10000, a => (120.8, 450), a => (135.5, 400) );

            var outputTemperature = segment.Output(i => 5).T;
            Assert.True(Math.Abs(117.3 - outputTemperature) < RoundingValue, $"Diff {117.3 - outputTemperature}");
        }
    }
}
