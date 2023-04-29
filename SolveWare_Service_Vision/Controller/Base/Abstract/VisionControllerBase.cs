using Basler.Pylon;
using HalconDotNet;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.Inspection.Base.Interface;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SolveWare_Service_Vision.Controller.Base.Abstract
{
    public abstract class VisionControllerBase : ToolElementBase, IVisionController
    {
        protected HWindowControl hWinCtrol;
        protected CameraMediaBase camrea;
        protected HImage hImage;
        protected List<HObjectEntry> HObjList;
        protected List<ROIBase> ROIList;
        protected GraphicsContext mGC;
        protected ROIBase roiMode;
        protected ROIBase cross = null;
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



      

        public void Setup(HWindowControl HWindow, ICameraMedia camera)
        {
            this.hWinCtrol = HWindow;
            this.camrea = camera as CameraMediaBase;
            Set_Hwindow_Part();
            HObjList = new List<HObjectEntry> ();
            ROIList = new List<ROIBase> ();
            mGC = new GraphicsContext ();

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

            HObjectEntry entry;

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

            entry = new HObjectEntry(obj, mGC.copyContextList());

            HObjList.Add(entry);
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
                    ROIList.Add(roiMode);
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
                    this.Location = $"R: {(int)e.X} C: {(int)e.Y}";
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

                    this.Location = $"R: {posX} C: {posY}";
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
      
        private void Media_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.camrea.Image))
            { 
                this.hImage = this.camrea.Image;
                Repaint();
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
                AddEntityObject(this.hImage);
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
            cross.draw(this.hWinCtrol.HalconWindow);
            isShowCrossLine = true;
        }

        public void GrabOneShot()
        {
            this.camrea.GrabOneShot();
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
                mGC.stateOfSettings.Clear();

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

        private void Highlight_Active_ROI()
        {
            if (ROIList.Count > 0)
            {        
                if (event_Mode == Mouse_Event_Mode.Add_ROI || event_Mode == Mouse_Event_Mode.Active_ROI)
                {
                    this.hWinCtrol.HalconWindow.SetColor(inactiveCol);
                    this.hWinCtrol.HalconWindow.SetDraw("margin");

                    this.hWinCtrol.HalconWindow.SetColor(activeCol);
                    ((ROIBase)ROIList[activeROIidx]).draw(this.hWinCtrol.HalconWindow);

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
                    ((ROIBase)ROIList[i]).draw(window);
                }

                if (event_Mode == Mouse_Event_Mode.Add_ROI || event_Mode == Mouse_Event_Mode.Active_ROI)
                {
                    window.SetColor(activeCol);
                    ((ROIBase)ROIList[activeROIidx]).draw(window);

                    window.SetColor(activeHdlCol);
                    ((ROIBase)ROIList[activeROIidx]).displayActive(window);
                }
            }
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

        public HImage Load_Model()
        {
            HObject ho_Img = new HObject(); 
            HImage hImg = new HImage(); 
            string bitmap_FileName = Path.Combine(SystemPath.GetVisionPatternPath, "圆形模板.jpg");
            HOperatorSet.ReadImage(out ho_Img, bitmap_FileName);

            HobjectToHimage(ho_Img, ref hImg);

            return hImg;
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
