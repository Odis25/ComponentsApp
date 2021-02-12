using ComponentsApp.Data.Constants;
using ComponentsApp.Data.Models;
using ComponentsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ComponentsApp.Services.Services
{
    public class CalculationService : ICalculationService
    {
        public Result Calculate(SamplePoint point1, SamplePoint point2, bool useDensity)
        {
            // Средние значения по каждой точке отбора
            var avgSample1 = GetAvgSample(point1.Samples.ToHashSet());
            var avgSample2 = GetAvgSample(point2.Samples.ToHashSet());

            // Пересчитываем в массовые проценты
            var massSample1 = GetMassSample(avgSample1);
            var massSample2 = GetMassSample(avgSample2);

            // Расчет плотности при ст.у.
            var density1 = useDensity ? avgSample1.Density : CalcDensity(avgSample1);
            var density2 = useDensity ? avgSample2.Density : CalcDensity(avgSample2);

            // Вычисление отобранной массы газа в точках отбора
            var mass1 = point1.Volume * 1000 * density1;
            var mass2 = point1.Volume * 1000 * density2;

            // Соотношение массы ПНГ
            var alpha = mass1 / (mass1 + mass2);

            // Сумма компонентов
            var compSumm1 = GetComponentsSumm(massSample1);
            var compSumm2 = GetComponentsSumm(massSample2);

            // Средневзвешенная массовая концентрация фракций углеводородов
            var wghtAvgConc = 10 * (alpha * density1 * compSumm1 + (1 - alpha) * density2 * compSumm2);

            // Оформление результата
            var result = new Result
            {
                SamplesCollection = new List<Sample> { massSample1, massSample2},
                MassCollection = new List<double> { mass1, mass2},
                DensityCollection = new List<double> { density1, density2},
                ComponentsSummCollection = new List<double> { compSumm1, compSumm2},
                WeightedAvgConc = wghtAvgConc
            };

            return result;
        }

        private double CalcDensity(Sample sample)
        {
            var totalComponentsMass =
                (sample.Methane * CompConstants.Methane +
                sample.Ethane * CompConstants.Ethane +
                sample.Propane * CompConstants.Propane +
                sample.Isobutane * CompConstants.Isobutane +
                sample.Butane * CompConstants.Butane +
                sample.Isopentane * CompConstants.Isopentane +
                sample.Pentane * CompConstants.Pentane +
                sample.Hexane * CompConstants.Hexane +
                sample.CarbonDioxide * CompConstants.CarbonDioxide +
                sample.Oxygen * CompConstants.Oxigen +
                sample.Nitrogen * CompConstants.Nitrogen) / 100;

            return totalComponentsMass / 23.9;
        }

        private Sample GetMassSample(Sample sample)
        {
            var result = new Sample();
            var summary =
                sample.Methane * CompConstants.Methane +
                sample.Ethane * CompConstants.Ethane +
                sample.Propane * CompConstants.Propane +
                sample.Isobutane * CompConstants.Isobutane +
                sample.Butane * CompConstants.Butane +
                sample.Isopentane * CompConstants.Isopentane +
                sample.Pentane * CompConstants.Pentane +
                sample.Hexane * CompConstants.Hexane +
                sample.CarbonDioxide * CompConstants.CarbonDioxide +
                sample.Oxygen * CompConstants.Oxigen +
                sample.Nitrogen * CompConstants.Nitrogen;

            result.Methane = 100 * sample.Methane * CompConstants.Methane / summary;
            result.Ethane = 100 * sample.Ethane * CompConstants.Ethane / summary;
            result.Propane = 100 * sample.Propane * CompConstants.Propane / summary;
            result.Isobutane = 100 * sample.Isobutane * CompConstants.Isobutane / summary;
            result.Butane = 100 * sample.Butane * CompConstants.Butane / summary;
            result.Isopentane = 100 * sample.Isopentane * CompConstants.Isopentane / summary;
            result.Pentane = 100 * sample.Pentane * CompConstants.Pentane / summary;
            result.Hexane = 100 * sample.Hexane * CompConstants.Hexane / summary;
            result.CarbonDioxide = 100 * sample.CarbonDioxide * CompConstants.CarbonDioxide / summary;
            result.Oxygen = 100 * sample.Oxygen * CompConstants.Oxigen / summary;
            result.Nitrogen = 100 * sample.Nitrogen * CompConstants.Nitrogen / summary;

            return result;
        }

        private Sample GetAvgSample(ICollection<Sample> samples)
        {
            return new Sample
            {
                Nitrogen = samples.Sum(s => s.Nitrogen) / samples.Count,
                CarbonDioxide = samples.Sum(s => s.CarbonDioxide) / samples.Count,
                Oxygen = samples.Sum(s => s.Oxygen) / samples.Count,
                Methane = samples.Sum(s => s.Methane) / samples.Count,
                Ethane = samples.Sum(s => s.Ethane) / samples.Count,
                Propane = samples.Sum(s => s.Propane) / samples.Count,
                Isobutane = samples.Sum(s => s.Isobutane) / samples.Count,
                Butane = samples.Sum(s => s.Butane) / samples.Count,
                Isopentane = samples.Sum(s => s.Isopentane) / samples.Count,
                Pentane = samples.Sum(s => s.Pentane) / samples.Count,
                Hexane = samples.Sum(s => s.Hexane) / samples.Count,
                Density = samples.Sum(s => s.Density) / samples.Count,
            };
        }

        private double GetComponentsSumm(Sample sample) =>
                sample.Propane +
                sample.Isobutane +
                sample.Butane +
                sample.Isopentane +
                sample.Pentane +
                sample.Hexane;
    }
}
