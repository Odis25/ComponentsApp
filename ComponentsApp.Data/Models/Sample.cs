using ComponentsApp.Data.Common;

namespace ComponentsApp.Data.Models
{
    public class Sample : BaseModel
    {
        #region Поля
        private double _methane;
        private double _ethane;
        private double _propane;
        private double _isobutane;
        private double _butane;
        private double _isopentane;
        private double _pentane;
        private double _hexane;
        private double _nitrogen;
        private double _oxygen;
        private double _carbonDioxide;
        #endregion

        public double Methane 
        { 
            get => _methane; 
            set => Set( ref _methane, value); 
        }
        public double Ethane 
        { 
            get => _ethane;
            set => Set(ref _ethane, value);
        }
        public double Propane 
        { 
            get => _propane;
            set => Set(ref _propane, value);
        }
        public double Isobutane 
        { 
            get => _isobutane;
            set => Set(ref _isobutane, value);
        }
        public double Butane 
        { 
            get => _butane;
            set => Set(ref _butane, value);
        }
        public double Isopentane 
        { 
            get => _isopentane;
            set => Set(ref _isopentane, value);
        }
        public double Pentane 
        { 
            get => _pentane;
            set => Set(ref _pentane, value);
        }
        public double Hexane 
        { 
            get => _hexane;
            set => Set(ref _hexane, value);
        }
        public double Nitrogen 
        { 
            get => _nitrogen;
            set => Set(ref _nitrogen, value);
        }
        public double Oxygen 
        { 
            get => _oxygen;
            set => Set(ref _oxygen, value);
        }
        public double CarbonDioxide 
        { 
            get => _carbonDioxide;
            set => Set(ref _carbonDioxide, value);
        }
        public double Density { get; set; }
    }
}
