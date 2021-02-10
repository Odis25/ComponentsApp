using ComponentsApp.Data.Models;

namespace ComponentsApp.Services.Interfaces
{
    public interface ICalculationService
    {
        double Calculate(SamplePoint point1, SamplePoint point2);
    }
}