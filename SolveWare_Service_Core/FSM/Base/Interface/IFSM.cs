using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
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
        Mission_Report Run_Auto_Cycle();
        Mission_Report Run_One_Cycle();
        void Build_Resource();
    }
}
