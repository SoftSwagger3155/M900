using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Tool.MasterDriver.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.MasterDriver.Data
{
    [ResourceBaseAttribute("Init_Master_Driver")]
    public class ConfigData_MasterDriver : ElementBase
    {
        public string Resource { get; set; } = "192.168.0.11";
        public bool Is_Simulation_Motor { get; set; }
        public bool Is_Simulation_IO { get; set; }

        private Master_Driver_Kind master_Driver_Motor = Master_Driver_Kind.Zmcaux;
        public Master_Driver_Kind Master_Driver_Motor { get => master_Driver_Motor; set => master_Driver_Motor = value; }

        private Master_Driver_Kind master_Driver_IO = Master_Driver_Kind.Zmcaux;
        public Master_Driver_Kind Master_Driver_IO { get => master_Driver_IO; set => master_Driver_IO = value; }

        public bool Is_Basler_Camera { get; set; }
        public bool Is_HIK_Camera { get; set; }

    }
}
