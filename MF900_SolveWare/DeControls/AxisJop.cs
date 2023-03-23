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
    public partial class AxisJop : UserControl
    {
        public AxisJop()
        {
            InitializeComponent();
        }

        [Description("轴名称"),Category("自定属性")]
        public string AxisName
        {
            get { return uiTitlePanel1.Text; }
            set { this.uiTitlePanel1.Text = value; }
        }

        private short axisNum;
        [Description("轴号"), Category("自定属性")]
        public short AxisNum
        {
            set { axisNum = value; }
        }

        
    }
}
