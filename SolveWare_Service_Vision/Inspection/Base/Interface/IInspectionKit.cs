using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Base.Interface
{
    public interface IInspectionKit: IJobFundamental
    {
        Mission_Report Set_Lighting();
        Mission_Report Set_Brightness();
        Mission_Report Do_PatternMath();
        Mission_Report Do_Blob();
    }
}
