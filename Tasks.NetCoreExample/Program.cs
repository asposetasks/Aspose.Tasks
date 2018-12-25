using System;
using System.IO;
using System.Linq;
using Aspose.Tasks;
using Aspose.Tasks.Saving;
using Aspose.Tasks.Visualization;
using Tasks.NetCoreExample.Examples.Bugfixes;
using Tasks.NetCoreExample.Examples.Enhancements;
using Tasks.NetCoreExample.Examples.Features;

namespace Tasks.NetCoreExample
{
    internal class Program
    {
        public static readonly string SavePath = Path.Combine(AppContext.BaseDirectory, "out");
        public static readonly string FilenameMppTask = "Blank2010.mpp";
        public static readonly string FilenameMppOutput = Path.Combine(SavePath, "sample_out.mpp");
        public static readonly string FilenamePdfOutput = Path.Combine(SavePath, "sample_out.pdf");
        public static readonly string FilenameStylePdfOutput = Path.Combine(SavePath, "TestTaskTextStyleCallback.pdf");
        private static Project _project;

        /// TASKSNET-2358  NET Standard 2.0
        private static void Main(string[] args)
        {
            Console.WriteLine("Program started...");

            _project = new Project(FilenameMppTask);

            try
            {
                var license = new License();
                license.SetLicense("Aspose.Tasks.lic");
                Directory.CreateDirectory(SavePath);

                Feature2358.FixBugSummaryTask();
                Feature2816.AddTaskStyle();

                Enhancement2860.FixGanttTable();
                Enhancement2829.CalculationConstraintDate();
                Enhancement2825.FixPeakValue();

                Bugfix2882.FixBugkey();
                Bugfix2855.FixBugSummaryTask();
                Bugfix2828.BugfixUpdatingPercent();
                Bugfix2712.LoadLicense();

                // TODO: 2790, 2732, 2727, 2689, 2653, 2622, 2507, 2852
                // Bugfix2852.TestBugfix2852();


                // Reading original file
                // ReadProject(_project);
                // AddTaskToProject(_project);


                // Add new attributes
                // AddCustomAttributes();

                // Reading original file again
                // ReadProject(_project);

                // Save files
                // SaveProject();

                // Adding custom style to fields
                // AddTaskStyle();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unhandled Error:" + e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Program stopped. Press any key to exit...");
            }

            Console.WriteLine("Done. Press Enter to exit...");
            Console.ReadLine();
        }


        /// <summary>
        ///     Save project in PDF and MPP format
        /// </summary>
        private static void SaveProject()
        {
            var view = ProjectView.GetDefaultTaskSheetView();

            _project.Save(FilenamePdfOutput, new PdfSaveOptions
            {
                PageSize = PageSize.A4,
                View = view,
                PresentationFormat = PresentationFormat.TaskSheet
            });

            // Save the project as MPP project file - license required
            // var mppSaveOptions = new MPPSaveOptions {WriteViewData = true}; - Bug TASKSNET-2897
            _project.Save(FilenameMppOutput, SaveFileFormat.MPP);
            Console.WriteLine("Files saved");
        }


        #region ReadProject Project File

