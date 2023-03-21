using SolveWare_Service_Offset.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Index.Data
{
    public class MF900_IndexData : Data_Offset_Base
    {
        [Category("MF900 索引参数")]
        [DisplayName("PCB X 数量")]
        public int No_Of_Count_PCB_X { get; set; }
    }
}
