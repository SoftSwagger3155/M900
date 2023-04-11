using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.FSM.Base.Abstract
{
    public abstract class FSMStationBase: JobFundamentalBase, IFSMStation
    {
        protected bool isSimulation;

        public FSMStationBase()
        {
            this.states = new List<StateBase>();

        }
        protected StateBase firstState;
        public StateBase FirstState { get=> firstState; }

        protected StateBase finalState;
        public StateBase FinalState { get=> finalState; }

        protected StateBase currentState;
        public StateBase CurrentState { get=> currentState; }
        
        protected IList<StateBase> states;
        public IList<StateBase> States { get=> states;}

        public void Add_State(params IState[] states)
        {
            foreach (var item in states)
            {
                this.states.Add(item as StateBase);
            }
        }

        public abstract void CreateObjectInstance();

        public int RunAutoCycle()
        {
            OnEntrance();
            errorCode = ErrorCodes.NoError;
            currentState = CurrentState ?? FirstState;

            try
            {
                if (CurrentState == null) return ErrorCodes.NoStateActionAssign;

                while (true)
                {
                    errorCode = currentState.Do_Job();
                    if (errorCode.NotPass()) break;


                    //自动
                    if (currentState == finalState)
                    {
                        currentState = firstState;
                        continue;
                    }

                    currentState = currentState.NextState;

                }
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.ActionFailed;
            }

            return ErrorCode;
        }

        public int RunSingleCycle()
        {
            OnEntrance();
            errorCode = ErrorCodes.NoError;
            currentState = CurrentState ?? FirstState;
            try
            {
                if (CurrentState == null) return ErrorCodes.NoStateActionAssign;

                while (true)
                {
                    errorCode = CurrentState.Do_Job();
                    if (errorCode.NotPass()) break;



                    //单循环停在此
                    if (currentState == finalState)
                    {
                        currentState = firstState;
                        break;
                    }

            
                    currentState = currentState.NextState;

                }
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.ActionFailed;
            }

            return ErrorCode;
        }

        public void SetCurrentState(IState state)
        {
            this.currentState = state as StateBase;
        }

        public void SetFinalState(IState state)
        {
            this.finalState = state as StateBase;
        }

        public void SetFirstState(IState state)
        {
            this.firstState = state as StateBase;
        }

        public abstract void SetStateChain();

        public void SetSimulationMode()
        {
            this.isSimulation = true;
            //this.states.ToList().ForEach(state => { state.IsSimulation = this.isSimulation; });
        }
    }
}
