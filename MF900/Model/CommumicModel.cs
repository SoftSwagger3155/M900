using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    [Serializable]
    public class CommumicModel
    {
        public SerialPortPara SerialPortModel { get; set; }
        public TCPModel MotionIP { get; set; }
        public TCPModel ServerIP { get; set; }

    }

    
}
