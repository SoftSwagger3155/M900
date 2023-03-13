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
    public partial class AxisControls : UserControl
    {
        public AxisControls()
        {
            InitializeComponent(); 
            ReloadBtnClick();
        }

        [Category("自定义属性")]
        public string AxisName 
        { 
            set { uiGroupBox1.Text = value; }
            get { return uiGroupBox1.Text; }
        }

        private MotionBase motion;
        [Category("自定义属性")]
        public MotionBase Motion
        {
            set { motion = value; }
        }
        private short axisNum;
        [Category("自定义属性"),Description("轴号")]
        public short AxisNum
        {
            get { return axisNum; }
            set { axisNum = value; }
        }
        [Category("自定义属性"),Description("速度")]
        public float Vel { get; set; } = 0.1f;

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

        public float AxisPoints
        {
            set { textBox1.Text = value.ToString(); }
        }


        [Category("自定义属性")]
        public bool IsFwdMove { get; set; } = true;
        [Category("自定义属性")]
        public bool IsRevMove { get; set; } = true;

        public void ReloadBtnClick()
        {
            uiButton1.MouseDown += UiButton1_MouseDown;
            uiButton1.MouseUp += UiButton1_MouseUp;
            uiButton2.MouseDown += UiButton2_MouseDown;
            uiButton2.MouseUp += UiButton2_MouseUp;
        }

        private void UiButton2_MouseUp(object sender, MouseEventArgs e)
        {
            motion.StopAxis(axisNum);
        }

        private void UiButton2_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsRevMove)
                motion.VMove(axisNum, Vel, true, 2, Vel * 1000, Vel * 1000, 0);
        }

        private void UiButton1_MouseUp(object sender, MouseEventArgs e)
        {
            motion.StopAxis(axisNum);
        }

        private void UiButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsFwdMove)
                motion.VMove(axisNum, Vel, true, 2, Vel * 1000, Vel * 1000, 0);
        }

        public void ReadAxisPoints()
        {
            AxisPoints = motion.GetPos(axisNum);
        }
    }
}
