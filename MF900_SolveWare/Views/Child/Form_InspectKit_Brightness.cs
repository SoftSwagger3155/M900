using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_InspectKit_Brightness : Form, IView
    {
        public Form_InspectKit_Brightness()
        {
            InitializeComponent();
        }

        Data_Inspection dataKit;
        CameraMediaBase camera;


        public void Setup<TObj>(TObj obj)
        {
            this.dataKit = obj as Data_Inspection;
            camera = dataKit.CameraName.GetCamera();

            MakeTrackBar();            
        }

        private void MakeTrackBar()
        {
           
            lbl_Gain_Minimum.Text = $"最小值 {camera.Minimum_Gain}";
            lbl_Gain_Maximum.Text = $"最大值 {camera.Maximum_Gain}";
            lbl_Exposure_Minimum.Text = $"最小值 {camera.Minimum_ExposureTime}";
            lbl_Exposure_Maximum.Text = $"最大值 {camera.Maximum_ExposureTime}";
            lbl_Current_Gain.Text = $"增益 : {dataKit.JobSheet_Brightness_Data.Gain}";
            lbl_Current_Exposure.Text = $"曝光 : {dataKit.JobSheet_Brightness_Data.ExposureTime}";

            tBar_Gain.Minimum = camera.Minimum_Gain;
            tBar_Gain.Maximum = camera.Maximum_Gain;
            tBar_Exposure.Minimum = camera.Minimum_ExposureTime;
            tBar_Exposure.Maximum = camera.Maximum_ExposureTime;

            tBar_Gain.Value = dataKit.JobSheet_Brightness_Data.Gain;
            tBar_Exposure.Value = dataKit.JobSheet_Brightness_Data.ExposureTime;

        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show($"是否确认更改数值\r\n原增益 {dataKit.JobSheet_Brightness_Data.Gain} 原曝光 {dataKit.JobSheet_Brightness_Data.ExposureTime} \r\n 新增益 {tBar_Gain.Value} 新曝光 {tBar_Exposure.Value}", "提问", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return; }



                dataKit.JobSheet_Brightness_Data.Gain = tBar_Gain.Value;
                dataKit.JobSheet_Brightness_Data.ExposureTime = tBar_Exposure.Value;
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage(ex.Message, true);
            }
        }

        private void tBar_Exposure_ValueChanged(object sender, EventArgs e)
        {
            this.lbl_Exposure_Value.Text = $"{(sender as TrackBar).Value.ToString()}";
        }

        private void tBar_Gain_ValueChanged(object sender, EventArgs e)
        {
            this.lbl_Gain_Value.Text = $"{(sender as TrackBar).Value.ToString()}";
        }

        

        private void btn_Set_Gain_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txb_Gain_Value.Text))
                {
                    SolveWare.Core.ShowMsg("增益设定数值栏位不得为空");
                    return;
                }

                int gain = int.Parse(txb_Gain_Value.Text);
                if(gain< camera.Minimum_Gain || gain> camera.Maximum_Gain)
                {
                    SolveWare.Core.ShowMsg($"增益数值 只接受 {camera.Minimum_Gain} 至 {camera.Maximum_Gain} 之间");
                    return;
                }

                
                this.tBar_Gain.Value = gain;


            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void btn_Set_Exposure_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txb_Exposure_Value.Text))
                {
                    SolveWare.Core.ShowMsg("曝光设定数值栏位不得为空");
                    return;
                }

                int exposure = int.Parse(txb_Exposure_Value.Text);

                if (exposure < camera.Minimum_ExposureTime || exposure > camera.Maximum_ExposureTime)
                {
                    SolveWare.Core.ShowMsg($"曝光数值 只接受 {camera.Minimum_ExposureTime} 至 {camera.Maximum_ExposureTime} 之间");
                    return;
                }

                this.tBar_Exposure.Value = exposure;


            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                int gain = int.Parse(this.lbl_Gain_Value.Text);
                int exposure = int.Parse(this.lbl_Exposure_Value.Text);

                lbl_Current_Gain.Text = $"增益 : {gain}";
                lbl_Current_Exposure.Text = $"曝光 : {exposure}";
                
                dataKit.JobSheet_Brightness_Data.Gain = gain;
                dataKit.JobSheet_Brightness_Data.ExposureTime = exposure;  
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            try
            {
                if(camera == null)
                {
                    SolveWare.Core.ShowMsg("无相机物件");
                    return;
                }

                if (camera.IsSimulation)
                {
                    SolveWare.Core.ShowMsg("相机目前是模拟状态");
                    return;
                }

                camera.SetExposureTime(dataKit.JobSheet_Brightness_Data.ExposureTime);
                camera.SetGain(dataKit.JobSheet_Brightness_Data.Gain);
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }
    }
}
