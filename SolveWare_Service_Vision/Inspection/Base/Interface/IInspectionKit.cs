using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Base.Interface
{
    public interface IInspectionKit
    {
        void Set_Lighting();
        void Set_Brightness();
        int Do_PatternMath();
        int Do_Blob();
        int Do_Inspec();
        void Save();
    }
}
