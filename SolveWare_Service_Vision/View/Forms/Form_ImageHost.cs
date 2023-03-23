﻿using HalconDotNet;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.ROIs.Base.Abstract;
using SolveWare_Service_Vision.ROIs.Base.Interface;
using SolveWare_Service_Vision.ROIs.Business;
using SolveWare_Service_Vision.ROIs.Manage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SolveWare_Service_Vision.View.Forms
{
    public partial class Form_ImageHost : Form, IView
    {
        Manage_HWindow_Controller mmgr = null;
        CameraBase camera;

        public Form_ImageHost()
        {
            InitializeComponent();
            
        }

        public void Setup<TObj>(TObj obj)
        {
            this.camera = obj as CameraBase;
            mmgr = new Manage_HWindow_Controller(this.hWindowControl1, camera);
            this.camera.SetWindowHost(this.hWindowControl1.HalconWindow);
            Label lab = new Label();

            this.camera.PropertyChanged -= Camera_PropertyChanged;
            this.camera.PropertyChanged += Camera_PropertyChanged;
        }

        private void Camera_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(camera.CameraGrabCapabilityInfo))
            {
                this.Invoke(new Action(() =>
                {
                    this.tssl_CameraCapabilityInfo.Text = camera.CameraGrabCapabilityInfo;
                }) );
            }
        }

        private void ViewPort_HMouseMove(object sender, HMouseEventArgs e)
        {
            
        }

        private void tsb_AutoImage_Click(object sender, EventArgs e)
        {

        }

        private void tsb_OpenImage_Click(object sender, EventArgs e)
        {
            this.mmgr.Open_File_To_Get_Image();
        }

<<<<<<< HEAD
        bool isShowCross = false;
=======
        private void tsb_DrawCircle_Click(object sender, EventArgs e)
        {
            mmgr.AddROI(new ROI_Circle());
        }

        private void tsb_DrawRectangle_Click(object sender, EventArgs e)
        {
            mmgr.AddROI(new ROI_Rectangle());
        }

>>>>>>> f13614b6d60c63a69de90ab4ffdfbf48a575aeac
        private void tsb_IsShowCrros_Click(object sender, EventArgs e)
        {
            if (mmgr.IsShowCross)
                mmgr.ClearCrossLine();
            else
                mmgr.GenerateCrossLine();
        }

        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {
            //this.tssl_Location.Text = mmgr.Location;
            //this.tssl_GrayValue.Text = mmgr.PointGrey;
        }

        private void tsb_AutoImage_Click_1(object sender, EventArgs e)
        {
            mmgr.Fit_Image();
        }

<<<<<<< HEAD
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

=======
        private void tsb_Play_Click(object sender, EventArgs e)
        {
            this.mmgr.StartLive();
        }

        private void tsb_Stop_Click(object sender, EventArgs e)
        {
            this.mmgr.StopLive();
        }

        private void tsb_GrabOneImage_Click(object sender, EventArgs e)
        {
            
        }

        private void tsb_DrawLines_Click(object sender, EventArgs e)
        {
            this.mmgr.AddROI(new ROI_Line());
        }

        private void tsb_ClearHwindow_Click(object sender, EventArgs e)
        {
            this.mmgr.ClearROIs();
        }



>>>>>>> f13614b6d60c63a69de90ab4ffdfbf48a575aeac
        ////变量
        //CameraBase camera;
        //HWndCtrl _viewCtrl;
        //ROIController _roiCtrl;
        //HImage hImage;

        //public Form_ImageHost()
        //{
        //    InitializeComponent();
        //}

        //public void Setup<TObj>(TObj obj)
        //{
        //    this.camera = obj as CameraBase;
        //    _viewCtrl = new HWndCtrl(this.hWindowControl1);
        //    _roiCtrl = new ROIController();
        //    this.camera.SetWindowHost(this.hWindowControl1.HalconWindow);
        //    _viewCtrl.useROIController(_roiCtrl);
        //    _roiCtrl.viewController.viewPort.HMouseMove += ViewPort_HMouseMove; ;
        //}

        //private void ViewPort_HMouseMove(object sender, HMouseEventArgs e)
        //{
        //    var roi = _roiCtrl.getActiveROI();
        //    if (roi == null) return;

        //    for (var i = 0; i < roi.getModelDataName().Length; i++)
        //    {

        //        Debug.WriteLine($"{roi.getModelDataName()[i].S} : {roi.getModelData()[i].D}");
        //    }
        //}

        //private void tsb_AutoImage_Click(object sender, EventArgs e)
        //{
        //    if(camera == null)
        //    {
        //        SolveWare.Core.MMgr.Infohandler.LogMessage("无相机物件", true, true);
        //        return;
        //    }
        //    camera.StartLive(50);
        //}

        //private void tsb_OpenImage_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog dialog = new OpenFileDialog();
        //    dialog.RestoreDirectory = true;
        //    dialog.Multiselect = false;
        //    dialog.Filter = "图片|*.*";
        //    dialog.Title = "选择一张图片";
        //    DialogResult result = dialog.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        hImage = new HImage(dialog.FileName);
        //        _viewCtrl.addIconicVar(hImage);
        //        _viewCtrl.resetWindow();
        //        _viewCtrl.repaint();
        //        _viewCtrl.setViewState(HWndCtrl.MODE_VIEW_ZOOM_Wheel);
        //    }
        //}

        //private void tsb_DrawCircle_Click(object sender, EventArgs e)
        //{
        //    ROIBase roi = new ROI_Circle();
        //    _roiCtrl.setROIShape(roi);
        //}

        //private void tsb_DrawRectangle_Click(object sender, EventArgs e)
        //{
        //    ROIBase roi = new ROI_Rectangle();
        //    _roiCtrl.setROIShape(roi);
        //}

        //bool isShowCross = false;
        //private void tsb_IsShowCrros_Click(object sender, EventArgs e)
        //{
        //    if(!isShowCross)
        //    {
        //        _roiCtrl.GenerateCrossLine(this.hWindowControl1.ImagePart.Width / 2, this.hWindowControl1.ImagePart.Height / 2);
        //        isShowCross = true;
        //    }
        //    else
        //    {
        //        isShowCross = false;
        //        _roiCtrl.ClearCrossLine();
        //    }
        //}
    }
}
