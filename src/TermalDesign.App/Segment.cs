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
        private readonly bool _bypass;
        private const double AmbientTemperature = 17.5;
        public const double Density = 0.277;
        public const double SpecificHeatCapacity = 2400;

        public Segment(double u,
            double area,
            bool bypass = false,
            params (double T, int Q)[] inputs)
        {
            _inputs = inputs;
            _u = u;
            _area = area;
            _bypass = bypass;
        }

        public double OutputTemperature
        {
            get
            {
                if (_bypass)
                {
                    return CalculateInputTemperature();
                }

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