using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    public class JipDataModel
    {
        public string JipName { get; set; }
        public Point JipStepXY { get; set; }
        public Pofloat JipPos { get; set; }
        public Pofloat JipOffsetPos1 { get; set; }
        public Pofloat JipOffsetPos2 { get; set; }
    }
}
