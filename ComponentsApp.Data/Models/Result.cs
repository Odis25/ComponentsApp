using System.Collections.Generic;

namespace ComponentsApp.Data.Models
{
    public class Result
    {
        public IList<Sample> SamplesCollection { get; set; }
        public IList<double> MassCollection { get; set; }
        public IList<double> DensityCollection { get; set; }
        public IList<double> ComponentsSummCollection { get; set; }

        public double WeightedAvgConc { get; set; }
    }
}
