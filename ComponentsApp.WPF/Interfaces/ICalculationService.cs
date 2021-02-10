using ComponentsApp.UI.Models;

namespace ComponentsApp.UI.Interfaces
{
    public interface ICalculationService
    {
        double Calculate(SamplePoint samplePoint1, SamplePoint samplePoint2);
    }
}
