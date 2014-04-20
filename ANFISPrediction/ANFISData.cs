using System;
using System.Collections.Generic;

namespace ANFISPrediction
{
    [Serializable]
    public class ANFISData
    {
        public ANFISData()
        {
            Input = new Dictionary<int, double>();
        }
        public Dictionary<int,double> Input { get; set; }
        public double Ideal { get; set; }
    }
}
