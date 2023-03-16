using CCWin.Win32.Struct;
using MF900.Offset.Base.Abstract;
using MF900.Offset.Definition;
using SolveWare_Service_Core.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900.Offset.Data
{
    public class Data_Offset_Camera_To_Tool: OffsetDataBase
    {
        public double Add_PosX { get; set; }
        public double Add_PosY { get; set; }
        //治具中间Mark点
        public POINT Tool_First_Mark_Pos { get; set; }
        //治具二边柱点 和 中间MARK点的差距
        public double First_Pole_Mark_Gap { get; set; }
        public double Tool_Second_Mark_Gap { get; set; }
        public double Second_Pole_Mark_Gap { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }


    }
}
