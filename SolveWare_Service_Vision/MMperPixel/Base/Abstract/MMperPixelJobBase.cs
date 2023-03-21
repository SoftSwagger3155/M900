using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.MMperPixel.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.MMperPixel.Base.Abstract
{
    public class MMperPixelJobBase: JobFundamentalBase, IMMperPixelJob
    {
        CameraBase camera;
        public void Setup(ICamera camera)
        {
            this.camera = camera as CameraBase;
        }
    }
}
