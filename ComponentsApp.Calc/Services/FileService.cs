using ComponentsApp.Data.Models;
using ComponentsApp.Services.Interfaces;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ComponentsApp.Services.Services
{
    public class FileService : IFileService
    {
        private readonly JsonSerializerOptions _options;

        public FileService()
        {
            _options = new JsonSerializerOptions { WriteIndented = true };
        }

        public async Task<bool> SaveDataAsync(IEnumerable<SamplePoint> samplePoints)
        {

            var data = JsonSerializer.Serialize(samplePoints, _options);
            try
            {
                await File.WriteAllTextAsync("SavedData.json", data);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<SamplePoint>> LoadDataAsync()
        {
            try
            {
                var data = await File.ReadAllTextAsync("SavedData.json");

                return JsonSerializer.Deserialize<IEnumerable<SamplePoint>>(data);
            }
            catch
            {
                return null;
            }
        }

        public void SaveToPdf(Result data, string filePath)
        {
            var document = PdfBuilderService.BuildDocument(data);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var pdfRenderer = new PdfDocumentRenderer(true);

            pdfRenderer.Document = document;

            pdfRenderer.RenderDocument();

            pdfRenderer.PdfDocument.Save(filePath);
        }
    }
}
