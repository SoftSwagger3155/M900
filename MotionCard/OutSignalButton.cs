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
    public partial class OutSignalButton : UserControl
    {
        public OutSignalButton()
        {
            InitializeComponent();
        }

        private MotionBase motion;

        public MotionBase Motion
        {
            get { return motion; }
            set { motion = value; }
        }

        private int outIoNum1;
        [Category("IO号")]
        public int OutIoNum1
        {
            get { return outIoNum1; }
            set { outIoNum1 = value; }
        }
        private int outIoNum2;
        [Category("IO号")]
        public int OutIoNum2
        {
            get { return outIoNum2; }
            set { outIoNum2 = value; }
        }
        [Description("按钮1名称"), Category("自定义属性")]
        public string BtnName1
        {
            get { return uiButton1.Text; }
            set { uiButton1.Text = value; }
        }
        [Description("按钮2名称"), Category("自定义属性")]
        public string BtnName2
        {
            get { return uiButton2.Text; }
            set { uiButton2.Text = value; }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            motion.SetOutSignal(outIoNum1, 1);
            motion.SetOutSignal(outIoNum2, 0);
            uiButton1.FillColor = Color.FromArgb(50, 220, 255);
            uiButton2.FillColor = Color.FromArgb(80, 160, 255);
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            motion.SetOutSignal(outIoNum1, 0);
            motion.SetOutSignal(outIoNum2, 1);
            uiButton1.FillColor = Color.FromArgb(80, 160, 255);
            uiButton2.FillColor = Color.FromArgb(50, 220, 255);
        }
    }
}
