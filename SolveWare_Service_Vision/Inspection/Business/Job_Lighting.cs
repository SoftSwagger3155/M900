using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Vision.Inspection.JobSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{
    public class Job_Lighting : JobFundamentalBase, IDataModulePair
    {
        JobSheet_Lighting jobParam;
        public override int Do_Job()
        {
            return ErrorCode;
        }

        public void Setup(IElement data)
        {
            this.jobParam = data as JobSheet_Lighting;
        }
    }
}
