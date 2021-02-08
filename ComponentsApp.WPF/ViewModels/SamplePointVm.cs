using ComponentsApp.WPF.ViewModels.Base;
using System.Collections.ObjectModel;

namespace ComponentsApp.WPF.ViewModels
{
    public class SamplePointVm : ViewModel
    {
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public double Volume { get; set; }
        public ObservableCollection<SampleVm> Samples { get; set; }

        public SamplePointVm()
        {
            Samples = new ObservableCollection<SampleVm>();
        }
    }
}
