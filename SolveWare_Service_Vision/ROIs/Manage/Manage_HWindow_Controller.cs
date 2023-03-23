using HalconDotNet;
using SolveWare_Service_Core;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Vision.ROIs.Base.Abstract;
using SolveWare_Service_Vision.ROIs.Business;
using SolveWare_Service_Vision.ROIs.Defintions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SolveWare_Service_Vision.ROIs.Manage
{
    public class Manage_HWindow_Controller
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
        double midRow = 0;
        double midCol = 0;

        public HImage Image { get => image; }

        //窗体数值绑定
        public string Location { get; set; }
        public string PointGrey { get; set; }
        
        //ctor
        public Manage_HWindow_Controller(HWindowControl imageHost, ICamera camera)
        {
            this.image_Host = imageHost;
            DisplayObjs =new List<HObject>();
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
        }



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
            HTuple row1, col1, row2, col2;
            if(ratioWidth >= ratioHeight)
            {
                row1 = -1.0 * ((image_Host.Height * ratioWidth) - imageHeight) / 2;
                col1 = 0;
                row2 = row1 + image_Host.Height * ratioWidth;
                col2=col1+image_Host.Width* ratioWidth;
            }
            else
            {
                row1 = 0;
                col1 = -1.0 * ((image_Host.Width * ratioHeight) - imageWidth) / 2;
                row2 = row1 + image_Host.Height * ratioHeight;
                col2 = col1 + image_Host.Width * ratioHeight;
            }
            this.image_Host.HalconWindow.SetPart(row1, col1, row2, col2);

        }

        //MouseDown
        //MouseMove
        private void Image_Host_HMouseMove(object sender, HMouseEventArgs e)
        {
            HTuple Row, Column, Button, Width, Height;
            HTuple pointGray = new HTuple();
            HOperatorSet.GetMposition(this.image_Host.HalconWindow, out Row, out Column, out Button);

            if (imageHeight != null && (Row > 0 && Row < imageHeight) && (Column > 0 && Column < imageWidth))//判断鼠标在图像上
            {
                HOperatorSet.GetImageSize(Image, out Width, out Height);
                if (this.Image != null) HOperatorSet.GetGrayval(this.Image, Row, Column, out pointGray);
                this.Location = $"Row: {Row} Column: {Column}";
                this.PointGrey = $"Grey {pointGray}";
            }
            
           
        }

        //MouseWheel
        private void Image_Host_HMouseWheel(object sender, HMouseEventArgs e)
        {
            HTuple zoom, row, col, button;
            HTuple row0, col0, row00, col00, ht, wt, r1, c1,r2, c2;

            if(e.Delta> 0)
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

            if(ht*wt <32000*32000||zoom==1.5)
            {
                r1 = (row0 + ((1 - (1.0 / zoom)) * (row - row0)));
                c1= (col0 + ((1 - (1.0 / zoom)) * (col - col0)));
                r2 = r1 + (ht / zoom);
                c2 = c1 + (ht / zoom);
                this.image_Host.HalconWindow.SetPart(r1, c1, r2, c2);
                this.image_Host.HalconWindow.ClearWindow();
                this.image_Host.HalconWindow.DispImage(image);
            }


        }


        //Set Event Mode 
        public void SetEventMode(Mouse_Event_Mode mode)
        {
            this.event_Mode = mode;
        }

        //AddROI


        //OpenFile To Get Image
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
                Adapt_Window();
                this.image_Host.HalconWindow.DispImage(image);
            }
        }

        //Fit Image
        public void Fit_Image()
        {
            this.image_Host.HalconWindow.ClearWindow();
            Adapt_Window();
            this.image_Host.HalconWindow.DispImage(image);
        }

        //Grab One
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
        

        //GenerateCrossLine
        ROIBase cross = null;
        public int activeROIidx = -1;
        public void GenerateCrossLine(double imgX, double imgY)
        {
            this.event_Mode = Mouse_Event_Mode.Add_ROI;
            cross = new ROI_CrossLine();
            cross.createROI(imgX/2, imgY/2);
            cross.draw(this.image_Host.HalconWindow);
           
            
            ROIList.Add(cross);
        }

        //Clear Cross Line
        public void ClearCrossLine()
        {
            if (cross == null) return;
            this.event_Mode = Mouse_Event_Mode.Zoom;
            ROIList.Remove(cross);
            Repaint();
            cross = null;
        }

        //Write Text
        public void WriteText(string msg)
        {

            this.image_Host.HalconWindow.WriteString(msg);
        }


        //Repain
        public void Repaint()
        {
            HSystem.SetSystem("flush_graphic", "false");
            this.image_Host.HalconWindow.ClearWindow();
            //mGC.stateOfSettings.Clear();

            foreach (var entry in DisplayObjs)
            {
                //mGC.applyContext(window, entry.gContext);
                this.image_Host.HalconWindow.DispObj(entry);
            }

            //addInfoDelegate();

            //if (roiManager != null && (dispROI == MODE_INCLUDE_ROI))
                paintData(this.image_Host.HalconWindow);

            HSystem.SetSystem("flush_graphic", "true");

            this.image_Host.HalconWindow.SetColor("black");
            this.image_Host.HalconWindow.DispLine(-100.0, -100.0, -101.0, -101.0);
        }

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

                if (event_Mode == Mouse_Event_Mode.Add_ROI)
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
