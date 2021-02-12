using ComponentsApp.Data.Common;
using System.Collections.ObjectModel;

namespace ComponentsApp.UI.ViewModels
{
    public class SamplePointVm : BaseModel
    {
        private ObservableCollection<SampleVm> _samples;
        private double _volume;

        public string Header { get; set; }
        public string SubHeader { get; set; }
        public double Volume 
        { 
            get => _volume; 
            set => Set(ref _volume, value); 
        }
        public ObservableCollection<SampleVm> Samples 
        { 
            get => _samples; 
            set => Set(ref _samples, value);
        }

        public SamplePointVm()
        {
            Samples = new ObservableCollection<SampleVm>();
        }
    }
}
