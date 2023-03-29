using HalconDotNet;
using SolveWare_Service_Vision.ROIs.Attributes;
using SolveWare_Service_Vision.ROIs.Base.Abstract;
using SolveWare_Service_Vision.ROIs.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.ROIs.Business
{
    [ROIKindAttribute]
    public class ROI_Rectangle : ROIBase, IROI
    {

        private double row1, col1;   // upper left
        private double row2, col2;   // lower right 
        private double midR, midC;   // midpoint 


        /// <summary>Constructor</summary>
        public ROI_Rectangle()
        {
            RoiType = GetType().ToString();
            NumHandles = 5; // 4 corner points + midpoint
            activeHandleIdx = 4;
        }

        /// <summary>Creates a new ROI instance at the mouse position</summary>
        /// <param name="midX">
        /// x (=column) coordinate for interactive ROI
        /// </param>
        /// <param name="midY">
        /// y (=row) coordinate for interactive ROI
        /// </param>
        public override void createROI(double midX, double midY)
        {
            midR = midY;
            midC = midX;

            row1 = midR - 50;
            col1 = midC - 50;
            row2 = midR + 50;
            col2 = midC + 50;
        }


        public override void createROI(HTuple RoiParameters)
        {

            //return new HTuple(new double[] { row1, col1, row2, col2 });


            row1 = RoiParameters[0];
            col1 = RoiParameters[1];
            row2 = RoiParameters[2];
            col2 = RoiParameters[3];

            double tmp;
            if (row2 <= row1)
            {
                tmp = row1;
                row1 = row2;
                row2 = tmp;
            }

            if (col2 <= col1)
            {
                tmp = col1;
                col1 = col2;
                col2 = tmp;
            }

            midR = ((row2 - row1) / 2) + row1;
            midC = ((col2 - col1) / 2) + col1;
        }

        /// <summary>Paints the ROI into the supplied window</summary>
        /// <param name="window">HALCON window</param>
        public override void draw(HalconDotNet.HWindow window)
        {
            //window.DispText("Search Area", "window", row1 - 10,; col1 - 10, "white", new HTuple(), new HTuple());
            //window.SetColor("yellow");
            //Disp_Text(window, (int)row1 - 25, (int)col1 + 6, "yellow", "搜索范围");

            window.SetColor("cyan");
            window.DispRectangle1(row1, col1, row2, col2);
            window.DispRectangle2(row1, col1, 0, 5, 5);
            window.DispRectangle2(row1, col2, 0, 5, 5);
            window.DispRectangle2(row2, col2, 0, 5, 5);
            window.DispRectangle2(row2, col1, 0, 5, 5);
            window.DispRectangle2(midR, midC, 0, 5, 5);
        }

        /// <summary> 
        /// Returns the distance of the ROI handle being
        /// closest to the image point(x,y)
        /// </summary>
        /// <param name="x">x (=column) coordinate</param>
        /// <param name="y">y (=row) coordinate</param>
        /// <returns> 
        /// Distance of the closest ROI handle.
        /// </returns>
        private void Disp_Text(HalconDotNet.HWindow win, int row, int col, string color, string msg)
        {
            HTuple hv_FontWithSize = null;
            HOperatorSet.SetLineWidth(win, 2);
            //设置字体颜色
            win.SetColor(color);
            //设置字体风格

            HTuple hv_Font;

            // 获取halcon支持的字体
            HOperatorSet.QueryFont(win, out hv_Font);

            //50表示字体大小，括号中的数字表示第几种字体
            //hv_FontWithSize = "微软雅黑-11";//hv_Font.TupleSelect(0) + "-9";//缺点在此，不知道想要的黑体排第几，所以括号里不知道填几
            //win.SetFont("微软雅黑-11");

            hv_FontWithSize = (hv_Font.TupleSelect(0)) + "-20";
            win.SetFont( hv_FontWithSize);

            //设置显示的位置（坐标）
            win.SetTposition(row, col);
            //设置显示的内容
            win.WriteString(msg);
        }
        public override double distToClosestHandle(double x, double y)
        {

            double max = 10000;
            double[] val = new double[NumHandles];

            midR = ((row2 - row1) / 2) + row1;
            midC = ((col2 - col1) / 2) + col1;

            val[0] = HMisc.DistancePp(y, x, row1, col1); // upper left 
            val[1] = HMisc.DistancePp(y, x, row1, col2); // upper right 
            val[2] = HMisc.DistancePp(y, x, row2, col2); // lower right 
            val[3] = HMisc.DistancePp(y, x, row2, col1); // lower left 
            val[4] = HMisc.DistancePp(y, x, midR, midC); // midpoint 

            for (int i = 0; i < NumHandles; i++)
            {
                if (val[i] < max)
                {
                    max = val[i];
                    activeHandleIdx = i;
                }
            }// end of for 

            return val[activeHandleIdx];
        }

        /// <summary> 
        /// Paints the active handle of the ROI object into the supplied window
        /// </summary>
        /// <param name="window">HALCON window</param>
        public override void displayActive(HalconDotNet.HWindow window)
        {
            switch (activeHandleIdx)
            {
                case 0:
                    window.DispRectangle2(row1, col1, 0, 5, 5);
                    break;
                case 1:
                    window.DispRectangle2(row1, col2, 0, 5, 5);
                    break;
                case 2:
                    window.DispRectangle2(row2, col2, 0, 5, 5);
                    break;
                case 3:
                    window.DispRectangle2(row2, col1, 0, 5, 5);
                    break;
                case 4:
                    window.DispRectangle2(midR, midC, 0, 5, 5);
                    break;
            }
        }

        /// <summary>Gets the HALCON region described by the ROI</summary>
        public override HRegion getRegion()
        {
            HRegion region = new HRegion();
            region.GenRectangle1(row1, col1, row2, col2);
            return region;
        }

        /// <summary>
        /// Gets the model information described by 
        /// the interactive ROI
        /// </summary> 
        public override HTuple getModelData()
        {
            return new HTuple(new double[] { row1, col1, row2, col2 });
        }

        public override HTuple getModelDataName()
        {
            return new HTuple(new string[] { "row1", "col1", "row2", "col2" });
        }

        /// <summary> 
        /// Recalculates the shape of the ROI instance. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y)
        /// </summary>
        /// <param name="newX">x mouse coordinate</param>
        /// <param name="newY">y mouse coordinate</param>
        public override void moveByHandle(double newX, double newY)
        {
            double len1, len2;
            double tmp;

            switch (activeHandleIdx)
            {
                case 0: // upper left 
                    row1 = newY;
                    col1 = newX;
                    break;
                case 1: // upper right 
                    row1 = newY;
                    col2 = newX;
                    break;
                case 2: // lower right 
                    row2 = newY;
                    col2 = newX;
                    break;
                case 3: // lower left
                    row2 = newY;
                    col1 = newX;
                    break;
                case 4: // midpoint 
                    len1 = ((row2 - row1) / 2);
                    len2 = ((col2 - col1) / 2);

                    row1 = newY - len1;
                    row2 = newY + len1;

                    col1 = newX - len2;
                    col2 = newX + len2;

                    break;
            }

            if (row2 <= row1)
            {
                tmp = row1;
                row1 = row2;
                row2 = tmp;
            }

            if (col2 <= col1)
            {
                tmp = col1;
                col1 = col2;
                col2 = tmp;
            }

            midR = ((row2 - row1) / 2) + row1;
            midC = ((col2 - col1) / 2) + col1;

        }//end of method
    }
}
