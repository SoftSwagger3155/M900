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
            if(dataKit == null) { return; }
            this.dataKit = obj as Data_Inspection;
            camera = dataKit.CameraName.GetCamera();

            MakeTrackBar();            
        }

        private void MakeTrackBar()
        {
            lbl_Exposure_Value.Text = $"数值: {dataKit.JobSheet_Brightness_Data.ExposureTime}";
            lbl_Gain_Value.Text = $"数值: {dataKit.JobSheet_Brightness_Data.Gain}";

            tBar_Gain.Minimum = camera.Minimum_Gain;
            tBar_Gain.Maximum = camera.Maximum_Gain;
            tBar_Exposure.Minimum = camera.Minimum_ExposureTime;
            tBar_Exposure.Maximum = camera.Minimum_ExposureTime;
                        
            tBar_Gain.TabIndexChanged -= TBar_Gain_TabIndexChanged;
            tBar_Gain.TabIndexChanged += TBar_Gain_TabIndexChanged;
            tBar_Exposure.TabIndexChanged -= TBar_Exposure_TabIndexChanged;
            tBar_Exposure.TabIndexChanged += TBar_Exposure_TabIndexChanged;
        }

        private void TBar_Gain_TabIndexChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.lbl_Gain_Value.Text = $"数值: {(sender as TrackBar).Value.ToString()}";
            }));
        }

        private void TBar_Exposure_TabIndexChanged(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.lbl_Exposure_Value.Text = $"数值: {(sender as TrackBar).Value.ToString()}";
            }));
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
    }
}
