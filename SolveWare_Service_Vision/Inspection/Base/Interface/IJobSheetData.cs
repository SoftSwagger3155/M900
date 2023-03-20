using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Base.Interface
{
    public interface IJobSheetData: IElement
    {
        string InspectKitData_Name { get; set; }
    }
}
