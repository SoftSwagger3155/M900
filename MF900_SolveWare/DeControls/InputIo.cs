using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Base.Interface;
using SolveWare_Service_Tool.IO.Definition;
using SolveWare_Service_Tool.Motor.Base.Abstract;
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
    public enum IoStatus 
    { 
        ON,
        OFF
    }
  
    public partial class InputIo : UserControl, IView
    {
        IOBase mtr = null;
        
        public InputIo()
        {
            InitializeComponent();
        }

        private string ioName;

        [Description("IO名称"),Category("自定义属性")]
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
        
        private IoStatus status;
        [Description("状态"), Category("自定义属性")]
        public IoStatus Status
        {
            get { return status; }
            set { status = value;
                switch (status)
                {
                    case IoStatus.ON:
                        label1.Image = Properties.Resources.Circle_Green;
                        break;
                    case IoStatus.OFF:
                        label1.Image = Properties.Resources.Circle_Gray;
                        break;
                }
            }
        }

        public override void Refresh()
        {
            Status = mtr.Status == IO_Status.On ? IoStatus.ON : IoStatus.OFF;
        }

        
        public void Setup<TObj>(TObj obj)
        {
            
        }
        
    }
}
