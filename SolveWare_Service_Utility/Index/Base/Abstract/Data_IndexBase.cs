using SolveWare_Service_Core.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Index.Base.Abstract
{
    public class Data_IndexBase : ElementBase
    {
        public int Current_Number { get; set; }
        public int Current_Number_Column_X { get; set; }
        public int Current_Number_Row_Y { get; set; }
        public int Number_ToGo { get; set; }
        public int Number_Column_X_ToGo { get; set; }
        public int Number_Column_Y_ToGo { get; set; }
        public double MoveGap_Column_X { get; set; }
        public double MoveGap_Row_Y { get; set; }
        public Point BasePoint { get; set; }
    }
}
