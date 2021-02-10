﻿using ComponentsApp.UI.Infrastructure.Commands.Base;
using ComponentsApp.UI.Interfaces;
using ComponentsApp.UI.Models;
using ComponentsApp.UI.Services;
using ComponentsApp.UI.ViewModels.Base;
using Microsoft.Win32;

namespace ComponentsApp.UI.ViewModels
{
    internal class ResultWindowVm : ViewModel
    {
        private ResultData _resultData;
        private RelayCommand _saveToPdfCommand;

        public ResultData ResultData
        {
            get { return _resultData; }
            set { _resultData = value; }
        }

        public RelayCommand SaveToPdfCommand
        {
            get
            {
                if (_saveToPdfCommand == null)
                {
                    _saveToPdfCommand = new RelayCommand(obj =>
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Pdf Document|*.pdf";
                        sfd.Title = "Save an Pdf File";
                        if (sfd.ShowDialog() == true)
                        {
                            IFileService fileService = new FileService();
                            fileService.SaveToPdf(ResultData, sfd.FileName);
                        }
                    });
                }
                return _saveToPdfCommand;
            }
        }
    }
}
