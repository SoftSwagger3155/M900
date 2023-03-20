using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Helper;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{
    public class Inspect : IInspectionKit
    {
        string id_Camera;
        Data_InspectionKit dataKit;
        CameraBase camera;
        public void Setup(IElement data)
        {
            dataKit = data as Data_InspectionKit;
        }

        public int Do_Blob()
        {
            int errorCode = ErrorCodes.NoError;
            if (dataKit == null) return ErrorCodes.VisionFailed;

            IDataModulePair pair =  dataKit.JobSheet_Blob_Data.GetModule();
            errorCode = pair.Do_Job();

            return errorCode;
        }

        public int Do_Inspec()
        {
            int errorCode = ErrorCodes.NoError;
            Set_Brightness();
            Set_Lighting();

            errorCode = Do_PatternMath();
            errorCode = Do_Blob();


            return errorCode;
        }

        public int Do_PatternMath()
        {
            int errorCode = ErrorCodes.NoError;
            if (dataKit == null) return ErrorCodes.VisionFailed;

            IDataModulePair pair = dataKit.JobSheet_PatternMatch_Data.GetModule();
            errorCode = pair.Do_Job();

            return errorCode;
        }

        public void Set_Brightness()
        {
            if (dataKit == null) return;

            dataKit.JobSheet_Brightness_Datas.ForEach(x =>
            {
               x.GetModule().Do_Job();       
            });
        }

        public void Set_Lighting()
        {
            if (dataKit == null) return;

            dataKit.JobSheet_Lighting_Datas.ForEach(x =>
            {
                x.GetModule().Do_Job();
            });
        }

        public void Save()
        {
            
        }
    }
}
