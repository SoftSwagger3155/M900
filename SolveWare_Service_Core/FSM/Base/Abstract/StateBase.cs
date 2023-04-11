using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.FSM.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolveWare_Service_Core.FSM.Base.Abstract
{
    public delegate int ExecuteHandler(StateBase sender);
    public delegate int ExecuteErrorHandler(StateBase sender);
    public class StateBase : JobFundamentalBase, IState
    {
        public StateBase()
        {

        }
        public StateBase(string name, ExecuteHandler onExecute, ExecuteErrorHandler onExecuteError, bool isSimulation)
        {
            this.Name = name;
            this.IsSimulation = isSimulation;
            this.OnExecuteHandler = onExecute;
            this.OnExecuteErrorHandler = onExecuteError;
        }

        protected ExecuteHandler OnExecuteHandler;
        protected ExecuteErrorHandler OnExecuteErrorHandler;

        [XmlIgnore]
        public bool IsSimulation { get; private set; }

        [XmlIgnore]
        public bool IsFirstState
        {
            get;
            set;
        }
        [XmlIgnore]
        public bool IsFinalState
        {
            get;
            set;
        }

        protected StateBase nextState;
        public StateBase NextState { get => nextState; }

        protected StateBase yesState;
        public StateBase YesState { get => yesState;  }

        protected StateBase noState;
        public StateBase NoState { get=> noState; }

        protected StateBase designatedState;

        public void Set_Designated_State(StateBase state) { this.designatedState = state; }

        public void SetStateChain(IState yesState, IState noState = null)
        {
            this.yesState = yesState as StateBase;
            this.noState = noState as StateBase;
        }
    }
}
