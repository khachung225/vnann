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
                task.PathFolder = path + "/" + task.TaskName;
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
    }
}
