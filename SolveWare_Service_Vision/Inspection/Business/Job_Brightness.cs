using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.JobSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{
    public class Job_Brightness : JobFundamentalBase, IDataModulePair
    {
        public Data_InspectionKit jobParam;
        public override Mission_Report Do_Job()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    CameraMediaBase camera = jobParam.CameraName.GetCamera();
                    if (camera == null) 
                    {
                        context.Set(ErrorCodes.NoRelevantObject, "无相机物件");
                        break;
                     }

                    camera.SetGain(this.jobParam.JobSheet_Brightness_Data.Gain);
                    camera.SetExposureTime(this.jobParam.JobSheet_Brightness_Data.ExposureTime);

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.VisionFailed);
            }

            return context;
        }
        public void Setup(IElement data)
        {
            this.jobParam = data as Data_InspectionKit;
        }
    }
}
