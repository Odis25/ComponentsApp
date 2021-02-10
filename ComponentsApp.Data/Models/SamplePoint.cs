using System.Collections.Generic;

namespace ComponentsApp.Data.Models
{
    public class SamplePoint
    {
        public ICollection<Sample> Samples { get; set; }
        public double Volume { get; set; }
    }
}
