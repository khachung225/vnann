﻿using System;
using System.Collections.Generic;
using BaseEntity.Entity;

namespace CommodityPredictor
{
   public class AppGlobol
    {
       public static string FolderPath { get; set; }
       public static bool IsAutoRun { get; set; }

       public static int Status { get; set; }

       public static TimeSpan TimeSpan { get; set; }
       public static int Counter { get; set; }
       public static bool Ishoitu { get; set; }
       public static List<CommodityResults> ListResult { get; set; }
       public static DateTime PredicDate { get; set; }

       public static string InitWieght { get; set; }
    }
}
