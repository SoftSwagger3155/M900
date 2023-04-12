using MF900_SolveWare.Offset.Data;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Offset.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Offset.Job
{
    //TODO 杨工UI | Stanley 实现细节
    [ResourceBase(ConstantProperty.ResourceKey_Offset)]
    public class Job_Offset_TopCamera_BtmPin : DataJobPairFundamentalBase<Data_OffsetData>, IOffset
    {
        /// <summary>
        /// 安全措施
        /// </summary>
        /// <returns></returns>
        public int Do_Safe_Prevention()
        {
            return 0;
        }

        /// <summary>
        /// 执行内容
        /// </summary>
        /// <returns></returns>
        public override int Do_Job()
        {
            return ErrorCode;
        }
    }
}
