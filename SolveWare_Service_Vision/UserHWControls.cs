using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Vision.Controller.Base.Abstract;
using SolveWare_Service_Vision.Controller.Base.Interface;
using SolveWare_Service_Vision.ROIs.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolveWare_Service_Vision
{
    public partial class UserHWControl : UserControl, IView
    {
        public VisionControllerBase Controller { get; private set; }
        public CameraMediaBase Camera { get; private set; }
        public UserHWControl()
        {
            InitializeComponent();
            tssl_CameraCapabilityInfo.Text = "停止拍摄 00 fps";
        }

        public void Setup<TObj>(TObj camera)
        {
            this.Camera = camera as CameraMediaBase;
            this.Controller = new VisionController();
            this.Controller.Setup(this.hWindowControl1, this.Camera);

            this.hWindowControl1.HMouseMove -= HWindowControl1_HMouseMove;
            this.hWindowControl1.HMouseMove += HWindowControl1_HMouseMove;

            this.Camera.PropertyChanged -= Camera_PropertyChanged;
            this.Camera.PropertyChanged += Camera_PropertyChanged;
        }

        private void HWindowControl1_HMouseMove(object sender, HalconDotNet.HMouseEventArgs e)
        {
            if(Controller == null) return;
            tssl_GrayValue.Text = $"{Controller.PointGray}";
            tssl_Location.Text = $"{Controller.Location}";
        }

        private void HController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
          
        }

        private void Camera_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(this.Camera.GrabTime_ms))
            {
                this.tssl_CameraCapabilityInfo.Text = $"实时拍摄 {Camera.GrabTime_ms} fps ";
            }
        }

        private void tsb_Play_Click(object sender, EventArgs e)
        {
            try
            {
                if (Controller == null) return;
                this.Controller.StartLive(100);
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void tsb_Stop_Click(object sender, EventArgs e)
        {
            try
            {

                if (Controller == null) return;
                this.Controller.StopLive();

                tssl_CameraCapabilityInfo.Text = "停止拍摄 00 fps";
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void tsb_OpenImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Controller == null) return;
                this.Controller.Open_File_To_Get_Image();
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void tsb_Savemage_Click(object sender, EventArgs e)
        {

        }

        private void tsb_AutoImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Controller == null) return;
                this.Controller.Fit_Image();
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void tsb_IsShowCrros_Click(object sender, EventArgs e)
        {
            try
            {
                if (Controller == null) return;
                if (this.Controller.IsShowCrossLine)
                    this.Controller.ClearCrossLine();
                else
                    this.Controller.GenerateCrossLine();
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void tsb_DrawCircle_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Controller == null) return;
                this.Controller.AddROI(new ROI_Circle());
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void tsb_DrawRectangle_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Controller == null) return;
                this.Controller.AddROI(new ROI_Rectangle());
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void tsb_DrawLines_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Controller == null) return; 
                this.Controller.AddROI(new ROI_Line());
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }        
        }

        private void tsb_ClearHwindow_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Controller == null) return;
                this.Controller.ClearROIs();
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }         
        }

        private void tsb_GrabOne_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Controller == null) return;
                this.Controller.GrabOneShot();
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }
    }
}
