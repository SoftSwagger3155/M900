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
    public enum ImageList
    {
        None,
        Down,
        Up,
        LeftRotate,
        RightRotate,
        ArrowLeft,
        ArrowRight
    }
    public partial class AxisMoving : UserControl
    {
        public AxisMoving()
        {
            InitializeComponent();
        }
        public enum AxisOrCylinder 
        {
            axis,
            cylinder
        }
        

        private MotionBase motion;
        [Category("自定义属性")]
        public MotionBase Motion
        {
            set { motion = value; }
        }

        private AxisOrCylinder typeSelect;
        [Description("类型选择"),Category("自定义属性")]
        public AxisOrCylinder TypeSelect
        {
            get { return typeSelect; }
            set { typeSelect = value; }
        }

        private short axisNum;
        [Description("轴号"), Category("自定义属性")]
        public short AxisNum
        {
            get { return axisNum; }
            set { axisNum = value; }
        }

        private string axisName;
        [Description("轴名称"), Category("自定义属性")]
        public string AxisName
        {
            get { return axisName; }
            set { axisName = value; }
        }

        private float vel;
        [Category("自定义属性")]
        public float Vel
        {
            get { return vel; }
            set { vel = value; }
        }


        private int outIoNum;
        [Description("IO号"), Category("自定义属性")]
        public int OutIoNum
        {
            get { return outIoNum; }
            set { outIoNum = value; }
        }
        [Description("按钮1名称"), Category("自定义属性")]
        public string BtnName1
        {
            get { return uiButton1.Text ; }
            set { uiButton1.Text = value; }
        }
        [Description("按钮2名称"), Category("自定义属性")]
        public string BtnName2
        {
            get { return uiButton2.Text; }
            set { uiButton2.Text = value; }
        }

        private ImageList imageBtn1;
        [Category("自定义属性")]
        public ImageList ImageBtn1
        {
            get { return imageBtn1; }
            set
            {
                imageBtn1 = value;
                switch (imageBtn1)
                {
                    case ImageList.None:
                        this.uiButton1.Image = null;
                        break;
                    case ImageList.Down:
                        this.uiButton1.Image = Properties.Resources.Down;
                        break;
                    case ImageList.Up:
                        this.uiButton1.Image = Properties.Resources.Up;
                        break;
                    case ImageList.LeftRotate:
                        this.uiButton1.Image = Properties.Resources.LeftRotate;
                        break;
                    case ImageList.RightRotate:
                        this.uiButton1.Image = Properties.Resources.RightRotate;
                        break;
                    case ImageList.ArrowLeft:
                        this.uiButton1.Image = Properties.Resources.arroyLeft;
                        break;
                    case ImageList.ArrowRight:
                        this.uiButton1.Image = Properties.Resources.arroyRight;
                        break;
                }
            }
        }

        
        private ImageList imageBtn2;
        [Category("自定义属性")]
        public ImageList ImageBtn2
        {
            get { return imageBtn2; }
            set
            {
                imageBtn2 = value;
                switch (imageBtn2)
                {
                    case ImageList.None:
                        this.uiButton2.Image = null;
                        break;
                    case ImageList.Down:
                        this.uiButton2.Image = Properties.Resources.Down;
                        break;
                    case ImageList.Up:
                        this.uiButton2.Image = Properties.Resources.Up;
                        break;
                    case ImageList.LeftRotate:
                        this.uiButton2.Image = Properties.Resources.LeftRotate;
                        break;
                    case ImageList.RightRotate:
                        this.uiButton2.Image = Properties.Resources.RightRotate;
                        break;
                    case ImageList.ArrowLeft:
                        this.uiButton2.Image = Properties.Resources.arroyLeft;
                        break;
                    case ImageList.ArrowRight:
                        this.uiButton2.Image = Properties.Resources.arroyRight;
                        break;
                }
            }
        }
        [Category("自定义属性")]
        public bool IsFwdMove { get; set; } = true;
        [Category("自定义属性")]
        public bool IsRevMove { get; set; } = true;


        private void uiButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (typeSelect == AxisOrCylinder.axis && IsFwdMove)
                motion.VMove(axisNum, vel, true, 2, vel * 1000, vel * 1000, 0);
        }

        private void uiButton1_MouseUp(object sender, MouseEventArgs e)
        {
            if (typeSelect == AxisOrCylinder.axis)
                motion.StopAxis(axisNum);
        }
        private void uiButton2_MouseDown(object sender, MouseEventArgs e)
        {
            if (typeSelect == AxisOrCylinder.axis && IsRevMove)
                motion.VMove(axisNum, vel, false, 2, vel * 1000, vel * 1000, 0);
        }

        private void uiButton2_MouseUp(object sender, MouseEventArgs e)
        {
            if (typeSelect == AxisOrCylinder.axis)
                motion.StopAxis(axisNum);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (typeSelect == AxisOrCylinder.cylinder)
                motion.SetOutSignal(outIoNum, 1);
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            //if (typeSelect == AxisOrCylinder.cylinder)
            //    motion.SetOutSignal(outIoNum, 0);
        }

    }
}
