using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Tasks;
using Aspose.Tasks.Saving;

namespace Tasks.NetCoreExample.Examples.Features
{
    class Feature2358
    {

        /// <summary>
        /// Test: support of .NET Standard 2.0
        /// </summary>
        public static void FixBugSummaryTask()
        {

            License license = new License();
            license.SetLicense("Aspose.Tasks.lic");

            Project project = new Project();

            Console.WriteLine("Adding new task to project ...");

            // Add new task 
            var task = project.RootTask.Children.Add("New task 1");
            var secondTask = project.RootTask.Children.Add("New task 2");

            project.Save(Path.Combine(Program.SavePath, "Sample2358.pdf"), SaveFileFormat.PDF);

            Console.WriteLine("Hello World from .NET Core 2.0!");
        }

    }
}
