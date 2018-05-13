using System;
using System.Collections.Generic;
using System.Linq;

namespace TermalDesign.App
{
    public class Segment
    {
        private readonly IEnumerable<(double T, int Q)> _inputs;
        private readonly double _u;
        private readonly double _area;
        private const double AmbientTemperature = 17.5;
        public const double Density = 0.277;
        public const double SpecificHeatCapacity = 2400;

        public Segment(IEnumerable<(double T, int Q)> inputs,
            double u,
            double area)
        {
            _inputs = inputs;
            _u = u;
            _area = area;
        }

        public Segment((double T, int Q) inputs, 
            double u, 
            double area)
        {
            _inputs = new []{ inputs};
            _u = u;
            _area = area;
        }

        public double OutputTemperature
        {
            get
            {
                if (Q == 0)
                {
                    return AmbientTemperature;
                }

                return (CalculateInputTemperature() - AmbientTemperature)
                       * Math.Exp((-1 * _area * _u) / (MassPerSecond() * SpecificHeatCapacity))
                       + AmbientTemperature;
            }
        }

        public double InputTemperature => CalculateInputTemperature();

        private double CalculateInputTemperature()
        {
            return _inputs.Sum(i => i.T * i.Q) / _inputs.Sum(i => i.Q);
        }

        public int Q => _inputs.Sum(i => i.Q);

        public double MassPerSecond()
        {
            return Q * Density;
        }
    }
}