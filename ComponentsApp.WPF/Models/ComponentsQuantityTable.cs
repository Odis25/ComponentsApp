namespace ComponentsApp.WPF.Models
{
    public class ComponentsQuantityTable
    {
        public double C3 { get; set; }
        public double IC4 { get; set; }
        public double NC4 { get; set; }
        public double IC5 { get; set; }
        public double NC5 { get; set; }
        public double C6 { get; set; }
        public double QuantityC3 { get; set; }
        public double QuantityIC4 { get; set; }
        public double QuantityNC4 { get; set; }
        public double QuantityIC5 { get; set; }
        public double QuantityNC5 { get; set; }
        public double QuantityC6 { get; set; }
        public double Density { get; set; }
        public double Summ => QuantityC3 + QuantityIC4 + QuantityNC4 + QuantityIC5 + QuantityNC5 + QuantityC6;
    }
}
