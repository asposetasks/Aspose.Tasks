using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Tasks;
using Aspose.Tasks.Saving;

namespace Tasks.NetCoreExample.Examples.Bugfixes
{
    static class Bugfix2828
    {

        #region 2828 Updating of percent complete doesn't work for milestone tasks Bug

        /// <summary>
        /// Test bugfix: Updating of percent complete doesn't work for milestone tasks 
        /// </summary>
        public static void BugfixUpdatingPercent()
        {

            Project project = new Project("Sample-2828.mpp");

            // recursive update percent complete
            UpdatePercentComplete(project.RootTask, 33);

            project.Save("Sample1.mpp", SaveFileFormat.MPP);
        }
        private static void UpdatePercentComplete(Task task, int percentComplete)
        {
            if (task.Children.Count == 0)
            {
                task.Set(Tsk.PercentComplete, percentComplete);
            }
            else
            {
                foreach (Task t in task.Children)
                {
                    UpdatePercentComplete(t, percentComplete);
                }
            }
        }
        #endregion
    }
}
