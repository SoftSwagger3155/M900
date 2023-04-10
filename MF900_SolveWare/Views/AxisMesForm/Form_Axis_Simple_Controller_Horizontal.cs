using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Data;
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
                    if (this.lbl_CurrentPhysicalPos.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            lbl_CurrentPhysicalPos.Text = $"{this.axis.CurrentPhysicalPos.ToString("F3")}";
                        }));
                    }
                    if (this.lbl_Lmt_Negative.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            Color bcolor = this.axis.IsOrg ? Color.Green : Color.Red;
                            this.lbl_Lmt_Negative.BackColor = bcolor;
                        }));
                    }
                
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
                axis.Jog(true);
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
                axis.Jog(false);
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
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;

            Task.Run(() =>
            {
                try
                {
                    do
                    {
                        if (Check_Machine_Status() == false)
                        {
                            errMsg += "机器状态不允许此时按钮运行";
                            break;
                        }
                        if (string.IsNullOrEmpty(txb_AbsolutePos.Text))
                        {
                            errMsg += "绝对位置栏位不得为空";
                            break;
                        }

                        errorCode = axis.MoveTo(double.Parse(txb_AbsolutePos.Text)) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                        errMsg += errorCode != ErrorCodes.NoError ? ErrorCodes.GetErrorDescription(errorCode) : string.Empty;

                    } while (false);
                }
                catch (Exception ex)
                {
                    errMsg += ex.Message;
                }
                CheckErrMsg(errMsg);
            });
        }

        private void btn_Go_Relative_Positive_Click(object sender, EventArgs e)
        {
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;

            Task.Run(() =>
            {
                try
                {
                    do
                    {
                        if (Check_Machine_Status() == false)
                        {
                            errMsg += "机器状态不允许此时按钮运行";
                            break;
                        }
                        if (string.IsNullOrEmpty(txb_RelativePos.Text))
                        {
                            errMsg += "相对位置栏位不得为空";
                            break;
                        }

                        errorCode = axis.MoveRelative(1 * double.Parse(txb_RelativePos.Text)) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                        errMsg += errorCode != ErrorCodes.NoError ? ErrorCodes.GetErrorDescription(errorCode) : string.Empty;

                    } while (false);
                }
                catch (Exception ex)
                {
                    errMsg += ex.Message;
                }
                CheckErrMsg(errMsg);
            });
        }

        private void btn_Go_Relative_Negative_Click(object sender, EventArgs e)
        {
            int errorCode = ErrorCodes.NoError;
            string errMsg = string.Empty;

            Task.Run(() =>
            {
                try
                {
                    do
                    {
                        if (Check_Machine_Status() == false)
                        {
                            errMsg += "机器状态不允许此时按钮运行";
                            break;
                        }
                        if (string.IsNullOrEmpty(txb_RelativePos.Text))
                        {
                            errMsg += "相对位置栏位不得为空";
                            break;
                        }

                        errorCode = axis.MoveRelative(-1 * double.Parse(txb_RelativePos.Text)) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                        errMsg += errorCode != ErrorCodes.NoError ? ErrorCodes.GetErrorDescription(errorCode) : string.Empty;

                    } while (false);
                }
                catch (Exception ex)
                {
                    errMsg += ex.Message;
                }
                CheckErrMsg(errMsg);
            });
        }
        #endregion

        private void btn_Home_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            int errorCode = ErrorCodes.NoError;


            Task task = Task.Run(() =>
            {
                try
                {
                    do
                    {
                        if (Check_Machine_Status() == false)
                        {
                            errMsg += "机器状态不允许此时按钮运行";
                            break;
                        }

                        errorCode = axis.HomeMove() ? ErrorCodes.NoError : ErrorCodes.MotorHomingError;
                        errMsg += errorCode == ErrorCodes.NoError ? string.Empty : ErrorCodes.GetErrorDescription(errorCode);

                    } while (false);
                }
                catch (Exception ex)
                {
                    errMsg += ex.Message;
                }

                CheckErrMsg(errMsg);
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
        }
    }
}
