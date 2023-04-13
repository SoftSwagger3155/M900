using MF900_SolveWare.Offset.Data;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Utility.Offset.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Offset.Job
{
    public class Job_Offset : DataJobPairFundamentalBase<Data_Offset>, IOffset
    {
        public Job_Offset()
        {
          
        }

        public int GoFirstPos()
        {


            return errorCode;
        }
        public int GoSecondPos() {


            return errorCode;
        }

        public int Do_Safe_Prevention()
        {
            throw new NotImplementedException();
        }
        public override int Do_Job()
        {
            return base.Do_Job();
        }


    }
}
