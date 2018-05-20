using System.Collections.Generic;
using ThermalDesign.App.Segments;

namespace ThermalDesign.App.Models
{
    public interface IModel
    {
        IModelCase[] Cases { get; }
        ThermalGenome Genome { get; }
        string Output(int[] genes);
        double Failure(IDictionary<string, SegmentOutput> segments);
    }
}