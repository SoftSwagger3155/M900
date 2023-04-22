using HalconDotNet;
using MF900_SolveWare.Resource;
using SolveWare_Service_Core.FSM.Base.Abstract;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.FSM.FSMState;
using SolveWare_Service_Core.FSM.Helper;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MF900_SolveWare.FSM.Home.Stations
{
    public class FSM_Home_Table_Station : FSMStationBase
    {
        #region State
        BasicState st_Start_Home;
        BasicState st_Home_Table;
        BasicState st_Set_Home_Table_Done;
        BasicState st_EndHome;
        #endregion



        FSM_Home_MachineEvent mcEvent;
        public FSM_Home_Table_Station(FSM_Home_MachineEvent mcEvent, bool isSimulation)
        {
            CreateObjectInstance();
            SetStateChain();
            this.mcEvent = mcEvent;
            this.isSimulation = isSimulation;
            this.Name = $"FSM_Table_Home_Station";    
        }


        public override void CreateObjectInstance()
        {
            this.Add_State(
                 st_Start_Home = new BasicState("开始复位", StartHome, OnErrorHanding, this.isSimulation),
                 st_Home_Table = new BasicState("平台复位", HomeTable, OnErrorHanding, this.isSimulation),
                 st_Set_Home_Table_Done = new BasicState("平台复位完成", SetHomeTableDone, OnErrorHanding, this.isSimulation),
                 st_EndHome = new BasicState("复位结束", EndHome, OnErrorHanding, this.isSimulation)
                );
        }

        

        #region State 方法
        private Mission_Report StartHome(StateBase sender)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    //事先准备，目前尚无



                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
        private Mission_Report HomeTable(StateBase sender)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                     context = ResourceKey.Motor_Table.GetAxisBase().HomeMove();
                    if (context.NotPass()) break;


                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }
            
            return context;
        }
        private Mission_Report SetHomeTableDone(StateBase sender)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    mcEvent.Evnt_HomeTable_Done.Reset();
                    Thread.Sleep(10);
                    mcEvent.Evnt_HomeTable_Done.Set();

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
            return sender.FinalReport;
        }

     

        public override void SetStateChain()
        {
            FSMHelper.SetStateChain(st_Start_Home, st_Home_Table);
            FSMHelper.SetStateChain(st_Home_Table, st_Set_Home_Table_Done);
            FSMHelper.SetStateChain(st_Set_Home_Table_Done, st_EndHome);

            this.SetFirstState(st_Start_Home);
            this.SetFinalState(st_EndHome);
        }
    }
}
