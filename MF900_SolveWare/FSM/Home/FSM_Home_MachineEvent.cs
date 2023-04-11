using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MF900_SolveWare.FSM.Home
{
    public class FSM_Home_MachineEvent
    {
        public ManualResetEvent Evnt_HomeTopZ_Done  = new ManualResetEvent(false);
        public ManualResetEvent Evnt_HomeBtmZ_Done = new ManualResetEvent(false);
        public ManualResetEvent Evnt_HomeTable_Done = new ManualResetEvent(false);
        public FSM_Home_MachineEvent()
        {
            
        }
    }
}
