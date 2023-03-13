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
    public partial class AxisPointsControl : UserControl
    {
        public AxisPointsControl()
        {
            InitializeComponent();
        }
        private string axisName;
        [Category("自定义属性")]
        public string AxisName
        {
            get { return axisName; }
            set { axisName = value; }
        }
        private short axisNum;
        [Category("自定义属性")]
        public short AxisNum
        {
            get { return axisNum; }
            set { axisNum = value; }
        }
        
        [Description("点位值"), Category("自定义属性")]
        public string AxisPointValue
        {
            //get { return Convert.ToSingle(txt_PointsNum.Text); }
            set { txt_PointsNum.Text = value; }
        }
        private MotionBase motion;
        [Category("自定义属性")]
        public MotionBase Motion
        {
            set { motion = value; }
        }

        public void GetAxisPoint()
        {
            AxisPointValue = motion.GetMpos(axisNum).ToString();
        }
    }
}
