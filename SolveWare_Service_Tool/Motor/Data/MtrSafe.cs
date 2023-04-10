using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Motor.Data
{
    public class MtrSafe : ElementBase
    {
        public List<Data_Pos_Safety> Data_Pos_Safetys { get; set; }
        public List<Data_IO_Safety> Data_IO_Safetys { get; set; }
        public MtrSafe()
        {
            this.Data_Pos_Safetys = new List<Data_Pos_Safety>();
            this.Data_IO_Safetys = new List<Data_IO_Safety>();
        }      
    }

    public class Data_Pos_Safety 
    {
        public bool IsSelected { get; set; }
        public string MotorName { get; set; }
        public string Operand { get; set; }
        public double Pos { get; set; }
    }
    public class Data_IO_Safety 
    {
        public bool IsSelected { get; set; }
        public string IOName { get; set; }
        public string IOType { get; set; }
        public string TriggerMode { get; set; }
    }
}
