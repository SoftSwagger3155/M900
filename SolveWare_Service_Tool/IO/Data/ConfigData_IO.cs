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
    public class ConfigData_IO : ElementBase
    {
        string name;
        IO_Type ioType = IO_Type.Input;
        Master_Driver_IO ioMasterDriver = Master_Driver_IO.ZMCAUX;

        public ConfigData_IO()
        {
            if (Id == 0) Id = IdentityGenerator.IG.GetIdentity();
        }
        public long Id
        {
            get;
            set;
        }
        public string Name
        {
            get => name;
            set => UpdateProper(ref name, value);
        }
        public string Description
        {
            get;
            set;
        }
        public int CardNo { get; set; }
        public int Bit { get; set; }
        public int Logic { get; set; }
        public bool Simulation { get; set; }
        public bool IsForSelect { get; set; }
        public string DynamicStatus
        {
            get;
        }
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
}
