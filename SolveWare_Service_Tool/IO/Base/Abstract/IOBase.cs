using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.IO.Base.Interface;
using SolveWare_Service_Tool.IO.Data;
using SolveWare_Service_Tool.IO.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.IO.Base.Abstract
{
    public abstract class IOBase : ElementBase, IIOBase
    {
        protected ConfigData_IO configData;
        IO_Status status = IO_Status.On;
        IO_Type ioType = IO_Type.Input;
        #region ctor
        public IOBase(IElement data)
        {
            this.configData = data as ConfigData_IO;
            this.Simulation = (data as ConfigData_IO).Simulation;
            if (Id == 0) id = IdentityGenerator.IG.GetIdentity();
        }
        public void Setup(IElement configData)
        {
            this.configData = configData as ConfigData_IO;
        }
        #endregion

        public string Description
        {
            get;
            set;
        }
        public IO_Status Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string DynamicStatus
        {
            get;
        }
        public IO_Type IOType
        {
            get => ioType;
            set => UpdateProper(ref ioType, value);
        }

        public bool Simulation
        {
            get;
            set;
        }

        public bool IsOff()
        {
            return Status == IO_Status.Off;
        }
        public bool IsOn()
        {
            return Status == IO_Status.On;
        }
        public abstract void Off();
        public abstract void On();
        public abstract void UpdateStatus();
    }
}
