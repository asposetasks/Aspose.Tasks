using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Aspose.Tasks;
using Aspose.Tasks.Saving;
using Aspose.Tasks.Visualization;

namespace Tasks.NetCoreExample.Examples.Features
{
    static class Feature2816
    {
        
        #region Feature 2816 Implement a feature to customize styles of custom fields

        /// <summary>
        /// Implement a feature to customize styles of custom fields
        /// </summary>
        public static void AddTaskStyle()
        {
            Console.WriteLine("Adding new task style to project ...");

            var project = new Project(Program.FilenameMppTask);

            // Add new task 
            var task = project.RootTask.Children.Add("New task 0");
            var task1 = project.RootTask.Children.Add("New task 1");
            var task2 = project.RootTask.Children.Add("New task 2");
            var task3 = project.RootTask.Children.Add("New task 3");
            var task4 = project.RootTask.Children.Add("New task 4");
            var task5 = project.RootTask.Children.Add("New task 5");

            var view = ProjectView.GetDefaultTaskSheetView();

            foreach (var column in view.Columns) column.TextStyleModificationCallback = new GrayTextStyleCallback();

            project.Save(Program.FilenameStylePdfOutput,
                new PdfSaveOptions
                {
                    PageSize = PageSize.A4,
                    View = view,
                    PresentationFormat = PresentationFormat.TaskSheet
                });
        }

        private class GrayTextStyleCallback : ITextStyleModificationCallback
        {
            public void BeforeTaskTextStyleApplied(TaskTextStyleEventArgs args)
            {
                if (args.Task.Get(Tsk.Uid) % 2 == 0)
                {
                    args.CellTextStyle.Color = Color.Black;
                    args.CellTextStyle.BackgroundColor = Color.Gray;
                    args.CellTextStyle.BackgroundPattern = BackgroundPattern.SolidFill;
                    args.CellTextStyle.FontStyle = FontStyle.Bold;
                    args.CellTextStyle.SizeInPoints = 16;
                }
                else
                {
                    args.CellTextStyle.Color = Color.Black;
                    args.CellTextStyle.BackgroundColor = Color.Empty;
                    args.CellTextStyle.FontStyle = FontStyle.Bold;
                    args.CellTextStyle.SizeInPoints = 16;
                }
            }
        }

        #endregion

    }
}
