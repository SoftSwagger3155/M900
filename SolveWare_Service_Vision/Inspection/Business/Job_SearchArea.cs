﻿using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.JobSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{
    public class Job_SearchArea : JobFundamentalBase, IDataModulePair
    {
        Data_Inspection jobParam;

        public override Mission_Report Do_Job()
        {
            //TODO: Stanley 1.实现 Search Area 细节

            return new Mission_Report();
        }

        public void Setup(IElement data)
        {
            this.jobParam = data as Data_Inspection;
        }
    }
}
