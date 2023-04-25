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

        private Mission_Report Do_Top_Module_Safe_Prevention()
        {
            //TODO - Job_GlobalWorldCenter-
            //降下平台-上下Z退回安全位置-上打标气缸收回
           Mission_Report context = new Mission_Report ();

            try
            {
                do
                {
                    context = Job_Safe.Do_Safe_Proection(this.Data.Data_Safe_Top_Module);

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
        private Mission_Report Do_Btm_Module_Safe_Prevention()
        {
            //TODO - Job_GlobalWorldCenter- Do_Btm_Module_Safe_Prevention
            //TODO - Job_GlobalWorldCenter-
            //降下平台-上下Z退回安全位置-上打标气缸收回
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    context = Job_Safe.Do_Safe_Proection(this.Data.Data_Safe_Btm_Module);

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }

        public Mission_Report Go_Top_Module_Pos()
        {
            Mission_Report context = new Mission_Report(); 
            try
            {
                do
                {
                    context = Do_Top_Module_Safe_Prevention();
                    if (context.NotPass()) break;
               
                    context = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = Data.Top_Module_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = Data.Top_Module_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_T, Pos = Data.Top_Module_PosT }
                        );
                    if (context.NotPass()) break;

                    context = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Z, Pos = Data.Top_Module_PosZ });
                    if (context.NotPass()) break;


                } while (false);
            }
            catch (Exception ex)
            {
               context.Set(ErrorCodes.ActionFailed, ex.Message);
            }
            
            return context;
        }
        public Mission_Report Go_Top_WorldCenter_Pos()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    context = Do_Top_Module_Safe_Prevention();
                    if (context.NotPass()) break;

                    context = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = Data.Top_WorldCenter_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = Data.Top_WorldCenter_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_T, Pos = Data.Top_WorldCenter_PosZ }
                        );
                    if (context.NotPass()) break;

                    context = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Z, Pos = Data.Top_WorldCenter_PosT });
                    if (context.NotPass()) break;


                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
        public Mission_Report Go_Btm_WorldCenter_Pos()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    context = Do_Btm_Module_Safe_Prevention();
                    if (context.NotPass()) break;

                    context = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = Data.Btm_WorldCenter_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = Data.Btm_WorldCenter_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_T, Pos = Data.Btm_WorldCenter_PosT }
                        );
                    if (context.NotPass()) break;

                    context = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Z, Pos = Data.Btm_WorldCenter_PosZ });
                    if (context.NotPass()) break;


                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
        public Mission_Report Go_Btm_Module_Pos()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    context = Do_Top_Module_Safe_Prevention();
                    if (context.NotPass()) break;

                    context = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = Data.Btm_Module_PosX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = Data.Btm_Module_PosY },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_T, Pos = Data.Btm_Module_PosZ }
                        );
                    if (context.NotPass()) break;

                    context = MotionHelper.Move_Motor(new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Z, Pos=Data.Btm_Module_PosZ });
                    if (context.NotPass()) break;

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }

        public Mission_Report Do_Top_Module_Inspect()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    var job = SolveWare.Core.MMgr.Get_PairJob(ResourceKey.InspectKit_Top_Camera_Git_Hole);
                    context = job.Do_Job();
                    if (context.NotPass()) break;

                    if (!Data.Top_Module_Move_To_Center) break;

                    double posX = ResourceKey.Motor_Top_X.GetUnitPos() + (job as Job_Inspect).OffsetX;
                    double posY = ResourceKey.Motor_Top_Y.GetUnitPos() + (job as Job_Inspect).OffsetY;

                    context = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_X, Pos = posX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Top_Y, Pos = posY }
                        );
                    if (context.NotPass()) break;

                    //记忆当前位置
                    Data.Top_WorldCenter_PosX = Math.Round(ResourceKey.Motor_Top_X.GetUnitPos(), 3);
                    Data.Top_WorldCenter_PosY = Math.Round(ResourceKey.Motor_Top_Y.GetUnitPos(), 3);
                    Data.Top_WorldCenter_PosZ = Math.Round(ResourceKey.Motor_Top_Z.GetUnitPos(), 3);
                    Data.Top_WorldCenter_PosT = Math.Round(ResourceKey.Motor_Top_T.GetUnitPos(), 3);


                } while (false);

            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }
            return context;
        }
        public Mission_Report Do_Btm_Module_Inspect()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    var job = SolveWare.Core.MMgr.Get_PairJob(ResourceKey.InspectKit_Btm_Camera_Git_Hole);
                    context = job.Do_Job();
                    if (context.NotPass()) break;

                    if (!Data.Btm_Module_Move_To_Center) break;

                    double posX = ResourceKey.Motor_Btm_X.GetUnitPos() + (job as Job_Inspect).OffsetX;
                    double posY = ResourceKey.Motor_Btm_Y.GetUnitPos() + (job as Job_Inspect).OffsetY;

                    context = MotionHelper.Move_Multiple_Motors(
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_X, Pos = posX },
                        new Info_Motion { Motor_Name = ResourceKey.Motor_Btm_Y, Pos = posY }
                        );
                    if (context.NotPass()) break;

                    //记忆当前位置
                    Data.Btm_WorldCenter_PosX = Math.Round(ResourceKey.Motor_Btm_X.GetUnitPos(), 3);
                    Data.Btm_WorldCenter_PosY = Math.Round(ResourceKey.Motor_Btm_Y.GetUnitPos(), 3);
                    Data.Btm_WorldCenter_PosZ = Math.Round(ResourceKey.Motor_Btm_Z.GetUnitPos(), 3);
                    Data.Btm_WorldCenter_PosT = Math.Round(ResourceKey.Motor_Btm_T.GetUnitPos(), 3);

                } while (false);

            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
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


        public override Mission_Report Do_Job()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                List<int> errors = new List<int>();
                List<Task> tasks = new List<Task>();
                Task topTask = Task.Factory.StartNew((object obj) =>
                {
                    Data_Mission_Report mReport = obj as Data_Mission_Report;
                    Mission_Report topContext = new Mission_Report();
                    do
                    {
                        topContext = Do_Top_Module_Safe_Prevention();
                        if (topContext.NotPass()) break;

                        topContext = Go_Top_Module_Pos();
                        if (topContext.NotPass()) break;

                        topContext = Do_Top_Module_Inspect();
                        if (topContext.NotPass()) break;

                    } while (false);
                    mReport.Context = topContext;

                }, new Data_Mission_Report());
                
                Task btmTask = Task.Factory.StartNew((object obj) =>
                {
                    Data_Mission_Report mReport = obj as Data_Mission_Report;
                    Mission_Report btmContext = new Mission_Report();
                    do
                    {
                        btmContext = Do_Btm_Module_Safe_Prevention();
                        if (btmContext.NotPass()) break;

                        btmContext = Go_Btm_Module_Pos();
                        if (btmContext.NotPass()) break;

                        btmContext = Do_Btm_Module_Inspect();
                        if (btmContext.NotPass()) break;

                    } while (false);

                    mReport.Context = btmContext;
                }, new Data_Mission_Report());


                Task.WaitAll(new[] {topTask, btmTask}); 
                context = tasks.Converto_Mission_Report();  

            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
    }
}
