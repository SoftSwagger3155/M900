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

                    errorCode = this.OnExecuteHandler(this);
                    if (errorCode != ErrorCodes.NoError) break;

                    this.nextState = this.BoolResult || IsSimulation? this.yesState : this.noState;
                    break;

                } while (false);
            }
            catch
            {
                errorCode = ErrorCodes.ActionNotTaken;
            }
            OnExit();

            if (IsSimulation)
                Thread.Sleep(500);

            return errorCode;  // 回自已拼的组合  
        }
        #endregion
    }
}
