using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Offset.Base.Interface
{
    public interface IOffset: ICommonJobFundamental
    {
        Mission_Report Do_Safe_Prevention();

    }
}
