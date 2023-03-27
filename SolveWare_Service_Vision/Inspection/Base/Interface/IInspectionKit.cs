﻿using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Base.Interface
{
    public interface IInspectionKit: IJobFundamental
    {
        int Set_Lighting();
        int Set_Brightness();
        int Do_PatternMath();
        int Do_Blob();
    }
}
