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
        [STAThread]
        static void Main()
        {
            var systemPArth = System.Reflection.Assembly.GetExecutingAssembly().Location;
            AppGlobol.FolderPath = systemPArth.Remove(systemPArth.LastIndexOf("\\", System.StringComparison.Ordinal)) +"/ket qua";
            
            AppGlobol.IsAutoRun = true;
             
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinFinancialMarketPredictor());
            
        }
        [STAThread]
        static void Main(string folderName)
        {
            AppGlobol.FolderPath = folderName;
            AppGlobol.IsAutoRun = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinFinancialMarketPredictor());

        }
    }
}
