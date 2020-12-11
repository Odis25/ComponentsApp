using ComponentsApp.WPF.Infrastructure.Commands.Base;
using ComponentsApp.WPF.Interfaces;
using ComponentsApp.WPF.Models;
using ComponentsApp.WPF.Services;
using ComponentsApp.WPF.ViewModels.Base;
using Microsoft.Win32;

namespace ComponentsApp.WPF.ViewModels
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
