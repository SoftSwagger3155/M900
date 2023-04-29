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
    public class ROI_Circle : ROIBase, IROI
    {

        public double Radius1 { get; private set; } 
        public double Radius2 { get; private set; }
        public double Row1 { get; private set; }
        public double Col1 { get; private set; } 
        public double Row2 { get; private set; } 
        public double Col2 { get; private set; }  // first handle
        public double MidR { get; private set; } 
        public double MidC { get; private set; }  // second handle



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
            MidR = midY;
            MidC = midX;

            Radius1 = 100;
            Radius2 = 120;

            Row1 = MidR;
            Col1 = MidC + Radius1;

            Row2 = MidR;
            Col2 = MidC + Radius2;
        }


        public override void createROI(HTuple RoiParameters)
        {

            //return new HTuple(new double[] { midR, midC, radius });

            MidR = RoiParameters[0];
            MidC = RoiParameters[1];

            Radius1 = RoiParameters[2];

            Row1 = MidR;
            Col1 = MidC + Radius1;

            Row2 = MidR;
            Col2 = MidC + Radius2;
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
                window.DispCircle(MidR, MidC, Radius1);
                //window.DispCircle(midR, midC, radius2);
                window.DispRectangle2(Row1, Col1, 0, 5, 5);
                // window.DispRectangle2(row2, col2, 0, 5, 5);
                window.DispCross(MidR, MidC, 20, 0);
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
                HOperatorSet.AddMetrologyObjectCircleMeasure(hv_MetrologyHandle, MidR, MidC, Radius1, 18, 2, 1, 50, new HTuple(), new HTuple(), out hv_Index);
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
                    //hv_MetrologyHandle.Dispose();
                }

                HOperatorSet.SetLineWidth(window, 2);
                HOperatorSet.SetColor(window, "cyan");
                window.DispCircle(MidR, MidC, Radius1);
                //window.DispCircle(midR, midC, radius2);
                window.DispRectangle2(Row1, Col1, 0, 5, 5);
                // window.DispRectangle2(row2, col2, 0, 5, 5);
                window.DispCross(MidR, MidC, 20, 0);




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

            val[0] = HMisc.DistancePp(y, x, Row1, Col1); // border handle 
            val[1] = HMisc.DistancePp(y, x, MidR, MidC); // midpoint 
            val[2] = HMisc.DistancePp(y, x, Row2, Col2); // midpoint 

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
                    window.DispRectangle2(Row1, Col1, 0, 5, 5);
                    break;
                case 1:
                    window.DispCross(MidR, MidC, 20, 0);
                    break;
                case 2:
                    window.DispRectangle2(Row2, Col2, 0, 5, 5);
                    //window.DispRectangle2(midR, midC, 0, 5, 5);
                    break;
            }
        }

        /// <summary>Gets the HALCON region described by the ROI</summary>
        public override HRegion getRegion()
        {
            HRegion region = new HRegion();
            region.GenCircle(MidR, MidC, Radius1);
            return region;
        }

        public override double getDistanceFromStartPoint(double row, double col)
        {
            double sRow = MidR; // assumption: we have an angle starting at 0.0
            double sCol = MidC + 1 * Radius1;

            double angle = HMisc.AngleLl(MidR, MidC, sRow, sCol, MidR, MidC, row, col);

            if (angle < 0)
                angle += 2 * Math.PI;

            return (Radius1 * angle);
        }

        /// <summary>
        /// Gets the model information described by 
        /// the  ROI
        /// </summary> 
        public override HTuple getModelData()
        {
            return new HTuple(new double[] { MidR, MidC, Radius1 });
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

                    Row1 = newY;
                    Col1 = newX;
                    HOperatorSet.DistancePp(new HTuple(Row1), new HTuple(Col1),
                                            new HTuple(MidR), new HTuple(MidC),
                                            out distance);

                    Radius1 = distance[0].D;
                    break;
                case 1: // midpoint 

                    shiftY = MidR - newY;
                    shiftX = MidC - newX;

                    MidR = newY;
                    MidC = newX;

                    Row1 -= shiftY;
                    Col1 -= shiftX;

                    Row2 -= shiftY;
                    Col2 -= shiftX;
                    break;
                case 2: // handle at Outer circle border

                    Row2 = newY;
                    Col2 = newX;
                    HOperatorSet.DistancePp(new HTuple(Row2), new HTuple(Col2),
                                            new HTuple(MidR), new HTuple(MidC),
                                            out distance);
                    Radius2 = distance[0].D;
                    break;
            }
        }
    }
}
