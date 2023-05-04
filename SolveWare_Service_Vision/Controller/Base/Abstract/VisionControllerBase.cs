using Basler.Pylon;
using HalconDotNet;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using SolveWare_Service_Vision.Inspection.Business;
using SolveWare_Service_Vision.ROIs.Base.Abstract;
using SolveWare_Service_Vision.ROIs.Business;
using SolveWare_Service_Vision.ROIs.Defintions;
using SolveWare_Service_Vision.Templates;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SolveWare_Service_Vision.Controller.Base.Abstract
{
    public struct Pattern_Match_Result
    {
        public bool IsPass { get; set; }
        public string Message { get; set; }
    }
    public abstract class VisionControllerBase : ToolElementBase, IVisionController
    {
        protected HWindowControl hWinCtrol;
        protected CameraMediaBase camrea;
        protected HImage hImage;
        protected List<HObjectEntry> HObjList;
        protected List<ROIBase> ROIList;
        protected ROIBase roiMode;
        protected ROIBase cross = null;
        protected ROIBase patternROI;
        protected Mouse_Event_Mode event_Mode = Mouse_Event_Mode.None;
        protected HTuple hImageSize_Width = new HTuple();
        protected HTuple hImageSize_Height = new HTuple();
        protected HTuple pixel_Row = new HTuple();
        protected HTuple pixel_Column = new HTuple();
        protected HTuple hWinDow_Button = new HTuple();
        protected HTuple hPointGray = new HTuple();
        

        public CameraMediaBase Media
        {
            get => camrea;
        }


        protected double StartX = 0;
        protected double StartY = 0;
        protected string activeCol = "green";
        protected string activeHdlCol = "red";
        protected string inactiveCol = "blue";
        protected int activeROIidx = -1;
        protected bool isShowCrossLine = false;
        public bool IsShowCrossLine
        {
            get => isShowCrossLine;
        }

        protected string location;
        public string Location
        {
            get => location;
            set => UpdateProper(ref location, value);
        }

        protected string pointGray;
        public string PointGray
        {
            get => pointGray;
            set => UpdateProper(ref pointGray, value);
        }
        public Pattern_Match_Result PatternMatchResult
        {
            get;
            private set;
        }
       
        public Job_Inspect job_Inspect { get; private set; }
        private bool is_Show_Metrology
        {
            get
            {
                bool isShow = false;
                if(job_Inspect != null)
                {
                    isShow = this.job_Inspect.Is_Show_Metrology;
                } 

                return isShow;
            }
        }
      

        public void Setup(HWindowControl HWindow, ICameraMedia camera, IInspectionKit job)
        {
            this.hWinCtrol = HWindow;
            this.camrea = camera as CameraMediaBase;
            this.job_Inspect = job as Job_Inspect; 
            Set_Hwindow_Part();
            HObjList = new List<HObjectEntry> ();
            ROIList = new List<ROIBase> ();

            if (hWinCtrol != null) Invoke_HWindow_Related_Event();
            if (camrea != null) Invoke_Media_Related_Event();
        }

        private void Set_Hwindow_Part()
        {
            this.hWinCtrol.ImagePart = new System.Drawing.Rectangle(0, 0, this.camrea.ImagePartX, this.camrea.ImagePartY);
        }

        private void Invoke_HWindow_Related_Event()
        {
            hWinCtrol.HMouseUp -= HWinCtrol_HMouseUp;
            hWinCtrol.HMouseUp += HWinCtrol_HMouseUp;

            hWinCtrol.HMouseDown -= HWinCtrol_HMouseDown;
            hWinCtrol.HMouseDown += HWinCtrol_HMouseDown;

            hWinCtrol.HMouseMove -= HWinCtrol_HMouseMove;
            hWinCtrol.HMouseMove += HWinCtrol_HMouseMove;

            hWinCtrol.HMouseWheel -= HWinCtrol_HMouseWheel;
            hWinCtrol.HMouseWheel += HWinCtrol_HMouseWheel;
        }

        private void Invoke_Media_Related_Event()
        {
            camrea.PropertyChanged -= Media_PropertyChanged;
            camrea.PropertyChanged += Media_PropertyChanged;
        }

        private void AddEntityObject(HObject obj)
        {

           

            switch (obj)
            {
                case null:
                    return;
                case HImage image:
                    {
                        if (image.Key == IntPtr.Zero)
                        {
                            return;
                        }

                        this.hImage = image;
                        break;
                    }
            }

            
            Repaint();
        }

        /// <summary>
        /// 清除 Entities
        /// </summary>
        private void ClearEntityObject()
        {
            this.HObjList.Clear();
            this.hWinCtrol.HalconWindow.ClearWindow();
        }

        private void HWinCtrol_HMouseUp(object sender, HMouseEventArgs e)
        {
            event_Mode = Mouse_Event_Mode.Active_Image;
        }

        private void HWinCtrol_HMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                int idxROI = -1;
                double max = 10000, dist = 0;
                double epsilon = 35.0;          //maximal shortest distance to one of the handles


                if (roiMode != null)
                {
                    roiMode.createROI(pixel_Column, pixel_Row);
                    if(ROIList.Count > 0) { ROIList.Clear(); }  
                    ROIList.Add(roiMode);
                    this.patternROI = roiMode;
                    roiMode = null;
                    activeROIidx = ROIList.Count - 1;
                    Repaint_AddROI();

                    this.event_Mode = Mouse_Event_Mode.Add_ROI;
                }
                else if (ROIList.Count > 0)     // ... or an existing one is manipulated
                {
                    activeROIidx = -1;

                    for (int i = 0; i < ROIList.Count; i++)
                    {
                        dist = ((ROIBase)ROIList[i]).distToClosestHandle(pixel_Column, pixel_Row);
                        if ((dist < max) && (dist < epsilon))
                        {
                            max = dist;
                            idxROI = i;
                        }
                    }//end of for

                    if (idxROI >= 0)
                    {
                        activeROIidx = idxROI;
                        event_Mode = Mouse_Event_Mode.Active_ROI;
                    }
                    else
                    {
                        event_Mode = Mouse_Event_Mode.Active_Image;
                    }

                    Highlight_Active_ROI();
                }
                else if (e.Button == MouseButtons.Right && hImage != null)
                {
                    StartX = e.X;
                    StartY = e.Y;
                    event_Mode = Mouse_Event_Mode.Active_Image;
                }
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"Error: HWindow Mouse Down\r\n{ex.Message}", true, true);
            }
        }

        private void HWinCtrol_HMouseMove(object sender, HMouseEventArgs e)
        {
            try
            {
                UpdateCameraInfo(e);


                if (event_Mode == Mouse_Event_Mode.Active_ROI)
                {
                    this.ROIList[activeROIidx].moveByHandle(this.pixel_Column, this.pixel_Row);
                    Repaint();
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right && event_Mode == Mouse_Event_Mode.Active_Image)
                {
                    double motionX = ((e.X - StartX));
                    double motionY = ((e.Y - StartY));

                    if (((int)motionX != 0) || ((int)motionY != 0))
                    {
                        MoveImage(motionX, motionY);
                        StartX = e.X - motionX;
                        StartY = e.Y - motionY;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void HWinCtrol_HMouseWheel(object sender, HMouseEventArgs e)
        {
            if (hImage == null) return;

            HTuple zoom, row, col, button;
            HTuple row0, col0, row00, col00, ht, wt, r1, c1, r2, c2;

            if (e.Delta > 0)
            {
                zoom = 1.5;
            }
            else
            {
                zoom = 0.5;
            }

            HOperatorSet.GetMposition(this.hWinCtrol.HalconWindow, out row, out col, out button);
            HOperatorSet.GetPart(this.hWinCtrol.HalconWindow, out row0, out col0, out row00, out col00);
            ht = row00 - row0;
            wt = col00 - col0;

            if (ht * wt < 32000 * 32000 || zoom == 1.5)
            {
                r1 = (row0 + ((1 - (1.0 / zoom)) * (row - row0)));
                c1 = (col0 + ((1 - (1.0 / zoom)) * (col - col0)));
                r2 = r1 + (ht / zoom);
                c2 = c1 + (ht / zoom);
                this.hWinCtrol.HalconWindow.SetPart(r1, c1, r2, c2);
                this.hWinCtrol.HalconWindow.ClearWindow();
                this.hWinCtrol.HalconWindow.DispImage(hImage);
                if (IsShowCrossLine) GenerateCrossLine();
            }
        }
        private void UpdateCameraInfo(HMouseEventArgs e)
        {
            try
            {
                if (this.hImage == null)
                {
                    this.pixel_Row = e.Y;
                    this.pixel_Column = e.X;
                    this.Location = $"Row: {(int)e.X} Col: {(int)e.Y}";
                    this.PointGray = $"0";
                    OnPropertyChanged(nameof(Location));
                    OnPropertyChanged(nameof(pointGray));
                    return;
                }


                HOperatorSet.GetMposition(this.hWinCtrol.HalconWindow, out pixel_Row, out pixel_Column, out hWinDow_Button);
                if (hImageSize_Height != null && (pixel_Row > 0 && pixel_Row < hImageSize_Height) && (pixel_Column > 0 && pixel_Column < hImageSize_Width))//判断鼠标在图像上
                {
                    HOperatorSet.GetImageSize(this.hImage, out hImageSize_Width, out hImageSize_Height);
                    if (this.hImage != null) HOperatorSet.GetGrayval(this.hImage, pixel_Row, pixel_Column, out hPointGray);
                    double colValue = pixel_Column;
                    double rowValue = pixel_Row;
                    double widthValue = hImageSize_Width;
                    double heightValue = hImageSize_Height;
                    double factorX = this.hImageSize_Width / widthValue;
                    double factorY = this.hImageSize_Height / heightValue;

                    int posX = (int)(colValue * factorX);
                    int posY = (int)(rowValue * factorY);

                    this.Location = $"Row: {posY} Col: {posX}";
                    this.PointGray = $"{hPointGray}";
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void MoveImage(double motionX, double motionY)
        {
            image_Row_1 += -motionY;
            image_Row_2 += -motionY;

            image_Column_1 += -motionX;
            image_Column_2 += -motionX;

            System.Drawing.Rectangle rect = this.hWinCtrol.ImagePart;
            rect.X = (int)Math.Round((double)image_Column_1);
            rect.Y = (int)Math.Round((double)image_Row_1);
            hWinCtrol.ImagePart = rect;

            ROIList.Clear();
            Repaint_Movement();
        }
      
        AutoResetEvent grabOne = new AutoResetEvent(false); 
        private void Media_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.camrea.Image))
            { 
                this.hImage = this.camrea.Image;
                if(!camrea.IsOneShot) Repaint();
                else
                {
                   grabOne.Set();
                }

            }
        }

        public void AddROI(ROIBase roiMode)
        {
            string errMsg = string.Empty;
            try
            {
                do
                {
                    if (this.camrea.IsGrabing())
                    {
                        errMsg += "相机拍摄中，请关闭实时";
                        break;
                    }

                    this.roiMode = roiMode;
                    this.job_Inspect.Is_Show_Metrology = false;
                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            Get_Result(nameof(this.AddROI), errMsg);
        }

        public void ClearCrossLine()
        {
            string errMsg = string.Empty;
            
            try
            {
                this.event_Mode = Mouse_Event_Mode.Active_Image;
                isShowCrossLine = false;
                Repaint();
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            Get_Result(nameof(this.ClearCrossLine), errMsg);
        }

        public void ClearROIs()
        {
            try
            {
                if(ROIList.Count == 0) { return; }
                if (ROIList[activeROIidx] is ROI_CrossLine) return;

                ROIList.RemoveAt(activeROIidx);
                Repaint();  

            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        public int Do_Inspection(IInspectionKit kit)
        {
            return 0;
        }

        public void Fit_Image()
        {
            string errMsg = string.Empty;
            try
            {
                if (this.hImage == null) return;
                Adapt_Window(); 
                Repaint();
            }
            catch(Exception ex)
            {
                errMsg += ex.Message;
            }

            Get_Result(nameof(this.Fit_Image), errMsg); 
        }

        public void GenerateCrossLine()
        {
            cross = new ROI_CrossLine();
            double r1 = this.hWinCtrol.ImagePart.Top;
            double c1 = this.hWinCtrol.ImagePart.Left;
            double r2 = this.hWinCtrol.ImagePart.Bottom;
            double c2 = this.hWinCtrol.ImagePart.Right;

            double midX = (int)(c1 + c2) / 2;
            double midY = (int)(r1 + r2) / 2;

            cross.createROI(midY, midX);
            cross.draw(this.hWinCtrol.HalconWindow, false);
            isShowCrossLine = true;
        }

        public void GrabOneShot()
        {
            this.camrea.IsOneShot = true;
            this.camrea.GrabOneShot();

            grabOne.WaitOne(1000);
            this.camrea.IsOneShot = false;
        }

        public void Open_File_To_Get_Image()
        {
            string errMsg = string.Empty;
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.RestoreDirectory = true;
                dialog.Multiselect = false;
                dialog.Filter = "图片|*.*";
                dialog.Title = "选择一张图片";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.hWinCtrol.HalconWindow.ClearWindow();
                    this.hImage = new HImage(dialog.FileName);
                    Adapt_Window();
                    hWinCtrol.HalconWindow.DispImage(hImage);
                }
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            Get_Result(nameof(this.Open_File_To_Get_Image), errMsg);
        }

        public int StartLive(int delayTime_ms = 100)
        {
            return this.camrea.StartLive(delayTime_ms);
        }

        public int StopLive()
        {
           return this.camrea.StopLive();
        }

        public void WriteText(string msg)
        {
            
        }

        protected HTuple image_Row_1;
        protected HTuple image_Row_2;
        protected HTuple image_Column_1;
        protected HTuple image_Column_2;
        private void Adapt_Window()
        {
            //确认是否有图像
            if (this.hImage == null) return;

            //获取图像大小
            HOperatorSet.GetImageSize(hImage, out hImageSize_Width, out hImageSize_Height);

            //计算比例
            double ratioWidth = 1.0 * hImageSize_Width / this.hWinCtrol.Width;
            double ratioHeight = 1.0 * hImageSize_Height / this.hWinCtrol.Height;

            //设置窗体

            if (ratioWidth >= ratioHeight)
            {
                image_Row_1 = -1.0 * ((this.hWinCtrol.Height * ratioWidth) - hImageSize_Height) / 2;
                image_Column_1 = 0;
                image_Row_2 = image_Row_1 + this.hWinCtrol.Height * ratioWidth;
                image_Column_2 = image_Column_1 + this.hWinCtrol.Width * ratioWidth;
            }
            else
            {
                image_Row_1 = 0;
                image_Column_1 = -1.0 * ((this.hWinCtrol.Width * ratioHeight) - hImageSize_Width) / 2;
                image_Row_2 = image_Row_1 + this.hWinCtrol.Height * ratioHeight;
                image_Column_2 = image_Column_1 + this.hWinCtrol.Width * ratioHeight;
            }
            this.hWinCtrol.HalconWindow.SetPart(image_Row_1, image_Column_1, image_Row_2, image_Column_2);
            //ClearEntityObject();
            //AddEntityObject(hImage);
        }

        HTuple hRow1;
        HTuple hRow2;
        HTuple hCol1;
        HTuple hCol2;
        HTuple img_Width = new HTuple();
        HTuple img_Height = new HTuple();
        public void Adapt_Window_And_Attach(HImage img, HWindowControl ctrl)
        {
            //确认是否有图像
            if (img == null) return;
            ctrl.HalconWindow.ClearWindow();

            //获取图像大小
            HOperatorSet.GetImageSize(img, out img_Width, out img_Height);

            //计算比例
            double ratioWidth = 1.0 * img_Width / ctrl.Width;
            double ratioHeight = 1.0 * img_Height / ctrl.Height;

            //设置窗体
          

            if (ratioWidth >= ratioHeight)
            {
                hRow1 = -1.0 * ((ctrl.Height * ratioWidth) - img_Height) / 2;
                hCol1 = 0;
                hRow2 = hRow1 + ctrl.Height * ratioWidth;
                hCol2 = hCol1 + ctrl.Width * ratioWidth;
            }
            else
            {
                hRow1 = 0;
                hCol1 = -1.0 * ((ctrl.Width * ratioHeight) - img_Width) / 2;
                hRow2 = hRow1 + ctrl.Height * ratioHeight ;
                hCol2 = hCol1 + ctrl.Width * ratioHeight;
            }
            ctrl.HalconWindow.SetPart(hRow1, hCol1, hRow2, hCol2);

            ctrl.HalconWindow.DispImage(img);
            HSystem.SetSystem("flush_graphic", "true");

        }

        public void Repaint_Movement()
        {
            try
            {
                HSystem.SetSystem("flush_graphic", "false");
                this.hWinCtrol.HalconWindow.ClearWindow();

                if (hImage != null)
                {
                   
                    hWinCtrol.HalconWindow.DispImage(hImage);
                }

                if (IsShowCrossLine)
                    GenerateCrossLine();


                HSystem.SetSystem("flush_graphic", "true");

                this.hWinCtrol.HalconWindow.SetColor("black");
                this.hWinCtrol.HalconWindow.DispLine(-100.0, -100.0, -101.0, -101.0);
            }
            catch (Exception ex)
            {

            }
        }

        private void Repaint_AddROI()
        {
            try
            {
                HSystem.SetSystem("flush_graphic", "false");

             
                if (IsShowCrossLine)
                    GenerateCrossLine();

                paintData(this.hWinCtrol.HalconWindow);

                HSystem.SetSystem("flush_graphic", "true");

                this.hWinCtrol.HalconWindow.SetColor("black");
                this.hWinCtrol.HalconWindow.DispLine(-100.0, -100.0, -101.0, -101.0);
            }
            catch (Exception ex)
            {

            }
        }

        public void Repaint()
        {
            try
            {
                HSystem.SetSystem("flush_graphic", "false");
                this.hWinCtrol.HalconWindow.ClearWindow();
                //mGC.stateOfSettings.Clear();

                if(hImage != null)
                {
                    Adapt_Window();
                    hWinCtrol.HalconWindow.DispImage(hImage);
                }

                //foreach (var entry in HObjList)
                //{
                //    mGC.applyContext(this.hWinCtrol.HalconWindow, entry.gContext);
                //    this.hWinCtrol.HalconWindow.DispObj(entry.HObj);
                //}

                if (IsShowCrossLine)
                    GenerateCrossLine();

                paintData(this.hWinCtrol.HalconWindow);

                HSystem.SetSystem("flush_graphic", "true");

                this.hWinCtrol.HalconWindow.SetColor("black");
                this.hWinCtrol.HalconWindow.DispLine(-100.0, -100.0, -101.0, -101.0);
            }
            catch (Exception ex)
            {

            }
        }

        private void Repaint_FindPattern()
        {
            try
            {
                HSystem.SetSystem("flush_graphic", "false");
                this.hWinCtrol.HalconWindow.ClearWindow();
                //mGC.stateOfSettings.Clear();

                if (hImage != null)
                {
                    Adapt_Window();
                    hWinCtrol.HalconWindow.DispImage(hImage);
                }

      

                if (IsShowCrossLine)
                    GenerateCrossLine();

                HSystem.SetSystem("flush_graphic", "true");

                this.hWinCtrol.HalconWindow.SetColor("black");
                this.hWinCtrol.HalconWindow.DispLine(-100.0, -100.0, -101.0, -101.0);
            }
            catch (Exception ex)
            {

            }
        }

        private void Highlight_Active_ROI()
        {
            if (ROIList.Count > 0)
            {        
                if (event_Mode == Mouse_Event_Mode.Add_ROI || event_Mode == Mouse_Event_Mode.Active_ROI)
                {
                    this.hWinCtrol.HalconWindow.SetColor(inactiveCol);
                    this.hWinCtrol.HalconWindow.SetDraw("margin");

                    this.hWinCtrol.HalconWindow.SetColor(activeCol);
                    ((ROIBase)ROIList[activeROIidx]).draw(this.hWinCtrol.HalconWindow, this.is_Show_Metrology);

                    this.hWinCtrol.HalconWindow.SetColor(activeHdlCol);
                    ((ROIBase)ROIList[activeROIidx]).displayActive(this.hWinCtrol.HalconWindow);
                }
            }
        }

        /// <summary>
        /// 彩绘 ROI List 在 halcon 窗体
        /// </summary>  
        public void paintData(HalconDotNet.HWindow window)
        {
            window.SetDraw("margin");
            window.SetLineWidth(4);

            if (ROIList.Count > 0)
            {
                window.SetColor(inactiveCol);
                window.SetDraw("margin");

                for (int i = 0; i < ROIList.Count; i++)
                {
                    if (this.is_Show_Metrology)
                    {
                        ROIList[i].VerticalMeasureLength = job_Inspect.Data.JobSheet_PatternMatch_Data.VerticalMeasureLength;
                        ROIList[i].HorizontalMeasureLength = job_Inspect.Data.JobSheet_PatternMatch_Data.HorizontalMeasureLength;
                        ROIList[i].MeasureSigma = job_Inspect.Data.JobSheet_PatternMatch_Data.MeasureSigma;
                        ROIList[i].MeasureThreshold = job_Inspect.Data.JobSheet_PatternMatch_Data.Threshold;

                    }

                    ((ROIBase)ROIList[i]).draw(window, this.is_Show_Metrology);
                }

                if (event_Mode == Mouse_Event_Mode.Add_ROI || event_Mode == Mouse_Event_Mode.Active_ROI)
                {
                    window.SetColor(activeCol);
                    ((ROIBase)ROIList[activeROIidx]).draw(window, this.is_Show_Metrology);

                    window.SetColor(activeHdlCol);
                    ((ROIBase)ROIList[activeROIidx]).displayActive(window);
                }
            }
        }

        public Mission_Report Learn_Circle_Pattern(Data_Inspection data)
        {
            Mission_Report context = new Mission_Report();
            try
            {

                HObject ho_ImageReduced = new HObject();
                HObject ho_Circle = new HObject();
                HTuple hv_ModelID = new HTuple();
                HImage saveImage = new HImage();
                HTuple tempArea = new HTuple();
                HTuple tempRow = new HTuple();
                HTuple tempcol = new HTuple();

                ROI_Circle circle = (ROIList[activeROIidx] as ROI_Circle);
                HRegion cirRegion = new HRegion();
                cirRegion.GenCircle((HTuple)circle.MidR, (HTuple)circle.MidC, (HTuple)circle.Radius1);
                HOperatorSet.GenCircle(out ho_Circle, circle.MidR, circle.MidC, circle.Radius1);


                HOperatorSet.AreaCenter(ho_Circle, out tempArea, out tempRow, out tempcol);
                HOperatorSet.AreaCenter(this.hImage, out tempArea, out tempRow, out tempcol);
                HOperatorSet.ReduceDomain(this.hImage, ho_Circle, out ho_ImageReduced);
                HOperatorSet.AreaCenter(hImage, out tempArea, out tempRow, out tempcol);


                //Pattern Match
                HOperatorSet.AreaCenter(ho_ImageReduced, out tempArea, out tempRow, out tempcol);
                //HOperatorSet.CreateScaledShapeModel(ho_ImageReduced,
                //     data.JobSheet_PatternMatch_Data.NumLevels,
                //     (new HTuple(data.JobSheet_PatternMatch_Data.AngleStart)).TupleRad(),
                //     (new HTuple(data.JobSheet_PatternMatch_Data.AngleExtent)).TupleRad(),
                //     "auto",
                //     data.JobSheet_PatternMatch_Data.MinScale,
                //     data.JobSheet_PatternMatch_Data.MaxScale,
                //     "auto",
                //     data.JobSheet_PatternMatch_Data.Optimization,
                //     data.JobSheet_PatternMatch_Data.Metric,
                //     data.JobSheet_PatternMatch_Data.Contrast,
                //     data.JobSheet_PatternMatch_Data.MinContrast, out hv_ModelID);
                HTuple angleStart = (new HTuple(data.JobSheet_PatternMatch_Data.AngleStart)).TupleRad();
                HTuple angleExtent = (new HTuple(data.JobSheet_PatternMatch_Data.AngleExtent)).TupleRad();
                double minScale = data.JobSheet_PatternMatch_Data.MinScale;
                double maxScale = data.JobSheet_PatternMatch_Data.MaxScale;
                HOperatorSet.CreateScaledShapeModel(ho_ImageReduced, "auto", angleStart, angleExtent, "auto",
                minScale, maxScale, "auto", "auto", "use_polarity", "auto", "auto", out hv_ModelID);

                HobjectToHimage(ho_ImageReduced, ref saveImage);
                string fileName = Path.Combine(SystemPath.GetVisionPatternPath, $"{data.Name}-圆形模板.shm");
                string bitmap_FileName = Path.Combine(SystemPath.GetVisionPatternPath, $"{data.Name}-圆形模板.jpg");
                HOperatorSet.WriteShapeModel(hv_ModelID, fileName);
                HOperatorSet.WriteImage(ho_ImageReduced, "jpg", 0, bitmap_FileName);

                this.job_Inspect.Data.JobSheet_PatternMatch_Data.Radius_PatternMatch = circle.Radius1;
            }
            catch (Exception)
            {
                context.Set(ErrorCodes.LearnPatternMatchError);
            }

            return context;
        }

        public Mission_Report Learn_Pattern(Data_Inspection data)
        {
            Mission_Report context = default(Mission_Report);   
            if(this.patternROI is ROI_Circle)
            {
                context = Learn_Circle_Pattern(data);  
            }
            else
            {

            }

            return context;
        }


        public void Learn_Pattern()
        {
            HObject ho_ImageReduced = new HObject();
            HObject ho_Circle = new HObject();
            HTuple hv_ModelID = new HTuple();
            HImage saveImage = new HImage();
            HTuple tempArea = new HTuple();
            HTuple tempRow = new HTuple();
            HTuple tempcol = new HTuple();

            ROI_Circle circle = (ROIList[activeROIidx] as ROI_Circle);
            HRegion cirRegion  = new HRegion();
            cirRegion.GenCircle((HTuple)circle.MidR, (HTuple)circle.MidC, (HTuple)circle.Radius1);
            HOperatorSet.GenCircle(out ho_Circle, circle.MidR, circle.MidC, circle.Radius1);
         
            
            HOperatorSet.AreaCenter(ho_Circle, out tempArea, out tempRow, out tempcol);
            HOperatorSet.AreaCenter(this.hImage, out tempArea, out tempRow, out tempcol);
            HOperatorSet.ReduceDomain(this.hImage, ho_Circle, out ho_ImageReduced);
            HOperatorSet.AreaCenter(hImage, out tempArea, out tempRow, out tempcol);

            HOperatorSet.AreaCenter(ho_ImageReduced, out tempArea, out tempRow, out tempcol);
            HOperatorSet.CreateScaledShapeModel(ho_ImageReduced, "auto", -0.39, 0.79, "auto",
                0.5, 1.1, "auto", "auto", "use_polarity", "auto", "auto", out hv_ModelID);

            HobjectToHimage(ho_ImageReduced, ref saveImage);
            string fileName = Path.Combine(SystemPath.GetVisionPatternPath, "圆形模板.shm");
            string bitmap_FileName = Path.Combine(SystemPath.GetVisionPatternPath, "圆形模板.jpg");
            HOperatorSet.WriteShapeModel(hv_ModelID, fileName);
            HOperatorSet.WriteImage(ho_ImageReduced, "jpg", 0, bitmap_FileName);
        }
        public Mission_Report Find_Pattern()
        {
            Mission_Report context = new Mission_Report();
            try
            {
                HTuple hv_ModelID = new HTuple();
                string fileName = Path.Combine(SystemPath.GetVisionPatternPath, "圆形模板.shm");
                HOperatorSet.ReadShapeModel(fileName, out hv_ModelID);

                new Template_Scaled_PatternMatch().Execute(this.hWinCtrol.HalconWindow, hv_ModelID, this.hImage);
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }

            return context;
        }
        public Mission_Report Find_Pattern(Data_Inspection data)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    if (!camrea.IsSimulation)
                    {

                        StopLive();
                        GrabOneShot();
                    }

                    Repaint_FindPattern();

                    if(this.hImage == null)
                    {
                        context.Set(ErrorCodes.PatternMatchFailed, "无图像");
                        break;
                    }

                    HTuple hv_Row1 = new HTuple();
                    HTuple hv_Column1 = new HTuple();
                    HTuple hv_Angle = new HTuple();
                    HTuple hv_Scale = new HTuple();
                    HTuple hv_Score = new HTuple();
                    HTuple hv_ModelID = new HTuple();

                    string fileName = Path.Combine(SystemPath.GetVisionPatternPath, $"{data.Name}-圆形模板.shm");
                    HOperatorSet.ReadShapeModel(fileName, out hv_ModelID);

                    #region Pattern Match
                    HTuple angleStart = (new HTuple(data.JobSheet_PatternMatch_Data.AngleStart)).TupleRad();
                    HTuple angleExtent = (new HTuple(data.JobSheet_PatternMatch_Data.AngleExtent)).TupleRad();
                    double minScale = data.JobSheet_PatternMatch_Data.MinScale;
                    double maxScale = data.JobSheet_PatternMatch_Data.MaxScale;
                    double minScore = 0.5;
                    int numMatches = data.JobSheet_PatternMatch_Data.NumMatches;
                    double maxOverlap = data.JobSheet_PatternMatch_Data.MaxOverLap;
                    string subPixel = data.JobSheet_PatternMatch_Data.SubPixel;
                    int numLevels = data.JobSheet_PatternMatch_Data.NumLevels;
                    double greediness = data.JobSheet_PatternMatch_Data.Greediness;

                    HOperatorSet.FindScaledShapeModel(this.hImage, hv_ModelID,
                       angleStart, angleExtent, minScale, maxScale, minScore, numMatches, maxOverlap, subPixel, numLevels, greediness,
                       out hv_Row1, out hv_Column1,
                       out hv_Angle, out hv_Scale, out hv_Score);
                    #endregion

                    if (hv_Score.Length == 0)
                    {
                        context.Set(ErrorCodes.PatternMatchFailed);
                        if (context.NotPass()) break;
                    }

                    #region Blob
                    HTuple metrologyHandle = new HTuple();
                    HTuple index = new HTuple();
                    HTuple objectRow = new HTuple();
                    HTuple objectColumn = new HTuple();
                    HObject contour = new HObject();
                    HObject contours = new HObject();
                    HTuple hv_Row2 = new HTuple();
                    HTuple hv_Column2 = new HTuple();
                    HTuple hv_Radius1 = new HTuple();
                    HTuple hv_StartPhi = new HTuple();
                    HTuple hv_EndPhi = new HTuple();
                    HTuple hv_PointOrder = new HTuple();

                    double radius = this.job_Inspect.Data.JobSheet_PatternMatch_Data.Radius_PatternMatch;
                    int verticalLength = this.job_Inspect.Data.JobSheet_PatternMatch_Data.VerticalMeasureLength;
                    int horizontalLength = this.job_Inspect.Data.JobSheet_PatternMatch_Data.HorizontalMeasureLength;
                    double sigma = this.job_Inspect.Data.JobSheet_PatternMatch_Data.MeasureSigma;
                    int threshold = this.job_Inspect.Data.JobSheet_PatternMatch_Data.Threshold;
                    string transition = this.job_Inspect.Data.JobSheet_PatternMatch_Data.TransitionDirection == "黑到白" ? "negative" : "positive";

                    HOperatorSet.CreateMetrologyModel(out metrologyHandle);
                    HOperatorSet.AddMetrologyObjectCircleMeasure(metrologyHandle, hv_Row1, hv_Column1, radius, verticalLength, horizontalLength, sigma, threshold, new HTuple(), new HTuple(), out index);
                    HOperatorSet.SetMetrologyObjectParam(metrologyHandle, "all", "measure_transition", transition);
                    HOperatorSet.ApplyMetrologyModel(this.hImage, metrologyHandle);
                    HOperatorSet.GetMetrologyObjectMeasures(out contours, metrologyHandle, "all", "all", out objectRow, out objectColumn);
                    HOperatorSet.GetMetrologyObjectResultContour(out contour, metrologyHandle, "all", "all", 1.5);
                    HOperatorSet.FitCircleContourXld(contour, "algebraic", -1, 0, 0, 3, 2, out hv_Row2, out hv_Column2, out hv_Radius1, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);


                    this.hWinCtrol.HalconWindow.SetColor("green");
                    this.hWinCtrol.HalconWindow.SetLineWidth(1);
                    this.hWinCtrol.HalconWindow.DispObj(contour);
                    #endregion

                    if (context.NotPass()) break;

                    //判断是否成功
                    bool isPass = false;
                    if (hv_Score.Length > 0)
                    {
                        if (hv_Score.D > data.JobSheet_PatternMatch_Data.MinScore)
                        {
                            Display_shape_matching_results(hv_ModelID, "green", hv_Row2, hv_Column2, hv_Angle, 1, 1, 0);

                            this.hWinCtrol.HalconWindow.SetLineWidth(1);
                            this.hWinCtrol.HalconWindow.SetColor("orange");
                            HOperatorSet.DispCross(this.hWinCtrol.HalconWindow, hv_Row2, hv_Column2, 200, 0);
                            isPass = true;
                        }
                    }
                    if (isPass)
                    {
                        context.ErrorCode = ErrorCodes.NoError;
                        context.Message = $"结果: 成功 得分 {Math.Round(hv_Score.D * 100, 3)} Row {(int)hv_Row2.D} Col {(int)hv_Column2.D}";
                    }
                    else
                    {
                        context.ErrorCode = ErrorCodes.PatternMatchFailed;
                        context.Message = $"结果: 失败";
                    }

                    #region 暂留
                    //HOperatorSet.FindScaledShapeModel(this.hImage, hv_ModelID, 
                    //    (new HTuple(data.JobSheet_PatternMatch_Data.AngleStart)).TupleRad(),
                    //    (new HTuple(data.JobSheet_PatternMatch_Data.AngleExtent)).TupleRad(),
                    //    data.JobSheet_PatternMatch_Data.MinScale,
                    //    data.JobSheet_PatternMatch_Data.MaxScale,
                    //    data.JobSheet_PatternMatch_Data.MinScore,
                    //    data.JobSheet_PatternMatch_Data.NumMatches,
                    //    data.JobSheet_PatternMatch_Data.MaxOverLap,
                    //    data.JobSheet_PatternMatch_Data.SubPixel,
                    //    data.JobSheet_PatternMatch_Data.NumLevels,
                    //    data.JobSheet_PatternMatch_Data.Greediness,
                    //    out hv_Row1, 
                    //    out hv_Column1,
                    //    out hv_Angle, 
                    //    out hv_Scale, 
                    //    out hv_Score);

                    //HOperatorSet.FindScaledShapeModel(this.hImage, hv_ModelID,
                    //    (new HTuple(data.JobSheet_PatternMatch_Data.AngleStart)).TupleRad(),
                    //    (new HTuple(data.JobSheet_PatternMatch_Data.AngleExtent)).TupleRad(), 
                    //    data.JobSheet_PatternMatch_Data.MinScale,
                    //    data.JobSheet_PatternMatch_Data.MaxScale, 
                    //    data.JobSheet_PatternMatch_Data.MinScore, 1, 0.5, "least_squares", 0, 
                    //    data.JobSheet_PatternMatch_Data.Greediness, out hv_Row1, out hv_Column1,
                    //    out hv_Angle, out hv_Scale, out hv_Score);

                    //HTuple angleStart = (new HTuple(data.JobSheet_PatternMatch_Data.AngleStart)).TupleRad();
                    //HTuple angleExtent = (new HTuple(data.JobSheet_PatternMatch_Data.AngleExtent)).TupleRad();
                    //double minScale = data.JobSheet_PatternMatch_Data.MinScale;
                    //double maxScale = data.JobSheet_PatternMatch_Data.MaxScale;
                    //double minScore = data.JobSheet_PatternMatch_Data.MinScore;


                    //HOperatorSet.FindScaledShapeModel(this.hImage, hv_ModelID, 
                    //   angleStart, angleExtent,minScale,maxScale, minScore, 1, 0.5, "least_squares", 0, 0.9, out hv_Row1, out hv_Column1,
                    //   out hv_Angle, out hv_Scale, out hv_Score);
                    #endregion

                } while (false);
            }
            catch (Exception ex)
            {
               context.Set(ErrorCodes.PatternMatchFailed, ex.Message);  
            }

            return context;
        }

        private void Display_shape_matching_results(HTuple hv_ModelID, HTuple hv_Color,
           HTuple hv_Row, HTuple hv_Column, HTuple hv_Angle, HTuple hv_ScaleR, HTuple hv_ScaleC,
           HTuple hv_Model)
        {



            // Local iconic variables 

            HObject ho_ModelContours = null, ho_ContoursAffinTrans = null;

            // Local control variables 

            HTuple hv_NumMatches = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Match = new HTuple(), hv_HomMat2DIdentity = new HTuple();
            HTuple hv_HomMat2DScale = new HTuple(), hv_HomMat2DRotate = new HTuple();
            HTuple hv_HomMat2DTranslate = new HTuple();
            HTuple hv_Model_COPY_INP_TMP = new HTuple(hv_Model);
            HTuple hv_ScaleC_COPY_INP_TMP = new HTuple(hv_ScaleC);
            HTuple hv_ScaleR_COPY_INP_TMP = new HTuple(hv_ScaleR);

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans);
            try
            {
                //This procedure displays the results of Shape-Based Matching.
                //
                hv_NumMatches.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumMatches = new HTuple(hv_Row.TupleLength()
                        );
                }
                if ((int)(new HTuple(hv_NumMatches.TupleGreater(0))) != 0)
                {
                    if ((int)(new HTuple((new HTuple(hv_ScaleR_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        {
                            HTuple ExpTmpOutVar_0;
                            HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleR_COPY_INP_TMP, out ExpTmpOutVar_0);
                            hv_ScaleR_COPY_INP_TMP.Dispose();
                            hv_ScaleR_COPY_INP_TMP = ExpTmpOutVar_0;
                        }
                    }
                    if ((int)(new HTuple((new HTuple(hv_ScaleC_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        {
                            HTuple ExpTmpOutVar_0;
                            HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleC_COPY_INP_TMP, out ExpTmpOutVar_0);
                            hv_ScaleC_COPY_INP_TMP.Dispose();
                            hv_ScaleC_COPY_INP_TMP = ExpTmpOutVar_0;
                        }
                    }
                    if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength())).TupleEqual(
                        0))) != 0)
                    {
                        hv_Model_COPY_INP_TMP.Dispose();
                        HOperatorSet.TupleGenConst(hv_NumMatches, 0, out hv_Model_COPY_INP_TMP);
                    }
                    else if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength()
                        )).TupleEqual(1))) != 0)
                    {
                        {
                            HTuple ExpTmpOutVar_0;
                            HOperatorSet.TupleGenConst(hv_NumMatches, hv_Model_COPY_INP_TMP, out ExpTmpOutVar_0);
                            hv_Model_COPY_INP_TMP.Dispose();
                            hv_Model_COPY_INP_TMP = ExpTmpOutVar_0;
                        }
                    }
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ModelID.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_ModelContours.Dispose();
                            HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelID.TupleSelect(
                                hv_Index), 1);
                        }
                        if (HDevWindowStack.IsOpen())
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.SetColor(HDevWindowStack.GetActive(), hv_Color.TupleSelect(
                                    hv_Index % (new HTuple(hv_Color.TupleLength()))));
                            }
                        }
                        HTuple end_val18 = hv_NumMatches - 1;
                        HTuple step_val18 = 1;
                        for (hv_Match = 0; hv_Match.Continue(end_val18, step_val18); hv_Match = hv_Match.TupleAdd(step_val18))
                        {
                            if ((int)(new HTuple(hv_Index.TupleEqual(hv_Model_COPY_INP_TMP.TupleSelect(
                                hv_Match)))) != 0)
                            {
                                hv_HomMat2DIdentity.Dispose();
                                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_HomMat2DScale.Dispose();
                                    HOperatorSet.HomMat2dScale(hv_HomMat2DIdentity, hv_ScaleR_COPY_INP_TMP.TupleSelect(
                                        hv_Match), hv_ScaleC_COPY_INP_TMP.TupleSelect(hv_Match), 0, 0,
                                        out hv_HomMat2DScale);
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_HomMat2DRotate.Dispose();
                                    HOperatorSet.HomMat2dRotate(hv_HomMat2DScale, hv_Angle.TupleSelect(
                                        hv_Match), 0, 0, out hv_HomMat2DRotate);
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_HomMat2DTranslate.Dispose();
                                    HOperatorSet.HomMat2dTranslate(hv_HomMat2DRotate, hv_Row.TupleSelect(
                                        hv_Match), hv_Column.TupleSelect(hv_Match), out hv_HomMat2DTranslate);
                                }
                                ho_ContoursAffinTrans.Dispose();
                                HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans,
                                    hv_HomMat2DTranslate);
                                if (HDevWindowStack.IsOpen())
                                {
                                    HOperatorSet.DispObj(ho_ContoursAffinTrans, HDevWindowStack.GetActive()
                                        );
                                }
                            }
                        }
                    }
                }
                ho_ModelContours.Dispose();
                ho_ContoursAffinTrans.Dispose();

                hv_Model_COPY_INP_TMP.Dispose();
                hv_ScaleC_COPY_INP_TMP.Dispose();
                hv_ScaleR_COPY_INP_TMP.Dispose();
                hv_NumMatches.Dispose();
                hv_Index.Dispose();
                hv_Match.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DScale.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_HomMat2DTranslate.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_ModelContours.Dispose();
                ho_ContoursAffinTrans.Dispose();

                hv_Model_COPY_INP_TMP.Dispose();
                hv_ScaleC_COPY_INP_TMP.Dispose();
                hv_ScaleR_COPY_INP_TMP.Dispose();
                hv_NumMatches.Dispose();
                hv_Index.Dispose();
                hv_Match.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DScale.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_HomMat2DTranslate.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public HImage Load_Model(Data_Inspection data)
        {
            HObject ho_Img = new HObject(); 
            HImage hImg = new HImage();
            string bitmap_FileName = Path.Combine(SystemPath.GetVisionPatternPath, $"{data.Name}-圆形模板.jpg");

            if (!File.Exists(bitmap_FileName)) return null;

            HOperatorSet.ReadImage(out ho_Img, bitmap_FileName);
            HobjectToHimage(ho_Img, ref hImg);

            return hImg;
        }

        public Mission_Report Delete_Pattern(Data_Inspection data, HWindowControl hControl)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                string bitmap_FileName = Path.Combine(SystemPath.GetVisionPatternPath, $"{data.Name}-圆形模板.jpg");
                string fileName = Path.Combine(SystemPath.GetVisionPatternPath, $"{data.Name}-圆形模板.shm");

                if( File.Exists( bitmap_FileName )) File.Delete(bitmap_FileName);
                if(File.Exists(fileName)) File.Delete(fileName);

                
                hControl.HalconWindow.ClearWindow();
            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }

            return context;
        }
     
        //函数原型 
        public void HobjectToHimage(HObject hobject, ref HImage image)
        {
            HTuple pointer, type, width, height;
            HOperatorSet.GetImagePointer1(hobject, out pointer, out type, out width, out height);
            image.GenImage1(type, width, height, pointer);
        }


        //转换彩色图像的方法 
        public void HobjectToRGBHimage(HObject hobject, ref HImage image)
        {
            HTuple pointerRed, pointerGreen, pointerBlue, type, width, height;
            HOperatorSet.GetImagePointer3(hobject, out pointerRed, out pointerGreen, out pointerBlue, out type, out width, out height);
            image.GenImage3(type, width, height, pointerRed, pointerGreen, pointerBlue);
        }
        //protected HWindowControl HWindow;
        //protected CameraBase cameraBase;
        //protected List<HObjectEntry> HObjList;
        //protected List<ROIBase> ROIList;
        //protected GraphicsContext mGC;
        //protected ROIBase roiMode;
        //protected ROIBase cross = null;
        //protected Mouse_Event_Mode event_Mode = Mouse_Event_Mode.None;

        //protected HTuple pixel_Row = new HTuple();
        //protected HTuple pixel_Column = new HTuple();
        //protected HTuple hWinDow_Button = new HTuple();
        //protected HTuple hImageSize_Width = new HTuple();
        //protected HTuple hImageSize_Height = new HTuple();
        //protected HTuple hPointGray = new HTuple();

        //protected HImage image;

        //public CameraBase CameraBase
        //{
        //    get => cameraBase;
        //}


        //protected double StartX = 0;
        //protected double StartY = 0;
        //protected string activeCol = "green";
        //protected string activeHdlCol = "red";
        //protected string inactiveCol = "blue";
        //protected int activeROIidx = -1;
        //protected bool isShowCrossLine = false;
        //public bool IsShowCrossLine
        //{
        //    get => isShowCrossLine;
        //}

        //protected string location;
        //public string Location
        //{
        //    get=> location;
        //    set=> UpdateProper(ref location, value);
        //}

        //protected string pointGray;
        //public string PointGray
        //{
        //    get => pointGray;
        //    set=> UpdateProper(ref pointGray, value);   
        //}



        //public VisionControllerBase(HWindowControl hWindow, ICameraBase camera)
        //{
        //    Setup(hWindow, camera);
        //    Add_HWindow_MouseEvent();
        //}

        //public void Setup(HWindowControl HWindow, ICameraBase camera)
        //{
        //    this.HWindow = HWindow;
        //    this.cameraBase = camera as CameraBase;
        //    //this.cameraBase.SetWindowHost(HWindow.HalconWindow);

        //    ROIList = new List<ROIBase>();
        //    HObjList = new List<HObjectEntry>();
        //    mGC = new GraphicsContext();
        //}
        //public void AddROI(ROIBase roiMode)
        //{
        //   this.roiMode = roiMode;
        //}

        //public void ClearCrossLine()
        //{
        //    this.event_Mode = Mouse_Event_Mode.Active_Image;
        //    isShowCrossLine = false;
        //    Repaint();
        //}

        //public void ClearROIs()
        //{
        //    if (ROIList.Count == 0) return;
        //    this.ROIList.Clear();
        //    Repaint();
        //}

        //public int Do_Inspection(IInspectionKit kit)
        //{
        //    return kit.Do_Job();
        //}

        //public void Fit_Image()
        //{
        //    try
        //    {
        //        if (this.image == null) return;
        //        AddEntityObject(this.image);
        //        Repaint();
        //    }
        //    catch
        //    {
        //        SolveWare.Core.MMgr.Infohandler.LogMessage("自适应图片失败", isWindowShow: true);
        //    }
        //}

        //public void GenerateCrossLine()
        //{
        //    cross = new ROI_CrossLine();
        //    double r1 = this.HWindow.ImagePart.Top;
        //    double c1 = this.HWindow.ImagePart.Left;
        //    double r2 = this.HWindow.ImagePart.Bottom;
        //    double c2 = this.HWindow.ImagePart.Right;

        //    double midX = (int)(c1 + c2) / 2;
        //    double midY = (int)(r1 + r2) / 2;

        //    cross.createROI(midY, midX);
        //    cross.draw(this.HWindow.HalconWindow);
        //    isShowCrossLine = true;
        //}

        //public void GrabOne()
        //{
        //    try
        //    {
        //        //判断
        //        if (this.cameraBase == null)
        //        {
        //            SolveWare.Core.MMgr.Infohandler.LogMessage("Error: 无相机物件,无法抓取图片", true, true);
        //            return;
        //        }
        //        if (this.cameraBase.ConfigData.IsSimulation)
        //        {
        //            SolveWare.Core.MMgr.Infohandler.LogMessage("虚拟相机, 无法抓取图", true);
        //            return;
        //        }

        //        this.cameraBase.GrabImageOnce();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public void Open_File_To_Get_Image()
        //{
        //    OpenFileDialog dialog = new OpenFileDialog();
        //    dialog.RestoreDirectory = true;
        //    dialog.Multiselect = false;
        //    dialog.Filter = "图片|*.*";
        //    dialog.Title = "选择一张图片";
        //    DialogResult result = dialog.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        this.image = new HImage(dialog.FileName);

        //        ClearEntityObject();
        //        AddEntityObject(this.image);
        //        Adapt_Window();
        //    }
        //}

        //public void StartLive(int delay_ms)
        //{
        //    try
        //    {
        //        //判断
        //        if(this.cameraBase == null)
        //        {
        //            SolveWare.Core.MMgr.Infohandler.LogMessage("Error: 无相机物件,无法实时拍摄功能", true, true);
        //            return;
        //        }
        //        if(this.cameraBase.ConfigData.IsSimulation)
        //        {
        //            SolveWare.Core.MMgr.Infohandler.LogMessage("虚拟相机, 摸拟实时功能", true);
        //            return;
        //        }

        //        //实现功能
        //        ClearEntityObject();
        //        this.cameraBase.StartLive(delay_ms);

        //    }
        //    catch (Exception ex)
        //    {
        //        SolveWare.Core.MMgr.Infohandler.LogMessage($"Error: 相机实时拍摄功能", true, true);
        //    }
        //}

        //public void StopLive(int delay_ms)
        //{
        //    try
        //    {
        //        //判断
        //        if (this.cameraBase == null)
        //        {
        //            SolveWare.Core.MMgr.Infohandler.LogMessage("Error: 无相机物件,无法停止实时拍摄功能", true, true);
        //            return;
        //        }
        //        if (this.cameraBase.ConfigData.IsSimulation)
        //        {
        //            SolveWare.Core.MMgr.Infohandler.LogMessage("虚拟相机, 摸拟停止实时功能", true);
        //            return;
        //        }

        //        //实现功能
        //        this.HObjList.Clear();
        //        this.image = null;
        //        this.cameraBase.StopLive(delay_ms);

        //    }
        //    catch (Exception ex)
        //    {
        //        SolveWare.Core.MMgr.Infohandler.LogMessage($"Error: 相机停止实时拍摄功能", true, true);
        //    }
        //}

        //public void WriteText(string msg)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public void Repaint()
        //{
        //    try
        //    {
        //        HSystem.SetSystem("flush_graphic", "false");
        //        this.HWindow.HalconWindow.ClearWindow();
        //        mGC.stateOfSettings.Clear();

        //        foreach (var entry in HObjList)
        //        {
        //            mGC.applyContext(this.HWindow.HalconWindow, entry.gContext);
        //            Adapt_Window();
        //            //this.image_Host.HalconWindow.DispObj(this.image);
        //            this.HWindow.HalconWindow.DispObj(entry.HObj);
        //        }

        //        if (IsShowCrossLine)
        //            GenerateCrossLine();

        //        paintData(this.HWindow.HalconWindow);

        //        HSystem.SetSystem("flush_graphic", "true");

        //        this.HWindow.HalconWindow.SetColor("black");
        //        this.HWindow.HalconWindow.DispLine(-100.0, -100.0, -101.0, -101.0);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        ///// <summary>
        ///// 彩绘 ROI List 在 halcon 窗体
        ///// </summary>  
        //public void paintData(HalconDotNet.HWindow window)
        //{
        //    window.SetDraw("margin");
        //    window.SetLineWidth(4);

        //    if (ROIList.Count > 0)
        //    {
        //        window.SetColor(inactiveCol);
        //        window.SetDraw("margin");

        //        for (int i = 0; i < ROIList.Count; i++)
        //        {
        //            ((ROIBase)ROIList[i]).draw(window);
        //        }

        //        if (event_Mode == Mouse_Event_Mode.Add_ROI || event_Mode == Mouse_Event_Mode.Active_ROI)
        //        {
        //            window.SetColor(activeCol);
        //            ((ROIBase)ROIList[activeROIidx]).draw(window);

        //            window.SetColor(activeHdlCol);
        //            ((ROIBase)ROIList[activeROIidx]).displayActive(window);
        //        }
        //    }
        //}

        //private void Add_HWindow_MouseEvent()
        //{
        //    this.cameraBase.PropertyChanged -= CameraBase_PropertyChanged;
        //    this.cameraBase.PropertyChanged += CameraBase_PropertyChanged;

        //    this.HWindow.HMouseWheel -= HWindow_HMouseWheel;
        //    this.HWindow.HMouseWheel += HWindow_HMouseWheel;

        //    this.HWindow.HMouseMove -= HWindow_HMouseMove;
        //    this.HWindow.HMouseMove += HWindow_HMouseMove;

        //    this.HWindow.HMouseDown -= HWindow_HMouseDown;
        //    this.HWindow.HMouseDown += HWindow_HMouseDown;

        //    this.HWindow.HMouseUp -= HWindow_HMouseUp;
        //    this.HWindow.HMouseUp += HWindow_HMouseUp;
        //}

        //private void CameraBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    this.image = cameraBase.Image;
        //    Adapt_Window();
        //}

        //private void HWindow_HMouseUp(object sender, HMouseEventArgs e)
        //{
        //    event_Mode = Mouse_Event_Mode.Active_Image;
        //}

        //private void HWindow_HMouseDown(object sender, HMouseEventArgs e)
        //{
        //    try
        //    {
        //        int idxROI = -1;
        //        double max = 10000, dist = 0;
        //        double epsilon = 35.0;          //maximal shortest distance to one of the handles


        //        if (roiMode != null)
        //        {
        //            roiMode.createROI(pixel_Column, pixel_Row);
        //            ROIList.Add(roiMode);
        //            roiMode = null;
        //            activeROIidx = ROIList.Count - 1;
        //            Repaint();

        //            this.event_Mode = Mouse_Event_Mode.Add_ROI;
        //        }
        //        else if (ROIList.Count > 0)     // ... or an existing one is manipulated
        //        {
        //            activeROIidx = -1;

        //            for (int i = 0; i < ROIList.Count; i++)
        //            {
        //                dist = ((ROIBase)ROIList[i]).distToClosestHandle(pixel_Column, pixel_Row);
        //                if ((dist < max) && (dist < epsilon))
        //                {
        //                    max = dist;
        //                    idxROI = i;
        //                }
        //            }//end of for

        //            if (idxROI >= 0)
        //            {
        //                activeROIidx = idxROI;
        //                event_Mode = Mouse_Event_Mode.Active_ROI;
        //            }
        //            else
        //            {
        //                event_Mode = Mouse_Event_Mode.Active_Image;
        //            }

        //            Repaint();
        //        }
        //        else if (e.Button == MouseButtons.Right && image != null)
        //        {
        //            StartX = e.X;
        //            StartY = e.Y;
        //            event_Mode = Mouse_Event_Mode.Active_Image;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SolveWare.Core.MMgr.Infohandler.LogMessage($"Error: HWindow Mouse Down\r\n{ex.Message}", true, true);
        //    }
        //}

        //private void HWindow_HMouseMove(object sender, HMouseEventArgs e)
        //{
        //    try
        //    {
        //        UpdateCameraInfo(e);


        //        if (event_Mode == Mouse_Event_Mode.Active_ROI)
        //        {
        //            this.ROIList[activeROIidx].moveByHandle(this.pixel_Column, this.pixel_Row);
        //            Repaint();
        //        }
        //        else if (e.Button == System.Windows.Forms.MouseButtons.Right && event_Mode == Mouse_Event_Mode.Active_Image)
        //        {
        //            double motionX = ((e.X - StartX));
        //            double motionY = ((e.Y - StartY));

        //            if (((int)motionX != 0) || ((int)motionY != 0))
        //            {
        //                MoveImage(motionX, motionY);
        //                StartX = e.X - motionX;
        //                StartY = e.Y - motionY;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void HWindow_HMouseWheel(object sender, HMouseEventArgs e)
        //{
        //    if (image == null) return;

        //    HTuple zoom, row, col, button;
        //    HTuple row0, col0, row00, col00, ht, wt, r1, c1, r2, c2;

        //    if (e.Delta > 0)
        //    {
        //        zoom = 1.5;
        //    }
        //    else
        //    {
        //        zoom = 0.5;
        //    }

        //    HOperatorSet.GetMposition(this.HWindow.HalconWindow, out row, out col, out button);
        //    HOperatorSet.GetPart(this.HWindow.HalconWindow, out row0, out col0, out row00, out col00);
        //    ht = row00 - row0;
        //    wt = col00 - col0;

        //    if (ht * wt < 32000 * 32000 || zoom == 1.5)
        //    {
        //        r1 = (row0 + ((1 - (1.0 / zoom)) * (row - row0)));
        //        c1 = (col0 + ((1 - (1.0 / zoom)) * (col - col0)));
        //        r2 = r1 + (ht / zoom);
        //        c2 = c1 + (ht / zoom);
        //        this.HWindow.HalconWindow.SetPart(r1, c1, r2, c2);
        //        this.HWindow.HalconWindow.ClearWindow();
        //        this.HWindow.HalconWindow.DispImage(image);
        //    }
        //}

        //HTuple img_Row1, img_Col1, img_Row2, img_Col2;
        //private void Adapt_Window()
        //{ 
        //    //确认是否有图像
        //    if (this.image == null) return;

        //    //获取图像大小
        //    HOperatorSet.GetImageSize(image, out hImageSize_Width, out hImageSize_Height);

        //    //计算比例
        //    double ratioWidth = 1.0 * hImageSize_Width / HWindow.Width;
        //    double ratioHeight = 1.0 * hImageSize_Height / HWindow.Height;

        //    //设置窗体

        //    if (ratioWidth >= ratioHeight)
        //    {
        //        img_Row1 = -1.0 * ((HWindow.Height * ratioWidth) - hImageSize_Height) / 2;
        //        img_Col1 = 0;
        //        img_Row2 = img_Row1 + HWindow.Height * ratioWidth;
        //        img_Col2 = img_Col1 + HWindow.Width * ratioWidth;
        //    }
        //    else
        //    {
        //        img_Row1 = 0;
        //        img_Col1 = -1.0 * ((HWindow.Width * ratioHeight) - hImageSize_Width) / 2;
        //        img_Row2 = img_Row1 + HWindow.Height * ratioHeight;
        //        img_Col2 = img_Col1 + HWindow.Width * ratioHeight;
        //    }
        //    this.HWindow.HalconWindow.SetPart(img_Row1, img_Col1, img_Row2, img_Col2);

        //}
        //private void UpdateCameraInfo(HMouseEventArgs e)
        //{
        //    try
        //    {
        //        if (this.image == null)
        //        {
        //            this.pixel_Row = e.Y;
        //            this.pixel_Column = e.X;
        //            this.Location = $"R: {(int)e.X} C: {(int)e.Y}";
        //            this.PointGray = $"0";
        //            OnPropertyChanged(nameof(Location));
        //            OnPropertyChanged(nameof(pointGray));
        //            return;
        //        }


        //        HOperatorSet.GetMposition(this.HWindow.HalconWindow, out pixel_Row, out pixel_Column, out hWinDow_Button);
        //        if (hImageSize_Height != null && (pixel_Row > 0 && pixel_Row < hImageSize_Height) && (pixel_Column > 0 && pixel_Column < hImageSize_Width))//判断鼠标在图像上
        //        {
        //            HOperatorSet.GetImageSize(this.image, out hImageSize_Width, out hImageSize_Height);
        //            if (this.image != null) HOperatorSet.GetGrayval(this.image, pixel_Row, pixel_Column, out hPointGray);
        //            double colValue = pixel_Column;
        //            double rowValue = pixel_Row;
        //            double widthValue = hImageSize_Width;
        //            double heightValue = hImageSize_Height;
        //            double factorX = this.hImageSize_Width / widthValue;
        //            double factorY = this.hImageSize_Height / heightValue;

        //            int posX = (int)(colValue * factorX);
        //            int posY = (int)(rowValue * factorY);

        //            this.Location = $"R: {posX} C: {posY}";
        //            this.PointGray = $"{hPointGray}";
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
        //private void MoveImage(double motionX, double motionY)
        //{
        //    img_Row1 += -motionY;
        //    img_Row2 += -motionY;

        //    img_Col1 += -motionX;
        //    img_Col2 += -motionX;

        //    System.Drawing.Rectangle rect = this.HWindow.ImagePart;
        //    rect.X = (int)Math.Round((double)img_Col1);
        //    rect.Y = (int)Math.Round((double)img_Row1);
        //    HWindow.ImagePart = rect;

        //    Repaint();
        //}
        //private void AddEntityObject(HObject obj)
        //{

        //    HObjectEntry entry;

        //    switch (obj)
        //    {
        //        case null:
        //            return;
        //        case HImage image:
        //            {
        //                if (image.Key == IntPtr.Zero)
        //                {
        //                    return;
        //                }

        //                this.image = image;
        //                break;
        //            }
        //    }

        //    entry = new HObjectEntry(obj, mGC.copyContextList());

        //    HObjList.Add(entry);
        //    Repaint();
        //}

        ///// <summary>
        ///// 清除 Entities
        ///// </summary>
        //private void ClearEntityObject()
        //{
        //    this.HObjList.Clear();
        //    this.HWindow.HalconWindow.ClearWindow();
        //}

    }
}
