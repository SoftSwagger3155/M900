using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.JobSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{
    public class Job_PatternMatch : JobFundamentalBase, IDataModulePair
    {
        public Data_InspectionKit jobParam;


        public override int Do_Job()
        {
            //TODO: Stanley 1.实现 Pattern Math 细节

            return ErrorCode;
        }
        public void Setup(IElement data)
        {
            this.jobParam = data as Data_InspectionKit;
        }
    }
}
