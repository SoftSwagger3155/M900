using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Motor.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Motor.Data
{
    [ResourceBaseAttribute(ConstantProperty.ReosurceKey_Motor)]
    public class ConfigData_Motor : ElementBase
    {

        MtrTable mtrTable;
        MtrConfig mtrConfig;
        MtrSpeed mtrSpeed;
        MtrSafe mtrSafe;
        MtrMisc mtrMisc;
        bool simulation = true;
        Master_Driver_Motor driver = Master_Driver_Motor.Zmcaux;


        public string Description
        {
            get;
            set;
        }
        public bool Simulation { get; set; }

        public MtrTable MtrTable
        {
            get => mtrTable;
            set => UpdateProper(ref mtrTable, value);
        }
        public MtrConfig MtrConfig
        {
            get => mtrConfig;
            set => UpdateProper(ref mtrConfig, value);
        }
        public MtrSpeed MtrSpeed
        {
            get => mtrSpeed;
            set => UpdateProper(ref mtrSpeed, value);
        }
        public MtrSafe MtrSafe
        {
            get => mtrSafe;
            set => UpdateProper(ref mtrSafe, value);
        }
        public MtrMisc MtrMisc
        {
            get => mtrMisc;
            set => UpdateProper(ref mtrMisc, value);
        }
        public Master_Driver_Motor Driver
        {
            get => driver;
            set => UpdateProper(ref driver, value);
        }
        public string DynamicStatus
        {
            get;
        }
        public double SpeedRate_Home { get; set; }
        public double SpeedRate_Jog { get; set; }


        public ConfigData_Motor()
        {
            mtrTable = new MtrTable();
            mtrConfig = new MtrConfig();
            mtrSpeed = new MtrSpeed();
            mtrSafe = new MtrSafe();
            mtrMisc = new MtrMisc();
            SpeedRate_Home = 0.1;
            SpeedRate_Jog = 1;
        }

    }
}
