using HalconDotNet;
using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Camera.Base.Interface
{
    public interface ICameraMedia: IElement
    {
        void AssignCameraMedia(object obj_Media);
        bool SetExposureTime(int exposureTime);
        bool SetGain(int gain);
        bool SetAutoGain(bool autoGain);
        bool SetAutoExposure(bool autoExposure);
        int StartLive(int delayTime_ms = 100);
        int StopLive();
        bool Open();
        bool Close();
    }
}
