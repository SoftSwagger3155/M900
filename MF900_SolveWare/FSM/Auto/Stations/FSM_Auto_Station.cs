using SolveWare_Service_Core.FSM.Base.Abstract;
using SolveWare_Service_Core.FSM.FSMState;
using SolveWare_Service_Core.FSM.Helper;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.FSM.Auto.Stations
{
    public class FSM_Auto_Station : FSMStationBase
    {
        FSM_Auto_MachineEvent mcEvent;
        public FSM_Auto_Station(FSM_Auto_MachineEvent mcEvent, bool isSimulation)
        {
            this.mcEvent = mcEvent;
            this.isSimulation = isSimulation;
        }

        #region States
        BasicState st_StartAuto;
        BoolState st_Check_Cycling_Is_Continuable;
        BasicState st_GoNext;
        BasicState st_EndAuto;
        #endregion

        public override void CreateObjectInstance()
        {
            st_StartAuto = new BasicState("开始自动运行", StartAuto, OnErrorHanding, isSimulation);
        }

        private int StartAuto(StateBase sender)
        {
            errorCode = ErrorCodes.NoError;
           
            try
            {

            }
            catch (Exception ex)
            {

            }
            
            return errorCode;
        }

        private int OnErrorHanding(StateBase sender)
        {
            return 0;
        }

        public override void SetStateChain()
        {
            FSMHelper.SetStateChain(st_StartAuto, st_Check_Cycling_Is_Continuable);
            FSMHelper.SetStateChain(st_Check_Cycling_Is_Continuable, st_GoNext, st_EndAuto);
            FSMHelper.SetStateChain(st_GoNext, st_Check_Cycling_Is_Continuable);

            SetFirstState(st_StartAuto);
            SetFinalState(st_EndAuto);
        }
    }
}
