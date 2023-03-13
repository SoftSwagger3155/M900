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
    public partial class OutSwitch : UserControl
    {
        private bool singState;
        private bool isInital = true;
        public OutSwitch()
        {
            InitializeComponent();
            //设置控件样式
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private MotionBase motion;

        public MotionBase Motion
        {
            get { return motion; }
            set 
            { 
                motion = value;
                if(motion!=null)
                {
                    motion.GetOutSignal(ioNumber, ref singState);
                    SetState(singState);
                }
                
            }
        }


        public string IoNmae
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        private int ioNumber;

        public int IoNum
        {
            get { return ioNumber; }
            set { ioNumber = value; }
        }

        public bool SinglChecked
        {
            get { return xktToggle1.Checked; }
            set
            {
                if (motion != null)
                {
                    xktToggle1.Checked = value;
                    isInital = false;
                }
            }
        }

        public void SetState(bool state)
        {
            SinglChecked = state;
        }
        private void xktToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInital)
                motion?.SetOutSignal(ioNumber, SinglChecked ? (uint)1 : (uint)0);
            
        }
    }
}
