using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BaseEntity.Utils;

namespace CommodityPredictor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    var systemPArth = System.Reflection.Assembly.GetExecutingAssembly().Location;
        //    AppGlobol.FolderPath = systemPArth.Remove(systemPArth.LastIndexOf("\\", System.StringComparison.Ordinal)) + "/ket qua";

        //    AppGlobol.IsAutoRun = true;

        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new WinMarketPredictor());

        //}
        [STAThread]
        static void Main(string[]  folderName)
        {
            string exef="";
            try
            {

            
            if (folderName != null && folderName.Count() > 0)
            {
                if (folderName[0] != null)
                {
                    var mystring = folderName[0];
                    var list = mystring.Split('#');
                    if (list.Count() == 2)
                    {
                        AppGlobol.FolderPath = list[0];
                        AppGlobol.IsAutoRun = true;
                        var date = list[1];
                        DateTime outDate;
                        if(!DateTime.TryParseExact(date,
                       "yyyyMMdd",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out outDate))
                            return;
                         
                        AppGlobol.PredicDate = outDate;
                    }
                    


                }

            }
            //else
            //{
            //    var systemPArth = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //    AppGlobol.FolderPath = systemPArth.Remove(systemPArth.LastIndexOf("\\", System.StringComparison.Ordinal)) + "/ket qua";
            //    AppGlobol.PredicDate = new DateTime(2013,07,02);
            //    AppGlobol.IsAutoRun = true;
            //}
            }
            catch (Exception ex)
            {
                exef = ex.ToString();

            }
            DirectionIO.WriteLogFile("IsAutoRun:" + AppGlobol.IsAutoRun.ToString() + " FolderPath:" + AppGlobol.FolderPath + " PredicDate:" + AppGlobol.PredicDate.ToString(CultureInfo.InvariantCulture) + " EX:" + exef);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinMarketPredictor());

        }
    }
}
