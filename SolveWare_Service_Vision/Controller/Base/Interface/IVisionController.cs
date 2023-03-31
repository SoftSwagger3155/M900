using HalconDotNet;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using SolveWare_Service_Vision.ROIs.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Controller.Base
{
    public interface IVisionController: IElement
    {
        void Setup(HWindowControl HWindow, ICameraMedia camera);
        void ClearROIs();
        void ClearCrossLine();
        void GenerateCrossLine();
        void Fit_Image();
        void Open_File_To_Get_Image();
        void WriteText(string msg);
        void AddROI(ROIBase roiMode);
        int Do_Inspection(IInspectionKit kit);
        int StartLive(int delayTime_ms = 100);
        int StopLive();
        void GrabOneShot();
    }
}
