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
        VisionControllerBase hController;
        CameraBase camera;
        public UserHWControl()
        {
            InitializeComponent();
        }

        public void Setup<TObj>(TObj camera)
        {
            this.camera = camera as CameraBase;
            this.hController = new VisionController(this.hWindowControl1, this.camera);

            this.hController.PropertyChanged -= HController_PropertyChanged;
            this.hController.PropertyChanged += HController_PropertyChanged;

            this.hController.CameraBase.PropertyChanged -= Camera_PropertyChanged;
            this.hController.CameraBase.PropertyChanged += Camera_PropertyChanged;
        }

        private void HController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(hController.Location) || e.PropertyName == nameof(hController.PointGray))
            {
                this.Invoke(new Action(() =>
                {
                    this.tssl_Location.Text = hController.Location;
                    this.tssl_GrayValue.Text = hController.PointGray;
                }));
            }
        }

        private void Camera_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(this.camera.CameraGrabCapabilityInfo))
            {
                this.Invoke(new Action(() =>
                {
                    this.tssl_CameraCapabilityInfo.Text = this.camera.CameraGrabCapabilityInfo;
                }));
            }
        }

        private void tsb_Play_Click(object sender, EventArgs e)
        {
            this.hController.StartLive(100);
        }

        private void tsb_Stop_Click(object sender, EventArgs e)
        {
            this.hController.StopLive(100);
        }

        private void tsb_OpenImage_Click(object sender, EventArgs e)
        {
            this.hController.Open_File_To_Get_Image();
        }

        private void tsb_Savemage_Click(object sender, EventArgs e)
        {

        }

        private void tsb_AutoImage_Click(object sender, EventArgs e)
        {
            this.hController.Fit_Image();
        }

        private void tsb_IsShowCrros_Click(object sender, EventArgs e)
        {
            if (this.hController.IsShowCrossLine)
                this.hController.ClearCrossLine();
            else
                this.hController.GenerateCrossLine();
        }

        private void tsb_DrawCircle_Click(object sender, EventArgs e)
        {
            this.hController.AddROI(new ROI_Circle());
        }

        private void tsb_DrawRectangle_Click(object sender, EventArgs e)
        {
            this.hController.AddROI(new ROI_Rectangle());
        }

        private void tsb_DrawLines_Click(object sender, EventArgs e)
        {
            this.hController.AddROI(new ROI_Line());
        }

        private void tsb_ClearHwindow_Click(object sender, EventArgs e)
        {
            this.hController.ClearROIs();
        }
    }
}
