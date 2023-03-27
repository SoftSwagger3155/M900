using MF900_SolveWare.Offset.Data;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Utility.Offset.Base.Interface;
using SolveWare_Service_Vision.MMperPixel.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Offset.Job
{
    //TODO: 杨工 UI | Stanley 实现细节 
    public class Job_Offset_BtmCamera_BtmProber : DataJobPairFundamentalBase<Data_OffsetData>, IOffset
    {
        /// <summary>
        /// 安全措施
        /// </summary>
        /// <returns></returns>
        public int Do_Safe_Prevention()
        {
            return 0;
        }

        public override int Do_Job()
        {
            return ErrorCode;
        }

    }
}
