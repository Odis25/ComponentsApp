using ComponentsApp.WPF.ViewModels.Base;
using System.Collections.ObjectModel;

namespace ComponentsApp.WPF.Models
{
    public class SamplePoint : ViewModel
    {

        private string _name;
        private double _volume;
        private ObservableCollection<Sample> _samples;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value); 
        }
        public ObservableCollection<Sample> Samples 
        {
            get => _samples;
            set => Set(ref _samples, value);
        }
        public double Volume 
        { 
            get => _volume;
            set => Set(ref _volume, value);
        }

        public SamplePoint()
        {
            _samples = new ObservableCollection<Sample>();

            _samples.Add(new Sample { SampleNumber = 1 });
            _samples.Add(new Sample { SampleNumber = 2 });
            _samples.Add(new Sample { SampleNumber = 3 });
            _samples.Add(new Sample { SampleNumber = 4 });
        }
    }
}
