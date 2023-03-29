using HalconDotNet;
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
    public interface IVisionController
    {
        void Setup(HWindowControl HWindow, ICameraBase camera);
        void ClearROIs();
        void ClearCrossLine();
        void GenerateCrossLine();
        void GrabOne();
        void StartLive(int delay_ms);
        void StopLive(int delay_ms);
        void Fit_Image();
        void Open_File_To_Get_Image();
        void WriteText(string msg);
        void AddROI(ROIBase roiMode);
        int Do_Inspection(IInspectionKit kit);
    }
}
