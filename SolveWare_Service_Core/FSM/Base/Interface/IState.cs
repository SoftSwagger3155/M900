using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.FSM.Base.Interface
{
    public interface IState: IElement, IJobFundamental
    {
     
        void SetStateChain(IState yesState, IState noState = null);

    }
}
