using ComponentsApp.WPF.Models;
using ComponentsApp.WPF.ViewModels.Base;

namespace ComponentsApp.WPF.ViewModels
{
    public class ResultWindowVm : ViewModel
    {
        private ResultData _resultData;

        public ResultData ResultData
        {
            get { return _resultData; }
            set { _resultData = value; }
        }
    }
}
