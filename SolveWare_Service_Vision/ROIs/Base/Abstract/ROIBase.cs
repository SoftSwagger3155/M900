using HalconDotNet;
using SolveWare_Service_Vision.ROIs.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.ROIs.Base.Abstract
{
    public abstract class ROIBase
    {
        public string RoiType { get; set; }

        public string RoiName { get; set; }
        // class members of inheriting ROI classes
        protected int NumHandles;
        protected int activeHandleIdx;

        /// <summary>
        /// Flag to define the ROI to be 'positive' or 'negative'.
        /// </summary>
        protected int OperatorFlag;

        /// <summary>Parameter to define the line style of the ROI.</summary>
        public HTuple flagLineStyle;

        /// <summary>Constant for a positive ROI flag.</summary>
        public const int POSITIVE_FLAG = ROIController.MODE_ROI_POS;

        /// <summary>Constant for a negative ROI flag.</summary>
        public const int NEGATIVE_FLAG = ROIController.MODE_ROI_NEG;

        public const int ROI_TYPE_LINE = 10;
        public const int ROI_TYPE_CIRCLE = 11;
        public const int ROI_TYPE_CIRCLEARC = 12;
        public const int ROI_TYPE_RECTANCLE1 = 13;
        public const int ROI_TYPE_RECTANGLE2 = 14;

        protected HObject ho_Contours = new HObject();
        protected bool is_Metrology_Displayed = true;
        public bool Is_Metrology_Displayed { get; set; }

        public HalconDotNet.HWindow main_Window;

        public virtual void DisplayContours(bool show)
        {

        }


        protected HTuple posOperation = new HTuple();
        protected HTuple negOperation = new HTuple(new int[] { 2, 2 });

        /// <summary>Constructor of abstract ROI class.</summary>
        public ROIBase() { }

        /// <summary>Creates a new ROI instance at the mouse position.</summary>
        /// <param name="midX">
        /// x (=column) coordinate for ROI
        /// </param>
        /// <param name="midY">
        /// y (=row) coordinate for ROI
        /// </param>
        public virtual void createROI(double midX, double midY) { }


        public virtual void createROI(HTuple RoiParameters) { }
        /// <summary>Paints the ROI into the supplied window.</summary>
        /// <param name="window">HALCON window</param>
        public virtual void draw(HalconDotNet.HWindow window) { }

        /// <summary> 
        /// Returns the distance of the ROI handle being
        /// closest to the image point(x,y)
        /// </summary>
        /// <param name="x">x (=column) coordinate</param>
        /// <param name="y">y (=row) coordinate</param>
        /// <returns> 
        /// Distance of the closest ROI handle.
        /// </returns>
        public virtual double distToClosestHandle(double x, double y)
        {
            return 0.0;
        }

        /// <summary> 
        /// Paints the active handle of the ROI object into the supplied window. 
        /// </summary>
        /// <param name="window">HALCON window</param>
        public virtual void displayActive(HalconDotNet.HWindow window) { }

        /// <summary> 
        /// Recalculates the shape of the ROI. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y).
        /// </summary>
        /// <param name="x">x (=column) coordinate</param>
        /// <param name="y">y (=row) coordinate</param>
        public virtual void moveByHandle(double x, double y) { }

        /// <summary>Gets the HALCON region described by the ROI.</summary>
        public virtual HRegion getRegion()
        {
            return null;
        }

        public virtual double getDistanceFromStartPoint(double row, double col)
        {
            return 0.0;
        }
        /// <summary>
        /// Gets the model information described by 
        /// the ROI.
        /// </summary> 
        public virtual HTuple getModelData()
        {
            return null;
        }

        public virtual HTuple getModelDataName()
        {
            return null;
        }

        /// <summary>Number of handles defined for the ROI.</summary>
        /// <returns>Number of handles</returns>
        public int getNumHandles()
        {
            return NumHandles;
        }


        /// <summary>Gets the active handle of the ROI.</summary>
        /// <returns>Index of the active handle (from the handle list)</returns>
        public int getActHandleIdx()
        {
            return activeHandleIdx;
        }

        /// <summary>
        /// Gets the sign of the ROI object, being either 
        /// 'positive' or 'negative'. This sign is used when creating a model
        /// region for matching applications from a list of ROIs.
        /// </summary>
        public int getOperatorFlag()
        {
            return OperatorFlag;
        }

        /// <summary>
        /// Sets the sign of a ROI object to be positive or negative. 
        /// The sign is used when creating a model region for matching
        /// applications by summing up all positive and negative ROI models
        /// created so far.
        /// </summary>
        /// <param name="flag">Sign of ROI object</param>
        public void setOperatorFlag(int flag)
        {
            OperatorFlag = flag;

            switch (OperatorFlag)
            {
                case ROIBase.POSITIVE_FLAG:
                    flagLineStyle = posOperation;
                    break;
                case ROIBase.NEGATIVE_FLAG:
                    flagLineStyle = negOperation;
                    break;
                default:
                    flagLineStyle = posOperation;
                    break;
            }
        }




    }
}
