using SolveWare_Service_Core.FSM.Base.Abstract;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.FSM.FSMState
{
    public class WaitState : StateBase
    {
        private CancellationTokenSource source;

        #region ctor
        public WaitState()
        {
            source = new CancellationTokenSource();
        }
        public WaitState(string name, ExecuteHandler executeHandler, ExecuteErrorHandler errorHandler, bool isSimulation) : base(name, executeHandler, errorHandler,  isSimulation)
        {

        }
        #endregion

        public void SetFlag()
        {
            if (source == null) return;
            source.Cancel();
        }
        public override Mission_Report Do_Job()
        {
            Mission_Report context = new Mission_Report();
            this.Status = Definition.JobStatus.Entrance;
            try
            {
                do
                {
                    if (IsSimulation)
                    {
                        Thread.Sleep(1000);
                       
                    }
                    else if (OnExecuteHandler != null)
                    {
                        OnExecuteHandler(this);
                    }


                    source = null;
                    source = new CancellationTokenSource();

                    this.nextState = yesState;

                } while (false);
            }
            catch
            {
                context.Set(ErrorCodes.ActionNotTaken);
            }

            this.Status = context.ErrorCode == ErrorCodes.NoError ? Definition.JobStatus.Done : Definition.JobStatus.Fail;
            if (IsSimulation) Thread.Sleep(500);


            return context;
        }
    }
}
