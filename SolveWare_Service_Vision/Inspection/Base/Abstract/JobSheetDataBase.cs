using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Base.Abstract
{
    public class JobSheetDataBase : ElementBase, IJobSheetData
    {
        public string InspectKitData_Name { get; set; }
    }
}
