using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Tasks;

namespace Tasks.NetCoreExample.Examples.Bugfixes
{
    static class Bugfix2855
    {
        #region 2855 Bug Failed to Set Start and End Date for Summary Task

        /// <summary>
        /// Test bugfix: Failed to Set Start and End Date for Summary Task
        /// </summary>
        public static void FixBugSummaryTask()
        {
            Console.WriteLine("2855 Bugfix test ...");

            Project project = new Project()
            {
                CalculationMode = CalculationMode.Manual
            };

            project.Set(Prj.StartDate, Convert.ToDateTime("03-Jul-2017"));
            project.Set(Prj.FinishDate, Convert.ToDateTime("10-Jul-2017"));

            Task task = project.RootTask.Children.Add("Task");

            task.Set(Tsk.Start, Convert.ToDateTime("05-Jul-2017"));
            task.Set(Tsk.Finish, Convert.ToDateTime("07-Jul-2017"));

            Console.WriteLine($"Calculation Mode: {project.CalculationMode}");
            Console.WriteLine($"Project Start Date: {project.Get(Prj.StartDate)}");
            Console.WriteLine($"Project Finish Date: {project.Get(Prj.FinishDate)}");
            Console.WriteLine($"Task Start Date: {task.Get(Tsk.Start)}");
            Console.WriteLine($"Task Finish Date: {task.Get(Tsk.Finish)}");
        }

        #endregion
    }
}

