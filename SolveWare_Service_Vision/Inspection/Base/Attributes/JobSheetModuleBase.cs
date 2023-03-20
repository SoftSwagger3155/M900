using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Base.Attributes
{
    public abstract class JobSheetModuleBase : JobSheetModule
    {
        ICamera camera;
        IJobSheetData data;
        public JobSheetModuleBase(ICamera camera, IJobSheetData data)
        {
            this.camera = camera;
        }
        public abstract int Do_Job();
    }
}
