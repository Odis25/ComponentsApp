using ComponentsApp.Data.Common;
using System.ComponentModel;

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

        [DisplayName("Метан")]
        public double Methane 
        { 
            get => _methane; 
            set => Set( ref _methane, value); 
        }
        [DisplayName("Этан")]
        public double Ethane 
        { 
            get => _ethane;
            set => Set(ref _ethane, value);
        }
        [DisplayName("Пропан")]
        public double Propane 
        { 
            get => _propane;
            set => Set(ref _propane, value);
        }
        [DisplayName("и-Бутан")]
        public double Isobutane 
        { 
            get => _isobutane;
            set => Set(ref _isobutane, value);
        }
        [DisplayName("н-Бутан")]
        public double Butane 
        { 
            get => _butane;
            set => Set(ref _butane, value);
        }
        [DisplayName("и-Пентан")]
        public double Isopentane 
        { 
            get => _isopentane;
            set => Set(ref _isopentane, value);
        }
        [DisplayName("н-Пентан")]
        public double Pentane 
        { 
            get => _pentane;
            set => Set(ref _pentane, value);
        }
        [DisplayName("Гексан+высшие")]
        public double Hexane 
        { 
            get => _hexane;
            set => Set(ref _hexane, value);
        }
        [DisplayName("Азот")]
        public double Nitrogen 
        { 
            get => _nitrogen;
            set => Set(ref _nitrogen, value);
        }
        [DisplayName("Кислород")]
        public double Oxygen 
        { 
            get => _oxygen;
            set => Set(ref _oxygen, value);
        }
        [DisplayName("Диоксид углерода")]
        public double CarbonDioxide 
        { 
            get => _carbonDioxide;
            set => Set(ref _carbonDioxide, value);
        }
        public double Density { get; set; }
    }
}
