using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
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
            try
            {
                jobParam.JobSheet_Brightness_Datas.ForEach(x =>
                {
                    x.Camera.Gain = x.Gain;
                    x.Camera.ExposureTime = x.ExposureTime;
                    x.Camera.SetBrightness();
                });
            }
            catch (Exception ex)
            {
                this.errorMsg += ex.Message;
                errorCode = ErrorCodes.ActionFailed;
            }

            return ErrorCode;
        }
        public void Setup(IElement data)
        {
            this.jobParam = data as Data_InspectionKit;
        }
    }
}
