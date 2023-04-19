using SolveWare_Service_Core.Base.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Index.Base.Abstract
{
    public class Data_IndexBase : ElementBase
    {

        public Data_IndexBase()
        {
            Data_Display = new Data_Index_UIDisplay();
            Data_Operation = new Data_Index_Operation();
            Data_Setup = new Data_Index_Setup();
            Data_FirstPos = new Data_Index_FirstPos();
        }

        public Data_Index_UIDisplay Data_Display { get; set; }
        public Data_Index_Operation Data_Operation { get; set; }
        public Data_Index_Setup Data_Setup { get; set; }
        
        public Data_Index_FirstPos Data_FirstPos { get; set; }
    }
}

public class Data_Index_Operation
{
    public int Go_Number { get; set; } = 1;
    public int Go_Number_X { get; set; } = 1;
    public int Go_Number_Y { get; set; } = 1;
}
public class Data_Index_UIDisplay
{
    public int Current_No { get; set; } = 1;
    public int Current_X { get; set; }
    public int Current_Y { get; set; }
}


public class Data_Index_Setup
{
    [DisplayName("1. 总数-X")]
    public int Total_Nos_Of_X { get; set; } = 20;
    
    [DisplayName("2. 总数-Y")]
    public int Total_Nos_Of_Y { get; set; } = 20;

    [DisplayName("3. 间距-X")]
    public double Pitch_X { get; set; } = 1;

    [DisplayName("4. 间距-Y")]
    public double Pitch_Y { get; set; } = 1;

    [DisplayName("5. 位移顺序")]
    public IndexPriority Move_Priority { get; set; } = IndexPriority.ColumnFirst;
}

public class Data_Index_FirstPos
{
    public double Top_PosX { get; set; }
    public double Top_PosY { get; set; }
    public double Top_PosZ { get; set; }
    public double Top_PosT { get; set; }

    public double Btm_PosX { get; set; }
    public double Btm_PosY { get; set; }
    public double Btm_PosZ { get; set; }
    public double Btm_PosT { get; set; }
}

public enum IndexPriority
{
    RowFirst,
    ColumnFirst
}


