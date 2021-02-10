﻿using ComponentsApp.UI.Models;

namespace ComponentsApp.UI.Interfaces
{
    public interface IFileService
    {
        void SaveToPdf(ResultData data, string filePath);

        bool SaveSamples(SamplePoint[] samplePoints);
        SamplePoint[] LoadSamples();
    }
}
