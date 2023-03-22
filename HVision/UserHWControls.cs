using HalconDotNet;
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

namespace HVision
{
    public partial class UserHWControls : UserControl
    {
        private HObject line1 = new HObject();
        private HObject line2 = new HObject();
        private HTuple m_imageWidth, m_imageHeight;
        private double m_dbCrossRow = 0;
        private double m_dbCrossCol = 0;
        public UserHWControls()
        {
            InitializeComponent();
            NewHObject();
        }
        public void ShowSourceImage(HObject iamge)
        {
            m_sourceImage = new HImage();
            m_sourceImage = iamge;

            if (m_sourceImage != null)
            {
                GetImageSize();
                ResetShowImage(true);
            }
        }
        private void GetImageSize()
        {
            HTuple channel;
            HOperatorSet.CountChannels(m_sourceImage, out channel);
            HOperatorSet.GetImageSize(m_sourceImage, out m_imageWidth, out m_imageHeight);//获取图像大小
            m_dbCrossRow = m_imageHeight / 2;
            m_dbCrossCol = m_imageWidth / 2;
        }
        public HWindow HWindows { get { return hWindowControl1.HalconWindow; } }

        public List<HObject> M_HObjects { get; set; }

        private HObject m_sourceImage;

        public HObject SourceImage
        {
            get { return m_sourceImage; }
        }
        private bool m_bIsShowCross;
        [Category("自定义"), Description("是否展示十字架"), Browsable(true)]
        public bool IsShowCross
        {
            get { return m_bIsShowCross; }
            set
            {
                m_bIsShowCross = value;
                ResetShowImage(true);
            }
        }
        public void AddHObject(HObject hObject)
        {
            M_HObjects.Add(hObject.Clone());
            ShowDrawRoi();
        }
        public void NewHObject()
        {
            M_HObjects = new List<HObject>();
        }
        public void ShowDrawRoi()
        {
            HOperatorSet.SetColor(HWindows, "green");
            if (M_HObjects?.Count > 0)
            {
                for (int i = 0; i < M_HObjects.Count; i++)
                {
                    HOperatorSet.DispObj(M_HObjects[i], HWindows);
                }
            }
        }

        private void GenCrrosLine()
        {
            if (line1 != null) line1.Dispose();
            if (line2 != null) line2.Dispose();
            HOperatorSet.GenRegionLine(out line1, (HTuple)0, (HTuple)m_dbCrossCol, m_imageHeight, (HTuple)m_dbCrossCol);
            HOperatorSet.GenRegionLine(out line2, (HTuple)m_dbCrossRow, (HTuple)0, (HTuple)m_dbCrossRow, m_imageWidth);
            if (m_bIsShowCross)
            {
                HOperatorSet.SetColor(HWindows, "blue");
                HOperatorSet.DispObj(line1, HWindows);
                HOperatorSet.DispObj(line2, HWindows);
            }
        }
        public void WriteString(string str)
        {
            HWindows.SetColor("red");
            HWindows.SetTposition(30, 30);
            //HWindows.SetFont("黑体" + "-20");
            HWindows.WriteString(str);
        }
        private void ResetShowImage(bool isZoom)
        {
            try
            {
                if (SourceImage != null)
                {
                    //HOperatorSet.ClearWindow(HWindows);//清空窗体
                    HTuple row1, column1, row2, column2;
                    if (isZoom)
                    {
                        ZoomToFit(out row1, out column1, out row2, out column2);
                    }
                    else
                    {
                        HOperatorSet.GetPart(HWindows, out row1, out column1, out row2, out column2);//得到当前的窗口坐标
                    }
                    //HSystem.SetSystem("flush_graphic", "true");
                    HOperatorSet.SetPart(HWindows, row1, column1, row2, column2);//设置显示在窗体的图像大小
                    HOperatorSet.DispObj(m_sourceImage, HWindows);//显示图像
                    GenCrrosLine();

                }
            }
            catch (HOperatorException he)
            { }
        }

