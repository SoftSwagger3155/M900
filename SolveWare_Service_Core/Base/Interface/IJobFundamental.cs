﻿using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Base.Interface
{
    public  interface IJobFundamental: IElement
    {
        Mission_Report Do_Job();
    }
}
