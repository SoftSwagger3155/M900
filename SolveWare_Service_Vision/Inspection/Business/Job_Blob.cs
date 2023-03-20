using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Vision.Inspection.JobSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{
    public class Job_Blob : IDataModulePair
    {
        JobSheet_Blob jobParam = null;


        public int Do_Job()
        {
            return 0;
        }

        public void Setup(IElement data)
        {
            throw new NotImplementedException();
        }
    }
}
