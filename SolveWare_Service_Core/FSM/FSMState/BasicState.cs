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

        public override Mission_Report Do_Job()
        {
            this.context = new Mission_Report();
            this.Status = Definition.JobStatus.Entrance;
            try
            {
                do
                {                 
                    if (IsSimulation)
                        Thread.Sleep(500);

                    this.Status = Definition.JobStatus.Active;
                    if (OnExecuteHandler == null)
                    {
                        this.context.Set(ErrorCodes.NoStateActionAssign);
                        break;
                    }

                    this.context = OnExecuteHandler(this);
                    if (this.context.NotPass())
                    {
                        if(OnExecuteErrorHandler !=  null)
                            this.context = OnExecuteErrorHandler(this);

                        if (this.context.NotPass())
                        {
                            break;
                        }
                        else
                        {
                            if (designatedState != null) this.nextState = designatedState;
                            break;
                        }                   
                    }

                    this.nextState = YesState;

                } while (false);
            }
            catch(Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
            this.Status = context.ErrorCode == ErrorCodes.NoError ? Definition.JobStatus.Done : Definition.JobStatus.Fail;       
            if (IsSimulation) Thread.Sleep(300);
          
            return this.context;
        }
    }
}
