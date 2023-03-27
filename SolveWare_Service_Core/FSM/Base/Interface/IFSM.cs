using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.FSM.Base.Interface
{
    public interface IFSM: IElement
    {
        IList<IFSMStation> Stations { get; set; }
        void Stop();
        int Run_Auto_Cycle();
        int Run_One_Cycle();
        void Build_Resource();
    }
}
