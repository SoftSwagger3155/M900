using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Camera.Base.Interface
{
    public interface ICameraBase : IElement
    {
        void AssingCamera(object obj_Camera);
        void GetExposureTime();
        int SetBrightness();
        void GetFrameRate();
        int SetFrameRate();
        int StartLive(int delayTime_ms = 100);
        int StopLive(int delayTime__ms = 100);
        int GrabImageOnce();
        void CloseCamera();
    }
}
