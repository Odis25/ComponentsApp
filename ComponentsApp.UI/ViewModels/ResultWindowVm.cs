using ComponentsApp.Data.Common;
using ComponentsApp.Services.Interfaces;
using ComponentsApp.UI.Infrastructure.Commands.Base;
using Microsoft.Win32;

namespace ComponentsApp.UI.ViewModels
{
    internal class ResultWindowVm : BaseModel
    {
        private RelayCommand _saveToPdfCommand;

        //public ResultData ResultData{ get; set; }

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
                            //IFileService fileService = new FileService();
                            //fileService.SaveToPdf(ResultData, sfd.FileName);
                        }
                    });
                }
                return _saveToPdfCommand;
            }
        }
    }
}
