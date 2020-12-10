using ComponentsApp.WPF.Models;

namespace ComponentsApp.WPF.Interfaces
{
    public interface ICalculationService
    {
        ResultData Calculate(SamplePoint samplePoint1, SamplePoint samplePoint2);
    }
}
