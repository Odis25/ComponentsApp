using ComponentsApp.Data.Common;
using ComponentsApp.Data.Models;
using ComponentsApp.Services.Interfaces;
using ComponentsApp.Services.Services;
using ComponentsApp.UI.Infrastructure.Commands.Base;
using ComponentsApp.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ComponentsApp.UI.ViewModels
{
    internal class MainWindowVm : BaseModel
    {
        #region Поля

        private readonly IFileService _fileService;
        private readonly ICalculationService _calculationService;

        private bool _inputDensity;

        private SamplePointVm _samplePoint1;
        private SamplePointVm _samplePoint2;

        private RelayCommand _closeApplicationCommand;
        private RelayCommand _addSampleToSamplePoint1Command;
        private RelayCommand _removeSampleFromSamplePoint1Command;
        private RelayCommand _addSampleToSamplePoint2Command;
        private RelayCommand _removeSampleFromSamplePoint2Command;
        private RelayCommand _calculateCommand;
        private RelayCommand _saveToFileCommand;
        private RelayCommand _loadFromFileCommand;
        #endregion

        #region Свойства
        // Вычислять плотность
        public bool InputDensity
        {
            get => _inputDensity;
            set => Set(ref _inputDensity, value);
        }

        // Коллекция проб с точки отбора №1
        public SamplePointVm SamplePoint1
        {
            get => _samplePoint1;
            set => Set(ref _samplePoint1, value);
        }

        // Коллекция проб с точки отбора №2
        public SamplePointVm SamplePoint2
        {
            get => _samplePoint2;
            set => Set(ref _samplePoint2, value);
        }

        private ResultWindowVm ResultWindowVm { get; }
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
                        SamplePoint1.Samples.Add(new SampleVm { SampleNumber = sampleNumber });
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
                        SamplePoint2.Samples.Add(new SampleVm { SampleNumber = sampleNumber });
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
                        else
                        {
                            if (InputDensity)
                            {
                                if (SamplePoint1.Samples.Any(s => s.Sample.Density == 0) ||
                                    SamplePoint2.Samples.Any(s => s.Sample.Density == 0))
                                {
                                    MessageBox.Show("Плотность каждой из проб должна быть больше 0.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    return;
                                }
                            }

                            var samplePoint1 = new SamplePoint
                            {
                                Samples = SamplePoint1.Samples.Select(s => s.Sample).ToHashSet(),
                                Volume = SamplePoint1.Volume
                            };

                            var samplePoint2 = new SamplePoint
                            {
                                Samples = SamplePoint2.Samples.Select(s => s.Sample).ToHashSet(),
                                Volume = SamplePoint2.Volume
                            };

                            ResultWindowVm.Result = _calculationService.Calculate(samplePoint1, samplePoint2, InputDensity);

                            var resultWindow = new ResultWindow { DataContext = ResultWindowVm };

                            resultWindow.ShowDialog();
                        }
                    });
                }
                return _calculateCommand;
            }
        }

        public RelayCommand SaveToFileCommand
        {
            get
            {
                if (_saveToFileCommand == null)
                {
                    _saveToFileCommand = new RelayCommand(async obj =>
                    {
                        var data = new List<SamplePoint>
                        {
                            new SamplePoint{ Samples = SamplePoint1.Samples.Select(s=> s.Sample).ToHashSet(), Volume = SamplePoint1.Volume },
                            new SamplePoint{ Samples = SamplePoint2.Samples.Select(s=> s.Sample).ToHashSet(), Volume = SamplePoint2.Volume },
                        };
                        var result = await _fileService.SaveDataAsync(data);

                        if (result)
                        {
                            MessageBox.Show("Данные успешно сохранены", "Сохранение данных", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка сохранения", "Сохранение данных", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });
                }
                return _saveToFileCommand;
            }
        }

        public RelayCommand LoadFromFileCommand
        {
            get
            {
                if (_loadFromFileCommand == null)
                {
                    _loadFromFileCommand = new RelayCommand(async obj =>
                    {
                        var result = (await _fileService.LoadDataAsync()).ToArray();

                        if (result != null)
                        {
                            var samples1 = new List<SampleVm>();
                            foreach (var sample in result[0].Samples)
                            {
                                samples1.Add(new SampleVm { Sample = sample, SampleNumber = samples1.Count + 1 });
                            }

                            var samples2 = new List<SampleVm>();
                            foreach (var sample in result[1].Samples)
                            {
                                samples2.Add(new SampleVm { Sample = sample, SampleNumber = samples2.Count + 1 });
                            }

                            SamplePoint1.Volume = result[0].Volume;
                            SamplePoint1.Samples = new ObservableCollection<SampleVm>(samples1);

                            SamplePoint2.Volume = result[1].Volume;
                            SamplePoint2.Samples = new ObservableCollection<SampleVm>(samples2);
                        }
                        else
                        {
                            MessageBox.Show("Нет сохраненных данных", "Загрузка данных", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    });
                }
                return _loadFromFileCommand;
            }
        }
        #endregion

        public MainWindowVm(ResultWindowVm resultWindowVm)
        {
            ResultWindowVm = resultWindowVm;

            _fileService = new FileService();
            _calculationService = new CalculationService();

            SamplePoint1 = new SamplePointVm
            {
                Header = "ВГПП ПНГ Сепарация УВКС Е-1/1,2",
                SubHeader = "Варьеганское, Тагринское и Новоаганское месторождения"
            };

            SamplePoint2 = new SamplePointVm
            {
                Header = "ВГПП ПНГ Сепарация Узел №1",
                SubHeader = "Рославльское и Западно-Варьеганское месторождения"
            };

            SamplePoint1.Samples.Add(new SampleVm { SampleNumber = 1 });
            SamplePoint1.Samples.Add(new SampleVm { SampleNumber = 2 });
            SamplePoint1.Samples.Add(new SampleVm { SampleNumber = 3 });
            SamplePoint1.Samples.Add(new SampleVm { SampleNumber = 4 });

            SamplePoint2.Samples.Add(new SampleVm { SampleNumber = 1 });
            SamplePoint2.Samples.Add(new SampleVm { SampleNumber = 2 });
            SamplePoint2.Samples.Add(new SampleVm { SampleNumber = 3 });
            SamplePoint2.Samples.Add(new SampleVm { SampleNumber = 4 });
        }
    }
}
