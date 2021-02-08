using ComponentsApp.WPF.Models;
using ComponentsApp.WPF.ViewModels.Base;
using System.ComponentModel;

namespace ComponentsApp.WPF.ViewModels
{
    public class SampleVm : ViewModel
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
