using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using AppTestTool.Data;
using AppTestTool.Utils;
using BaseEntity.Entity;
using ExcelLibrary.SpreadSheet;
using Excel = Microsoft.Office.Interop.Excel;
 


namespace AppTestTool
{
    public partial class FrmTestTool : Form
    {
        private string _taskManager = "/TaskManager.tsk";
        private string _fileResult = "/Result.tsk";
        public FrmTestTool()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { FileName = "", Filter = @"(*.exe)|*.exe" };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = Path.GetFullPath(ofd.FileName);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var path = DirectionIO.GetPath();
            int count;
            if (!Int32.TryParse(textBox1.Text, out count))
                count = 0;
            var listTask = new List<TaskManager>();
            for (int i = 0; i < count; i++)
            {

                var task = new TaskManager
                    {
                        TaskCouter = i + 1,
                        TaskName = string.Format("ANN{0}", i + 1),
                        Status = 1,
                    };
                task.PathFolder = path + "\\" + task.TaskName;
                DirectionIO.CreateNewFolder(task.PathFolder);
                listTask.Add(task);
            }

            var fileconten = JsonUtils.Serialize(listTask);
            DirectionIO.WriteAllText(DirectionIO.GetPath() + _taskManager, fileconten);

            progressBar1.Minimum = 0;
            progressBar1.Maximum = count;
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }
       
