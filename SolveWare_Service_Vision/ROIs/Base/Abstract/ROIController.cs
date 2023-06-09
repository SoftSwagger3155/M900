﻿using HalconDotNet;
using SolveWare_Service_Vision.ROIs.Base.Interface;
using SolveWare_Service_Vision.ROIs.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.ROIs.Base.Abstract
{
    public delegate void FuncROIDelegate();
    public class ROIController
    {

        /// <summary>
        /// Constant for setting the ROI mode: positive ROI sign.
        /// </summary>
        public const int MODE_ROI_POS = 21;

        /// <summary>
        /// Constant for setting the ROI mode: negative ROI sign.
        /// </summary>
        public const int MODE_ROI_NEG = 22;

        /// <summary>
        /// Constant for setting the ROI mode: no model region is computed as
        /// the sum of all ROI objects.
        /// </summary>
        public const int MODE_ROI_NONE = 23;

        /// <summary>Constant describing an update of the model region</summary>
        public const int EVENT_UPDATE_ROI = 50;

        public const int EVENT_CHANGED_ROI_SIGN = 51;

        /// <summary>Constant describing an update of the model region</summary>
        public const int EVENT_MOVING_ROI = 52;

        public const int EVENT_DELETED_ACTROI = 53;

        public const int EVENT_DELETED_ALL_ROIS = 54;

        public const int EVENT_ACTIVATED_ROI = 55;

        public const int EVENT_CREATED_ROI = 56;



        private ROIBase roiMode;
        private int stateROI;
        private double currX, currY;


        /// <summary>Index of the active ROI object</summary>
        public int activeROIidx;
        public int deletedIdx;

        /// <summary>List containing all created ROI objects so far</summary>
        public List<ROIBase> ROIList;
        [NonSerialized]
        /// <summary>
        /// Region obtained by summing up all negative 
        /// and positive ROI objects from the ROIList 
        /// </summary>
        public HRegion ModelROI;

        private string activeCol = "green";
        private string activeHdlCol = "red";
        private string inactiveCol = "blue";

        /// <summary>
        /// Reference to the HWndCtrl, the ROI Controller is registered to
        /// </summary>
        public HWndCtrl viewController;

        /// <summary>
        /// Delegate that notifies about changes made in the model region
        /// </summary>
        public IconicDelegate NotifyRCObserver;

        /// <summary>Constructor</summary>
        public ROIController()
        {
            stateROI = MODE_ROI_NONE;
            ROIList = new List<ROIBase>();
            activeROIidx = -1;
            ModelROI = new HRegion();
            NotifyRCObserver = new IconicDelegate(dummyI);
            deletedIdx = -1;
            currX = currY = -1;
        }

        /// <summary>Registers the HWndCtrl to this ROIController instance</summary>
        public void setViewController(HWndCtrl view)
        {
            viewController = view;
        }

        /// <summary>Gets the ModelROI object</summary>
        public HRegion getModelRegion()
        {
            return ModelROI;
        }

        /// <summary>Gets the List of ROIs created so far</summary>
        public List<ROIBase> getROIList()
        {
            return ROIList;
        }

        /// <summary>Get the active ROI</summary>
        public ROIBase getActiveROI()
        {
            if (activeROIidx != -1)
                return ROIList[activeROIidx];

            return null;
        }

        public int getActiveROIIdx()
        {
            return activeROIidx;
        }

        public void setActiveROIIdx(int active)
        {
            activeROIidx = active;
        }

        public int getDelROIIdx()
        {
            return deletedIdx;
        }

        /// <summary>
        /// To create a new ROI object the application class initializes a 
        /// 'seed' ROI instance and passes it to the ROIController.
        /// The ROIController now responds by manipulating this new ROI
        /// instance.
        /// </summary>
        /// <param name="r">
        /// 'Seed' ROI object forwarded by the application forms class.
        /// </param>
        public void setROIShape(ROIBase r)
        {
            roiMode = r;
            roiMode.setOperatorFlag(stateROI);
        }





        /// <summary>
        /// Sets the sign of a ROI object to the value 'mode' (MODE_ROI_NONE,
        /// MODE_ROI_POS,MODE_ROI_NEG)
        /// </summary>
        public void setROISign(int mode)
        {
            stateROI = mode;

            if (activeROIidx != -1)
            {
                ((ROIBase)ROIList[activeROIidx]).setOperatorFlag(stateROI);
                viewController.repaint();
                NotifyRCObserver(ROIController.EVENT_CHANGED_ROI_SIGN);
            }
        }

        /// <summary>
        /// Removes the ROI object that is marked as active. 
        /// If no ROI object is active, then nothing happens. 
        /// </summary>
        public void removeActive()
        {
            if (activeROIidx != -1)
            {
                ROIList.RemoveAt(activeROIidx);
                deletedIdx = activeROIidx;
                activeROIidx = -1;
                viewController.repaint();
                NotifyRCObserver(EVENT_DELETED_ACTROI);
            }
        }




        /// <summary>
        /// Calculates the ModelROI region for all objects contained 
        /// in ROIList, by adding and subtracting the positive and 
        /// negative ROI objects.
        /// </summary>
        public bool defineModelROI()
        {
            HRegion tmpAdd, tmpDiff, tmp;
            double row, col;

            if (stateROI == MODE_ROI_NONE)
                return true;

            tmpAdd = new HRegion();
            tmpDiff = new HRegion();
            tmpAdd.GenEmptyRegion();
            tmpDiff.GenEmptyRegion();

            for (int i = 0; i < ROIList.Count; i++)
            {
                switch (((ROIBase)ROIList[i]).getOperatorFlag())
                {
                    case ROIBase.POSITIVE_FLAG:
                        tmp = ((ROIBase)ROIList[i]).getRegion();
                        tmpAdd = tmp.Union2(tmpAdd);
                        break;
                    case ROIBase.NEGATIVE_FLAG:
                        tmp = ((ROIBase)ROIList[i]).getRegion();
                        tmpDiff = tmp.Union2(tmpDiff);
                        break;
                    default:
                        break;
                }//end of switch
            }//end of for

            ModelROI = null;

            if (tmpAdd.AreaCenter(out row, out col) > 0)
            {
                tmp = tmpAdd.Difference(tmpDiff);
                if (tmp.AreaCenter(out row, out col) > 0)
                    ModelROI = tmp;
            }

            //in case the set of positiv and negative ROIs dissolve 
            if (ModelROI == null || ROIList.Count == 0)
                return false;

            return true;
        }


        /// <summary>
        /// Clears all variables managing ROI objects
        /// </summary>
        public void reset()
        {
            ROIList.Clear();
            activeROIidx = -1;
            ModelROI = null;
            roiMode = null;
            NotifyRCObserver(EVENT_DELETED_ALL_ROIS);
        }


        /// <summary>
        /// Deletes this ROI instance if a 'seed' ROI object has been passed
        /// to the ROIController by the application class.
        /// 
        /// </summary>
        public void resetROI()
        {
            activeROIidx = -1;
            roiMode = null;
        }

        /// <summary>Defines the colors for the ROI objects</summary>
        /// <param name="aColor">Color for the active ROI object</param>
        /// <param name="inaColor">Color for the inactive ROI objects</param>
        /// <param name="aHdlColor">
        /// Color for the active handle of the active ROI object
        /// </param>
        public void setDrawColor(string aColor,
                                   string aHdlColor,
                                   string inaColor)
        {
            if (aColor != "")
                activeCol = aColor;
            if (aHdlColor != "")
                activeHdlCol = aHdlColor;
            if (inaColor != "")
                inactiveCol = inaColor;
        }


        /// <summary>
        /// Paints all objects from the ROIList into the HALCON window
        /// </summary>
        /// <param name="window">HALCON window</param>
        public void paintData(HalconDotNet.HWindow window, bool isMetrologyDisplayed)
        {
            window.SetDraw("margin");
            window.SetLineWidth(4);

            if (ROIList.Count > 0)
            {
                window.SetColor(inactiveCol);
                window.SetDraw("margin");

                for (int i = 0; i < ROIList.Count; i++)
                {
                    window.SetLineStyle(((ROIBase)ROIList[i]).flagLineStyle);
                    ((ROIBase)ROIList[i]).draw(window, isMetrologyDisplayed);
                }

                if (activeROIidx != -1)
                {
                    window.SetColor(activeCol);
                    window.SetLineStyle(((ROIBase)ROIList[activeROIidx]).flagLineStyle);
                    ((ROIBase)ROIList[activeROIidx]).draw(window, isMetrologyDisplayed);

                    window.SetColor(activeHdlCol);
                    ((ROIBase)ROIList[activeROIidx]).displayActive(window);
                }
            }
        }




        public void createExistingROI(HTuple RoiParameters)
        {
            if (roiMode != null)             //either a new ROI object is created
            {
                roiMode.createROI(RoiParameters);
                ROIList.Add(roiMode);
                roiMode = null;
                activeROIidx = ROIList.Count - 1;
                viewController.repaint();


                NotifyRCObserver(ROIController.EVENT_CREATED_ROI);
            }
        }



        /// <summary>
        /// Reaction of ROI objects to the 'mouse button down' event: changing
        /// the shape of the ROI and adding it to the ROIList if it is a 'seed'
        /// ROI.
        /// </summary>
        /// <param name="imgX">x coordinate of mouse event</param>
        /// <param name="imgY">y coordinate of mouse event</param>
        /// <returns></returns>
        public int mouseDownAction(double imgX, double imgY)
        {
            int idxROI = -1;
            double max = 10000, dist = 0;
            double epsilon = 35.0;          //maximal shortest distance to one of
                                            //the handles

            if (roiMode != null)             //either a new ROI object is created
            {
                roiMode.createROI(imgX, imgY);
                ROIList.Add(roiMode);
                roiMode = null;
                activeROIidx = ROIList.Count - 1;
                viewController.repaint();

                NotifyRCObserver(ROIController.EVENT_CREATED_ROI);
            }
            else if (ROIList.Count > 0)     // ... or an existing one is manipulated
            {
                activeROIidx = -1;

                for (int i = 0; i < ROIList.Count; i++)
                {
                    dist = ((ROIBase)ROIList[i]).distToClosestHandle(imgX, imgY);
                    if ((dist < max) && (dist < epsilon))
                    {
                        max = dist;
                        idxROI = i;
                    }
                }//end of for

                if (idxROI >= 0)
                {
                    activeROIidx = idxROI;
                    NotifyRCObserver(ROIController.EVENT_ACTIVATED_ROI);
                }

                viewController.repaint();
            }
            return activeROIidx;
        }

        /// <summary>
        /// Reaction of ROI objects to the 'mouse button move' event: moving
        /// the active ROI.
        /// </summary>
        /// <param name="newX">x coordinate of mouse event</param>
        /// <param name="newY">y coordinate of mouse event</param>
        public void mouseMoveAction(double newX, double newY)
        {
            if ((newX == currX) && (newY == currY))
                return;

            ((ROIBase)ROIList[activeROIidx]).moveByHandle(newX, newY);
            viewController.repaint();
            currX = newX;
            currY = newY;
            NotifyRCObserver(ROIController.EVENT_MOVING_ROI);
        }


        /***********************************************************/
        public void dummyI(int v)
        {
        }

        ROIBase cross = null;
        public void GenerateCrossLine(double imgX, double imgY)
        {
            cross = new ROI_CrossLine();
            cross.createROI(imgX, imgY);
            ROIList.Add(cross);
            roiMode = null;
            activeROIidx = ROIList.Count - 1;
            viewController.repaint();

            NotifyRCObserver(ROIController.EVENT_CREATED_ROI);
        }

        public void ClearCrossLine()
        {
            if (cross == null) return;
            ROIList.Remove(cross);
            activeROIidx = -1;
            viewController.repaint();
            NotifyRCObserver(ROIController.EVENT_DELETED_ACTROI);
            cross = null;
        }

    }
}
