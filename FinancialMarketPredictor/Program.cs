using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Encog.MathUtil.Randomize;

namespace FinancialMarketPredictor
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
        //    Application.Run(new WinFinancialMarketPredictor());

        //}
        [STAThread]
        static void Main(string[]  folderName)
        {
            if (folderName != null && folderName.Count() > 0)
            {
                if (folderName[0] != null)
                {
                    AppGlobol.FolderPath = folderName[0];
                    AppGlobol.IsAutoRun = true;
                }
               
            } else
                {
                    var systemPArth = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    AppGlobol.FolderPath = systemPArth.Remove(systemPArth.LastIndexOf("\\", System.StringComparison.Ordinal)) + "/ket qua";

                    AppGlobol.IsAutoRun = true;
                }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinFinancialMarketPredictor());

        }
    }
}
