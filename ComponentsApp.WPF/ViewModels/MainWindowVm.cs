using ComponentsApp.WPF.Models;
using ComponentsApp.WPF.Services;
using ComponentsApp.WPF.ViewModels.Base;
using ComponentsApp.WPF.Views;
using ComponentsApp.WPF.Infrastructure.Commands.Base;
using ComponentsApp.WPF.Interfaces;
using System.Linq;
using System.Windows;

namespace ComponentsApp.WPF.ViewModels
{
    internal class MainWindowVm : ViewModel
    {
        #region Поля

        private SamplePoint _samplePoint1;
        private SamplePoint _samplePoint2;
        private RelayCommand _closeApplicationCommand;
        private RelayCommand _addSampleToSamplePoint1Command;
        private RelayCommand _removeSampleFromSamplePoint1Command;
        private RelayCommand _addSampleToSamplePoint2Command;
        private RelayCommand _removeSampleFromSamplePoint2Command;
        private RelayCommand _calculateCommand;
        #endregion

        #region Свойства

        // Коллекция проб с точки отбора №1
        public SamplePoint SamplePoint1
        {
            get => _samplePoint1;
            set
            {
                Set(ref _samplePoint1, value);
            }
        }

        // Коллекция проб с точки отбора №2
        public SamplePoint SamplePoint2
        {
            get => _samplePoint2;
            set => Set(ref _samplePoint2, value);
        }
        #endregion

        #region Комманды

        // Закрыть приложение
        public RelayCommand CloseApplicationCommand
        {
            get
            {
                if (_closeApplicationCommand == null)
                {
                    _closeApplicationCommand = new RelayCommand(obj =>
                    {
                        Application.Current.Shutdown();
                    });
                }
                return _closeApplicationCommand;
            }
        }
        // Добавить пробу для точки отбора #1
        public RelayCommand AddSampleToSamplePoint1Command
        {
            get 
            {
                if (_addSampleToSamplePoint1Command == null)
                {
                    _addSampleToSamplePoint1Command = new RelayCommand(obj => 
                    { 
                        var sampleNumber = SamplePoint1.Samples.Count + 1; 
                        SamplePoint1.Samples.Add(new Sample { SampleNumber = sampleNumber }); 
                    }, 
                    c => SamplePoint1.Samples.Count != 10);
                }
                return _addSampleToSamplePoint1Command;
            }
        }
        // Удалить  пробу для точки отбора #1
        public RelayCommand RemoveSampleFromSamplePoint1Command
        {
            get
            {
                if (_removeSampleFromSamplePoint1Command == null)
                {
                    _removeSampleFromSamplePoint1Command = new RelayCommand(obj =>
                    {
                        var sample = SamplePoint1.Samples.Last();
                        SamplePoint1.Samples.Remove(sample);
                    },
                    c => SamplePoint1.Samples.Count > 1);
                }
                return _removeSampleFromSamplePoint1Command;
            }
        }
        // Добавить пробу для точки отбора #2
        public RelayCommand AddSampleToSamplePoint2Command
        {
            get
            {
                if (_addSampleToSamplePoint2Command == null)
                {
                    _addSampleToSamplePoint2Command = new RelayCommand(obj =>
                    {
                        var sampleNumber = SamplePoint2.Samples.Count + 1;
                        SamplePoint2.Samples.Add(new Sample { SampleNumber = sampleNumber });
                    },
                    c => SamplePoint2.Samples.Count != 10);
                }
                return _addSampleToSamplePoint2Command;
            }
        }
        // Удалить пробу для точки отбора #2
        public RelayCommand RemoveSampleFromSamplePoint2Command
        {
            get
            {
                if (_removeSampleFromSamplePoint2Command == null)
                {
                    _removeSampleFromSamplePoint2Command = new RelayCommand(obj =>
                    {
                        var sample = SamplePoint2.Samples.Last();
                        SamplePoint2.Samples.Remove(sample);
                    },
                    c => SamplePoint2.Samples.Count > 1);
                }
                return _removeSampleFromSamplePoint2Command;
            }
        }
        // Расчитать результат
        public RelayCommand CalculateCommand
        {
            get
            {
                if (_calculateCommand == null)
                {
                    _calculateCommand = new RelayCommand(obj =>
                    {
                        if (SamplePoint1.Volume == 0)
                        {
                            MessageBox.Show("Объем газа для точки отбора №1 не может быть 0.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else if (SamplePoint2.Volume == 0)
                        {
                            MessageBox.Show("Объем газа для точки отбора №2 не может быть 0.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else if (!SamplePoint1.Samples.All(s => s.Summ == 100.0m))
                        {
                            MessageBox.Show("Сумма компонентов для каждой пробы в точке отбора №1 должна быть 100%.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else if (!SamplePoint2.Samples.All(s => s.Summ == 100.0m))
                        {
                            MessageBox.Show("Сумма компонентов для каждой пробы в точке отбора №2 должна быть 100%.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);                            
                        }
                        else if (SamplePoint1.Samples.Any(s => s.Density == 0) || SamplePoint2.Samples.Any(s => s.Density == 0))
                        {
                            MessageBox.Show("Плотность каждой из проб должна быть больше 0.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            ICalculationService calculation = new CalculationService();

                            var result = calculation.Calculate(SamplePoint1, SamplePoint2);

                            var resultWindow = new ResultWindow
                            {
                                DataContext = new ResultWindowVm { ResultData = result }
                            };

                            resultWindow.ShowDialog();
                        }
                    });                   
                }
                return _calculateCommand;
            }
        }

        #endregion

        internal MainWindowVm()
        {
            SamplePoint1 = new SamplePoint { Name = "ВГПП ПНГ Сепарация УВКС Е-1/1,2 – Варьеганское, Тагринское и Новоаганское месторождения" };
            SamplePoint2 = new SamplePoint { Name = "ВГПП ПНГ Сепарация Узел №1 – Рославльское и Западно-Варьеганское месторождения" };
        }
    }
}
