using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppTestTool.Utils;
using BaseEntity.Common;

namespace AppTestTool
{
    public partial class PredicDayByDay : Form
    {
        private FinancialPredictorManager _manager;

        private string _pathtosp = "S&P500Index_1104.csv";

        /// <summary>
        /// Default path to Prime interest rates csv
        /// </summary>
        private string _pathtoEurUsd = "EUR_USD_1990.csv";
        private string _pathToUsdJpy = "USD-JPY_1367.csv";
        
        /// <summary>
        /// Default path to Dow indexes csv
        /// </summary>
        private string _pathToDow = "DOWI_index_1104.csv";

        private string _pathToCommodity = "KCZ13_718.csv";

        private string _pathToXauUsd = "XAU-USD.csv";
        private string _pathToNikkie = "NKY_index_1074.csv";

        private TaskTimer _taskTimer = new TaskTimer();
        public PredicDayByDay()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void PredicDayByDay_Load(object sender, EventArgs e)
        {
             _manager = new FinancialPredictorManager(1, 1);     /*Create new financial predictor manager*/
            _manager.Load(_pathtosp, _pathToCommodity, _pathToUsdJpy, _pathtoEurUsd, _pathToXauUsd, _pathToNikkie,_pathToDow);     /*Load S&P 500 and prime interest rates*/
          
        }
        private List<DateTime> GetListDate()
        {
            var listdate = new List<DateTime>();
            DateTime daytime = _dtpPredictFrom.Value.AddDays(-1);
            while (true)
            {
                
            }
            return listdate;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            int count;

            var listdate = GetListDate();
            _taskTimer.CreateTask(listdate);

            progressBar1.Minimum = 0;
            progressBar1.Maximum = listdate.Count;
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }
    }
}
