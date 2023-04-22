using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.MMperPixel.Base.Interface
{
    public interface IMMperPixelJob: IJobFundamental
    {
        /// <summary>
        /// 将轴或IO移到安全的位置
        /// </summary>
        /// <returns></returns>
        Mission_Report Do_Safe_Prevention();
        /// <summary>
        /// 请移到拍摄的位置
        /// </summary>
        /// <returns></returns>
        Mission_Report Move_To_Inspection_Pos();

        Mission_Report Do_MMperPixel_Conversion(ref double average);

    }
}
