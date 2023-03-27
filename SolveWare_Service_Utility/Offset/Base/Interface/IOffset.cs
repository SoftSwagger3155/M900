using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Offset.Base.Interface
{
    public interface IOffset: ICommonJobFundamental
    {
        int Do_Safe_Prevention();

    }
}
