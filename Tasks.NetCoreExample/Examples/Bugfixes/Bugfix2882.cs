using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Tasks;

namespace Tasks.NetCoreExample.Examples.Bugfixes
{
    static  class Bugfix2882
    {
        #region 2882 Bug An element with the same key has already been added.

        /// <summary>
        ///  Test bugfix: An element with the same key has already been added.
        /// </summary>
        public static void FixBugkey()
        {
            Console.WriteLine("2882 Bug same key has already been added ...");

            Project project = new Project();

            Task task = project.RootTask.Children.Add("Task");
            task.Set(Tsk.ConstraintType, ConstraintType.AsLateAsPossible);

            Task subTask = task.Children.Add("SubTask");

            // Release 18.11 - we get the error message: An element with the same key has already been added.
            // Release 18.12 - it will work.
            subTask.Set(Tsk.Start, DateTime.Now);
        }

        #endregion
    }
}
