using log4net.Core;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Utility.Extension;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.AxisMesForm
{
    public partial class Form_Axis_Configuration_Item_MtrSpeed : Form, IView
    {
        public Form_Axis_Configuration_Item_MtrSpeed()
        {
            InitializeComponent();
        }

        ConfigData_Motor configData;
        AxisBase axis = null;
        public void Setup<TObj>(TObj obj)
        {
            this.configData = obj as ConfigData_Motor;
            axis = configData.MtrTable.Name.GetAxisBase();

            if (axis == null) return;
            
            Fillup_Combobox_SpeedSetting();
            //DataBinding();

            this.cmb_Selector_SpeedSetting.SelectionChangeCommitted -= Cmb_Selector_SpeedSetting_SelectionChangeCommitted;
            this.cmb_Selector_SpeedSetting.SelectionChangeCommitted += Cmb_Selector_SpeedSetting_SelectionChangeCommitted;
        }

        CancellationTokenSource source = null;
        private void DataBinding()
        {
           source = new CancellationTokenSource();
            Task task = new Task(() =>
            {
                while(!source.IsCancellationRequested)
                {
                    if(!this.IsHandleCreated)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    if (this.lbl_TestPos.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            lbl_TestPos.Text = $"{this.axis.CurrentPhysicalPos.ToString("F3")}";
                        }));
                    }
                    if(this.lbl_TimeSpent.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            lbl_TimeSpent.Text = $"{this.axis.TimeSpent}";
                        }));
                    }
                    if (this.lbl_Servo.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            if (axis.IsServoOn)
                            {
                                lbl_Servo.Text = "使能中";
                                lbl_Servo.TextAlign = ContentAlignment.MiddleCenter;
                                lbl_Servo.BackColor = Color.Green;
                            }
                            else
                            {
                                lbl_Servo.Text = "无使能";
                                lbl_Servo.TextAlign = ContentAlignment.MiddleCenter;
                                lbl_Servo.BackColor = Color.Red;
                            }
                        }));
                    }
                    if (this.lbl_Org.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            string orgMsg = this.axis.IsOrg ? "未触发" : "触发";
                            lbl_Org.TextAlign = ContentAlignment.MiddleCenter;
                            lbl_Org.Text = $"原点状态 {orgMsg}";
                        }));
                    }
                    if (this.lbl_ErrorReport.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            if(this.axis.ErrorReport == string.Empty)
                            {
                                lbl_ErrorReport.Visible = false;
                            }
                            else
                            {
                                lbl_ErrorReport.Visible = true; 
                                lbl_ErrorReport.TextAlign = ContentAlignment.MiddleCenter;
                                lbl_ErrorReport.Text = $"失败报告 {axis.ErrorReport}";
                                lbl_ErrorReport.BackColor = Color.Red;
                            }
                        }));
                    }
                    if (lbl_Status.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            if (this.axis.IsMoving)
                            {
                                lbl_Status.Text = "运行中";
                                lbl_Status.AutoSize = false;
                                lbl_Status.Size = new Size(53, 26);
                                lbl_Status.Padding = new Padding(0,4, 0, 0);
                                lbl_Status.TextAlign = ContentAlignment.TopCenter;
                                lbl_Status.ForeColor = Color.Orange;
                            }
                            else
                            {
                                lbl_Status.Text = "静止";
                                lbl_Status.AutoSize = false;
                                lbl_Status.Size = new Size(53, 26);
                                lbl_Status.Padding = new Padding(0, 4, 0, 0);
                                lbl_Status.TextAlign = ContentAlignment.TopCenter;
                                lbl_Status.ForeColor = Color.Black;
                            }
                        }));
                    }
                    Thread.Sleep(5);

                }

            }, source.Token, TaskCreationOptions.LongRunning);
            task.Start();
        }

        private void Cmb_Selector_SpeedSetting_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectedTarget = (sender as ComboBox).SelectedItem as string;
            if (selectedTarget == null) return;

            List<string> temp = new List<string>();
            cmb_Selector_Copy.Items.Clear();
            axis.ConfigData.MtrSpeed.SpeedSettings.ForEach(x => { if (x.Name != selectedTarget) cmb_Selector_Copy.Items.Add(x.Name); });

            var targetObj = axis.ConfigData.MtrSpeed.SpeedSettings.FirstOrDefault(x=> x.Name == selectedTarget);
            this.pGrid_Speed.SelectedObject = targetObj;

        }

        private void Fillup_Combobox_SpeedSetting()
        {
            if(this.axis == null) return;
          
            this.cmb_Selector_SpeedSetting.Items.Clear();
            axis.ConfigData.MtrSpeed.SpeedSettings.ForEach(s => { this.cmb_Selector_SpeedSetting.Items.Add(s.Name); });

            if(cmb_Selector_SpeedSetting.Items.Count > 0)
            {
                cmb_Selector_SpeedSetting.SelectedItem = cmb_Selector_SpeedSetting.Items[0];
                SpeedSeting speed = GetMainSpeedSetting();
                this.pGrid_Speed.SelectedObject = speed;
            }

        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            SpeedSeting setting = GetMainSpeedSetting();
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (setting == null)
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "请选择一个使用项");
                            break;
                        }

                        context = axis.HomeMove(setting);
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

        private void SetSpentTime(Stopwatch sw)
        {
            Thread.Sleep(1);
            if (lbl_TimeSpent.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    lbl_TimeSpent.Text = $"{sw.Elapsed.TotalSeconds.ToString("F3")} 秒";
                }));
            }
        }
        private void SetStatus(bool isBegining = true, int errorCode = 0)
        {
            JobStatus jStatus = JobStatus.Unknown;
            if (isBegining) jStatus = JobStatus.Active;
            else jStatus = errorCode == ErrorCodes.NoError ? JobStatus.Done: JobStatus.Fail;

            Thread.Sleep(1);
            this.BeginInvoke(new Action(() =>
            {
                this.lbl_Status.SetStatus(jStatus);

            }));
        }
        private void btn_SetZero_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            try
            {
                do
                {

                    var result = MessageBox.Show("确认设立 此位置 设为原点?", "提问", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No) { return; }
                    axis.SetZero(37);

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            SolveWare.Core.ShowMsg(errMsg);
        }

        private void btn_Jog_Negative_MouseDown(object sender, MouseEventArgs e)
        {
            string errMsg = string.Empty;
            SpeedSeting speed = GetMainSpeedSetting();
            try
            {
                do
                {
                 
                    if(speed == null)
                    {
                        errMsg += "请选择一个使用项";
                        break;
                    }

                    Mission_Report context = axis.Jog(false, speed, ref errMsg);
                    if (context.NotPass(true)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
        }

        private void btn_Jog_Negative_MouseUp(object sender, MouseEventArgs e)
        {
            string errMsg = string.Empty;
            try
            {
                axis.Stop();
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            //CheckErrMsg(errMsg);
        }

        private void btn_Jog_Positive_MouseDown(object sender, MouseEventArgs e)
        {
            string errMsg = string.Empty;
            SpeedSeting speed =GetMainSpeedSetting();
            try
            {
                do
                {
                   
                    if (speed == null)
                    {
                        errMsg += "请选择一个使用项";
                        break;
                    }

                    Mission_Report context = axis.Jog(true, speed, ref errMsg);
                    if (context.NotPass(true)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
        }

        private void btn_Jog_Positive_MouseUp(object sender, MouseEventArgs e)
        {
            string errMsg = string.Empty;
            try
            {
                axis.Stop();
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            //CheckErrMsg(errMsg);
        }

        private void btn_Copy_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;

            if(cmb_Selector_Copy.SelectedItem == null)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage("请选择一个复制项", true);
                return;
            }
            if(cmb_Selector_SpeedSetting.SelectedItem == null)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage("请选择一个使用项", true);
                return;
            }


            try
            {
                var copy = this.configData.MtrSpeed.SpeedSettings.FirstOrDefault(x => x.Name == cmb_Selector_Copy.SelectedItem as string);
                var main = this.configData.MtrSpeed.SpeedSettings.FirstOrDefault(x => x.Name == cmb_Selector_SpeedSetting.SelectedItem as string);

                var reslut = MessageBox.Show($"确认复制?\r\n原 MinVel {main.Min_Velocity} MaxVel {main.Max_Velocity} Acc {main.Acceleration} Dec{main.Deceleration} \r\n" +
                                                                                        $"新 MinVel {copy.Min_Velocity} MaxVel {copy.Max_Velocity} Acc {copy.Acceleration} Dec{copy.Deceleration}", "提问", MessageBoxButtons.YesNo);
                if(reslut == DialogResult.No) { return; }


                main.Min_Velocity = copy.Min_Velocity;
                main.Max_Velocity = copy.Max_Velocity;
                main.Acceleration = copy.Acceleration;
                main.Deceleration = copy.Deceleration;

                this.pGrid_Speed.SelectedObject = main;

            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            SolveWare.Core.ShowMsg(errMsg);
        }

        private void btn_Relative_Negative_Click(object sender, EventArgs e)
        {
            SpeedSeting speed = GetMainSpeedSetting();
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (speed == null)
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "请选择一个使用项");
                            break;
                        }
                        if (string.IsNullOrEmpty(txb_RelativePos.Text))
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "相对位置栏位不得为空");
                            break;
                        }

     
                        context = axis.MoveRelative(-1 * double.Parse(txb_RelativePos.Text), speed);
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

        private void btn_Relative_Positive_Click(object sender, EventArgs e)
        {
            SpeedSeting speed = GetMainSpeedSetting();
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (speed == null)
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "请选择一个使用项");
                            break;
                        }
                        if (string.IsNullOrEmpty(txb_RelativePos.Text))
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "相对位置栏位不得为空");
                            break;
                        }
                        context = axis.MoveRelative(1 * double.Parse(txb_RelativePos.Text), speed);
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

        private void btn_Absolute_Click(object sender, EventArgs e)
        {
            SpeedSeting speed = GetMainSpeedSetting();
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (speed == null)
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "请选择一个使用项");
                            break;
                        }
                        if (string.IsNullOrEmpty(txb_AbsolutePos.Text))
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "绝对位置栏位不得为空");
                            break;
                        }
                        context = axis.MoveTo(double.Parse(txb_AbsolutePos.Text), speed);
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
        private SpeedSeting GetMainSpeedSetting()
        {
            string speedName = cmb_Selector_SpeedSetting.SelectedItem as string;
            if(string.IsNullOrEmpty(speedName))
            {
                return null;
            }
            var setting = this.configData.MtrSpeed.SpeedSettings.FirstOrDefault(x => x.Name == speedName);
            return setting;
        }

        private void Form_Axis_Configuration_Item_MtrSpeed_Load(object sender, EventArgs e)
        {
            DataBinding();
            lbl_ErrorReport.Visible = false;

            if (axis != null)
                this.ckb_Is_Jog_Monitoring.Checked = axis.Is_Safe_Checking;
        }

        private void Form_Axis_Configuration_Item_MtrSpeed_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (source == null) return;
            source.Cancel();

        }

        private void btn_Relay_Click(object sender, EventArgs e)
        {
            SpeedSeting speed = GetMainSpeedSetting();
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                relayStop = false;

                try
                {
                    do
                    {
                        if (string.IsNullOrEmpty(txb_RelayCount.Text))
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, "来回次数栏位不得为空");
                            break;
                        }
                        if (string.IsNullOrEmpty(txb_RelayGap.Text))
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, "距离设定栏位不得零");
                            break;
                        }

                        int count = int.Parse(txb_RelayCount.Text);
                        double pitch = double.Parse(txb_RelayGap.Text);

                        double fromPos = axis.Get_CurUnitPos();
                        double toPos = axis.Get_CurUnitPos() + pitch;
                        Stopwatch sw = Stopwatch.StartNew();
                        for (int i = 0; i < count; i++)
                        {
                            sw.Restart();
                            context = axis.MoveTo(toPos);
                            if (context.NotPass(true)) break;
                            if (relayStop) break;

                            Thread.Sleep(5);

                            context = axis.MoveTo(fromPos);
                            if (context.NotPass(true)) break;
                            if (relayStop) break;

                            Thread.Sleep(1);
                        }

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });
        }

        volatile bool relayStop = false;
      

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            relayStop = true;
            this.axis.Stop();
        }

        private void btn_Disable_Servo_Click(object sender, EventArgs e)
        {
            this.axis.Set_Servo(false);
        }

        private void btn_Enable_Servo_Click(object sender, EventArgs e)
        {
            this.axis.Set_Servo(true);
        }

        private void ckb_Is_Jog_Monitoring_CheckedChanged(object sender, EventArgs e)
        {
            if(axis != null)
                axis.Is_Safe_Checking = (sender as CheckBox).Checked;
        }
    }
}
