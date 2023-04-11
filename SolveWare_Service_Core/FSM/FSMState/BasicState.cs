using Microsoft.SqlServer.Server;
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
            string errMsg = string.Empty;
            OnEntrance();
            try
            {
                do
                {                 
                    if (IsSimulation)
                        Thread.Sleep(500);

                    if (OnExecuteHandler == null)
                    {
                        errorCode = ErrorCodes.NoStateActionAssign;
                        break;
                    }

                    errorCode = OnExecuteHandler(this);
                    if (errorCode != ErrorCodes.NoError)
                    {
                        if(OnExecuteErrorHandler !=  null)
                            errorCode = OnExecuteErrorHandler(this);

                        if (errorCode != ErrorCodes.NoError)
                        {
                            if (designatedState != null) this.nextState = designatedState;
                            errMsg += ErrorCodes.GetErrorDescription(errorCode);
                            break;
                        }
                        
                    }

                    this.nextState = YesState;

                } while (false);
            }
            catch(Exception ex)
            {
                errMsg += ex.Message;
            }

            OnExit();
            if (IsSimulation) Thread.Sleep(300);

            return errorCode;
        }
    }
}
