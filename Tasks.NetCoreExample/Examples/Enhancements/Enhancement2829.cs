using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Tasks;

namespace Tasks.NetCoreExample.Examples.Enhancements
{
    static class Enhancement2829
    {

        #region Enhancement 2829 Add calculation of ConstraintDate for all calculation modes

        /// <summary>
        /// Test Enhancement: add calculation of ConstraintDate for all calculation modes
        /// </summary>
        public static void CalculationConstraintDate()
        {
            Console.WriteLine("Add calculation of ConstraintDate for all calculation modes ...");

            foreach (CalculationMode calculationMode in (CalculationMode[])Enum.GetValues(typeof(CalculationMode)))
            {
                foreach (ConstraintType constraintType in (ConstraintType[])Enum.GetValues(typeof(ConstraintType)))
                {
                    // for each value CalculationMode and ConstraintType we run a test
                    TestCalculationMode(calculationMode, constraintType);
                }
            }
        }

        private static void TestCalculationMode(CalculationMode calculationMode, ConstraintType constraintType)
        {
            var start = new DateTime(2018, 11, 8, 8, 0, 0);
            var finish = new DateTime(2018, 11, 8, 17, 0, 0);

            Project project = new Project();

            project.CalculationMode = calculationMode;

            project.Set(Prj.NewTasksAreManual, false);
            project.Set(Prj.StartDate, start);

            var task = project.RootTask.Children.Add("T1");

            task.Set(Tsk.Start, start);
            task.Set(Tsk.Duration, project.GetDuration(1, TimeUnitType.Day));
            task.Set(Tsk.Finish, finish);
            task.Set(Tsk.ConstraintType, constraintType);

            // When CalulationMode.None it must be se after project.Recalculate() call.
            // When we change constraint type of task in all calculation modes the constraint date must be set accordingly.

            if (calculationMode != CalculationMode.Automatic)
            {
                project.Recalculate();
            }

            var date = GetValidConstraintDate(constraintType, start, finish);

            Console.WriteLine($"calculationMode: {calculationMode}    constraintType: {constraintType}   date: {date}");
        }

        private static DateTime GetValidConstraintDate(ConstraintType constraintType, DateTime start, DateTime finish)
        {
            switch (constraintType)
            {
                case ConstraintType.Undefined:
                case ConstraintType.AsSoonAsPossible:
                case ConstraintType.AsLateAsPossible:
                    return DateTime.MinValue;

                case ConstraintType.StartNoEarlierThan:
                case ConstraintType.StartNoLaterThan:
                case ConstraintType.MustStartOn:
                    return start;

                case ConstraintType.MustFinishOn:
                case ConstraintType.FinishNoEarlierThan:
                case ConstraintType.FinishNoLaterThan:
                    return finish;

                default:
                    throw new ArgumentOutOfRangeException("constraintType", constraintType, null);
            }
        }

        #endregion


    }
}
