using ComponentApp.WPF.Infrastructure.Commands.Base;
using ComponentApp.WPF.Models;
using ComponentApp.WPF.ViewModels.Base;
using System.Linq;
using System.Windows;

namespace ComponentApp.WPF.ViewModels
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
            get => _closeApplicationCommand ??= new RelayCommand(obj =>
                {
                    Application.Current.Shutdown();
                });
        }
        // Добавить пробу для точки отбора #1
        public RelayCommand AddSampleToSamplePoint1Command
        {
            get => _addSampleToSamplePoint1Command ??= new RelayCommand(obj =>
            {
                var sampleNumber = SamplePoint1.Samples.Count +  1;
                SamplePoint1.Samples.Add(new Sample { SampleNumber = sampleNumber });
            }, 
                c => SamplePoint1.Samples.Count != 10);
        }

        // Удалить  пробу для точки отбора #1
        public RelayCommand RemoveSampleFromSamplePoint1Command
        {
            get => _removeSampleFromSamplePoint1Command ??= new RelayCommand(obj =>
            {
                var sample = SamplePoint1.Samples.Last();
                SamplePoint1.Samples.Remove(sample);
            },
                c=> SamplePoint1.Samples.Count > 1);
        }
        // Добавить пробу для точки отбора #1
        public RelayCommand AddSampleToSamplePoint2Command
        {
            get => _addSampleToSamplePoint2Command ??= new RelayCommand(obj =>
            {
                var sampleNumber = SamplePoint2.Samples.Count + 1;
                SamplePoint2.Samples.Add(new Sample { SampleNumber = sampleNumber });
            },
                c => SamplePoint2.Samples.Count != 10);
        }

        // Удалить  пробу для точки отбора #1
        public RelayCommand RemoveSampleFromSamplePoint2Command
        {
            get => _removeSampleFromSamplePoint2Command ??= new RelayCommand(obj =>
            {
                var sample = SamplePoint2.Samples.Last();
                SamplePoint2.Samples.Remove(sample);
            },
                c => SamplePoint2.Samples.Count > 1);
        }

        #endregion

        internal MainWindowVm()
        {
            SamplePoint1 = new SamplePoint { Name = "ВГПП ПНГ Сепарация УВКС Е-1/1,2 – Варьеганское, Тагринское и Новоаганское месторождения" };
            SamplePoint2 = new SamplePoint { Name = "ВГПП ПНГ Сепарация Узел №1 – Рославльское и Западно-Варьеганское месторождения" };
        }
    }
}
