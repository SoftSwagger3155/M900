using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.IO.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.IO.Data
{
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_IO)]
    public class ConfigData_IO : ElementBase
    {
        IO_Type ioType = IO_Type.Input;
        Master_Driver_IO ioMasterDriver = Master_Driver_IO.ZMCAUX;

        public ConfigData_IO()
        {
            Data_Buzzer = new Data_Buzzer_Setting();
            Data_DualChannel = new Data_DualChannel_Setting();  
        }

        public bool Simulation { get; set; }
        public bool IsBuzzer { get; set; }
        public bool IsDualChannel { get; set; }
        public int CardNo { get; set; }
        public int Bit { get; set; }

        public Data_Buzzer_Setting Data_Buzzer { get; set; }
        public Data_DualChannel_Setting Data_DualChannel { get; set; }
        public IO_Type IOType
        {
            get => ioType;
            set => UpdateProper(ref ioType, value);
        }
        public Master_Driver_IO IOMasterDriver
        {
            get => ioMasterDriver;
            set => UpdateProper(ref ioMasterDriver, value);
        }
    }

    public class Data_Buzzer_Setting
    {
        public bool Is_Interval_Buzzing { get; set; }
        public int Interval_ms { get; set; }
    }
    public class Data_DualChannel_Setting
    {
        public int Bit_First { get; set; }
        public string TriggerMode_First { get; set; } = ConstantProperty.ON;
        public int Bit_Second { get; set; }
        public string TriggerMode_Second { get; set; } = ConstantProperty.OFF;
    }
}
