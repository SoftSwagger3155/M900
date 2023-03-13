using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionCard
{
    public partial class AxisPoint : UserControl
    {
        public AxisPoint()
        {
            InitializeComponent();
        }

        private short axisNum;
        [Category("自定义属性")]
        public short AxisNum
        {
            get { return axisNum; }
            set { axisNum = value; }
        }

        private string axisName;
        [Category("自定义属性")]
        public string AxisName
        {
            get { return axisName; }
            set { axisName = value; }
        }

        [Description("点位值"), Category("自定义属性")]
        public float AxisPointValue
        {
            get { return Convert.ToSingle(txt_PointsNum.Text); }
            set { txt_PointsNum.Text = value.ToString(); }
        }

        [Category("自定义属性")]
        public string PointName
        {
            get { return lbl_PointName.Text; }
            set { lbl_PointName.Text = value; }
        }

        [Description("单位颜色"), Category("自定义属性")]
        public Color unitBkColor
        {
            get { return lbl_unit.BackColor; }
            set { lbl_unit.BackColor = value; }
        }
        public override void Refresh()
        {
            base.Refresh();
            {

            }
        }
    }
}
