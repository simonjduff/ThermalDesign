using System.Collections.Generic;

namespace TermalDesign.App
{
    public class SegmentSps : Segment
    {
        public SegmentSps(double u, bool bypass = false, params(double T, int Q)[] inputs) 
            : base(u, 2000.0, bypass, inputs)
        {
            
        }
    }
}