using HalconDotNet;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.ROIs.Base.Abstract;
using SolveWare_Service_Vision.ROIs.Base.Interface;
using SolveWare_Service_Vision.ROIs.Business;
using SolveWare_Service_Vision.ROIs.Defintions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SolveWare_Service_Vision.ROIs.Manage
{
    public class Manage_HWindow_Controller: ElementBase
    {
        //变量
        Mouse_Event_Mode event_Mode = Mouse_Event_Mode.None;
        HWindowControl image_Host;
        HTuple imageWidth = new HTuple();
        HTuple imageHeight = new HTuple();
        HImage image;
        List<HObject> DisplayObjs;
        List<HObjectEntry> HObjList;
        List<ROIBase> ROIList;
        GraphicsContext mGC;
        CameraBase camera;
        ROIBase roiMode = null;
        double midRow = 0;
        double midCol = 0;
        HTuple Row, Column, Button, Width, Height;
        HTuple pointGray = new HTuple();

        public HImage Image { get => image; }
        public bool IsShowCross { get;  private set; }


        //窗体数值绑定
        public string Location { get; set; }
        public string PointGrey { get; set; }


        //ctor
        public Manage_HWindow_Controller(HWindowControl imageHost, ICamera camera)
        {
            this.image_Host = imageHost;
            DisplayObjs = new List<HObject>();
            ROIList = new List<ROIBase>();
            event_Mode = Mouse_Event_Mode.None;
            this.camera = camera as CameraBase;

            // graphical stack 
            HObjList = new List<HObjectEntry>();
            mGC = new GraphicsContext();

            this.image_Host.HMouseWheel -= Image_Host_HMouseWheel;
            this.image_Host.HMouseWheel += Image_Host_HMouseWheel;

            this.image_Host.HMouseMove -= Image_Host_HMouseMove;
            this.image_Host.HMouseMove += Image_Host_HMouseMove;

            this.image_Host.HMouseDown -= Image_Host_HMouseDown;
            this.image_Host.HMouseDown += Image_Host_HMouseDown;

            this.image_Host.HMouseUp -= Image_Host_HMouseUp;
            this.image_Host.HMouseUp += Image_Host_HMouseUp;
        }

        private void Image_Host_HMouseUp(object sender, HMouseEventArgs e)
        {
            event_Mode = Mouse_Event_Mode.Active_Image;
        }

        double StartX = 0;
        double StartY = 0;
        private void Image_Host_HMouseDown(object sender, HMouseEventArgs e)
        {
            int idxROI = -1;
            double max = 10000, dist = 0;
            double epsilon = 35.0;          //maximal shortest distance to one of
                                            //the handles

           

            if (roiMode != null)            
            {              
                roiMode.createROI(Column, Row);
                ROIList.Add(roiMode);
                roiMode = null;
                activeROIidx = ROIList.Count - 1;
                Repaint();

                this.event_Mode = Mouse_Event_Mode.Add_ROI;
            }
            else if (ROIList.Count > 0)     // ... or an existing one is manipulated
            {
                activeROIidx = -1;

                for (int i = 0; i < ROIList.Count; i++)
                {
                    dist = ((ROIBase)ROIList[i]).distToClosestHandle(Column, Row);
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

                Repaint();
            }
            else if (e.Button == MouseButtons.Right && image != null)
            {
                StartX = e.X;
                StartY = e.Y;
                event_Mode = Mouse_Event_Mode.Active_Image;
            }
        }

        HTuple img_Row1, img_Col1, img_Row2, img_Col2;
        private void Adapt_Window()
        {
            //确认是否有图像
            if (this.image == null) return;

            //获取图像大小
            HOperatorSet.GetImageSize(image, out imageWidth, out imageHeight);

            //计算比例
            double ratioWidth = 1.0 * imageWidth / image_Host.Width;
            double ratioHeight = 1.0 * imageHeight / image_Host.Height;

            //设置窗体

            if (ratioWidth >= ratioHeight)
            {
                img_Row1 = -1.0 * ((image_Host.Height * ratioWidth) - imageHeight) / 2;
                img_Col1 = 0;
                img_Row2 = img_Row1 + image_Host.Height * ratioWidth;
                img_Col2 = img_Col1 + image_Host.Width * ratioWidth;
            }
            else
            {
                img_Row1 = 0;
                img_Col1 = -1.0 * ((image_Host.Width * ratioHeight) - imageWidth) / 2;
                img_Row2 = img_Row1 + image_Host.Height * ratioHeight;
                img_Col2 = img_Col1 + image_Host.Width * ratioHeight;
            }
            this.image_Host.HalconWindow.SetPart(img_Row1, img_Col1, img_Row2, img_Col2);

        }

        /// <summary>
        /// Halcon MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_Host_HMouseMove(object sender, HMouseEventArgs e)
        {
            UpdateCameraInfo(e);


            if (event_Mode == Mouse_Event_Mode.Active_ROI)
            {
                this.ROIList[activeROIidx].moveByHandle(this.Column, this.Row);
                Repaint();
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right && event_Mode == Mouse_Event_Mode.Active_Image)
            {
                double motionX = ((e.X - StartX));
                double motionY = ((e.Y - StartY));

                if (((int)motionX != 0) || ((int)motionY != 0))
                {
                    moveImage(motionX, motionY);
                    StartX = e.X - motionX;
                    StartY = e.Y - motionY;
                }

            }
        }

        private void UpdateCameraInfo(HMouseEventArgs e)
        {
            try
            {
                if(this.image == null)
                {
                    this.Location = $"R: {(int)e.X} C: {(int)e.Y}";
                    this.PointGrey = $"0";
                    return;
                }


                HOperatorSet.GetMposition(this.image_Host.HalconWindow, out Row, out Column, out Button);
                if (imageHeight != null && (Row > 0 && Row < imageHeight) && (Column > 0 && Column < imageWidth))//判断鼠标在图像上
                {
                    HOperatorSet.GetImageSize(Image, out Width, out Height);
                    if (this.Image != null) HOperatorSet.GetGrayval(this.Image, Row, Column, out pointGray);
                    double colValue = Column;
                    double rowValue = Row;
                    double widthValue = Width;
                    double heightValue = Height;
                    double factorX = 1080 / widthValue;
                    double factorY = 1920 / heightValue;

                    int posX = (int)(colValue * factorX);
                    int posY = (int)(rowValue * factorY);

                    this.Location = $"R: {posX} C: {posY}";
                    this.PointGrey = $"{pointGray}";
                }



            }
            catch (Exception ex)
            {

            }

        }
        private void moveImage(double motionX, double motionY)
        {
            img_Row1 += -motionY;
            img_Row2 += -motionY;

            img_Col1 += -motionX;
            img_Col2 += -motionX;

            System.Drawing.Rectangle rect = this.image_Host.ImagePart;
            rect.X = (int)Math.Round((double)img_Col1);
            rect.Y = (int)Math.Round((double)img_Row1);
            image_Host.ImagePart = rect;

            Repaint();
        }

        private void Image_Host_HMouseWheel(object sender, HMouseEventArgs e)
        {
            if (image == null) return;

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

            HOperatorSet.GetMposition(this.image_Host.HalconWindow, out row, out col, out button);
            HOperatorSet.GetPart(this.image_Host.HalconWindow, out row0, out col0, out row00, out col00);
            ht = row00 - row0;
            wt = col00 - col0;

            if (ht * wt < 32000 * 32000 || zoom == 1.5)
            {
                r1 = (row0 + ((1 - (1.0 / zoom)) * (row - row0)));
                c1 = (col0 + ((1 - (1.0 / zoom)) * (col - col0)));
                r2 = r1 + (ht / zoom);
                c2 = c1 + (ht / zoom);
                this.image_Host.HalconWindow.SetPart(r1, c1, r2, c2);     
                this.image_Host.HalconWindow.ClearWindow();
                this.image_Host.HalconWindow.DispImage(image);
            }          
        }

        /// <summary>
        /// 新增 ROI
        /// </summary>
        /// <param name="roiMode"></param>
        public void AddROI(ROIBase roiMode)
        {
            this.roiMode = roiMode;
        }

        /// <summary>
        /// 开启档案读取图像
        /// </summary>
        public void Open_File_To_Get_Image()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Multiselect = false;
            dialog.Filter = "图片|*.*";
            dialog.Title = "选择一张图片";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.image = new HImage(dialog.FileName);
                ClearEntityObject();
                AddEntityObject(this.image);
                //this.image_Host.HalconWindow.ClearWindow();

                //this.image = new HImage(dialog.FileName);
                //Adapt_Window();
                //this.image_Host.HalconWindow.DispImage(image);
            }
        }

        /// <summary>
        /// 自适应窗体
        /// </summary>
        public void Fit_Image()
        {
            try
            {
                if (this.image == null) return;
                AddEntityObject(this.image);
                Repaint();
            }
            catch 
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage("自适应图片失败", isWindowShow: true);
            }
        }

        /// <summary>
        /// 开启 实时摄影
        /// </summary>
        public void StartLive()
        {
            this.camera.StartLive(100);
        }

        /// <summary>
        /// 停止 实时摄影
        /// </summary>
        public void StopLive()
        {
            this.camera.StopLive(100);
        }

        /// <summary>
        /// 单拍 TODO
        /// </summary>
        public void GrabOne()
        {
            int errorCode = ErrorCodes.NoError;
            try
            {
                camera.GrabImageOnce();
                this.image = camera.Image;
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.VisionFailed;
            }
        }


        /// <summary>
        /// 画十字线
        /// </summary>
        ROIBase cross = null;
        public int activeROIidx = -1;
        public void GenerateCrossLine()
        {  
            cross = new ROI_CrossLine();
            double r1 = this.image_Host.ImagePart.Top;
            double c1 = this.image_Host.ImagePart.Left;
            double r2 = this.image_Host.ImagePart.Bottom;
            double c2 = this.image_Host.ImagePart.Right;

            double midX = (int)(c1 + c2) / 2;
            double midY = (int)(r1 + r2) / 2;

            cross.createROI(midY, midX);
            cross.draw(this.image_Host.HalconWindow);
            IsShowCross = true;
        }

       /// <summary>
       /// 清除十字线
       /// </summary>
        public void ClearCrossLine()
        {
            this.event_Mode = Mouse_Event_Mode.Active_Image;
            IsShowCross = false;
            Repaint();
        }

        /// <summary>
        /// 清除所有ROI
        /// </summary>
        public void ClearROIs()
        {
            if (ROIList.Count == 0) return;
            this.ROIList.Clear();
            Repaint();
        }
       
       /// <summary>
       /// 写讯息在 Halcon Window
       /// </summary>
       /// <param name="msg"></param>
        public void WriteText(string msg)
        {
            try
            {
                this.image_Host.HalconWindow.ClearWindow();
                this.image_Host.HalconWindow.SetTposition(10, 10);
                this.image_Host.HalconWindow.WriteString(msg);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 增加 Entity
        /// </summary>
        /// <param name="obj"></param>
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

                        this.image = image;                   
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
            this.image_Host.HalconWindow.ClearWindow();
        }

        /// <summary>
        /// 彩绘 halcon 窗体
        /// </summary>
        public void Repaint()
        {
            HSystem.SetSystem("flush_graphic", "false");
            this.image_Host.HalconWindow.ClearWindow();
            mGC.stateOfSettings.Clear();

            foreach (var entry in HObjList)
            {
                mGC.applyContext(this.image_Host.HalconWindow, entry.gContext);
                Adapt_Window();
                //this.image_Host.HalconWindow.DispObj(this.image);
                this.image_Host.HalconWindow.DispObj(entry.HObj);
            }

            if (IsShowCross)
                GenerateCrossLine();

            paintData(this.image_Host.HalconWindow);

            HSystem.SetSystem("flush_graphic", "true");

            this.image_Host.HalconWindow.SetColor("black");
            this.image_Host.HalconWindow.DispLine(-100.0, -100.0, -101.0, -101.0);
        }

        /// <summary>
        /// 彩绘 ROI List 在 halcon 窗体
        /// </summary>
        private string activeCol = "green";
        private string activeHdlCol = "red";
        private string inactiveCol = "blue";
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
    }
}
