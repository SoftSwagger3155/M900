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
    public class BasicState : StateBase
    {
        public BasicState(string name, ExecuteHandler executeHandler, ExecuteErrorHandler errorHandler, bool isSimulation) : base(name, executeHandler, errorHandler, isSimulation)
        {

        }

        public override int Do_Job()
        {
            OnEntrance();
            try
            {
                do
                {                 
                    if (IsSimulation)
                        Thread.Sleep(1000);

                    if (OnExecuteHandler == null)
                    {
                        errorCode = ErrorCodes.NoStateActionAssign;
                        break;
                    }

                    errorCode = OnExecuteHandler(this);
                    if (errorCode != ErrorCodes.NoError)
                    {
                        SolveWare.Core.MMgr.Infohandler.LogActionMessage($"{this.Name} Error | ErrorCode {errorCode} | Description {ErrorCodes.GetErrorDescription(errorCode)}");
                        break;
                    }

                    this.nextState = YesState;


                } while (false);
            }
            catch
            {
                errorCode = ErrorCodes.ActionNotTaken;
            }
            
            OnExit();
            if (IsSimulation)
                Thread.Sleep(500);

            return errorCode;
        }
    }
}
