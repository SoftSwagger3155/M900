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
                mtr = (IOBase)SolveWare.Core.MMgr.Get_Single_Element_Form_Tool_Resource(SolveWare_Service_Core.Definition.Tool_Resource_Kind.IO, this.ioName);

            }
        }

        [Description("IO号"), Category("自定义属性")]
        public float IoNum { get; set; }

        private IO_Status status;
        [Description("状态"), Category("自定义属性")]
        public IO_Status Status
        {
            get { return status; }
            set
            {
                status = value;
            }
        }
        private bool isRefresh = false;
        public override void Refresh()
        {
            //mtr.Status
            switch (mtr.Status)
            {
                case IO_Status.On:
                    isRefresh = true;
                    uiSwitch1.Active = true;
                    break;
                case IO_Status.Off:
                    isRefresh = true;
                    uiSwitch1.Active = false;
                    break;
            }
        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            if (uiSwitch1.Active)
            {
                mtr.On();
            }
            else
            {
                mtr.Off();
            }

            //if(isRefresh)
            //{
            //    isRefresh = false;
            //}
            //else
            //{
            //    if(uiSwitch1.Active)
            //    {
            //        mtr.On();
            //    }
            //    else
            //    {
            //        mtr.Off();
            //    }
            //}
        }
    }
}
