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
    public class Job_Blob : JobFundamentalBase, IDataModulePair
    {
        Data_Inspection jobParam = null;

        public override Mission_Report Do_Job()
        {
            //TODO: Stanley 1.实现 Blob 的细节




            return new Mission_Report();
        }

        public void Setup(IElement data)
        {
            this.jobParam = data as Data_Inspection;
        }
    }
}
