using ComponentsApp.WPF.ViewModels.Base;
using System;

namespace ComponentsApp.WPF.Models
{
    public class Sample : ViewModel
    {
        #region Поля
        private int _sampleNumber;
        private double _o2;
        private double _n2;
        private double _cO2;
        private double _h;
        private double _he;
        private double _cH4;
        private double _c2H6;
        private double _c3H8;
        private double _iC4H10;
        private double _nC4H10;
        private double _iC5H12;
        private double _nC5H12;
        private double _c6H14;
        private double _density;
        private DateTime _sampleDate;
        #endregion

        public int SampleNumber 
        { 
            get => _sampleNumber;
            set => Set(ref _sampleNumber, value);
        }
        public double O2
        {
            get => _o2;
            set { Set(ref _o2, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double N2
        {
            get => _n2;
            set { Set(ref _n2, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double CO2
        {
            get => _cO2;
            set { Set(ref _cO2, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double H
        {
            get => _h;
            set { Set(ref _h, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double He
        {
            get => _he;
            set { Set(ref _he, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double CH4
        {
            get => _cH4;
            set { Set(ref _cH4, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double C2H6
        {
            get => _c2H6;
            set { Set(ref _c2H6, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double C3H8
        {
            get => _c3H8;
            set { Set(ref _c3H8, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double IC4H10
        {
            get => _iC4H10;
            set { Set(ref _iC4H10, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double NC4H10
        {
            get => _nC4H10;
            set { Set(ref _nC4H10, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double IC5H12
        {
            get => _iC5H12;
            set { Set(ref _iC5H12, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double NC5H12
        {
            get => _nC5H12;
            set { Set(ref _nC5H12, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double C6H14
        {
            get => _c6H14;
            set { Set(ref _c6H14, value); OnPropertyChanged(nameof(Summ)); }
        }
        public double Density
        {
            get => _density;
            set { Set(ref _density, value); OnPropertyChanged(nameof(Summ)); }
        }

        public decimal Summ => (decimal)(O2 + N2 + CO2 + H + He + CH4 + C2H6 + C3H8 + IC4H10 + NC4H10 + IC5H12 + NC5H12 + C6H14);
        public DateTime SampleDate 
        { 
            get => _sampleDate;
            set => Set(ref _sampleDate, value); 
        }
    }
}
