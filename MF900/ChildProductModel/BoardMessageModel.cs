using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    public enum OneBoard
    {
        左后,
        右后,
        左前,
        右前
    }
    public class BoardMessageModel
    {
        public OneBoard OneBoardSelect { get; set; }
        public string Priority { get; set; }
    }
}
