using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Tasks;

namespace Tasks.NetCoreExample.Examples.Enhancements
{
    static class Enhancement2825
    {
         #region 2825 Enhancement Fix the calculation of peak value while adding a custom timephased data in automatic mode.

         /// <summary>
         /// Test Enhancement: fix the calculation of peak value while adding a custom timephased data in automatic mode.
         /// </summary>
        public static void FixPeakValue()
        {
            Console.WriteLine("Fix the calculation of peak value while adding a custom timephased data in automatic mode ...");
            foreach (CalculationMode calculationMode in (CalculationMode[])Enum.GetValues(typeof(CalculationMode)))
            {
                // for each value CalculationMode
                TestCalculationOfPeak(calculationMode);
            }
        }
        private static void TestCalculationOfPeak(CalculationMode calculationMode)
        {
            var project = new Project()
            {
                CalculationMode = calculationMode
            };

            var task = project.RootTask.Children.Add("Task 1");
            task.Set(Tsk.IsManual, false);
            task.Set(Tsk.Start, new DateTime(2018, 10, 30, 8, 0, 0));
            task.Set(Tsk.Duration, project.GetDuration(16, TimeUnitType.Hour));

            var assn = project.ResourceAssignments.Add(task, project.Resources.Add("Resource 1"));
            assn.Set(Asn.WorkContour, WorkContourType.Contoured);
            assn.TimephasedData.Clear();
            assn.TimephasedData.Add(
                new TimephasedData
                {
                    Uid = 1,
                    Start = new DateTime(2018, 10, 30, 8, 0, 0),
                    Finish = new DateTime(2018, 10, 31, 8, 0, 0),
                    Value = "PT14H0M0S",
                    Unit = TimeUnitType.Hour,
                    TimephasedDataType = TimephasedDataType.AssignmentRemainingWork,
                });
            assn.TimephasedData.Add(
                new TimephasedData
                {
                    Uid = 1,
                    Start = new DateTime(2018, 10, 31, 8, 0, 0),
                    Finish = new DateTime(2018, 10, 31, 17, 0, 0),
                    Value = "PT2H0M0S",
                    Unit = TimeUnitType.Hour,
                    TimephasedDataType = TimephasedDataType.AssignmentRemainingWork,
                });

            if (calculationMode != CalculationMode.Automatic)
            {
                project.RecalculateResourceFields();
                project.RecalculateResourceStartFinish();
                project.Recalculate();
            }

            Console.WriteLine($"calculationMode: {calculationMode}   PeakUnits: {assn.Get(Asn.PeakUnits)}   WorkTimeSpan: {assn.Get(Asn.Work).TimeSpan}   TaskWorkTimeSpan: {assn.Get(Asn.Task).Get(Tsk.Work).TimeSpan}");
        }
        #endregion



    }
}
