using SolveWare_Service_Core.Attributes;
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
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{

    [ResourceBaseAttribute(ConstantProperty.ResourceKey_Inspect)]
    public class Job_Inspect : DataJobPairFundamentalBase<Data_Inspection>,  IInspectionKit
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

        public Job_Inspect(string name):base(name)
        {
            
        }
   
        public Mission_Report Do_Blob()
        {
            Mission_Report context = new Mission_Report();
            if (Data == null)
            {
                context.Set(ErrorCodes.VisionFailed);
                return context; 
            }

            IDataModulePair pair =  Data.JobSheet_Blob_Data.GetModule();
            context = pair.Do_PairModuleJob(this.Data);

            return context;
        }

        public Mission_Report Do_PatternMath()
        {
            Mission_Report context = new Mission_Report();
            if (Data == null)
            {
                context.Set(ErrorCodes.VisionFailed);
                return context;
            }

            IDataModulePair pair = Data.JobSheet_PatternMatch_Data.GetModule();
            context = pair.Do_PairModuleJob(this.Data);

            return context;
        }

        public Mission_Report Set_Brightness()
        {
            int errorCode = ErrorCodes.NoError;
            Mission_Report context = new Mission_Report();
            if (Data == null)
            {
                context.Set(ErrorCodes.VisionFailed);
                return context;
            }

            context = Data.JobSheet_Brightness_Data.GetModule().Do_PairModuleJob(this.Data);

            return context;
        }

        public Mission_Report Set_Lighting()
        {
            Mission_Report context = new Mission_Report();
            if (Data == null)
            {
                context.Set(ErrorCodes.VisionFailed);
                return context;
            }

            if (this.Data.JobSheet_Lighting_Datas.Count == 0) return context;
            context = Data.JobSheet_Lighting_Datas[0].GetModule().Do_PairModuleJob(this.Data);

            return context;
        }

        public override Mission_Report Do_Job()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    //.设置 BrightNess
                    context = Set_Brightness();
                    if (context.NotPass())
                    {
                        break;
                    }

                    //设置Lighting
                    context = Set_Lighting();
                    if (context.NotPass())
                    {
                        break;
                    }

                    //执行 Pattern Match
                    context = Do_PatternMath();
                    if (context.NotPass())
                    {
                        break;
                    }

                    //执行 Blob
                    context = Do_Blob();
                    if (context.NotPass())
                    {
                        break;
                    }


                    //计算Offset

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.VisionFailed);
            }

            return context;
        }
    }
}
