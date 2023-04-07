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

            this.tssl_CurPos.Text = $" 位置 {axis.CurrentPhysicalPos} mm";
            this.tssl_Status.SetStatus(JobStatus.Unknown);
            
            Fillup_Combobox_SpeedSetting();
            this.cmb_Selector_SpeedSetting.SelectionChangeCommitted -= Cmb_Selector_SpeedSetting_SelectionChangeCommitted;
            this.cmb_Selector_SpeedSetting.SelectionChangeCommitted += Cmb_Selector_SpeedSetting_SelectionChangeCommitted;
            axis.PropertyChanged -= Axis_PropertyChanged;
            axis.PropertyChanged += Axis_PropertyChanged;
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

        private void Axis_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(axis.CurrentPhysicalPos))
            {
                this.Invoke(new Action(() =>
                {
                    this.tssl_CurPos.Text = $" 位置 {axis.CurrentPhysicalPos} mm";
                }));
            }
        }

        private void Fillup_Combobox_SpeedSetting()
        {
            if(this.axis == null) return;
          
            this.cmb_Selector_SpeedSetting.Items.Clear();
            axis.ConfigData.MtrSpeed.SpeedSettings.ForEach(s => { this.cmb_Selector_SpeedSetting.Items.Add(s.Name); });
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickTask(() =>
            {
                try
                {
                    int errorCode = axis.HomeMove() ? ErrorCodes.NoError : ErrorCodes.MotorHomingError;
                    return errorCode;
                }
                catch (Exception ex)
                {
                    SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message);
                    return ErrorCodes.MotorHomingError;
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
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Jog_Negative_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
            
                this.axis.Jog(false, GetMainSpeedSetting());
            }
            catch (Exception ex)
            {
                tssl_Status.SetStatus(JobStatus.Fail);
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
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Jog_Positive_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.axis.Jog(true, GetMainSpeedSetting());
            }
            catch (Exception ex)
            {
                tssl_Status.SetStatus(JobStatus.Fail);
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
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Relative_Negative_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickTask(() =>
            {
                int errorCode = ErrorCodes.NoError;
                if(string.IsNullOrEmpty(txb_RelativePos.Text))
                {
                    SolveWare.Core.MMgr.Infohandler.LogMessage("相对位置栏位不得为空", true);
                    return 0;
                }

                errorCode = axis.MoveRelative( -1 * double.Parse(txb_RelativePos.Text), GetMainSpeedSetting()) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;

                return errorCode;
            });
        }

        private void btn_Relative_Positive_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickTask(() =>
            {
                int errorCode = ErrorCodes.NoError;
                if (string.IsNullOrEmpty(txb_RelativePos.Text))
                {
                    SolveWare.Core.MMgr.Infohandler.LogMessage("相对位置栏位不得为空", true);
                    return 0;
                }

                errorCode = axis.MoveRelative(1 * double.Parse(txb_RelativePos.Text), GetMainSpeedSetting()) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;

                return errorCode;
            });
        }

        private void btn_Absolute_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickTask(() =>
            {
                int errorCode = ErrorCodes.NoError;
                if (string.IsNullOrEmpty(txb_AbsolutePos.Text))
                {
                    SolveWare.Core.MMgr.Infohandler.LogMessage("相对位置栏位不得为空", true);
                    return 0;
                }

                errorCode = axis.MoveTo(double.Parse(txb_AbsolutePos.Text), GetMainSpeedSetting()) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;

                return errorCode;
            });
        }
        private SpeedSeting GetMainSpeedSetting()
        {
            var setting = this.configData.MtrSpeed.SpeedSettings.FirstOrDefault(x => x.Name == (cmb_Selector_SpeedSetting.SelectedItem as string));
            return setting;
        }

      
    }
}
