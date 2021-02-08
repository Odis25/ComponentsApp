using ComponentsApp.WPF.Models;

namespace ComponentsApp.WPF.Interfaces
{
    public interface ICalculationService
    {
        double Calculate(SamplePoint samplePoint1, SamplePoint samplePoint2);
    }
}
