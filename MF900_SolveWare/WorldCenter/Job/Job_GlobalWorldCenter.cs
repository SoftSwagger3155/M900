using MF900_SolveWare.Offset.Data;
using MF900_SolveWare.Resource;
using MF900_SolveWare.Safe;
using MF900_SolveWare.WorldCenter.Data;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Attributes;
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
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_WorldCenter)]
    public class Job_GlobalWorldCenter: DataJobPairFundamentalBase<Data_GlobalWorldCenter>
    {

        public Job_GlobalWorldCenter(string name):base(name)
        {
            
        }

        private int Do_Top_Module_Safe_Prevention()
        {
            //TODO - Job_GlobalWorldCenter-
            //降下平台-上下Z退回安全位置-上打标气缸收回
            errorMsg = string.Empty;
            errorCode = ErrorCodes.NoError;
            try
            {
                do
                {
                    errorCode = Job_Safe.Do_Safe_Proection(this.Data.Data_Safe_Top_Module, ref errorMsg);

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        private int Do_Btm_Module_Safe_Prevention()
        {
            //TODO - Job_GlobalWorldCenter- Do_Btm_Module_Safe_Prevention
            //TODO - Job_GlobalWorldCenter-
            //降下平台-上下Z退回安全位置-上打标气缸收回
            errorMsg = string.Empty;
            errorCode = ErrorCodes.NoError;
            try
            {
                do
                {
                    errorCode = Job_Safe.Do_Safe_Proection(this.Data.Data_Safe_Btm_Module, ref errorMsg);

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }

        public int Go_Top_Module_Pos()
        {
            errorMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Do_Top_Module_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg)) break;
               
                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = Data.Top_Module_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = Data.Top_Module_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_T, Pos = Data.Top_Module_PosT }
                        );
                    if (errorCode.NotPass(ref errorMsg)) break;

                    errorCode = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Z, Pos = Data.Top_Module_PosZ });
                    if (errorCode.NotPass(ref errorMsg)) break;


                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }
            
            return errorCode;
        }
        public int Go_Top_WorldCenter_Pos()
        {
            errorMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Do_Top_Module_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg)) break;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = Data.Top_WorldCenter_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = Data.Top_WorldCenter_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_T, Pos = Data.Top_WorldCenter_PosZ }
                        );
                    if (errorCode.NotPass(ref errorMsg)) break;

                    errorCode = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Z, Pos = Data.Top_WorldCenter_PosT });
                    if (errorCode.NotPass(ref errorMsg)) break;


                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        public int Go_Btm_WorldCenter_Pos()
        {
            errorMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Do_Btm_Module_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg)) break;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = Data.Btm_WorldCenter_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = Data.Btm_WorldCenter_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_T, Pos = Data.Btm_WorldCenter_PosT }
                        );
                    if (errorCode.NotPass(ref errorMsg)) break;

                    errorCode = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Z, Pos = Data.Btm_WorldCenter_PosZ });
                    if (errorCode.NotPass(ref errorMsg)) break;


                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        public int Go_Btm_Module_Pos()
        {
            errorMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Do_Top_Module_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg)) break;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = Data.Btm_Module_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = Data.Btm_Module_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_T, Pos = Data.Btm_Module_PosZ }
                        );
                    if (errorCode.NotPass(ref errorMsg)) break;

                    errorCode = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Z, Pos=Data.Btm_Module_PosZ });
                    if (errorCode.NotPass(ref errorMsg)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }

        public int Do_Top_Module_Inspect()
        {
            errorMsg = string.Empty;
            try
            {
                do
                {
                    var job = SolveWare.Core.MMgr.Get_PairJob(ResourceKey.InspectKit_Top_Camera_Git_Hole);
                    errorCode = job.Do_Job();
                    if (errorCode.NotPass(ref errorMsg, (job as Inspect).ErrorMsg)) break;

                    if (!Data.Top_Module_Move_To_Center) break;

                    double posX = ResourceKey.Motor_Top_X.GetUnitPos() + (job as Inspect).OffsetX;
                    double posY = ResourceKey.Motor_Top_Y.GetUnitPos() + (job as Inspect).OffsetY;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = posX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = posY }
                        );
                    if (errorCode.NotPass(ref errorMsg)) break;

                    //记忆当前位置
                    Data.Top_WorldCenter_PosX = Math.Round(ResourceKey.Motor_Top_X.GetUnitPos(), 3);
                    Data.Top_WorldCenter_PosY = Math.Round(ResourceKey.Motor_Top_Y.GetUnitPos(), 3);
                    Data.Top_WorldCenter_PosZ = Math.Round(ResourceKey.Motor_Top_Z.GetUnitPos(), 3);
                    Data.Top_WorldCenter_PosT = Math.Round(ResourceKey.Motor_Top_T.GetUnitPos(), 3);


                } while (false);

            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }
            return errorCode;
        }
        public int Do_Btm_Module_Inspect()
        {
            errorMsg = string.Empty;
            try
            {
                do
                {
                    var job = SolveWare.Core.MMgr.Get_PairJob(ResourceKey.InspectKit_Btm_Camera_Git_Hole);
                    errorCode = job.Do_Job();
                    if (errorCode.NotPass(ref errorMsg, (job as Inspect).ErrorMsg)) break;

                    if (!Data.Btm_Module_Move_To_Center) break;

                    double posX = ResourceKey.Motor_Btm_X.GetUnitPos() + (job as Inspect).OffsetX;
                    double posY = ResourceKey.Motor_Btm_Y.GetUnitPos() + (job as Inspect).OffsetY;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = posX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = posY }
                        );
                    if (errorCode.NotPass(ref errorMsg)) break;

                    //记忆当前位置
                    Data.Btm_WorldCenter_PosX = Math.Round(ResourceKey.Motor_Btm_X.GetUnitPos(), 3);
                    Data.Btm_WorldCenter_PosY = Math.Round(ResourceKey.Motor_Btm_Y.GetUnitPos(), 3);
                    Data.Btm_WorldCenter_PosZ = Math.Round(ResourceKey.Motor_Btm_Z.GetUnitPos(), 3);
                    Data.Btm_WorldCenter_PosT = Math.Round(ResourceKey.Motor_Btm_T.GetUnitPos(), 3);

                } while (false);

            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        public void Save_Pos()
        {
            Data.Top_WorldCenter_PosX = Math.Round(ResourceKey.Motor_Top_X.GetUnitPos(), 3);
            Data.Top_WorldCenter_PosY = Math.Round(ResourceKey.Motor_Top_Y.GetUnitPos(), 3);
            Data.Top_WorldCenter_PosZ = Math.Round(ResourceKey.Motor_Top_Z.GetUnitPos(), 3);
            Data.Top_WorldCenter_PosT = Math.Round(ResourceKey.Motor_Top_T.GetUnitPos(), 3);

            Data.Btm_WorldCenter_PosX = Math.Round(ResourceKey.Motor_Btm_X.GetUnitPos(), 3);
            Data.Btm_WorldCenter_PosY = Math.Round(ResourceKey.Motor_Btm_Y.GetUnitPos(), 3);
            Data.Btm_WorldCenter_PosZ = Math.Round(ResourceKey.Motor_Btm_Z.GetUnitPos(), 3);
            Data.Btm_WorldCenter_PosT = Math.Round(ResourceKey.Motor_Btm_T.GetUnitPos(), 3);
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

            return errorCode;
        }
    }
}
