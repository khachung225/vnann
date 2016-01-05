using System;

namespace DataCollector
{
    public class TradingData
    {
       public DateTime Day { get; set; }

       public double Open { get; set; }
       public double High { get; set; }
       public double Low { get; set; }
       public double Close { get; set; }
       public double Volume { get; set; }
       public string Symbol { get; set; }
    }
}
