using SolveWare_Service_Core.FSM.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.FSM.Helper
{
    public class FSMHelper
    {
        public static void SetStateChain(IState baseState, IState yesState, IState noState = null)
        {
            baseState.SetStateChain(yesState, noState);
        }
    }
}
