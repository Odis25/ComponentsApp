using ComponentsApp.Data.Common;
using ComponentsApp.Data.Models;
using System.ComponentModel;

namespace ComponentsApp.UI.ViewModels
{
    public class SampleVm : BaseModel
    {
        public int SampleNumber { get; set; }
        public Sample Sample { get; set; }

        public decimal Summ => (decimal)
            (Sample.Methane
            + Sample.Ethane
            + Sample.Propane
            + Sample.Isobutane
            + Sample.Butane
            + Sample.Isopentane
            + Sample.Pentane
            + Sample.Hexane
            + Sample.Nitrogen
            + Sample.Oxygen
            + Sample.CarbonDioxide);

        public SampleVm()
        {
            Sample = new Sample();
            Sample.PropertyChanged += SamplePropertyChanged;
        }

        private void SamplePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Summ));
        }
    }
}