        private void CallAppRunningPath(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = textBox2.Text;
            startInfo.Arguments = path;
            var pro = Process.Start(startInfo);
            
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if(worker == null) return;
            while (true)
            {

                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                var isnext = false;
                var inprocess = GetTaskNameProcessing();
                if (inprocess == null)
                {
                    isnext = true;
                }
                else
                {
                    if (DirectionIO.IsExistFile(inprocess.PathFolder + _fileResult))
                    {
                        inprocess.Status = 3;
                        UpdateStausTask(inprocess);
                        isnext = true;
                    }
                }
                if (isnext)
                {
                    var nexprocess = GetTaskNameProcessNext();
                    if (nexprocess == null) break;
                    nexprocess.Status = 2;
                    UpdateStausTask(nexprocess);
                    DirectionIO.RemoveFile(nexprocess.PathFolder + _fileResult);
                    CallAppRunningPath(nexprocess.PathFolder);
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
            if (task != null) lblTaskName.Text = task.TaskName;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
        }

        private void UpdateStausTask(TaskManager taskManagerModif)
        {
            var dta = DirectionIO.ReadAllText(DirectionIO.GetPath() + _taskManager);
            var listData = JsonUtils.Deserialize<List<TaskManager>>(dta);
            foreach (var taskManager in listData)
            {
                if (taskManager.TaskCouter == taskManagerModif.TaskCouter)
                {
                    taskManager.Status = taskManagerModif.Status;
                    break;
                }
               
            }
            var fileconten = JsonUtils.Serialize(listData);
            DirectionIO.WriteAllText(DirectionIO.GetPath() + _taskManager, fileconten);
        }

        #region TaskName process
        private TaskManager GetTaskNameProcessing()
        {
            var dta = DirectionIO.ReadAllText(DirectionIO.GetPath() + _taskManager);
            var listData = JsonUtils.Deserialize<List<TaskManager>>(dta);
            foreach (var taskManager in listData)
            {
                if (taskManager.Status == 2)
                    return taskManager;
            }
            return null;
        }
        private TaskManager GetTaskNameProcessNext()
        {
            var dta = DirectionIO.ReadAllText(DirectionIO.GetPath() + _taskManager);
            var listData = JsonUtils.Deserialize<List<TaskManager>>(dta);
            foreach (var taskManager in listData)
            {
                if (taskManager.Status == 3) continue;
                if (taskManager.Status == 1)
                    return taskManager;
            }
            return null;
        }
        #endregion

        #region Excel call
        private ResultRunANN GetResultRunANN(string pathfile)
        {
            if (DirectionIO.IsExistFile(pathfile))
            {
                var stringfile = DirectionIO.ReadAllText(pathfile);
                if (stringfile != null)
                {
                    var result = JsonUtils.Deserialize<ResultRunANN>(stringfile);
                    return result;
                }
            }
            return null;
        }
        #endregion


         private void btnExport_Click(object sender, EventArgs e)
         {
             Excel.Application xlApp;
             Excel.Workbook xlWorkBook;
             object misValue = System.Reflection.Missing.Value;

             xlApp = new Excel.ApplicationClass();

             xlWorkBook = xlApp.Workbooks.Add(misValue);


             try
             {
                 Excel.Worksheet xlWorkSheet1;
                 xlWorkSheet1 = (Excel.Worksheet) xlWorkBook.Worksheets.get_Item(1);
                 xlWorkSheet1.Name = "Summary";
                
                 xlWorkBook.SaveAs(DirectionIO.GetPath() + "\\KetQua1.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue,
                                   misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue,
                                   misValue, misValue, misValue, misValue);


                 //export to excelfile.
                 var dta = DirectionIO.ReadAllText(DirectionIO.GetPath() + _taskManager);
                 var listData = JsonUtils.Deserialize<List<TaskManager>>(dta);
                 progressBar1.Minimum = 0;
                 progressBar1.Maximum = listData.Count;
                 foreach (var taskManager in listData)
                 {
                     var result = GetResultRunANN(taskManager.PathFolder + "\\Result.tsk");
                     if (result != null)
                     {
                         var worksheet = (Excel.Worksheet) xlWorkBook.Worksheets.Add();
                         worksheet.Name = taskManager.TaskName;
                         SaveResultPredic(worksheet, result);
                         AddImageToCell(worksheet, 200, 15, taskManager.PathFolder + "\\ResultGraph.png");
                         SaveInfomationANN(worksheet, result);
                         //AddImageToCell(worksheet, 200, 230, taskManager.PathFolder + "\\Error.png");

                         ReleaseObject(worksheet);

                         progressBar1.Value++;
                     }
                     
                 }

                 UpdateSummerySheet(xlWorkSheet1, listData);

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

             MessageBox.Show("Xuat thang cong");

         }

        private void UpdateSummerySheet(Excel.Worksheet xlWorkSheet1, List<TaskManager> listData)
        {
            //infomation.
            xlWorkSheet1.Cells[1, 1] = "Thực hiện trên bộ dữ liệu DataTranning. Dùng mạng đó để dự đoán từ ngày 01/11/2013 tới ngày 16/11/2013.";
            xlWorkSheet1.Cells[2, 14] = "Thông tin mạng";
            xlWorkSheet1.Cells[3, 14] = "Số loại đầu vào"; xlWorkSheet1.Cells[3, 16] = "11";
            xlWorkSheet1.Cells[4, 14] = "Đầu vào"; xlWorkSheet1.Cells[4, 16] = "55";
            xlWorkSheet1.Cells[5, 14] = "Đầu ra"; xlWorkSheet1.Cells[5, 16] = "1";
            xlWorkSheet1.Cells[6, 14] = "Số lớp ẩn"; xlWorkSheet1.Cells[6, 16] = "3";
            xlWorkSheet1.Cells[7, 14] = "Số noron trong một lớp ẩn"; xlWorkSheet1.Cells[7, 16] = "14";
            int colum = 3;
            if (listData.Count > 0)
            {
                foreach (var taskManager in listData)
                {
                    var result = GetResultRunANN(taskManager.PathFolder + "\\Result.tsk");
                    if (result != null)
                    {

                        if (colum == 3)
                        {
                            xlWorkSheet1.Cells[9, colum - 2] = "Ngày";
                            xlWorkSheet1.Cells[9, colum - 1] = "Giá trị thực";


                            xlWorkSheet1.Cells[37, colum - 1] = "Thời gian học (phút):";
                            xlWorkSheet1.Cells[38, colum - 1] = "Số lần lặp:";
                            xlWorkSheet1.Cells[39, colum - 1] = "Mạng hội tụ";
                        }
                        xlWorkSheet1.Cells[9, colum] = taskManager.TaskName;
                        xlWorkSheet1.Cells[36, colum] = taskManager.TaskName;
                        int k = 9;
                        foreach (var results in result.ListResult)
                        {
                            k++;
                            if (colum == 3)
                            {
                                //thong tin ngày thang
                                xlWorkSheet1.Cells[k, colum - 2] = results.Date;
                                // thong tin gia trị thực
                                xlWorkSheet1.Cells[k, colum - 1] = results.ActualClose;
                            }
                            xlWorkSheet1.Cells[k, colum] = "=VLOOKUP(A" + k.ToString(CultureInfo.InvariantCulture) + "," + taskManager.TaskName + "!$A$2:$C$11,3,0)";
                        

                        }

                        xlWorkSheet1.Cells[37, colum] = result.TotalMinute;
                        xlWorkSheet1.Cells[38, colum] = result.Counter;
                        xlWorkSheet1.Cells[39, colum] = result.Ishoitu ? 1:0;


                        colum++;
                    }

                }
            }
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
        //private void btnExport_Click(object sender, EventArgs e)
        //{
        //    Workbook workbook = new Workbook();
        //    var sumary = CreateSumarySheet();
        //    workbook.Worksheets.Add(sumary);
        //    //export to excelfile.
        //    var dta = DirectionIO.ReadAllText(DirectionIO.GetPath() + _taskManager);
        //    var listData = JsonUtils.Deserialize<List<TaskManager>>(dta);
        //    foreach (var taskManager in listData)
        //    {
        //        var result = GetResultRunANN(taskManager.PathFolder + "/Result.tsk");
        //        if (result != null)
        //        {
        //            var worksheet = new Worksheet(taskManager.TaskName);
        //            SaveResultPredic(worksheet, result);
        //            AddImageToCell(worksheet, 1, 5, taskManager.PathFolder + "/ResultGraph.png");
        //            SaveInfomationANN(worksheet, result);
        //            AddImageToCell(worksheet, 24, 0, taskManager.PathFolder + "/Error.png");
        //            workbook.Worksheets.Add(worksheet);
        //        }
        //    }
        //    workbook.Save(DirectionIO.GetPath() + "/KetQua.xls");
        //}

        //private Worksheet CreateSumarySheet()
        //{
        //    var sumary = new Worksheet("Summary");

        //    return sumary;

        //}

        private void SaveResultPredic(Excel.Worksheet worksheet, ResultRunANN result)
        {
            worksheet.Cells[1, 1] = "Ngày";
            worksheet.Cells[1, 2] = "Giá trị thực";
            worksheet.Cells[1, 3] ="Giá trị tính toán";
            worksheet.Cells[1, 4] = "Lỗi MSE ";
            int rowindex = 2;
            foreach (var commodityResultse in result.ListResult)
            {
                worksheet.Cells[rowindex, 1] = commodityResultse.Date;
                worksheet.Cells[rowindex, 2] = commodityResultse.ActualClose;
                worksheet.Cells[rowindex, 3] = commodityResultse.PredictedClose;
                worksheet.Cells[rowindex, 4] = commodityResultse.Error;
                rowindex++;
            }
        }

        private void SaveInfomationANN(Excel.Worksheet worksheet, ResultRunANN result)
        {
            worksheet.Cells[18, 1] = "Thông tin mang học";
            worksheet.Cells[19, 1] = "Thời gian học (phút):"; worksheet.Cells[19,2] = result.TotalMinute;
            worksheet.Cells[20, 1] ="Số lần lặp:"; worksheet.Cells[20, 2] = result.Counter;
            worksheet.Cells[21, 1] = "Mạng hội tụ:"; worksheet.Cells[21, 2] = result.Ishoitu ? "hội tụ": "không hội tụ";
        }

        /// <summary>
        /// 350,15
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="rowindex"></param>
        /// <param name="colindex"></param>
        /// <param name="file"></param>
        private void AddImageToCell(Excel.Worksheet worksheet, ushort rowindex, ushort colindex, string file)
        {
            //Picture pic = new Picture();
            //pic.Image = Image.FromFile(file);
            //pic.TopLeftCorner = new CellAnchor(rowindex, colindex, 0, 0);
            //// pic.BottomRightCorner = new CellAnchor(12, 5, 592, 243);
            //worksheet.AddPicture(pic);

            worksheet.Shapes.AddPicture(file, Microsoft.Office.Core.MsoTriState.msoFalse,
       Microsoft.Office.Core.MsoTriState.msoCTrue, rowindex, colindex, 500, 200);
            //try
            //{
            //    Excel.Range oRange = (Excel.Range)worksheet.Cells[rowindex, colindex];

            //    Image oImage = Image.FromFile(file);
            //    System.Windows.Forms.Clipboard.SetDataObject(oImage, true);
            //    worksheet.Paste(oRange, file);
            //}
            //catch (Exception )
            //{
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.ApplicationClass();

            xlWorkBook = xlApp.Workbooks.Add(misValue);


            try
            {
                Excel.Worksheet xlWorkSheet1;
                xlWorkSheet1 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet1.Name = "Summary";
                
                xlWorkBook.SaveAs(DirectionIO.GetPath() + "\\KetQua11.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue,
                                  misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue,
                                  misValue, misValue, misValue, misValue);


                //export to excelfile.
                var dta = DirectionIO.ReadAllText(DirectionIO.GetPath() + _taskManager);
                var listData = JsonUtils.Deserialize<List<TaskManager>>(dta);

                xlWorkSheet1.Cells[2, 1] = "Thời gian học (phút):";
                xlWorkSheet1.Cells[3, 1] = "Số lần lặp:";
                xlWorkSheet1.Cells[4, 1] = "Mạng hội tụ::";

                progressBar1.Minimum = 0;
                progressBar1.Maximum = listData.Count;
                int k = 2;
                foreach (var taskManager in listData)
                {
                    var result = GetResultRunANN(taskManager.PathFolder + "\\Result.tsk");
                    if (result != null)
                    {
                        xlWorkSheet1.Cells[1, k] = taskManager.TaskName;
                        xlWorkSheet1.Cells[2, k] = result.TotalMinute;
                        xlWorkSheet1.Cells[3, k] = result.Counter;
                        xlWorkSheet1.Cells[4, k] = result.Ishoitu ? 1:0;
                        k++;
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

        }
    }
}
