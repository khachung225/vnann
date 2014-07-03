using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppTestTool.Data;
using AppTestTool.Utils;
using BaseEntity.Common;
using BaseEntity.Entity;
using BaseEntity.Utils;
using DirectionIO = AppTestTool.Utils.DirectionIO;
using JsonUtils = AppTestTool.Utils.JsonUtils;

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

        private string _pathToCommodity = "CTN13_647.csv";

        private string _pathToXauUsd = "XAU-USD.csv";
        private string _pathToNikkie = "NKY_index_1074.csv";

        private string _fileResult = "/Result.tsk";

        private readonly DateTime _predictFrom = CSVReader.ParseDate("2013-02-01");// CSVReader.ParseDate("2012-09-01");

        /// <summary>
        /// Predict the percentage movement to a specific date
        /// </summary>
        private readonly DateTime _predictTo = CSVReader.ParseDate("2013-07-30");

        private TaskTimer _taskTimer = new TaskTimer();
        public PredicDayByDay()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void PredicDayByDay_Load(object sender, EventArgs e)
        {
            _dtpPredictFrom.Value = _predictFrom;
            _dtpPredictTo.Value = _predictTo;
             _manager = new FinancialPredictorManager(1, 1);     /*Create new financial predictor manager*/
            _manager.Load(_pathtosp, _pathToCommodity, _pathToUsdJpy, _pathtoEurUsd, _pathToXauUsd, _pathToNikkie,_pathToDow);     /*Load S&P 500 and prime interest rates*/
          
        }
        private List<DateTime> GetListDate()
        {
            var listdate = new List<DateTime>();
            DateTime daytime = _dtpPredictFrom.Value.AddDays(-1);
            while (true)
            {
               var mydate= _manager.GetNextDate(daytime);
                if(mydate.Date > _dtpPredictTo.Value.Date )
                    break;
                if (mydate.Date == daytime.Date) break;
                listdate.Add(mydate);
                daytime = mydate;
            }
            return listdate;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            int count;

            var listdate = GetListDate();
            _taskTimer.CreateTask(listdate,ckbStart.Checked);

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
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
        }

        private void CallAppRunningPath(string path, DateTime day)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = textBox2.Text;
            startInfo.Arguments = string.Format("{0}#{1:yyyyMMdd}",path,day);
            var pro = Process.Start(startInfo);

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker == null) return;
            while (true)
            {

                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                var isnext = false;
                var inprocess = _taskTimer.GetTaskNameProcessing();
                if (inprocess == null)
                {
                    lblAction.Text =  "Next..";
                    isnext = true;
                }
                else
                {

                    var tic = DateTime.Now.Millisecond%2 == 0 ? "." : " ";
                    lblAction.Text = @"Task Name:" + inprocess.TaskName + tic;
                    if (DirectionIO.IsExistFile(inprocess.PathFolder + _fileResult))
                    {
                        inprocess.Status = 3;
                        _taskTimer.UpdateStausTask(inprocess);
                        isnext = true;
                    }

                }
                if (isnext)
                {
                    var nexprocess = _taskTimer.GetTaskNameProcessNext();
                    if (nexprocess == null) break;
                    nexprocess.Status = 2;
                    _taskTimer.UpdateStausTask(nexprocess);
                    DirectionIO.RemoveFile(nexprocess.PathFolder + _fileResult);
                    lblAction.Text = @"Running Task Name" + nexprocess.TaskName;
                    CallAppRunningPath(nexprocess.PathFolder, nexprocess.Day);
                    worker.ReportProgress(nexprocess.TaskCouter, nexprocess);
                }
                System.Threading.Thread.Sleep(500);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var task = e.UserState as TaskManager;
            progressBar1.Increment(1);
            if (task != null) txtDetail.Text +=@"\\n" + task.TaskName;
        }

        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { FileName = "", Filter = @"(*.exe)|*.exe" };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = Path.GetFullPath(ofd.FileName);
            }
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            btnBaoCao.Enabled = false;

            var listresoult = new List<CommodityResults>();
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

            xlWorkBook = xlApp.Workbooks.Add(misValue);


            try
            {
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet1;
                xlWorkSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet1.Name = "Summary";

                xlWorkBook.SaveAs(DirectionIO.GetPath() + "\\KetQua11.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue,
                                  misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue,
                                  misValue, misValue, misValue, misValue);


                //export to excelfile.
                var dta = DirectionIO.ReadAllText(DirectionIO.GetPath() + _taskTimer._taskManager);
                var listData = JsonUtils.Deserialize<List<TaskManager>>(dta);

                xlWorkSheet1.Cells[1, 1] = "Ngày";
                xlWorkSheet1.Cells[1, 2] = "Giá Trị Thực";
                xlWorkSheet1.Cells[1, 3] = "Giá Trị Mạng dự đoán";
                xlWorkSheet1.Cells[1, 4] = "Lỗi trên toàn mạng";

                progressBar1.Minimum = 0;
                progressBar1.Maximum = listData.Count;
                int k = 2;
                foreach (var taskManager in listData)
                {
                    var result = _taskTimer.GetResultRunANN(taskManager.PathFolder + "\\Result.tsk");
                    if (result != null)
                    {
                        if (result.ListResult!=null && result.ListResult.Count > 0)
                        {
                            xlWorkSheet1.Cells[k, 1] = result.ListResult[0].Date;
                            xlWorkSheet1.Cells[k, 2] = result.ListResult[0].ActualClose;
                            xlWorkSheet1.Cells[k, 3] = result.ListResult[0].PredictedClose;
                            xlWorkSheet1.Cells[k, 4] = result.ListResult[0].Error;
                             
                            k++;
                            listresoult.Add(new CommodityResults
                                {
                                    Date = result.ListResult[0].Date,
                                    ActualClose = result.ListResult[0].ActualClose,
                                    PredictedClose = result.ListResult[0].PredictedClose,
                                    Error = result.ListResult[0].Error,
                                });
                        }
                        progressBar1.Value++;
                    }

                }
                ReleaseObject(xlWorkSheet1);
                xlWorkBook.Save();
                xlWorkBook.Close(true, misValue, misValue);

            }
            catch (Exception)
            {


            }
            finally
            {
                xlApp.Quit();
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
            }
            btnBaoCao.Enabled = true;
            //ve do thị
            var grap = new CommodityResultsGraph();
            grap.GraphInit(_predictFrom,_predictTo);
            grap.DrawGraph(listresoult);
            grap.Show();
        }
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                // MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
