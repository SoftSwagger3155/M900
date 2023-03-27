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
        public override int Do_Job()
        {
            OnEntrance();
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
                errorCode = ErrorCodes.ActionNotTaken;
            }

            OnExit();
            if (IsSimulation)
                Thread.Sleep(500);


            return ErrorCodes.NoError;
        }
    }
}
