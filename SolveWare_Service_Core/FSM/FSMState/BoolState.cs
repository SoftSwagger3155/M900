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
    public class BoolState : StateBase
    {
        private bool boolResult;

        #region ctor
        public BoolState()
        {

        }
        public BoolState(string name, ExecuteHandler executeHandler, ExecuteErrorHandler errorHandler, bool isSimulation) : base(name, executeHandler, errorHandler, isSimulation)
        {

        }
        #endregion

        #region 属性
        public bool BoolResult
        {
            get { return boolResult; }
            set { boolResult = value; OnPropertyChanged(nameof(BoolResult)); }
        }
        #endregion

        #region 方法
        public override Mission_Report Do_Job()
        {
            Mission_Report context = new Mission_Report();
            this.Status = Definition.JobStatus.Entrance;
            try
            {
                do
                {  
                    if (IsSimulation)
                        Thread.Sleep(1000);

                    this.Status = Definition.JobStatus.Active;
                    if (OnExecuteHandler == null)
                    {
                        context.Set(ErrorCodes.NoStateActionAssign);
                        break;
                    }

                    context = this.OnExecuteHandler(this);
                    if (context.NotPass()) break;

                    this.nextState = this.BoolResult || IsSimulation? this.yesState : this.noState;
                    break;

                } while (false);
            }
            catch
            {
                context.Set(ErrorCodes.ActionNotTaken);
            }
            this.Status = context.ErrorCode == ErrorCodes.NoError ? Definition.JobStatus.Done : Definition.JobStatus.Fail;

            if (IsSimulation) Thread.Sleep(500);
            return context;  // 回自已拼的组合  
        }
        #endregion
    }
}
