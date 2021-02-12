using ComponentsApp.Data.Common;
using ComponentsApp.Data.Models;
using ComponentsApp.Services.Interfaces;
using ComponentsApp.Services.Services;
using ComponentsApp.UI.Infrastructure.Commands.Base;
using Microsoft.Win32;
using System;

namespace ComponentsApp.UI.ViewModels
{
    internal class ResultWindowVm : BaseModel
    {
        #region Поля

        private RelayCommand _saveToPdfCommand;

        #endregion

        #region Свойства

        public Result Result { get; set; }

        #endregion


        #region Комманды

        public RelayCommand SaveToPdfCommand
        {
            get
            {
                if (_saveToPdfCommand == null)
                {
                    _saveToPdfCommand = new RelayCommand(obj =>
                    {
                        var sfd = new SaveFileDialog
                        {
                            Filter = "Pdf Document|*.pdf",
                            Title = "Save an PDF File",
                            FileName = $"Протокол расчета {DateTime.Now.ToShortDateString()}.pdf"
                        };

                        if (sfd.ShowDialog() == true)
                        {
                            IFileService fileService = new FileService();
                            fileService.SaveToPdf(Result, sfd.FileName);
                        }
                    });
                }
                return _saveToPdfCommand;
            }
        }

        #endregion
    }
}
