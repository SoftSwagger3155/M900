using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Index.Base.Interface
{
    public interface IIndexData
    {
        int Current_No_Of_Unit_X { get; set; }
        int Current_No_Of_Unit_Y { get; set; }
        int Current_Unit_No { get; set; }
        int Total_Unit_No { get; set; }

    }
}
