﻿using ComponentsApp.WPF.Models;

namespace ComponentsApp.WPF.Interfaces
{
    public interface IFileService
    {
        void SaveToPdf(ResultData data, string filePath);
    }
}