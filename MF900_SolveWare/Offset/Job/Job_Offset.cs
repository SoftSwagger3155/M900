using MF900_SolveWare.Offset.Data;
using MF900_SolveWare.Resource;
using MF900_SolveWare.Safe;
using MF900_SolveWare.WorldCenter.Job;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Common;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Utility.Offset.Base.Interface;
using SolveWare_Service_Vision.Inspection.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Offset.Job
{
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_Offset)]
    public class Job_Offset : DataJobPairFundamentalBase<Data_Offset>, IOffset
    {
        public Job_Offset(string name, string module):base(name)
        {
          this.Data.Start_Based_Module = module;
        }
        public int Save_Start_Pos()
        {        
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;

            double offsetX = 0, offsetY = 0; 
            try
            {
                do
                {
                    Job_GlobalWorldCenter worldCenter = (Job_GlobalWorldCenter)SolveWare.Core.MMgr.Get_PairJob(ResourceKey.GlobalWorldCenter);
                    if(worldCenter == null)
                    {
                        errorMsg += "无 世界中心 位置资料";
                        break;
                    }

                    switch (Data.Start_Based_Module)
                    {
                        case Data_Offset.TopModule:
                            offsetX = ResourceKey.Motor_Top_X.GetUnitPos() - worldCenter.Data.Top_WorldCenter_PosX;
                            offsetY = ResourceKey.Motor_Top_Y.GetUnitPos() - worldCenter.Data.Top_WorldCenter_PosY;

                            Data.Start_Top_PosX = Math.Round(ResourceKey.Motor_Top_X.GetUnitPos(), 3);
                            Data.Start_Top_PosY = Math.Round(ResourceKey.Motor_Top_Y.GetUnitPos(), 3);
                            Data.Start_Top_PosZ = Math.Round(ResourceKey.Motor_Top_Z.GetUnitPos(), 3);
                            Data.Start_Top_PosT = Math.Round(ResourceKey.Motor_Top_T.GetUnitPos(), 3);

                            Data.Start_Btm_PosX = Math.Round(worldCenter.Data.Btm_WorldCenter_PosX + offsetX, 3);
                            Data.Start_Btm_PosY = Math.Round(worldCenter.Data.Btm_WorldCenter_PosY + offsetY, 3);
                            Data.Start_Btm_PosZ = Math.Round(ResourceKey.Motor_Btm_Z.GetUnitPos(), 3);
                            Data.Start_Btm_PosT = Math.Round(ResourceKey.Motor_Btm_T.GetUnitPos(), 3);

                            Data.Anchor_MotorX = ResourceKey.Motor_Top_X;
                            Data.Anchor_MotorY = ResourceKey.Motor_Top_Y;
                            Data.Anchor_MotorZ = ResourceKey.Motor_Top_X;
                            Data.Anchor_MotorT = ResourceKey.Motor_Top_T;
                            break;

                        case Data_Offset.BtmModule:
                            offsetX = ResourceKey.Motor_Btm_X.GetUnitPos() - worldCenter.Data.Btm_WorldCenter_PosX;
                            offsetY = ResourceKey.Motor_Btm_Y.GetUnitPos() - worldCenter.Data.Btm_WorldCenter_PosY;

                            Data.Start_Top_PosX = Math.Round(worldCenter.Data.Top_WorldCenter_PosX + offsetX, 3);
                            Data.Start_Top_PosY = Math.Round(worldCenter.Data.Top_WorldCenter_PosY + offsetY, 3);
                            Data.Start_Top_PosZ = Math.Round(ResourceKey.Motor_Top_Z.GetUnitPos(), 3);
                            Data.Start_Top_PosT = Math.Round(ResourceKey.Motor_Top_T.GetUnitPos(), 3);

                            Data.Start_Btm_PosX = Math.Round(ResourceKey.Motor_Btm_X.GetUnitPos());
                            Data.Start_Btm_PosY = Math.Round(ResourceKey.Motor_Btm_Y.GetUnitPos());
                            Data.Start_Btm_PosZ = Math.Round(ResourceKey.Motor_Btm_Z.GetUnitPos(), 3);
                            Data.Start_Btm_PosT = Math.Round(ResourceKey.Motor_Btm_T.GetUnitPos(), 3);

                            Data.Anchor_MotorX = ResourceKey.Motor_Btm_X;
                            Data.Anchor_MotorY = ResourceKey.Motor_Btm_Y;
                            Data.Anchor_MotorZ = ResourceKey.Motor_Btm_X;
                            Data.Anchor_MotorT = ResourceKey.Motor_Btm_T;
                            break;
                    }


                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        public int Go_Start_Pos()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Do_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    //上下模组 X Y T => 先行
                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = Data.Start_Top_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = Data.Start_Top_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_T, Pos = Data.Start_Top_PosT },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = Data.Start_Btm_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = Data.Start_Btm_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_T, Pos = Data.Start_Btm_PosT }
                        );
                    if(errorCode.NotPass( ref errorMsg, addtionalMsg)) break;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Z, Pos = Data.Start_Top_PosZ },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Z, Pos = Data.Start_Btm_PosZ }
                        );
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;


                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        
        public int Save_First_Pos()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    this.Data.First_MotorX = Data.Anchor_MotorX;
                    this.Data.First_MotorY = Data.Anchor_MotorY;
                    this.Data.FirstPosX = Data.Anchor_MotorX.GetUnitPos();
                    this.Data.FirstPosY = Data.Anchor_MotorY.GetUnitPos();
                    this.Data.FirstPosZ = Data.Anchor_MotorZ.GetUnitPos();
                    this.Data.FirstPosT = Data.Anchor_MotorT.GetUnitPos();

                    #region 备选
                    //switch (Data.Start_Based_Module)
                    //{
                    //    case Data_Offset.TopModule:
                    //        this.Data.FirstPosX = ResourceKey.Motor_Top_X.GetUnitPos();
                    //        this.Data.FirstPosY = ResourceKey.Motor_Top_Y.GetUnitPos();
                    //        break;

                    //    case Data_Offset.BtmModule:
                    //        this.Data.FirstPosX = ResourceKey.Motor_Btm_X.GetUnitPos();
                    //        this.Data.FirstPosY = ResourceKey.Motor_Btm_Y.GetUnitPos();
                    //        break;
                    //}
                    #endregion

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        public int GoFirstPos()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Do_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = Data.Anchor_MotorX, Pos = Data.FirstPosX },
                        new Info_Motion { Motor_Name = Data.Anchor_MotorY, Pos = Data.FirstPosY },
                        new Info_Motion { Motor_Name = Data.Anchor_MotorT, Pos = Data.FirstPosT });
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = MotionHelper.Move_Motor(
                        new Info_Motion { Motor_Name = Data.Anchor_MotorZ, Pos = Data.FirstPosZ });
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }

        public int Save_Second_Pos()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    this.Data.SecondPosX = Data.Anchor_MotorX.GetUnitPos();
                    this.Data.SecondPosY = Data.Anchor_MotorY.GetUnitPos();
                    this.Data.SecondPosZ = Data.Anchor_MotorZ.GetUnitPos();
                    this.Data.SecondPosT = Data.Anchor_MotorT.GetUnitPos();

                    #region 备选
                    //switch (Data.Start_Based_Module)
                    //{
                    //    case Data_Offset.TopModule:
                    //        this.Data.FirstPosX = ResourceKey.Motor_Top_X.GetUnitPos();
                    //        this.Data.FirstPosY = ResourceKey.Motor_Top_Y.GetUnitPos();
                    //        break;

                    //    case Data_Offset.BtmModule:
                    //        this.Data.FirstPosX = ResourceKey.Motor_Btm_X.GetUnitPos();
                    //        this.Data.FirstPosY = ResourceKey.Motor_Btm_Y.GetUnitPos();
                    //        break;
                    //}
                    #endregion

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        public int GoSecondPos() {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Do_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = Data.Anchor_MotorX, Pos = Data.SecondPosX },
                        new Info_Motion { Motor_Name = Data.Anchor_MotorY, Pos = Data.SecondPosY },
                        new Info_Motion { Motor_Name = Data.Anchor_MotorT, Pos = Data.SecondPosT });
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = MotionHelper.Move_Motor(
                        new Info_Motion { Motor_Name = Data.Anchor_MotorZ, Pos = Data.SecondPosZ });
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }

        public int Save_Inspect_Pos()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    this.Data.Inspect_PosX = Data.Anchor_MotorX.GetUnitPos();
                    this.Data.Inspect_PosY = Data.Anchor_MotorY.GetUnitPos();
                    this.Data.Inspect_PosZ = Data.Anchor_MotorZ.GetUnitPos();
                    this.Data.Inspect_PosT = Data.Anchor_MotorT.GetUnitPos();

                    #region 备选
                    //switch (Data.Start_Based_Module)
                    //{
                    //    case Data_Offset.TopModule:
                    //        this.Data.FirstPosX = ResourceKey.Motor_Top_X.GetUnitPos();
                    //        this.Data.FirstPosY = ResourceKey.Motor_Top_Y.GetUnitPos();
                    //        break;

                    //    case Data_Offset.BtmModule:
                    //        this.Data.FirstPosX = ResourceKey.Motor_Btm_X.GetUnitPos();
                    //        this.Data.FirstPosY = ResourceKey.Motor_Btm_Y.GetUnitPos();
                    //        break;
                    //}
                    #endregion

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        public int Go_Inspect_Pos()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Do_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                    new Info_Motion { Motor_Name = Data.Anchor_MotorX, Pos = Data.Inspect_PosX },
                    new Info_Motion { Motor_Name = Data.Anchor_MotorY, Pos = Data.Inspect_PosY },
                    new Info_Motion { Motor_Name = Data.Anchor_MotorT, Pos = Data.Inspect_PosT });
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = MotionHelper.Move_Motor(
                        new Info_Motion { Motor_Name = Data.Anchor_MotorZ, Pos = Data.Inspect_PosZ });
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;


                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }
        public int Do_Inspect()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    if (string.IsNullOrEmpty(Data.InspectKitName))
                    {
                        errorCode = ErrorCodes.NoRelevantData;
                        errorMsg += "无 视觉 物件";
                        break;
                    }

                    if (Data.Enable_InspectKit == false) break;

                    Inspect job = (Inspect)SolveWare.Core.MMgr.Get_PairJob(Data.InspectKitName);
                    errorCode = job.Do_Job();
                    if (errorCode.NotPass(ref errorMsg)) break;

                    if(Data.Move_To_Center == false) break;

                    double targetPosX = this.Data.Anchor_MotorX.GetUnitPos() + job.OffsetX;
                    double targetPosY = this.Data.Anchor_MotorY.GetUnitPos() + job.OffsetY;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = Data.Anchor_MotorX, Pos = targetPosX },
                        new Info_Motion { Motor_Name = Data.Anchor_MotorY, Pos = targetPosY });
                    if (errorCode.NotPass(ref errorMsg)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }

        public int Calculate_Offset()

        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    double factorX = this.Data.IsReverseX ? -1 : 1;
                    double factorY = this.Data.IsReverseY ? -1 : 1;

                    this.Data.OffsetX = Math.Round((this.Data.SecondPosX - this.Data.FirstPosX) * factorX, 3);
                    this.Data.OffsetY = Math.Round((this.Data.SecondPosY - this.Data.FirstPosY) * factorY, 3);

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }

        public int Do_Safe_Prevention()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Job_Safe.Do_Safe_Proection(this.Data.Data_Safe_Module, ref addtionalMsg);              
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }   
        public override int Do_Job()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    errorCode = Do_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    //TODO -- Job Offset -- Do_Job()
                    errorCode =Go_Start_Pos();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = Save_First_Pos();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = Go_Inspect_Pos();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = Do_Inspect();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = Save_Second_Pos();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                    errorCode = Calculate_Offset();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }

        public int Go_Offset()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    if(string.IsNullOrEmpty(Data.Anchor_MotorX) || string.IsNullOrEmpty(Data.Anchor_MotorY))
                    {
                        errorMsg += "无定位基础马达物件";
                        break;
                    }


                    errorCode = Do_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;



                    double posX = Data.Anchor_MotorX.GetUnitPos() + Data.OffsetX;
                    double posY = Data.Anchor_MotorY.GetUnitPos() + Data.OffsetY;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = Data.Anchor_MotorX, Pos = posX },
                        new Info_Motion { Motor_Name = Data.Anchor_MotorY, Pos = posY }
                        );
                    if (errorCode.NotPass(ref errorMsg)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }

        public int Return_Offset()
        {
            errorCode = ErrorCodes.NoError;
            errorMsg = string.Empty;
            string addtionalMsg = string.Empty;
            try
            {
                do
                {
                    if (string.IsNullOrEmpty(Data.Anchor_MotorX) || string.IsNullOrEmpty(Data.Anchor_MotorY))
                    {
                        errorMsg += "无定位基础马达物件";
                        break;
                    }


                    errorCode = Do_Safe_Prevention();
                    if (errorCode.NotPass(ref errorMsg, addtionalMsg)) break;



                    double posX = Data.Anchor_MotorX.GetUnitPos() + Data.OffsetX * -1;
                    double posY = Data.Anchor_MotorY.GetUnitPos() + Data.OffsetY * -1;

                    errorCode = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = Data.Anchor_MotorX, Pos = posX },
                        new Info_Motion { Motor_Name = Data.Anchor_MotorY, Pos = posY }
                        );

                    if (errorCode.NotPass(ref errorMsg)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
            }

            return errorCode;
        }


    

    }
}
