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

    
            this.lbl_Status.SetStatus(JobStatus.Unknown);
            
            Fillup_Combobox_SpeedSetting();
            //DataBinding();

            this.cmb_Selector_SpeedSetting.SelectionChangeCommitted -= Cmb_Selector_SpeedSetting_SelectionChangeCommitted;
            this.cmb_Selector_SpeedSetting.SelectionChangeCommitted += Cmb_Selector_SpeedSetting_SelectionChangeCommitted;
        }

        CancellationTokenSource source = null;
        AutoResetEvent stopFlag = new AutoResetEvent(false);
        private void DataBinding()
        {
           source = new CancellationTokenSource();
            Task task = new Task(() =>
            {
                while(true)
                {
                    if (this.lbl_TestPos.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            lbl_TestPos.Text = $"{this.axis.CurrentPhysicalPos}";
                        }));
                    }
                    if(this.lbl_TimeSpent.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            lbl_TimeSpent.Text = $"{this.axis.TimeSpent}";
                        }));
                    }
                    
                    if (source.IsCancellationRequested)
                    {
                        break;
                    }
                    Thread.Sleep(10);

                }
                stopFlag.Set();

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
            SolveWare.Core.MMgr.DoButtonClickTask(() =>
            {
                try
                {
                    if(setting == null)
                    {
                        return "请选择一个使用项";
                    }

                    string errMsg = string.Empty;
                    int errorCode = axis.HomeMove(setting) ? ErrorCodes.NoError : ErrorCodes.MotorHomingError;
                    SetStatus(errorCode);
                    return errMsg = errorCode == ErrorCodes.NoError ? string.Empty : ErrorCodes.GetErrorDescription(errorCode); 
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            });
        }

        private void btn_SetZero_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("确认设立 此位置 设为原点?", "提问", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return; }
                axis.SetZero();
            }
            catch (Exception ex)
            {
                SetStatus(ErrorCodes.MotorHomingError);
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Jog_Negative_MouseDown(object sender, MouseEventArgs e)
        {
            SpeedSeting speed = GetMainSpeedSetting();
            try
            {
                if (speed == null)
                {
                    SolveWare.Core.MMgr.Infohandler.LogMessage("请选择一个使用项", true, true); return;
                }
                this.axis.Jog(false, speed);
            }
            catch (Exception ex)
            {
                SetStatus(ErrorCodes.MotorMoveError);
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Jog_Negative_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                this.axis.Stop();
            }
            catch (Exception ex)
            {
                SetStatus(ErrorCodes.ActionFailed);
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Jog_Positive_MouseDown(object sender, MouseEventArgs e)
        {
            SpeedSeting speed =GetMainSpeedSetting();
            try
            {
                if (speed == null)
                {
                    SolveWare.Core.MMgr.Infohandler.LogMessage("请选择一个使用项", true, true); return;
                }
                this.axis.Jog(true, speed);
            }
            catch (Exception ex)
            {
                SetStatus(ErrorCodes.MotorMoveError);
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Jog_Positive_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                this.axis.Stop();
            }
            catch (Exception ex)
            {
                SetStatus(ErrorCodes.ActionFailed); 
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Copy_Click(object sender, EventArgs e)
        {
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
                SetStatus(ErrorCodes.ActionFailed);
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Relative_Negative_Click(object sender, EventArgs e)
        {
            SpeedSeting speed = GetMainSpeedSetting();
            SolveWare.Core.MMgr.DoButtonClickTask(() =>
            {
                int errorCode = ErrorCodes.NoError;
                if(string.IsNullOrEmpty(txb_RelativePos.Text))
                {
                    return "相对位置栏位不得为空";
                }
                if (speed == null)
                {
                    return "请选择一个使用项";
                }
                string errMsg = string.Empty;
                errorCode = axis.MoveRelative( -1 * double.Parse(txb_RelativePos.Text), speed) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                SetStatus(errorCode);
                return errMsg = errorCode == ErrorCodes.NoError ? string.Empty : ErrorCodes.GetErrorDescription(errorCode);
            });
        }

        private void btn_Relative_Positive_Click(object sender, EventArgs e)
        {
            SpeedSeting speed  = GetMainSpeedSetting();
            SolveWare.Core.MMgr.DoButtonClickTask(() =>
            {
                int errorCode = ErrorCodes.NoError;
                if (string.IsNullOrEmpty(txb_RelativePos.Text))
                {
                    return "相对位置栏位不得为空";
                }
                if (speed == null)
                {
                    return "请选择一个使用项";
                }
                string errMsg = string.Empty;
                errorCode = axis.MoveRelative(1 * double.Parse(txb_RelativePos.Text), speed) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                SetStatus (errorCode);  
                return errMsg = errorCode == ErrorCodes.NoError ? string.Empty: ErrorCodes.GetErrorDescription(errorCode);
            });
        }

        private void btn_Absolute_Click(object sender, EventArgs e)
        {
            SpeedSeting speed = GetMainSpeedSetting();
            SolveWare.Core.MMgr.DoButtonClickTask(() =>
            {
                int errorCode = ErrorCodes.NoError;
                if (string.IsNullOrEmpty(txb_AbsolutePos.Text))
                {
                    return "相对位置栏位不得为空";
                }
                if(speed == null)
                {
                    return "请选择一个使用项";
                }
                string errMsg = string.Empty;
                errorCode = axis.MoveTo(double.Parse(txb_AbsolutePos.Text), speed) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;               
                SetStatus(errorCode);
                return errMsg = errorCode == ErrorCodes.NoError ? string.Empty: ErrorCodes.GetErrorDescription(errorCode);
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
        }

        private void Form_Axis_Configuration_Item_MtrSpeed_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (source == null) return;
            source.Cancel();

            stopFlag.WaitOne(100);
            source = null;
        }

        private void btn_Relay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_RelayCount.Text))
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage("来回次数栏位不得为空", true, true);
                return;
            }
            if(string.IsNullOrEmpty (txb_RelayGap.Text))
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage("距离设定栏位不得零", true, true);
                return;
            }
            
            int count = int.Parse(txb_RelayCount.Text);
            double pitch = double.Parse(txb_RelayGap.Text);

            SolveWare.Core.MMgr.DoButtonClickTask(() =>
            {
                int errorCode = ErrorCodes.NoError;
                string errMsg = string.Empty;
                double fromPos = axis.Get_CurUnitPos() ;
                double toPos = axis.Get_CurUnitPos() + pitch;

                for (int i = 0; i < count; i++)
                {                    
                    errorCode = axis.MoveTo(toPos) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                    if (errorCode != ErrorCodes.NoError)
                    {
                        errMsg = ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    Thread.Sleep(5);

                    errorCode = axis.MoveTo(fromPos) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                    if (errorCode != ErrorCodes.NoError)
                    {
                        errMsg = ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    Thread.Sleep(1);
                    this.BeginInvoke(new Action(() =>
                    {
                        this.lbl_RelayCount.Text = $"{i + 1}";
                    }));
                }

                return errMsg;
            });
        }
        private void SetStatus(int errorCode)
        {
            JobStatus status = errorCode != ErrorCodes.NoError ? JobStatus.Fail : JobStatus.Done;
            Thread.Sleep(1);
            this.BeginInvoke(new Action(() =>
            {
                this.lbl_Status.SetStatus(status);
            }));
        }
    }
}
