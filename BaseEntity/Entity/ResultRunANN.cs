using System;
using System.Collections.Generic;

namespace BaseEntity.Entity
{
    public class ResultRunANN
    {
        public ResultRunANN()
        {
            ListResult= new List<CommodityResults>();
        }
        public DateTime PredicDate { get; set; }
        public double TotalMinute { get; set; }
        public  int Counter { get; set; }
        public  bool Ishoitu { get; set; }
        public  List<CommodityResults> ListResult { get; set; }
    }
}
