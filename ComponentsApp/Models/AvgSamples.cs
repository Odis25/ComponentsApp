using System.Collections.Generic;

namespace ComponentsApp.Models
{
    public class AvgSamples
    {
        public AvgSamples()
        {
            Samples = new List<SampleData>();
        }
        public IList<SampleData> Samples { get; set; }
        public double Volume { get; set; }
    }
}
