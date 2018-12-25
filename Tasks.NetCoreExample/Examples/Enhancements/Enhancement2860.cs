using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Tasks;
using Aspose.Tasks.Saving;
using Aspose.Tasks.Visualization;

namespace Tasks.NetCoreExample.Examples.Enhancements
{
    static class Enhancement2860
    {
        #region Enhancement 2860 fix test

        /// <summary>
        /// Test fix layout of Gantt table in generated PDF
        /// </summary>
        public static void FixGanttTable()
        {
            Console.WriteLine("Fix layout of Gantt table in generated PDF ...");

            var project = new Project("Sample-2860.mpp");

            project.Save(Path.Combine(Program.SavePath, "Sample-2860.pdf"),
                new PdfSaveOptions
                {
                    FitContent = true,
                    UseProjectDefaultFont = false,
                    PresentationFormat = PresentationFormat.GanttChart
                });
        }

        #endregion
    }
}
