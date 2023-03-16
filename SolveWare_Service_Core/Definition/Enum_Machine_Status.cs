using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.Definition
{
    public enum Machine_Status
    {
        UnInitialised,
        Initialising,
        Idle,
        Auto,
        SingleCycle,
        Busy,
        Error,
        Error_System_Loading,
        Stop,
        Reset
    }
}
