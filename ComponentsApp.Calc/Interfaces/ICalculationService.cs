using ComponentsApp.Data.Models;

namespace ComponentsApp.Services.Interfaces
{
    public interface ICalculationService
    {
        Result Calculate(SamplePoint point1, SamplePoint point2, bool useDensity);
    }
}