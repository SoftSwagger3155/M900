using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.FSM.Base.Interface
{
    public interface IFSMStation: IElement, IJobFundamental
    { 
        int RunAutoCycle();
        int RunSingleCycle();
        void CreateObjectInstance();
        void SetStateChain();
        void SetCurrentState(IState state);
        void SetFirstState(IState state);
        void SetFinalState(IState state);
        void Add_State(params IState[] states);
        void SetSimulationMode();
    }
}
