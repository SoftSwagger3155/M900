using SolveWare_Service_Core;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare
{
    public partial class OutputSwitch : UserControl
    {
        IOBase mtr = null;
        public OutputSwitch()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(this.IoName))
            {
                mtr = (IOBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(SolveWare_Service_Core.Definition.Tool_Resource_Kind.IO, this.IoName);

            }
        }

        private string ioName;
        [Description("IO名称"), Category("自定义属性")]
        public string IoName
        {
            get { return ioName; }
            set 
            { 
                ioName = value;
                lbl_Name.Text = ioName;
            }
        }

        [Description("IO号"), Category("自定义属性")]
        public float IoNum { get; set; }

        private IoStatus status;
        [Description("状态"), Category("自定义属性")]
        public IoStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                switch (status)
                {
                    case IoStatus.ON:
                        uiSwitch1.Active = true;
                        break;
                    case IoStatus.OFF:
                        uiSwitch1.Active = false;
                        break;
                }
            }
        }

        private bool setStatus;

        public bool SetStatus
        {
            get { return setStatus; }
            set 
            { 
                setStatus = uiSwitch1.Active;
                if(uiSwitch1.Active)
                {
                    mtr.Status = IO_Status.On;
                }
                else
                {
                    mtr.Status = IO_Status.Off;
                }
            }
        }


        public override void Refresh()
        {
            Status = mtr.Status == IO_Status.On ? IoStatus.ON : IoStatus.OFF;
        }
    }
}
