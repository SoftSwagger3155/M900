using SolveWare_Service_Core.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Index.Data
{
    public abstract class Data_IndexBase: ElementBase
    {
        public int Current_Number { get; set; }
        public int Current_Number_X { get; set; }
        public int Current_Number_Y { get; set;}
        public int Number_ToGo { get; set; }
        public int X_ToGo { get; set; }
        public int Y_ToGo { get; set; }
        public double MoveGap_X { get; set; }
        public double MoveGap_Y { get; set; }
        public Point FirstPos { get; set; }
    }
}
