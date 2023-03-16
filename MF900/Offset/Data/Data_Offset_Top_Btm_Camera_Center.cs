using CCWin.Win32.Struct;
using MF900.Offset.Base.Abstract;
using SolveWare_Service_Core.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900.Offset.Data
{
    public class Data_Offset_Top_Btm_Camera_Center: OffsetDataBase
    {
        public POINT Top_Camera_Inspect_Pos { get; set; }
        public POINT Btm_Camera_Inspect_Pos { get; set; }
        public POINT Top_Camera_Center_Master_Pos { get; set; }
        public POINT Btm_Camera_Center_Master_Pos { get; set; }
        public string Inspection_Name { get; set; }
    }
}
