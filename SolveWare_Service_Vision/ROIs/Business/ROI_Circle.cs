using HalconDotNet;
using SolveWare_Service_Vision.ROIs.Base.Abstract;
using SolveWare_Service_Vision.ROIs.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.ROIs.Business
{
    public class ROI_Circle : ROIBase, IROI
    {

        private double radius1, radius2;
        private double row1, col1, row2, col2;  // first handle
        private double midR, midC;  // second handle



        public ROI_Circle()
        {
            RoiType = GetType().ToString();
            NumHandles = 3; // one at corner of circle + midpoint
            activeHandleIdx = 1;
        }

        public ROI_Circle(string Name)
        {
            RoiName = Name;
            RoiType = GetType().ToString();
            NumHandles = 3; // one at corner of circle + midpoint
            activeHandleIdx = 1;
        }

        /// <summary>Creates a new ROI instance at the mouse position</summary>
        public override void createROI(double midX, double midY)
        {
            midR = midY;
            midC = midX;

            radius1 = 100;
            radius2 = 120;

            row1 = midR;
            col1 = midC + radius1;

            row2 = midR;
            col2 = midC + radius2;
        }


        public override void createROI(HTuple RoiParameters)
        {

            //return new HTuple(new double[] { midR, midC, radius });

            midR = RoiParameters[0];
            midC = RoiParameters[1];

            radius1 = RoiParameters[2];

            row1 = midR;
            col1 = midC + radius1;

            row2 = midR;
            col2 = midC + radius2;
        }



        /// <summary>Paints the ROI into the supplied window</summary>
        /// <param name="window">HALCON window</param>
        public override void draw(HalconDotNet.HWindow window)
        {
            Draw_Job(window, this.Is_Metrology_Displayed);
        }
        HTuple hv_MetrologyHandle;

        public void Draw_Job(HalconDotNet.HWindow window, bool isMetrologyDisplayed)
        {
            //window.ClearWindow();
            //main_Window = window;


            if (isMetrologyDisplayed)
            {
                HOperatorSet.SetLineWidth(window, 2);
                HOperatorSet.SetColor(window, "cyan");
                window.DispCircle(midR, midC, radius1);
                //window.DispCircle(midR, midC, radius2);
                window.DispRectangle2(row1, col1, 0, 5, 5);
                // window.DispRectangle2(row2, col2, 0, 5, 5);
                window.DispCross(midR, midC, 20, 0);
                //测试卡尺
                hv_MetrologyHandle = new HTuple();
                HTuple hv_Row = new HTuple();
                HTuple hv_Column = new HTuple();
                HTuple hv_Radius = new HTuple();
                HTuple hv_Row2 = new HTuple();
                HTuple hv_Column2 = new HTuple();
                HTuple hv_Radius2 = new HTuple();
                HTuple hv_StartPhi = new HTuple();
                HTuple hv_EndPhi = new HTuple();
                HTuple hv_PointOrder = new HTuple();
                HTuple hv_Index = new HTuple();
                HObject ho_Contour = new HObject();


                // add_metrology_object_line_measure(MetrologyHandle, Row1, Column1, Row2, Column2, 20, 5, 1, 30, [], [], Index)
                //get_metrology_object_model_contour(Contour, MetrologyHandle, 0, 1.5)
                //get_metrology_object_measures(Contours, MetrologyHandle, 'all', 'all', Row, Column)
                //dev_set_color('cyan')
                //dev_display(Contour)
                //dev_display(Contours)

                //string displayStr = isMetrologyDisplayed ? "cyan": "transparent";
                HOperatorSet.SetLineWidth(window, 1);
                HOperatorSet.SetColor(window, "cyan");
                HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
                HOperatorSet.AddMetrologyObjectCircleMeasure(hv_MetrologyHandle, midR, midC, radius1, 18, 2, 1, 50, new HTuple(), new HTuple(), out hv_Index);
                HOperatorSet.GetMetrologyObjectModelContour(out ho_Contour, hv_MetrologyHandle, 0, 1.5);
                HOperatorSet.GetMetrologyObjectMeasures(out ho_Contours, hv_MetrologyHandle, "all", "all", out hv_Row2, out hv_Column2);
                //window.DispObj(ho_Contour);11
                window.DispObj(ho_Contours);
                //HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_Row2, out hv_Column2, out hv_Radius2, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                //         if (isMetrologyDisplayed)
                //{
                //	hv_MetrologyHandle.Dispose();
                //}
                //         else 
                //	window.DispObj(ho_Contour);
            }
            else
            {
                if (hv_MetrologyHandle != null)
                {
                    HOperatorSet.ClearMetrologyModel(hv_MetrologyHandle);
                    hv_MetrologyHandle.Dispose();
                }

                HOperatorSet.SetLineWidth(window, 2);
                HOperatorSet.SetColor(window, "cyan");
                window.DispCircle(midR, midC, radius1);
                //window.DispCircle(midR, midC, radius2);
                window.DispRectangle2(row1, col1, 0, 5, 5);
                // window.DispRectangle2(row2, col2, 0, 5, 5);
                window.DispCross(midR, midC, 20, 0);




            }

        }


        public override void DisplayContours(bool show)
        {
            Draw_Job(main_Window, show);

        }


        /// <summary> 
        /// Returns the distance of the ROI handle being
        /// closest to the image point(x,y)
        /// </summary>
        public override double distToClosestHandle(double x, double y)
        {
            double max = 10000;
            double[] val = new double[NumHandles];

            val[0] = HMisc.DistancePp(y, x, row1, col1); // border handle 
            val[1] = HMisc.DistancePp(y, x, midR, midC); // midpoint 
            val[2] = HMisc.DistancePp(y, x, row2, col2); // midpoint 

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
        public override void displayActive(HalconDotNet.HWindow window)
        {

            switch (activeHandleIdx)
            {
                case 0:
                    window.DispRectangle2(row1, col1, 0, 5, 5);
                    break;
                case 1:
                    window.DispCross(midR, midC, 20, 0);
                    break;
                case 2:
                    window.DispRectangle2(row2, col2, 0, 5, 5);
                    //window.DispRectangle2(midR, midC, 0, 5, 5);
                    break;
            }
        }

        /// <summary>Gets the HALCON region described by the ROI</summary>
        public override HRegion getRegion()
        {
            HRegion region = new HRegion();
            region.GenCircle(midR, midC, radius1);
            return region;
        }

        public override double getDistanceFromStartPoint(double row, double col)
        {
            double sRow = midR; // assumption: we have an angle starting at 0.0
            double sCol = midC + 1 * radius1;

            double angle = HMisc.AngleLl(midR, midC, sRow, sCol, midR, midC, row, col);

            if (angle < 0)
                angle += 2 * Math.PI;

            return (radius1 * angle);
        }

        /// <summary>
        /// Gets the model information described by 
        /// the  ROI
        /// </summary> 
        public override HTuple getModelData()
        {
            return new HTuple(new double[] { midR, midC, radius1 });
        }

        public override HTuple getModelDataName()
        {
            return new HTuple(new string[] { "中心行", "中心列", "半径" });
        }

        /// <summary> 
        /// Recalculates the shape of the ROI. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y)
        /// </summary>
        public override void moveByHandle(double newX, double newY)
        {
            HTuple distance;
            double shiftX, shiftY;

            switch (activeHandleIdx)
            {
                case 0: // handle at circle border

                    row1 = newY;
                    col1 = newX;
                    HOperatorSet.DistancePp(new HTuple(row1), new HTuple(col1),
                                            new HTuple(midR), new HTuple(midC),
                                            out distance);

                    radius1 = distance[0].D;
                    break;
                case 1: // midpoint 

                    shiftY = midR - newY;
                    shiftX = midC - newX;

                    midR = newY;
                    midC = newX;

                    row1 -= shiftY;
                    col1 -= shiftX;

                    row2 -= shiftY;
                    col2 -= shiftX;
                    break;
                case 2: // handle at Outer circle border

                    row2 = newY;
                    col2 = newX;
                    HOperatorSet.DistancePp(new HTuple(row2), new HTuple(col2),
                                            new HTuple(midR), new HTuple(midC),
                                            out distance);
                    radius2 = distance[0].D;
                    break;
            }
        }
    }
}
