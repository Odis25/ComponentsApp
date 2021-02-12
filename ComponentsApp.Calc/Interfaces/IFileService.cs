using ComponentsApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComponentsApp.Services.Interfaces
{
    public interface IFileService
    {
        void SaveToPdf(Result data, string filePath);

        Task<bool> SaveDataAsync(IEnumerable<SamplePoint> samplePoints);
        Task<IEnumerable<SamplePoint>> LoadDataAsync();
    }
}
