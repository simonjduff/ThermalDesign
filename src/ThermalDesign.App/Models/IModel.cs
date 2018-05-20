namespace ThermalDesign.App.Models
{
    public interface IModel
    {
        ModelCase[] Cases { get; }
        ThermalGenome Genome { get; }
        string Output(int[] genes);
    }
}