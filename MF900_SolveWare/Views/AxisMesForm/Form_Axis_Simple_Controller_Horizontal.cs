using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Utility.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.AxisMesForm
{
    public partial class Form_Axis_Simple_Controller_Horizontal : Form, IView
    {
        public Form_Axis_Simple_Controller_Horizontal()
        {
            InitializeComponent();
        }

        AxisBase axis;
        public void Setup<TObj>(TObj obj)
        {
            axis = obj as AxisBase;
            
            this.lbl_Motor_Name.Text = axis.Name;          
            this.ckb_Servo_Switch.Checked = axis.IsServoOn;
            this.txb_AbsolutePos.Text = "0";
            this.txb_RelativePos.Text = "0";

          
            this.ckb_Servo_Switch.CheckedChanged -= Ckb_Servo_Switch_CheckedChanged;
            this.ckb_Servo_Switch.CheckedChanged += Ckb_Servo_Switch_CheckedChanged;
        }

        CancellationTokenSource source = null;
        AutoResetEvent stopFlag = new AutoResetEvent(false);
        private void DataBinding()
        {
            source = new CancellationTokenSource();
            Task task = new Task(() =>
            {
                while (!source.IsCancellationRequested)
                {
                    this.Refresh_UI_Item(lbl_CurrentPhysicalPos, () =>
                    {
                        lbl_CurrentPhysicalPos.Text = $"{this.axis.CurrentPhysicalPos.ToString("F3")}";
                    });
                    this.Refresh_UI_Item(lbl_Lmt_Negative, () =>
                    {
                        Color bcolor = this.axis.IsOrg ? Color.Green : Color.Red;
                        this.lbl_Lmt_Negative.BackColor = bcolor;
                    });
                
                    Thread.Sleep(5);
                }
                stopFlag.Set();

            }, source.Token, TaskCreationOptions.LongRunning);
            task.Start();
        }

        private void Ckb_Servo_Switch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool onOff = (sender as CheckBox).Checked;
                axis.Set_Servo(onOff);
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }


        #region Event
        private void btn_Jog_Positive_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                string msg = string.Empty;
                axis.Jog(true, ref msg);
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Jog_Positive_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                axis.Stop();
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Jog_Negative_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                string msg = string.Empty;
                axis.Jog(false, ref msg);
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Jog_Negative_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                axis.Stop();
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Go_Absolute_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickActionTask(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (string.IsNullOrEmpty(txb_AbsolutePos.Text))
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantData, "绝对位置栏位不得为空");
                            break;
                        }

                        context = axis.MoveTo(double.Parse(txb_AbsolutePos.Text));
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);  
                }

                return context;
            });
        }

        private void btn_Go_Relative_Positive_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickActionTask(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (string.IsNullOrEmpty(txb_RelativePos.Text))
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantData, "相对位置栏位不得为空");
                            break;
                        }

                        context = axis.MoveRelative(1 * double.Parse(txb_RelativePos.Text));
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });
        }

        private void btn_Go_Relative_Negative_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickActionTask(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (string.IsNullOrEmpty(txb_RelativePos.Text))
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantData, "相对位置栏位不得为空");
                            break;
                        }

                        context = axis.MoveRelative(-1 * double.Parse(txb_RelativePos.Text));
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });
        }
        #endregion

        private void btn_Home_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickActionTask(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {                   
                        context = axis.HomeMove();
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                   context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });
        }

        private void Form_Axis_Simple_Controller_Horizontal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (source == null) return;
            source.Cancel();

            stopFlag.WaitOne(100);
            source = null;
        }

        private bool Check_Machine_Status()
        {
            if (SolveWare.Core.MMgr.Status == Machine_Status.Busy) return false;
            if (SolveWare.Core.MMgr.Status == Machine_Status.Initialising) return false;
            if (SolveWare.Core.MMgr.Status == Machine_Status.SingleCycle) return false;
            if (SolveWare.Core.MMgr.Status == Machine_Status.Auto) return false;

            SolveWare.Core.MMgr.SetStatus(Machine_Status.Busy);
            return true;
        }

        private bool CheckErrMsg(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                SetStatus(msg);
                SolveWare.Core.MMgr.SetStatus(Machine_Status.Idle);
                return true;
            }
            else
            {
                SetStatus(msg);
                SolveWare.Core.MMgr.Infohandler.LogMessage(msg, true, true);
                SolveWare.Core.MMgr.SetStatus(Machine_Status.Error);
                return false;
            }
        }

        private void SetStatus(string msg)
        {
            JobStatus status = msg != string.Empty ? JobStatus.Fail : JobStatus.Done;
            //Thread.Sleep(1);
            //this.BeginInvoke(new Action(() =>
            //{
            //    this.lbl_Status.SetStatus(status);

            //}));
        }

        private void Form_Axis_Simple_Controller_Horizontal_Load(object sender, EventArgs e)
        {
            DataBinding();
            if(axis != null)
            {
                this.ckb_Is_Jog_Monitoring.Checked = axis.Is_Jog_Monitoring;
            }
        }

        private void ckb_Is_Jog_Monitoring_CheckedChanged(object sender, EventArgs e)
        {
            if (this.axis != null)
                axis.Is_Jog_Monitoring = (sender as CheckBox).Checked;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txb_AbsolutePos_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
