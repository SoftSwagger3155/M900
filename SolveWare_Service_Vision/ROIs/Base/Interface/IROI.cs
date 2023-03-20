using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.ROIs.Base.Interface
{
    public interface IROI
    {
        string RoiType { get; set; }
        string RoiName { get; set; }
    }
}
