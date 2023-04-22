using MF900_SolveWare.Resource;
using SolveWare_Service_Core.FSM.Base.Abstract;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.FSM.FSMState;
using SolveWare_Service_Core.FSM.Helper;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MF900_SolveWare.FSM.Home.Stations
{
    public enum HomeStation_Selector
    {
        Up,
        Bottom
    }
    public class FSM_Home_Station: FSMStationBase
    {
        private HomeStation_Selector selector;
        private FSM_Home_MachineEvent mcEvent;

        #region 
        BasicState st_HomeStart;
        BasicState st_HomeZ;
        BasicState st_Set_HomeZ_Done;
        BasicState st_Wait_Z_Table_Done;
        BasicState st_HomeT;
        BasicState st_HomeXY;
        BasicState st_EndHome;
        #endregion
        public FSM_Home_Station(HomeStation_Selector selector, FSM_Home_MachineEvent mcEvent, bool isSimulation)
        {
            this.selector = selector;
            this.mcEvent = mcEvent;
            this.isSimulation = isSimulation;
            this.Name = $"FSM_{selector}_Home_Station";

            CreateObjectInstance();
            SetStateChain();    
        }

        public override void CreateObjectInstance()
        {
            this.Add_State(
                st_HomeStart = new BasicState("开始复位", StartHome, OnErrorHanding, this.isSimulation),
                st_HomeZ = new BasicState("Z轴复位", HomeZ, OnErrorHanding, this.isSimulation),
                st_Set_HomeZ_Done = new BasicState("Z轴复位结束", SetHomeZDone,  OnErrorHanding, this.isSimulation),
                st_Wait_Z_Table_Done = new BasicState("等待Z轴 平台复位结束", WaitZandTableDone, OnErrorHanding, this.isSimulation),
                st_HomeT = new BasicState("T轴复位", HomeT, OnErrorHanding, this.isSimulation),
                st_HomeXY = new BasicState("T轴复位", HomeXY, OnErrorHanding, this.isSimulation),
                st_EndHome = new BasicState("T轴复位", EndHome, OnErrorHanding, this.isSimulation)
                );
        }

        #region State Jobs
        private Mission_Report StartHome(StateBase sender)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
        private Mission_Report HomeZ(StateBase sender)
        {
            Mission_Report context = new Mission_Report();
            
            try
            {
                do
                {
                    string mtr = this.selector == HomeStation_Selector.Up ? ResourceKey.Motor_Top_Z : ResourceKey.Motor_Btm_Z ;
                    context = mtr.GetAxisBase().HomeMove(); ;
                    if (context.NotPass()) break;
                    
                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
        private Mission_Report SetHomeZDone(StateBase sender)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    if (this.selector == HomeStation_Selector.Up)
                    {
                        mcEvent.Evnt_HomeTopZ_Done.Reset();
                        Thread.Sleep(10);
                        mcEvent.Evnt_HomeTopZ_Done.Set();
                    }
                    else if (this.selector == HomeStation_Selector.Bottom)
                    {
                        mcEvent.Evnt_HomeBtmZ_Done.Reset();
                        Thread.Sleep(10);
                        mcEvent.Evnt_HomeBtmZ_Done.Set();
                    }

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
        private Mission_Report WaitZandTableDone(StateBase sender)
        {
            Mission_Report context = new Mission_Report();
            Stopwatch sWatch = new Stopwatch();
            int timeOut = 300 * 1000; //300秒延迟

            try
            {
                do
                {
                    bool isOk = false;
                    sWatch.Start();
                    while(true)
                    {
                        isOk = mcEvent.Evnt_HomeBtmZ_Done.WaitOne(10) && mcEvent.Evnt_HomeTopZ_Done.WaitOne(10) && mcEvent.Evnt_HomeTable_Done.WaitOne(10);
                        if (isOk)
                            break;
                        if(sWatch.ElapsedMilliseconds > timeOut)
                        {
                            context.Set(ErrorCodes.WaitTimeOutError);
                            break;
                        }
                        if (context.NotPass()) break;
                    }


                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
        private Mission_Report HomeT(StateBase sender)
        {
            Mission_Report context = new Mission_Report();
            
            try
            {
                do
                {
                    string mtr = this.selector == HomeStation_Selector.Up ? ResourceKey.Motor_Top_T : ResourceKey.Motor_Btm_T;
                    context = mtr.GetAxisBase().HomeMove();

                    if (context.NotPass()) break;

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }
            
            return context;
        }
        private Mission_Report HomeXY(StateBase sender)
        {

            Mission_Report context = new Mission_Report();

            try
            {
                do
                {
                    string mtrX = this.selector == HomeStation_Selector.Up ? ResourceKey.Motor_Top_X : ResourceKey.Motor_Btm_X;
                    string mtrY = this.selector == HomeStation_Selector.Up ? ResourceKey.Motor_Top_Y : ResourceKey.Motor_Btm_Y;
                    
                    List<Task> tasks = new List<Task>();

                    Task task1 = Task.Factory.StartNew((object obj) =>
                    {
                        Data_Mission_Report mReport = obj as Data_Mission_Report;
                        mReport.Context = mtrX.GetAxisBase().HomeMove();

                    }, new Data_Mission_Report());
                    Task task2 = Task.Factory.StartNew((object obj) =>
                    {
                        Data_Mission_Report mReport = obj as Data_Mission_Report;
                        mReport.Context = mtrY.GetAxisBase().HomeMove();

                    }, new Data_Mission_Report());
                    tasks.AddRange(new[] { task1, task2 });
                    Task.WaitAll(tasks.ToArray());

                    context = tasks.Converto_Mission_Report();
                    
                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
        private Mission_Report EndHome(StateBase sender)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {




                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }
            return context;
        }
        #endregion
        private Mission_Report OnErrorHanding(StateBase sender)
        {      
            //Auto 比较多机会要用到此方法
            //此台一有事有停机，所以用处也不大
            return sender.FinalReport;
        }

        public override void SetStateChain()
        {
            FSMHelper.SetStateChain(st_HomeStart, st_HomeZ);
            FSMHelper.SetStateChain(st_HomeZ , st_Set_HomeZ_Done);
            FSMHelper.SetStateChain(st_Set_HomeZ_Done, st_Wait_Z_Table_Done) ;
            FSMHelper.SetStateChain(st_Wait_Z_Table_Done, st_HomeT);
            FSMHelper.SetStateChain(st_HomeT, st_HomeXY);
            FSMHelper.SetStateChain(st_HomeXY, st_EndHome);

            this.SetFirstState(st_HomeStart);
            this.SetFinalState(st_EndHome);
        }

    }
}