        /// <summary>
        ///     ReadProject Tasks Project File
        /// </summary>
        /// <param name="project"></param>
        private static void ReadProject(Project project)
        {
            Console.WriteLine("Reading project file...");

            Console.WriteLine($"\n\t Project has {project.RootTask.Children.Count} task(s)");

            // Read extended attributes for tasks
            foreach (var tsk in project.RootTask.Children)
                if (tsk.ExtendedAttributes.Any())
                {
                    Console.WriteLine($"\n\t\t ExtendedAttributes for task: {tsk.Get(Tsk.Name)} ");
                    Console.WriteLine($"\t\t Task has extended attributes count: {tsk.ExtendedAttributes.Count}");

                    foreach (var ea in tsk.ExtendedAttributes)
                    {
                        Console.WriteLine(
                            $"\t\t - Extended Attribute {ea.AttributeDefinition.FieldName} with alias {ea.AttributeDefinition.Alias} and type {ea.AttributeDefinition.ElementType.ToString()}");

                        Console.WriteLine($"\t\t - FieldId: {ea.FieldId}, ValueGuid: {ea.ValueGuid}");

                        switch (ea.AttributeDefinition.CfType)
                        {
                            case CustomFieldType.Date:
                            case CustomFieldType.Start:
                            case CustomFieldType.Finish:
                                Console.WriteLine($"\t\t\t - DateValue: {ea.DateValue}");
                                break;

                            case CustomFieldType.Text:
                                Console.WriteLine($"\t\t\t - TextValue: {ea.TextValue}");
                                break;

                            case CustomFieldType.Duration:
                                Console.WriteLine($"\t\t\t - DurationValue: {ea.DurationValue.ToString()}");
                                break;

                            case CustomFieldType.Cost:
                            case CustomFieldType.Number:
                                Console.WriteLine($"\t\t\t - NumericValue: {ea.NumericValue}");
                                break;

                            case CustomFieldType.Flag:
                                Console.WriteLine($"\t\t\t - NumericValue: {ea.FlagValue}");
                                break;
                            case CustomFieldType.Null:
                                break;
                            case CustomFieldType.OutlineCode:
                                break;
                            case CustomFieldType.RBS:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }

            Console.WriteLine("Reading completed...");
        }

        #endregion

        #region Add tasks

        /// <summary>
        ///     Add tasks
        /// </summary>
        /// <param name="project"></param>
        private static void AddTaskToProject(Project project)
        {
            Console.WriteLine("Adding new task to project ...");

            // Add new task 
            var task = project.RootTask.Children.Add("New task 1");
            var secondTask = project.RootTask.Children.Add("New task 2");
        }

        #endregion

        #region Add custom attributes

        /// <summary>
        ///     Add custom attributes
        /// </summary>
        private static void AddCustomAttributes()
        {
            // Add new text3 extended attribute with lookup and one lookup value
            var taskTextAttributeDefinition =
                ExtendedAttributeDefinition.CreateLookupTaskDefinition(ExtendedAttributeTask.Text3,
                    "New text3 attribute");
            taskTextAttributeDefinition.ElementType = ElementType.Task;
            _project.ExtendedAttributes.Add(taskTextAttributeDefinition);

            var textVal = new Value();
            textVal.Id = 1;
            textVal.Description = "Text value description";
            textVal.Val = "Text value 1";

            taskTextAttributeDefinition.AddLookupValue(textVal);

            // Add new cost1 extended attribute with lookup and two cost values
            var taskCostAttributeDefinition =
                ExtendedAttributeDefinition.CreateLookupTaskDefinition(ExtendedAttributeTask.Cost1,
                    "New cost 1 attribute");
            _project.ExtendedAttributes.Add(taskCostAttributeDefinition);

            var costVal1 = new Value { Id = 2, Description = "Cost value 1 description", Val = "55300" };

            var costVal2 = new Value { Id = 3, Description = "Cost value 2 description", Val = "210100" };

            taskCostAttributeDefinition.AddLookupValue(costVal1);
            taskCostAttributeDefinition.AddLookupValue(costVal2);

            // Add new task and assign attribute lookup value.
            var task = _project.RootTask.Children.Add("New task");

            var taskAttr = taskCostAttributeDefinition.CreateExtendedAttribute(costVal1);
            task.ExtendedAttributes.Add(taskAttr);

            var taskStartAttributeDefinition =
                ExtendedAttributeDefinition.CreateLookupTaskDefinition(ExtendedAttributeTask.Start7,
                    "New start 7 attribute");

            var startVal = new Value
            {
                Id = 4,
                DateTimeValue = new DateTime(1984, 01, 01),
                Description = "Start 7 value description"
            };

            taskStartAttributeDefinition.AddLookupValue(startVal);

            _project.ExtendedAttributes.Add(taskStartAttributeDefinition);

            var taskFinishAttributeDefinition =
                ExtendedAttributeDefinition.CreateLookupTaskDefinition(ExtendedAttributeTask.Finish4,
                    "New finish 4 attribute");

            var finishVal = new Value
            {
                Id = 5,
                DateTimeValue = new DateTime(2019, 01, 01),
                Description = "Finish 4 value description"
            };

            taskFinishAttributeDefinition.ValueList.Add(finishVal);

            _project.ExtendedAttributes.Add(taskFinishAttributeDefinition);

            var numberAttributeDefinition =
                ExtendedAttributeDefinition.CreateLookupTaskDefinition(ExtendedAttributeTask.Number20,
                    "New number attribute");

            var val1 = new Value { Id = 6, Val = "1", Description = "Number 1 value" };
            var val2 = new Value { Id = 7, Val = "2", Description = "Number 2 value" };
            var val3 = new Value { Id = 8, Val = "3", Description = "Number 3 value" };

            numberAttributeDefinition.AddLookupValue(val1);
            numberAttributeDefinition.AddLookupValue(val2);
            numberAttributeDefinition.AddLookupValue(val3);

            _project.ExtendedAttributes.Add(numberAttributeDefinition);


            var rscStartAttributeDefinition =
                ExtendedAttributeDefinition.CreateLookupResourceDefinition(ExtendedAttributeResource.Start5,
                    "New start5 attribute");

            var startVal2 = new Value
            {
                Id = 9,
                DateTimeValue = new DateTime(1984, 01, 01),
                Description = "this is start5 value description"
            };

            rscStartAttributeDefinition.AddLookupValue(startVal2);

            _project.ExtendedAttributes.Add(rscStartAttributeDefinition);

            // Define a duration attribute without lookup.
            var taskDurationAttributeDefinition =
                ExtendedAttributeDefinition.CreateTaskDefinition(ExtendedAttributeTask.Duration1, "New Duration");
            _project.ExtendedAttributes.Add(taskDurationAttributeDefinition);

            // Add new task and assign duration value to the previously defined duration attribute.
            var timeTask = _project.RootTask.Children.Add("New task");

            var durationExtendedAttribute = taskDurationAttributeDefinition.CreateExtendedAttribute();

            durationExtendedAttribute.DurationValue = _project.GetDuration(3.0, TimeUnitType.Hour);
            timeTask.ExtendedAttributes.Add(durationExtendedAttribute);
        }

        #endregion
    }
}