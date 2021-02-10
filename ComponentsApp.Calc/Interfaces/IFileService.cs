using ComponentsApp.Data.Models;

namespace ComponentsApp.Services.Interfaces
{
    public interface IFileService
    {
        //void SaveToPdf(ResultData data, string filePath);

        bool SaveSamples(SamplePoint[] samplePoints);
        SamplePoint[] LoadSamples();
    }
}
