using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    [Serializable]
    public class SerialPortPara
    {
        public string Port { get; set; }
        public int BaudRrate { get; set; }
        public string CheckBit { get; set; }
        public int DataBit { get; set; }
        public double StopBit { get; set; }
    }
}
