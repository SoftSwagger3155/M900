using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Motor.Base.Abstract;
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
    public partial class Form_Axis_Simple_Controller_Horizontal : Form, IView
    {
        public Form_Axis_Simple_Controller_Horizontal()
        {
            InitializeComponent();
        }

        AxisBase mtr;
        public void Setup<TObj>(TObj obj)
        {
            mtr = obj as AxisBase;
            
            this.lbl_Motor_Name.Text = mtr.Name;
            this.lbl_CurrentPhysicalPos.Text = $"当前位置: {mtr.CurrentPhysicalPos} mm";
            this.ckb_Servo_Switch.Checked = mtr.IsServoOn;
            this.txb_AbsolutePos.Text = "0";
            this.txb_RelativePos.Text = "0";

            this.ckb_Servo_Switch.CheckedChanged -= Ckb_Servo_Switch_CheckedChanged;
            this.ckb_Servo_Switch.CheckedChanged += Ckb_Servo_Switch_CheckedChanged;
            mtr.PropertyChanged -= Mtr_PropertyChanged;
            mtr.PropertyChanged += Mtr_PropertyChanged;
        }

        private void Ckb_Servo_Switch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool onOff = (sender as CheckBox).Checked;
                mtr.Set_Servo(onOff);
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }


        #region Event
        private void Mtr_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(mtr.CurrentPhysicalPos))
            {
                this.Invoke(new Action(() =>
                {
                    this.lbl_CurrentPhysicalPos.Text = $"当前位置: {mtr.CurrentPhysicalPos} mm";
                }));
            }
            if (e.PropertyName == nameof(mtr.IsPosLimit))
            {
                this.Invoke(new Action(() =>
                {
                    this.lbl_Lmt_Positive.BackColor = mtr.IsPosLimit ? Color.IndianRed : Color.Green;
                }));
            }
            if (e.PropertyName == nameof(mtr.IsNegLimit))
            {
                this.Invoke(new Action(() =>
                {
                    this.lbl_Lmt_Negative.BackColor = mtr.IsPosLimit ? Color.IndianRed : Color.Green;
                }));
            }

        }

        private void btn_Jog_Positive_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                mtr.Jog(true);
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
                mtr.Stop();
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
                mtr.Jog(false);
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
                mtr.Stop();
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Go_Absolute_Click(object sender, EventArgs e)
        {
            try
            {
                SolveWare.Core.MMgr.DoButtonClickTask(() =>
                {
                    if (string.IsNullOrEmpty(txb_AbsolutePos.Text))
                    {
                        SolveWare.Core.MMgr.Infohandler.LogMessage("相对位置 的 格子 不得为空", true);
                        return ErrorCodes.MotionFunctionError;
                    }

                    double pos = double.Parse(txb_AbsolutePos.Text);
                    int errorCode = mtr.MoveTo(pos) ? ErrorCodes.NoError : ErrorCodes.MotionFunctionError;

                    return errorCode;
                });
                
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Go_Relative_Positive_Click(object sender, EventArgs e)
        {
            try
            {
                SolveWare.Core.MMgr.DoButtonClickTask(() =>
                {
                    if (string.IsNullOrEmpty(txb_RelativePos.Text))
                    {
                        SolveWare.Core.MMgr.Infohandler.LogMessage("绝对位置 的 格子 不得为空", true);
                        return ErrorCodes.MotionFunctionError;
                    }

                    double pos = mtr.CurrentPhysicalPos + double.Parse(txb_AbsolutePos.Text);
                    int errorCode = mtr.MoveTo(pos) ? ErrorCodes.NoError : ErrorCodes.MotionFunctionError;

                    return errorCode;
                });

            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void btn_Go_Relative_Negative_Click(object sender, EventArgs e)
        {
            try
            {
                SolveWare.Core.MMgr.DoButtonClickTask(() =>
                {
                    if (string.IsNullOrEmpty(txb_RelativePos.Text))
                    {
                        SolveWare.Core.MMgr.Infohandler.LogMessage("相对位置 的 格子 不得为空", true);
                        return ErrorCodes.MotionFunctionError;
                    }

                    double pos = mtr.CurrentPhysicalPos - double.Parse(txb_AbsolutePos.Text);
                    int errorCode = mtr.MoveTo(pos) ? ErrorCodes.NoError : ErrorCodes.MotionFunctionError;

                    return errorCode;
                });

            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }
        #endregion
    }
}
