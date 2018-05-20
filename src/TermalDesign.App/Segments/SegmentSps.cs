using System;

namespace TermalDesign.App.Segments
{
    public class SegmentSps : Segment
    {
        public SegmentSps(string id, params Func<Func<string, double>, (double T, int Q)>[] inputs) 
            : base(id, 2000.0, inputs)
        {
            
        }
    }
}