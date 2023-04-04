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

namespace MF900_SolveWare.MMperPixel.Job
{
    //TODO 杨工: 1. UI 2. 实现细节
    public class Job_MMperPixel_BtmCamera : DataJobPairFundamentalBase<Data_MMperPixel>, IMMperPixelJob
    {
        CameraMediaBase camera;

        public Job_MMperPixel_BtmCamera(string name)
        {
            this.Name = name;
            camera = ResourceKey.Btm_Camera.GetCamera();     
        }

        /// <summary>
        /// 将轴或IO移到安全的位置
        /// </summary>
        /// <returns></returns>
        public int Do_Safe_Prevention()
        {

            return 0;
        }

        /// <summary>
        /// 请移到拍摄的位置
        /// </summary>
        /// <returns></returns>
        public int Move_To_Inspection_Pos()
        {
            return 0;
        }
        public int Do_MMperPixel_Conversion(ref double average)
        {
            int errorCode = ErrorCodes.NoError;
            try
            {



            }
            catch (Exception ex)
            {
                this.errorMsg += ex.Message;
                errorCode = ErrorCodes.ActionFailed;
            }

            return errorCode;
        }

        public override int Do_Job()
        {
            OnEntrance();

            try
            {
                do
                {
                    //1.移去安全位置
                    errorCode = Do_Safe_Prevention();
                    if (errorCode.NotPass()) break;

                    //2.移去拍摄位置
                    errorCode = Move_To_Inspection_Pos();
                    if (errorCode.NotPass()) break;

                    //3.执行MMperPixel任务
                    double averagePixel = 0;
                    errorCode = Do_MMperPixel_Conversion(ref averagePixel);
                    if (errorCode.NotPass()) break;


                    this.camera.Data_MMperPixal.Average_MMperPixel = averagePixel;
                    IResourceProvider provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Camera);
                    provider.SaveSingleData(this.camera);

                } while (false);
            }
            catch (Exception ex)
            {
                this.errorMsg += ex.Message;
                errorCode = ErrorCodes.ActionFailed;
            }

            OnExit();
            return ErrorCode;
        }

    }
}
