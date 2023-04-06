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
        public override int Do_Job()
        {
            string errMsg  = string.Empty;
            try
            {
                do
                {
                    CameraMediaBase camera = jobParam.CameraName.GetCamera();
                    if (camera == null) 
                    {
                        errorCode = ErrorCodes.NoRelevantObject;
                        errMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                     }

                    camera.SetGain(this.jobParam.JobSheet_Brightness_Data.Gain);
                    camera.SetExposureTime(this.jobParam.JobSheet_Brightness_Data.ExposureTime);

                } while (false);
            }
            catch (Exception ex)
            {
                this.errorMsg += ex.Message;
            }

            Get_Result(nameof(Do_Job), errMsg);
            return ErrorCode;
        }
        public void Setup(IElement data)
        {
            this.jobParam = data as Data_InspectionKit;
        }
    }
}
