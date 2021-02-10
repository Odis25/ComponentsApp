using System;
using System.Collections.Generic;

namespace ComponentsApp.UI.Models
{
    [Serializable]
    public class SamplePoint
    {
        public ICollection<Sample> Samples { get; set; }

        public double Volume { get; set; }
    }
}
