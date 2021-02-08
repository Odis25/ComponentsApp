using ComponentsApp.Data.Common;
using System.Collections.ObjectModel;

namespace ComponentsApp.Data.Models
{
    public class SamplePoint : BaseModel
    {
        private double _volume;
        private ObservableCollection<Sample> _samples;

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
    }
}