        private void tsb_OpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BMP File|*.bmp|PNG File|*.png|JPEG File|*.jpg|All|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(ofd.FileName))
                {
                    HWindows.ClearWindow();
                    if (m_sourceImage != null)
                    {
                        m_sourceImage.Dispose();
                    }
                    m_sourceImage = new HImage(ofd.FileName);
                    if (m_sourceImage != null)
                    {
                        GetImageSize();
                    }
                    ResetShowImage(true);
                }
            }
        }

        private void tsb_Savemage_Click(object sender, EventArgs e)
        {
            if (m_sourceImage == null) return;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "BMP File|*.bmp|PNG File|*.png|JPEG File|*.jpg|All|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    HOperatorSet.WriteImage(m_sourceImage, Path.GetExtension(sfd.FileName).Substring(1), 0, sfd.FileName);
                }
            }
        }

        private void tsb_AutoImage_Click(object sender, EventArgs e)
        {
            if (m_sourceImage == null) 
                return;
            ResetShowImage(true);
            ShowDrawRoi();
        }

        private void tsb_IsShowCrros_Click(object sender, EventArgs e)
        {
            if (m_sourceImage == null) return;
            IsShowCross = IsShowCross ? false : true;
        }
        //图片缩放
        private void hWindowControl1_HMouseWheel(object sender, HMouseEventArgs e)
        {
            if (m_sourceImage != null)
            {
                try
                {
                    HTuple Zoom, MouseRow, MouseCol, Button;
                    HTuple ImageLeftRow, ImageLeftCol, ImageRightRow, ImageRightCol, DisplayHeight, DisplayWidth, DisplayLeftRow, DisplayLeftCol, DisplayRightRow, DisplayRightCol;
                    if (e.Delta > 0)
                    {
                        Zoom = 1.5;
                    }
                    else
                    {
                        Zoom = 0.5;
                    }
                    HOperatorSet.GetMposition(HWindows, out MouseRow, out MouseCol, out Button);//获取当前鼠标位置
                    HOperatorSet.GetPart(HWindows, out ImageLeftRow, out ImageLeftCol, out ImageRightRow, out ImageRightCol);//获取显示在窗口中的图像内容像素位置
                    DisplayHeight = ImageRightRow - ImageLeftRow;
                    DisplayWidth = ImageRightCol - ImageLeftCol;
                    if (DisplayHeight * DisplayWidth < 32000 * 32000 || Zoom == 1.5)//普通版halcon能处理的图像最大尺寸是32K*32K。如果无限缩小原图像，导致显示的图像超出限制，则会造成程序崩溃
                    {
                        DisplayLeftRow = (ImageLeftRow + ((1 - (1.0 / Zoom)) * (MouseRow - ImageLeftRow)));
                        DisplayLeftCol = (ImageLeftCol + ((1 - (1.0 / Zoom)) * (MouseCol - ImageLeftCol)));
                        DisplayRightRow = DisplayLeftRow + (DisplayHeight / Zoom);
                        DisplayRightCol = DisplayLeftCol + (DisplayWidth / Zoom);
                        HOperatorSet.SetPart(HWindows, DisplayLeftRow, DisplayLeftCol, DisplayRightRow, DisplayRightCol);
                        HOperatorSet.ClearWindow(HWindows);
                        HOperatorSet.DispObj(m_sourceImage, HWindows);
                        GetImageSize();
                        GenCrrosLine();
                        ShowDrawRoi();
                    }
                }
                catch (HOperatorException he)
                { }
            }
        }

        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {
            if (m_imageHeight != null)
            {
                try
                {
                    HTuple Row, Column, Button, pointGray;
                    HOperatorSet.GetMposition(HWindows, out Row, out Column, out Button);//获取当前鼠标的坐标值
                    if (m_imageHeight != null && (Row > 0 && Row < m_imageHeight) && (Column > 0 && Column < m_imageWidth))//判断鼠标在图像上
                    {
                        HOperatorSet.GetGrayval(m_sourceImage, Row, Column, out pointGray);//获取当前点的灰度值
                        tssl_Location.Text = Column.ToString() + "," + Row.ToString();
                        tssl_GrayValue.Text = pointGray.ToString();
                    }
                    else
                    {
                        pointGray = "_";
                    }
                }
                catch
                { }
            }
        }

        private void ZoomToFit(out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2)
        {
            double ratioWidth = (1.0) * m_imageWidth[0].I / hWindowControl1.Width;
            double ratioHeight = (1.0) * m_imageHeight[0].I / hWindowControl1.Height;

            row1 = 0;
            column1 = 0;
            row2 = 0;
            column2 = 0;
            if (ratioWidth >= ratioHeight)
            {
                double overSize = ((hWindowControl1.Height * ratioWidth) - m_imageHeight) / 2;
                row1 = -overSize;
                column1 = 0;
                row2 = m_imageHeight + overSize;
                column2 = m_imageWidth - 1;
            }
            else
            {
                double overSize = ((hWindowControl1.Width * ratioHeight) - m_imageWidth) / 2;
                row1 = 0;
                column1 = -overSize;
                row2 = m_imageHeight - 1;
                column2 = m_imageWidth + overSize;
            }
        }

        #region 轮廓
        public RoiLine roiLine { get; set; }
        public RoiCircle roiCircle { get; set; }
        public RoiRectangle roiRectangle { get; set; }
        public RoiManage m_RoiManage { get; set; }
        private RoiDataBase roiDataBase;
        private void hWindowControl1_HMouseDown(object sender, HMouseEventArgs e)
        {
            
        }

        private void hWindowControl1_HMouseUp(object sender, HMouseEventArgs e)
        {
            if (m_RoiManage?.m_RoiBase != null)
            {
                m_RoiManage.m_RoiBase.GenerateParameter();
            }
        }

        private void tsb_ClearHwindow_Click(object sender, EventArgs e)
        {
            if (m_RoiManage?.m_RoiBase != null)
            {
                m_RoiManage.m_RoiBase.DrawingObject.Dispose();
                NewHObject();
                ResetShowImage(true);
            }
        }
        
        private void tsb_DrawRectangle_Click(object sender, EventArgs e)
        {
            if (m_sourceImage == null) return;
            roiDataBase = new RoiRectgancleData(m_dbCrossRow, m_dbCrossCol, m_dbCrossRow + 100, m_dbCrossCol + 70);
            m_RoiManage = new RoiManage(RoiType.rectangle, roiDataBase);
            m_RoiManage.m_RoiBase.CreateDrawingObject(Color.Red);
            HOperatorSet.AttachDrawingObjectToWindow(HWindows, m_RoiManage.m_RoiBase.DrawingObject);
            roiRectangle= (RoiRectangle)m_RoiManage.m_RoiBase;
            ResetShowImage(true);
        }

        private void tsb_DrawLines_Click(object sender, EventArgs e)
        {
            if (m_sourceImage == null) return;
            m_RoiManage?.m_RoiBase?.DrawingObject?.Dispose();
            roiDataBase = new RoiLineData(m_dbCrossRow, m_dbCrossCol - 100, m_dbCrossRow, m_dbCrossCol + 100);
            m_RoiManage = new RoiManage(RoiType.line, roiDataBase);
            m_RoiManage.m_RoiBase.CreateDrawingObject(Color.Red);
            HOperatorSet.AttachDrawingObjectToWindow(HWindows, m_RoiManage.m_RoiBase.DrawingObject);
            roiLine = (RoiLine)m_RoiManage.m_RoiBase;
            ResetShowImage(true);
        }

        private void tsb_DrawCircle_Click(object sender, EventArgs e)
        {
            if (m_sourceImage == null) return;
            m_RoiManage?.m_RoiBase?.DrawingObject?.Dispose();
            roiDataBase  = new RoiCircleData(m_dbCrossRow, m_dbCrossCol, 100);
            m_RoiManage = new RoiManage(RoiType.circle, roiDataBase);
            m_RoiManage.m_RoiBase.CreateDrawingObject(Color.Red);
            //m_RoiManage.m_RoiBase.DrawingObject.OnAttach(ProcessMethod);//AttachDrawingObjectToWindow时触发
            //m_RoiManage.m_RoiBase.DrawingObject.OnSelect(ProcessMethod);//选中触发
            //m_RoiManage.m_RoiBase.DrawingObject.OnDrag(ProcessMethod); //移动时触发
            //m_RoiManage.m_RoiBase.DrawingObject.OnResize(ProcessMethod); //拉缩时触发

            HWindows.AttachDrawingObjectToWindow(m_RoiManage.m_RoiBase.DrawingObject);
            roiCircle = (RoiCircle)m_RoiManage.m_RoiBase;
            ResetShowImage(true);
        }
        #endregion
    }
}
