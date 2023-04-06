using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Utility.Extension;
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
    
    public class Inspect : DataJobPairFundamentalBase<Data_InspectionKit>,  IInspectionKit
    {
        private double offsetX = 0;
        private double offsetY = 0;

        public double OffsetX
        {
            get => offsetX;
        }
        public double OffsetY
        {
            get=> offsetY;
        }

        public Inspect(string name):base(name)
        {
            
        }
   
        public int Do_Blob()
        {
            int errorCode = ErrorCodes.NoError;
            if (Data == null) return ErrorCodes.VisionFailed;

            IDataModulePair pair =  Data.JobSheet_Blob_Data.GetModule();
            errorCode = pair.Do_PairModuleJob(this.Data);

            return errorCode;
        }

        public int Do_PatternMath()
        {
            int errorCode = ErrorCodes.NoError;
            if (Data == null) return ErrorCodes.VisionFailed;

            IDataModulePair pair = Data.JobSheet_PatternMatch_Data.GetModule();
            errorCode = pair.Do_PairModuleJob(this.Data);

            return errorCode;
        }

        public int Set_Brightness()
        {
            int errorCode = ErrorCodes.NoError;
            if (Data == null) return ErrorCodes.VisionFailed;

            errorCode = Data.JobSheet_Brightness_Data.GetModule().Do_PairModuleJob(this.Data);

            return errorCode;
        }

        public int Set_Lighting()
        {
            int errorCode = ErrorCodes.NoError;
            if (Data == null) return ErrorCodes.VisionFailed;

            if (this.Data.JobSheet_Lighting_Datas.Count == 0) return errorCode;
            errorCode = Data.JobSheet_Lighting_Datas[0].GetModule().Do_PairModuleJob(this.Data);

            return errorCode;
        }

        public override int Do_Job()
        {
            OnEntrance();
            try
            {
                do
                {
                    //.设置 BrightNess
                    errorCode = Set_Brightness();
                    if (errorCode.NotPass()) break;

                    //设置Lighting
                    errorCode = Set_Lighting();
                    if (errorCode.NotPass()) break;

                    //执行 Pattern Match
                    errorCode = Do_PatternMath();
                    if (errorCode.NotPass()) break;

                    //执行 Blob
                    errorCode = Do_Blob();
                    if (errorCode.NotPass()) break;


                    //计算Offset

                } while (false);
            }
            catch (Exception ex)
            {
                this.errorMsg += ex.Message;
                errorCode = ErrorCodes.VisionFailed;
            }
            OnExit();

            return ErrorCode;
        }
    }
}
