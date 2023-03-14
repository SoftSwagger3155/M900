using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.IO.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.IO.Base.Interface
{
    public interface IIOBase : IElement
    {
        IO_Status Status { get; set; }
        IO_Type IOType { get; set; }
        bool Simulation { get; set; }
        bool IsOn();
        bool IsOff();
        void On();
        void Off();
        void UpdateStatus();
    }
}
