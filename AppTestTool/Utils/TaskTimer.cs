using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppTestTool.Data;
using BaseEntity.Entity;

namespace AppTestTool.Utils
{
    public class TaskTimer
    {
        public string _taskManager = "/TaskManager.tsk";

        #region TaskName process
        public TaskManager GetTaskNameProcessing()
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
        public TaskManager GetTaskNameProcessNext()
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

        public void UpdateStausTask(TaskManager taskManagerModif)
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

        public void CreateTask( int count)
        {
            var path = DirectionIO.GetPath();
           
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
        }
        public void CreateTask(List<DateTime> fromDate, bool isrestart = false)
        {
            var path = DirectionIO.GetPath();

            var listTask = new List<TaskManager>();
            int i = 0;
            foreach (var taskManager in fromDate)
            {
                var task = new TaskManager
                {
                    TaskCouter = i + 1,
                    TaskName = string.Format("ANN{0:yyyyMMdd}", taskManager),
                    Status = 1,
                    Day = taskManager.Date,
                };
                task.PathFolder = path + "\\" + task.TaskName;
                if (isrestart)
                {
                    DirectionIO.CreateNewFolder(task.PathFolder);
                }
                else 
                if (!DirectionIO.IsExistNewFolder(task.PathFolder))
                    DirectionIO.CreateNewFolder(task.PathFolder);
                listTask.Add(task);
                i++;
            }

            var fileconten = JsonUtils.Serialize(listTask);
            if (isrestart)
            {
                DirectionIO.WriteAllText(DirectionIO.GetPath() + _taskManager, fileconten);
            }else 
            if (!DirectionIO.IsExistFile(DirectionIO.GetPath() + _taskManager))
                DirectionIO.WriteAllText(DirectionIO.GetPath() + _taskManager, fileconten);
        }
        #region Excel call
        public ResultRunANN GetResultRunANN(string pathfile)
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
    }
}
