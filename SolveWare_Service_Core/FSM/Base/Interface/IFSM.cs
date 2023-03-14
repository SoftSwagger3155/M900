using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.FSM.Base.Interface
{
    public interface IFSM
    {
        IList<IStation> Stations { get; set; }
        void Home();
        void Reset();
        void Stop();
        void Run_Auto_Cycle();
        void Run_One_Cycle();
        void Build_Resource();
    }
}
