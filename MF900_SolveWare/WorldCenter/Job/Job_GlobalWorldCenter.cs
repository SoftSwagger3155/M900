using MF900_SolveWare.Offset.Data;
using MF900_SolveWare.Resource;
using MF900_SolveWare.WorldCenter.Data;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Common;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision.Inspection.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.WorldCenter.Job
{
    public class Job_GlobalWorldCenter: DataJobPairFundamentalBase<Data_GlobalWorldCenter>
    {

        private int Do_Top_Module_Safe_Prevention()
        {
            //TODO - Job_GlobalWorldCenter- Do_Top_Module_Safe_Prevention

            return 0;
        }
        private int Do_Btm_Module_Safe_Prevention()
        {
            //TODO - Job_GlobalWorldCenter- Do_Btm_Module_Safe_Prevention
            return 0;
        }

        public int Go_Top_Module_Pos()
        {
            try
            {
                do
                {
                    errorCode = Do_Top_Module_Safe_Prevention();
                    if (errorCode != 0)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }
               
                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = Data.Top_Module_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = Data.Top_Module_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_T, Pos = Data.Top_Module_PosT }
                        );
                    if(errorCode != ErrorCodes.NoError)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    errorCode = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Z, Pos = Data.Top_Module_PosZ });
                    if (errorCode != ErrorCodes.NoError)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }


                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            Get_Result(nameof(Go_Top_Module_Pos), errorMsg);
            return errorCode;
        }
        public int Go_Btm_Module_Pos()
        {
            try
            {
                do
                {
                    errorCode = Do_Top_Module_Safe_Prevention();
                    if (errorCode != 0)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = Data.Btm_Module_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = Data.Btm_Module_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_T, Pos = Data.Btm_Module_PosT }
                        );
                    if (errorCode != ErrorCodes.NoError)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    errorCode = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Z, Pos = Data.Btm_Module_PosZ });
                    if (errorCode != ErrorCodes.NoError)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }


                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            Get_Result(nameof(Go_Top_Module_Pos), errorMsg);
            return errorCode;
        }

        public int Do_Top_Module_Inspect()
        {

            try
            {
                do
                {
                    var job = SolveWare.Core.MMgr.Get_PairJob(ResourceKey.InspectKit_Top_Camera_Git_Hole);
                    errorCode = job.Do_Job();

                    if (errorCode != ErrorCodes.NoError)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    if (!Data.Top_Module_Move_To_Center) break;

                    double posX = ResourceKey.Motor_Top_X.GetUnitPos() + (job as Inspect).OffsetX;
                    double posY = ResourceKey.Motor_Top_Y.GetUnitPos() + (job as Inspect).OffsetY;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = posX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = posY }
                        );
                    if (errorCode != ErrorCodes.NoError)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    //记忆当前位置
                    this.Data.Top_Module_PosX = ResourceKey.Motor_Top_X.GetUnitPos();
                    this.Data.Top_Module_PosY = ResourceKey.Motor_Top_Y.GetUnitPos();
                    this.Data.Top_Module_PosZ = ResourceKey.Motor_Top_Z.GetUnitPos();
                    this.Data.Top_Module_PosT = ResourceKey.Motor_Top_T.GetUnitPos();


                } while (false);

            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            Get_Result(nameof(Do_Top_Module_Inspect), errorMsg);
            return errorCode;
        }
        public int Do_Btm_Module_Inspect()
        {

            try
            {
                do
                {
                    var job = SolveWare.Core.MMgr.Get_PairJob(ResourceKey.InspectKit_Btm_Camera_Git_Hole);
                    errorCode = job.Do_Job();

                    if (errorCode != ErrorCodes.NoError)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    if (!Data.Btm_Module_Move_To_Center) break;

                    double posX = ResourceKey.Motor_Btm_X.GetUnitPos() + (job as Inspect).OffsetX;
                    double posY = ResourceKey.Motor_Btm_Y.GetUnitPos() + (job as Inspect).OffsetY;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = posX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = posY }
                        );
                    if (errorCode != ErrorCodes.NoError)
                    {
                        errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    //记忆当前位置
                    this.Data.Btm_Module_PosX = ResourceKey.Motor_Btm_X.GetUnitPos();
                    this.Data.Btm_Module_PosY = ResourceKey.Motor_Btm_Y.GetUnitPos();
                    this.Data.Btm_Module_PosZ = ResourceKey.Motor_Btm_Z.GetUnitPos();
                    this.Data.Btm_Module_PosT = ResourceKey.Motor_Btm_T.GetUnitPos();


                } while (false);

            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            Get_Result(nameof(Do_Top_Module_Inspect), errorMsg);
            return errorCode;
        }


        public override int Do_Job()
        {
            try
            {
                List<int> errors = new List<int>();
                List<Task> tasks = new List<Task>();
                Task topTask = new Task(() =>
                {
                    int err = ErrorCodes.NoError;
                    do
                    {           
                        err = Do_Top_Module_Safe_Prevention();
                        if (err != ErrorCodes.NoError) break;

                        err = Go_Top_Module_Pos();
                        if (err != ErrorCodes.NoError) break;

                        err = Do_Top_Module_Inspect();
                        if (err != ErrorCodes.NoError) break;

                    } while (false);

                    errors.Add(err);
                });
                Task btmTask = new Task(() =>
                {
                    int err = ErrorCodes.NoError;
                    do
                    {
                        err = Do_Btm_Module_Safe_Prevention();
                        if (err != ErrorCodes.NoError) break;

                        err = Go_Btm_Module_Pos();
                        if (err != ErrorCodes.NoError) break;

                        err = Do_Btm_Module_Inspect();
                        if (err != ErrorCodes.NoError) break;

                    } while (false);

                    errors.Add(err);
                });

                tasks.Add(topTask);
                tasks.Add(btmTask);

                tasks.ForEach(x => x.Start());
                Task.Factory.ContinueWhenAll(tasks.ToArray(), act =>
                {
                    int foundError = errors.FirstOrDefault(x => x != ErrorCodes.NoError);
                    errorCode = foundError >= 0 ? errors[foundError] : ErrorCodes.NoError;
                });
                if (errorCode != ErrorCodes.NoError) errorMsg += ErrorCodes.GetErrorDescription(errorCode);
                

            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            Get_Result(nameof(Do_Job), errorMsg);
            return errorCode;
        }
    }
}
