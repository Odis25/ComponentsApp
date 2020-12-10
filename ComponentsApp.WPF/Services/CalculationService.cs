using ComponentsApp.WPF.Models;
using ComponentsApp.WPF.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ComponentsApp.WPF.Services
{
    public class CalculationService : ICalculationService
    {
        public ResultData Calculate(SamplePoint samplePoint1, SamplePoint samplePoint2)
        {
            // Средние значения по первой точке отбора
            var avgSample1 = GetAvgSample(samplePoint1.Samples);

            // Средние значения по второй точке отбора
            var avgSample2 = GetAvgSample(samplePoint2.Samples);

            // Средневзвешенные значения
            var total = GetWeightedAvgTotal(avgSample1, samplePoint1.Volume / 1000, avgSample2, samplePoint2.Volume / 1000);
           
            var table3 = new AvgSamplesTable
            {
                AverageSampleFromPoint1 = avgSample1,
                AverageSampleFromPoint2 = avgSample2,
                AverageSampleTotal = total
            };

            var table4 = new ComponentsQuantityTable
            {
                C3 = total.C3H8,
                IC4 = total.IC4H10,
                NC4 = total.NC4H10,
                IC5 = total.IC5H12,
                NC5 = total.NC5H12,
                C6 = total.C6H14,
                Density = total.Density,
                QuantityC3 = total.C3H8 * total.Density * 10,
                QuantityIC4 = total.IC4H10 * total.Density * 10,
                QuantityNC4 = total.NC4H10 * total.Density * 10,
                QuantityIC5 = total.IC5H12 * total.Density * 10,
                QuantityNC5 = total.NC5H12 * total.Density * 10,
                QuantityC6 = total.C6H14 * total.Density * 10
            };

            return new ResultData
            {
                AvgSamplesTable = table3,
                ComponentsQuantityTable = table4
            };
        }

        private Sample GetAvgSample(ICollection<Sample> samples)
        {
            return new Sample
            {
                CH4 = samples.Sum(s => s.CH4) / samples.Count,
                C2H6 = samples.Sum(s => s.C2H6) / samples.Count,
                C3H8 = samples.Sum(s => s.C3H8) / samples.Count,
                IC4H10 = samples.Sum(s => s.IC4H10) / samples.Count,
                NC4H10 = samples.Sum(s => s.NC4H10) / samples.Count,
                IC5H12 = samples.Sum(s => s.IC5H12) / samples.Count,
                NC5H12 = samples.Sum(s => s.NC5H12) / samples.Count,
                C6H14 = samples.Sum(s => s.C6H14) / samples.Count,
                Density = samples.Sum(s => s.Density) / samples.Count,
            };
        }
        private Sample GetWeightedAvgTotal(Sample avgSample1, double volume1, Sample avgSample2, double volume2)
        {
            return new Sample
            {
                CH4 = (avgSample1.CH4 * volume1 + avgSample2.CH4 * volume2) / (volume1 + volume2),
                C2H6 = (avgSample1.C2H6 * volume1 + avgSample2.C2H6 * volume2) / (volume1 + volume2),
                C3H8 = (avgSample1.C3H8 * volume1 + avgSample2.C3H8 * volume2) / (volume1 + volume2),
                IC4H10 = (avgSample1.IC4H10 * volume1 + avgSample2.IC4H10 * volume2) / (volume1 + volume2),
                NC4H10 = (avgSample1.NC4H10 * volume1 + avgSample2.NC4H10 * volume2) / (volume1 + volume2),
                IC5H12 = (avgSample1.IC5H12 * volume1 + avgSample2.IC5H12 * volume2) / (volume1 + volume2),
                NC5H12 = (avgSample1.NC5H12 * volume1 + avgSample2.NC5H12 * volume2) / (volume1 + volume2),
                C6H14 = (avgSample1.CH4 * volume1 + avgSample2.C6H14 * volume2) / (volume1 + volume2),
                Density = (avgSample1.Density * volume1 + avgSample2.Density * volume2) / (volume1 + volume2)
            };
        }
    }
}
