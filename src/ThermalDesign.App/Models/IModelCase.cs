using System.Collections.Generic;
using ThermalDesign.App.Segments;

namespace ThermalDesign.App.Models
{
    public interface IModelCase
    {
        IDictionary<string, Segment> Segments { get; }
        IDictionary<string, SegmentOutput> Run(int[] genes);
    }
}