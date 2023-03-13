using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    [Serializable]
    public class TCPModel
    {
        public string Ip { get; set; }
        public int Port { get; set; }
    }
}
