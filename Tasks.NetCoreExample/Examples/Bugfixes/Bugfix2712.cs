using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Tasks;

namespace Tasks.NetCoreExample.Examples.Bugfixes
{
    class Bugfix2712
    {
        #region 2712 Bug Aspose.Tasks License/Assembly not found

        /// <summary>
        /// Test bug fix: Aspose.Tasks License/Assembly not found
        /// </summary>
        public static void LoadLicense()
        {
            License license = new License();
            license.SetLicense("Aspose.Tasks.lic");
        }


        #endregion
    }
}
