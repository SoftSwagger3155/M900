using CCWin.Win32.Struct;
using SolveWare_Service_Core.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900.Index.Data
{
    public class Data_M900_Index: ElementBase
    {
        public int Total_Number_X { get; set; }
        public int Total_Number_Y { get; set; }
        public int Total_Number_Tool_Zone_X { get; set; }
        public int Total_Number_Tool_Zone_Y { get; set; }
        public double Pitch_X { get; set; }
        public double Pitch_Y { get; set; }
        public int Current_Number_X { get; set; }
        public int Current_Number_Y { get; set; }
        public int Current_Unit_Number { get; set; }
        public POINT Left_Btm_Pos { get; set; }
        public POINT Left_Top_Pos { get; set; }
        public POINT Right_Btm_Pos { get; set; }
        public POINT Right_Top_Pos { get; set; }
        public POINT First_Unit_Pos { get; set; }
    }
}
