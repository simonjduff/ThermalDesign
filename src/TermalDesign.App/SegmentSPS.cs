using System.Collections.Generic;

namespace TermalDesign.App
{
    public class SegmentSps : Segment
    {
        public SegmentSps(IEnumerable<(double T, int Q)> inputs,
            double u) : base(inputs, u, 2000.0)
        {
            
        }

        public SegmentSps((double T, int Q) inputs,
            double u) : base(inputs, u, 2000.0)
        {

        }
    }
}