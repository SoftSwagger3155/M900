using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Vision.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{
    public class Job_SearchArea : JobFundamentalBase, IDataModulePair
    {
        Data_InspectionKit dataKit = null;

        public override int Do_Job()
        {
            throw new NotImplementedException();
        }

        public void Setup(IElement data)
        {
            
        }
    }
}
