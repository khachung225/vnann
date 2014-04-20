using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialMarketPredictor.Entities
{
    public class ResultRunANN
    {
        public ResultRunANN()
        {
            ListResult= new List<CommodityResults>();
        }

        public  TimeSpan TimeSpan { get; set; }
        public  int Counter { get; set; }
        public  bool Ishoitu { get; set; }
        public  List<CommodityResults> ListResult { get; set; }
    }
}
