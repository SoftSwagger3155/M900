using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Index.Base.Interface
{
    public interface IIndex: ICommonJobFundamental
    {
        Mission_Report Do_Save_Prevention();
        Mission_Report GoNext();
        Mission_Report GoPrevious();
        Mission_Report Go(int number);
    }
}
