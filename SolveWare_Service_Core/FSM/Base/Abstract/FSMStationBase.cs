using log4net.Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.FSM.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public Mission_Report RunAutoCycle()
        {
            this.states.ToList().ForEach(state => { state.Info = this.Name; });
            Status = Definition.JobStatus.Entrance;
            Mission_Report mReport = new Mission_Report();
            currentState = CurrentState ?? FirstState;

            try
            {
                do
                {
                    if (CurrentState == null)
                    {
                        mReport.ErrorCode = ErrorCodes.NoStateActionAssign;
                        mReport.Message = ErrorCodes.GetErrorDescription(ErrorCodes.NoStateActionAssign);
                        break;
                    }
                    Status = Definition.JobStatus.Active;
                    while (true)
                    {
                        mReport = currentState.Do_Job();
                        if (mReport.NotPass()) break;


                        //自动
                        if (currentState == finalState)
                        {
                            currentState = firstState;
                            continue;
                        }

                        currentState = currentState.NextState;

                    }

                } while (false);
            }
            catch (Exception ex)
            {
                mReport.ErrorCode = ErrorCodes.ActionFailed;
                mReport.Message = $"{ErrorCodes.GetErrorDescription(ErrorCodes.ActionFailed)}\r\n{ex.Message}";
            }
            finally
            {
                if (mReport.NotPass())
                    Status = Definition.JobStatus.Fail;
                else
                    status = Definition.JobStatus.Done;
            }

            return mReport;
        }

        public Mission_Report RunSingleCycle()
        {
            this.states.ToList().ForEach(state => { state.Info = this.Name; });
            this.Status = Definition.JobStatus.Entrance;
            currentState = CurrentState ?? FirstState;
            Mission_Report context = new Mission_Report();

            try
            {
                do
                {
                    if (CurrentState == null)
                    {
                        context.ErrorCode = ErrorCodes.NoStateActionAssign;
                        context.Message = ErrorCodes.GetErrorDescription(ErrorCodes.NoStateActionAssign);
                        break;
                    }

                    while (true)
                    {
                        context = CurrentState.Do_Job();
                        if (context.NotPass())
                        {
                            SolveWare.Core.MMgr.Stop();
                            break;
                        }



                        //单循环停在此
                        if (currentState == finalState)
                        {
                            currentState = firstState;
                            break;
                        }


                        currentState = currentState.NextState;

                    }

                } while (false);
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.FSMRunningFailed, ex.Message);
            }

            return context;
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
