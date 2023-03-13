using CCWin;
using MotionCard;
using Newtonsoft.Json;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public partial class FormDebug : Form
    {
        //private Dictionary<string, AxisPara> axisPara;
        //private Dictionary<string, AxisSpeed> axisSpeed;
        private AxisMoving axisMoving;
        private MotionBase motion;
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        public FormDebug()
        {
            InitializeComponent();
            this.motion = MotionCommons.motion;
            UpdateAxisVel(true, true, true);
            TimerUpdate();
            SetControlTag();
        }

        #region 控件大小自适应
        private void SetControlTag()
        {
            x = this.Width;
            y = this.Height;
            AutoSizeControls.SetTag(this);
            this.Refresh();
        }
        private void FormDebug_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            AutoSizeControls.SetControls(newx, newy, this);
            this.Refresh();
        }

        #endregion

        /// <summary>
        /// 初始化轴控件
        /// </summary>
        public void UpdateAxisVel(bool isMotion,bool isFwdMove,bool isRevMove)
        {
            foreach (Control tab in uiTabControl1.Controls)
            {
                if (tab is TabPage)
                {
                    foreach (Control ctr in tab.Controls)
                    {
                        if(ctr is UITableLayoutPanel)
                        {
                            foreach (Control Tlp in ctr.Controls)
                            {
                                if (Tlp is UIGroupBox)
                                {
                                    foreach (Control item in Tlp.Controls)
                                    {
                                        if (item is Panel)
                                        {
                                            foreach (Control axCtr in item.Controls)
                                            {
                                                if (axCtr is AxisMoving)
                                                {
                                                    axisMoving = (AxisMoving)axCtr;
                                                    SetAxisJopVel(axisMoving);
                                                    //axisMoving.Vel = axisSpeed[axisMoving.AxisName].vel * BaseTask.vecPercent;
                                                    if (isMotion) axisMoving.Motion = motion;
                                                    axisMoving.IsFwdMove = isFwdMove;
                                                    axisMoving.IsRevMove = isRevMove;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void TimerUpdate()
        {
            outSignalButton2.Motion = motion;
            outSignalButton4.Motion = motion;
            outSignalButton5.Motion = motion;
            outSignalButton6.Motion = motion;
            axisPointsControl1.Motion = motion;
            axisPointsControl2.Motion = motion;
            axisPointsControl3.Motion = motion;
            axisPointsControl4.Motion = motion;
            axisPointsControl5.Motion = motion;
            axisPointsControl6.Motion = motion;
            axisPointsControl7.Motion = motion;
            axisPointsControl8.Motion = motion;
            axisPointsControl9.Motion = motion;
            Task.Factory.StartNew(new Action(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);
                    if (this.IsHandleCreated)
                        UpdateAxisPoint();
                    UpdateAxisMoveStatus();
                }
            }));
        }

        public void UpdateAxisPoint()
        {
            this.Invoke(new Action(() =>
            {
                axisPointsControl1.GetAxisPoint();
                axisPointsControl2.GetAxisPoint();
                axisPointsControl3.GetAxisPoint();
                axisPointsControl4.GetAxisPoint();
                axisPointsControl5.GetAxisPoint();
                axisPointsControl6.GetAxisPoint();
                axisPointsControl7.GetAxisPoint();
                axisPointsControl8.GetAxisPoint();
                axisPointsControl9.GetAxisPoint();
            }));
        }
        /// <summary>
        /// 轴限位
        /// </summary>
        public void UpdateAxisMoveStatus()
        {
            bool sin1 = false;
            bool sin2 = false;
            //升降轴限位上下模Y轴
           if(motion.GetPos(ProgramParamMange.AxisPara["托板升降轴"].AxisNum) > 0 && 
                motion.GetPos(ProgramParamMange.AxisPara["托板升降轴"].AxisNum) < 185)
            {
                if (motion.IsMoving(axisMoving5.AxisNum))
                    motion.StopAxis(axisMoving5.AxisNum);
                if(motion.IsMoving(axisMoving7.AxisNum))
                    motion.StopAxis(axisMoving7.AxisNum);
                axisMoving5.IsFwdMove = false;
                axisMoving5.IsRevMove = false;
                axisMoving7.IsFwdMove = false;
                axisMoving7.IsRevMove = false;
            }
           else
            {
                axisMoving5.IsFwdMove = true;
                axisMoving5.IsRevMove = true;
                axisMoving7.IsFwdMove = true;
                axisMoving7.IsRevMove = true;
            }
            //治具感应限位所有轴
            motion.GetInSignal(ProgramParamMange.InIo["上模治具到位感应"], ref sin1);
            motion.GetInSignal(ProgramParamMange.InIo["下模治具到位感应"], ref sin2);
            if (!sin1 || !sin2)
            {
                UpdateAxisVel(false, false, false);
            }
            else if(sin1 && sin2)
            {
                UpdateAxisVel(false, true, true);
            }

        }

        private void SetAxisJopVel(AxisMoving axisMoving)
        {
            axisMoving.Vel = ProgramParamMange.AxisSpeed[axisMoving.AxisName].vel * 0.3f;
        }
       
    }
}
