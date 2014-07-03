using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppTestTool.Data
{
   public class PredicError
    {
       public DateTime Day { get; set; }
       public double ActualValue { get; set; }
       public double PredicValue { get; set; }
       public double Error { get; set; }
    }
}
