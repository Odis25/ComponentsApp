using ComponentsApp.Data.Common;
using System.Collections.ObjectModel;

namespace ComponentsApp.UI.ViewModels
{
    public class SamplePointVm : BaseModel
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
