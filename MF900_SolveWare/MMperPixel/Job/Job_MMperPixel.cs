using MF900_SolveWare.Resource;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Interface;
using SolveWare_Service_Core;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Utility.Common.Motion;
using SolveWare_Service_Vision.MMperPixel.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MF900_SolveWare.MMperPixel.Data;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Core.Attributes;

namespace MF900_SolveWare.MMperPixel.Job
{
    //TODO 杨工: 1. UI 2. 实现细节
    [ResourceBase(ConstantProperty.ResourceKey_MMperPixel)]
    public class Job_MMperPixel : DataJobPairFundamentalBase<Data_MMperPixel>, IMMperPixelJob
    {
        CameraMediaBase camera;

        public string Module { get; protected set; }
        public string CameraName { get; protected set; }
        public Job_MMperPixel(string name, string cameraName, string module):base(name) 
        {
            this.Name = name;
            this.Module = module;   
            this.CameraName = cameraName;   
        }

        /// <summary>
        /// 将轴或IO移到安全的位置
        /// </summary>
        /// <returns></returns>
        public Mission_Report Do_Safe_Prevention()
        {

            return new Mission_Report();
        }

        /// <summary>
        /// 请移到拍摄的位置
        /// </summary>
        /// <returns></returns>
        public Mission_Report Move_To_Inspection_Pos()
        {
            return new Mission_Report();
        }
        public Mission_Report Do_MMperPixel_Conversion(ref double average)
        {
            Mission_Report context = new Mission_Report();
            try
            {



            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed);
            }

            return context;
        }

        public override Mission_Report Do_Job()
        {
            this.Status = JobStatus.Entrance;
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    //1.移去安全位置
                    context = Do_Safe_Prevention();
                    if (context.NotPass()) break;

                    //2.移去拍摄位置
                    context = Move_To_Inspection_Pos();
                    if (context.NotPass()) break;

                    //3.执行MMperPixel任务
                    double averagePixel = 0;
                    context = Do_MMperPixel_Conversion(ref averagePixel);
                    if (context.NotPass()) break;


                   // this.camera.Data_MMperPixal.Average_MMperPixel = averagePixel;
                    IResourceProvider provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Camera);
                    provider.SaveSingleData(this.camera);

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed);
            }
            this.Status = context.ErrorCode == ErrorCodes.NoError ? JobStatus.Done : JobStatus.Fail;
            return context;
        }

    }
}
