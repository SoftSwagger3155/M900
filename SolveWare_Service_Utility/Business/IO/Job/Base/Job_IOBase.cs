using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Business.IO.Job.Base
{  
    public class Job_IOBase : ElementBase, IDataModulePair
    {
        public virtual int Do_Job()
        {
            throw new NotImplementedException();
        }

        public void Save(bool isShowWindow = false)
        {
            throw new NotImplementedException();
        }

        public void Setup(IElement data)
        {
            throw new NotImplementedException();
        }
    }
}
