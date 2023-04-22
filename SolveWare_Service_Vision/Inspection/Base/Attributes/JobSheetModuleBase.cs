using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Base.Attributes
{
    public abstract class JobSheetModuleBase : ElementBase, JobSheetModule
    {
        ICameraBase camera;
        IJobSheetData data;
        public JobSheetModuleBase(ICameraBase camera, IJobSheetData data)
        {
            this.camera = camera;
        }
        public abstract Mission_Report Do_Job();
    }
}
