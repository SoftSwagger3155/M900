using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class HalconTool
    {
        
        /// <summary>
		/// 创建模板
		/// </summary>
		/// <param name="hWindowControl1"></param>
		/// <param name="ho_Image"></param>
		/// <param name="cmbModelName"></param>
		/// <returns></returns>
		public bool CreateModel(HWindow hWindow, HObject ho_Image, string modelFile,
            out HTuple modelRow, out HTuple modelCol)
        {
            modelRow = null; modelCol = null;
            HTuple hv_RectangleRow = null, hv_RectangleCol = null, hv_RectangleLength1 = null, hv_RectangleLength2 = null;
            HTuple RectangleRow = null, RectangleCol = null, RectangleLength1 = null, RectangleLength2 = null;
            HTuple ModelID = null;
            HTuple modelArea = null;
            HTuple hv_Area, hv_Row4, hv_Column4, hv_HomMat2DIdentity, hv_HomMat2DTranslate;
            HObject ho_Region, ho_Region1, ho_Region2, ho_FindModelRegion, ho_Mean;
            HObject ho_ImageReduced, ho_ModelContours, ho_ContoursAffinTrans, modelRgion, connModelRgion;
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_Region2);
            HOperatorSet.GenEmptyObj(out ho_FindModelRegion);
            HOperatorSet.GenEmptyObj(out modelRgion);
            HOperatorSet.GenEmptyObj(out connModelRgion);
            HOperatorSet.GenEmptyObj(out ho_Mean);
            try
            {
                HOperatorSet.ClearAllShapeModels();
                HOperatorSet.ClearWindow(hWindow);
                HOperatorSet.SetColor(hWindow, "red");
                ho_Image.DispObj(hWindow);
                HOperatorSet.WriteString(hWindow, "请绘制模板寻找区域");
                HOperatorSet.DrawRectangle1(hWindow, out hv_RectangleRow, out hv_RectangleCol, out hv_RectangleLength1, out hv_RectangleLength2);
                ho_FindModelRegion.Dispose();
                HOperatorSet.GenRectangle1(out ho_FindModelRegion, hv_RectangleRow, hv_RectangleCol, hv_RectangleLength1, hv_RectangleLength2);
                HOperatorSet.WriteRegion(ho_FindModelRegion, $"{modelFile}.reg");

                HOperatorSet.ClearWindow(hWindow);
                ho_Image.DispObj(hWindow);
                HOperatorSet.WriteString(hWindow, "请绘制模板");
                HOperatorSet.DrawRectangle1(hWindow, out RectangleRow, out RectangleCol, out RectangleLength1, out RectangleLength2);
                ho_Region.Dispose();
                HOperatorSet.GenRectangle1(out ho_Region, RectangleRow, RectangleCol, RectangleLength1, RectangleLength2);

                HOperatorSet.ClearWindow(hWindow);
                ho_Image.DispObj(hWindow);
                HOperatorSet.WriteString(hWindow, "请绘制扣除区域");
                ho_Region1.Dispose();
                HOperatorSet.DrawRectangle1(hWindow, out RectangleRow, out RectangleCol, out RectangleLength1, out RectangleLength2);
                HOperatorSet.GenRectangle1(out ho_Region1, RectangleRow, RectangleCol, RectangleLength1, RectangleLength2);
                ho_Region2.Dispose();
                HOperatorSet.Difference(ho_Region, ho_Region1, out ho_Region2);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_Image, ho_Region2, out ho_ImageReduced);
                HOperatorSet.Emphasize(ho_ImageReduced, out ho_Mean, 7, 7, 1);
                HOperatorSet.AreaCenter(ho_Region2, out hv_Area, out hv_Row4, out hv_Column4);
                HOperatorSet.SetSystem("border_shape_models", "false");

                HOperatorSet.CreateShapeModel(ho_ImageReduced, 7, (new HTuple(0)).TupleRad(), (new HTuple(360)).TupleRad(),
                    (new HTuple(0.3153)).TupleRad(), (new HTuple("none")).TupleConcat("no_pregeneration"), "use_polarity", "auto", 3, out ModelID);
                ho_ModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_ModelContours, ModelID, 1);
                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                HOperatorSet.HomMat2dTranslate(hv_HomMat2DIdentity, hv_Row4, hv_Column4, out hv_HomMat2DTranslate);
                ho_ContoursAffinTrans.Dispose();
                modelRgion.Dispose();
                HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans, hv_HomMat2DTranslate);
                HOperatorSet.GenRegionContourXld(ho_ContoursAffinTrans, out modelRgion, "filled");
                HOperatorSet.Union1(modelRgion, out connModelRgion);
                HOperatorSet.AreaCenter(connModelRgion, out modelArea, out modelRow, out modelCol);
                HOperatorSet.ClearWindow(hWindow);
                HOperatorSet.DispObj(ho_Image, hWindow);
                HOperatorSet.SetColor(hWindow, "green");
                HOperatorSet.DispObj(ho_ContoursAffinTrans, hWindow);
                HOperatorSet.WriteShapeModel(ModelID, $"{modelFile}.shm");
                HOperatorSet.WriteImage(ho_Image, "bmp", 0, modelFile);
                HOperatorSet.ClearShapeModel(ModelID);
                ho_FindModelRegion.Dispose();
                ho_Region.Dispose();
                ho_ImageReduced.Dispose();
                ho_ModelContours.Dispose();
                ho_ContoursAffinTrans.Dispose();
                return true;
            }
            catch (Exception)
            {
                ho_FindModelRegion.Dispose();
                ho_Region.Dispose();
                ho_ImageReduced.Dispose();
                ho_ModelContours.Dispose();
                ho_ContoursAffinTrans.Dispose();
                return false;
            }
        }
        /// <summary>
		/// 查找模板
		/// </summary>
		/// <param name="ho_Image"></param>
		/// <param name="hWindow"></param>
		/// <param name="d_MinScore"></param>
		/// <param name="s_ModelPath"></param>
		/// <param name="hv_HomMat2D"></param>
		/// <returns></returns>
		public bool FindShapeModel(HObject ho_Image, HWindow hWindow, double d_MinScore, string s_ModelPath,
            ref HTuple hv_HomMat2D, out HTuple hTuple2, out HTuple hTuple3, out HTuple hTuple4, out HTuple hTuple5)
        {
            hv_HomMat2D = null;
            hTuple2 = null;
            hTuple3 = null;
            HTuple modelId = null;
            HTuple row = null;
            HTuple column = null;
            HObject ho_ModelRgion;
            HOperatorSet.GenEmptyObj(out ho_ModelRgion);
            HObject hObject2;
            HOperatorSet.GenEmptyObj(out hObject2);
            HObject hObject3;
            HOperatorSet.GenEmptyObj(out hObject3);
            HObject ho_Empha;
            HOperatorSet.GenEmptyObj(out ho_Empha);
            bool result;
            try
            {
                HOperatorSet.ReadRegion(out ho_ModelRgion, s_ModelPath + ".reg");
                HOperatorSet.ReadShapeModel(s_ModelPath + ".shm", out modelId);
                HOperatorSet.ReduceDomain(ho_Image, ho_ModelRgion, out hObject2);
                HOperatorSet.MeanImage(hObject2, out ho_Empha, 9, 5);
                //HOperatorSet.Emphasize(hObject2, out ho_Empha, 7, 7, 1);
                HOperatorSet.FindShapeModel(ho_Empha, modelId, new HTuple(-20).TupleRad(), new HTuple(20).TupleRad(), d_MinScore, 1, 0.5, "least_squares", 0, 0.1,
                    out hTuple2, out hTuple3, out hTuple4, out hTuple5);
                if (hTuple5.TupleLength() != 1)
                {
                    HOperatorSet.ClearShapeModel(modelId);
                    ho_ModelRgion.Dispose();
                    hObject2.Dispose();
                    hObject3.Dispose();
                    result = false;
                }
                else
                {
                    HOperatorSet.VectorAngleToRigid(row, column, 0, hTuple2, hTuple3, hTuple4, out hv_HomMat2D);
                    HOperatorSet.GenCrossContourXld(out hObject3, hTuple2, hTuple3, 200, 0);
                    HOperatorSet.SetColor(hWindow, "green");
                    HOperatorSet.DispObj(ho_Image, hWindow);
                    //dev_display_shape_matching_results(hwd_Window.HalconWindow, hTuple, "green", hTuple2, hTuple3, hTuple4, 1, 1, 0);
                    HOperatorSet.DispObj(hObject3, hWindow);
                    HOperatorSet.ClearShapeModel(modelId);
                    ho_ModelRgion.Dispose();
                    hObject2.Dispose();
                    hObject3.Dispose();
                    result = true;
                }
                return result;
            }
            catch
            {
                ho_ModelRgion.Dispose();
                hObject2.Dispose();
                hObject3.Dispose();
                hTuple4 = null;
                hTuple5 = null;
                return false;
            }
        }

        public bool RakeCircle(string path, HObject ho_Image, HWindowControl hWindowControl, HObject ho_FindCircleRegion, int iElement, double dSigma,
            int iThreshold, string sTransition, string sSelect, out HObject ho_Region, out HTuple hv_RakeResult, out HObject ho_Circle)
        {
            HTuple hv_cRow = null, hv_cColumn = null, hv_cRadius = null;
            hv_RakeResult = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_Region);
            HObject ho_Regions, ho_Cross, ho_Mean;
            HTuple hv_Row = null, hv_Column = null, hv_Radius = null, hv_TempRow;
            HTuple hv_TempCol, hv_ResultRow, hv_ResultColumn, hv_ArcType;
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out ho_Mean);
            try
            {
                HOperatorSet.SetColor(hWindowControl.HalconWindow, "red");
                HOperatorSet.DrawCircle(hWindowControl.HalconWindow, out hv_Row, out hv_Column, out hv_Radius);
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius);
                HTuple hv_Circle = new HTuple();
                hv_Circle[0] = hv_Row;
                hv_Circle[1] = hv_Column;
                hv_Circle[2] = hv_Radius;
                HOperatorSet.WriteTuple(hv_Circle, path + ".tup");
                HOperatorSet.WriteRegion(ho_Circle, path + ".reg");
                hv_TempRow = new HTuple(); hv_TempCol = new HTuple();
                hv_TempRow = hv_TempRow.TupleConcat(hv_Row);
                hv_TempRow = hv_TempRow.TupleConcat(hv_Row + hv_Radius);
                hv_TempRow = hv_TempRow.TupleConcat(hv_Row);
                hv_TempRow = hv_TempRow.TupleConcat(hv_Row - hv_Radius);
                hv_TempRow = hv_TempRow.TupleConcat(hv_Row);

                hv_TempCol = hv_TempCol.TupleConcat(hv_Column - hv_Radius);
                hv_TempCol = hv_TempCol.TupleConcat(hv_Column);
                hv_TempCol = hv_TempCol.TupleConcat(hv_Column + hv_Radius);
                hv_TempCol = hv_TempCol.TupleConcat(hv_Column);
                hv_TempCol = hv_TempCol.TupleConcat(hv_Column - hv_Radius);
                ho_Regions.Dispose();
                HOperatorSet.Emphasize(ho_Image, out ho_Mean, 7, 7, 1);
                spoke(ho_Mean, out ho_Regions, iElement, 60, 15, dSigma, iThreshold, sTransition, sSelect, hv_TempRow,
                    hv_TempCol, "inner", out hv_ResultRow, out hv_ResultColumn, out hv_ArcType);
                if (hv_ResultColumn.Length < 3)
                {
                    return false;
                }
                ho_Circle.Dispose();
                pts_to_best_circle(out ho_Circle, hv_ResultRow, hv_ResultColumn, 0.6 * hv_ResultColumn.Length, "circle",
                    out hv_cRow, out hv_cColumn, out hv_cRadius);
                HOperatorSet.GenCrossContourXld(out ho_Cross, hv_cRow, hv_cColumn, 6, 0.785398);
                if (hv_cRow.Length != 0)
                {
                    hv_RakeResult[0] = hv_cRow.D;
                    hv_RakeResult[1] = hv_cColumn.D;
                    hv_RakeResult[2] = hv_cRadius.D;
                    ho_Cross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_Cross, hv_cRow, hv_cColumn, 10, 0);
                    HOperatorSet.DispObj(ho_Image, hWindowControl.HalconWindow);
                    HOperatorSet.DispObj(ho_Circle, hWindowControl.HalconWindow);
                    HOperatorSet.DispObj(ho_Cross, hWindowControl.HalconWindow);
                    ho_Circle.Dispose();
                    ho_Cross.Dispose();
                    GC.Collect();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
       
        public void spoke(HObject ho_Image, out HObject ho_Regions, HTuple hv_Elements, HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Sigma, HTuple hv_Threshold, HTuple hv_Transition, HTuple hv_Select, 
            HTuple hv_ROIRows, HTuple hv_ROICols, HTuple hv_Direct, out HTuple hv_ResultRow, out HTuple hv_ResultColumn, out HTuple hv_ArcType)
        {
            HObject[] array = new HObject[20];
            long num = 0L;
            HObject hObject = null;
            HObject hObject2 = null;
            HTuple hTuple = new HTuple();
            HTuple hTuple2 = new HTuple();
            HTuple hTuple3 = new HTuple();
            HTuple hTuple4 = new HTuple();
            HTuple hv_Row = new HTuple();
            HTuple hv_Row2 = new HTuple();
            HTuple hv_Column = new HTuple();
            HTuple hv_Column2 = new HTuple();
            HTuple measureHandle = new HTuple();
            HTuple hTuple5 = new HTuple();
            HTuple hTuple6 = new HTuple();
            HTuple hTuple7 = new HTuple();
            HTuple hTuple8 = new HTuple();
            HTuple t = new HTuple();
            HTuple t2 = new HTuple();
            HTuple hTuple9 = new HTuple();
            HTuple hTuple10 = new HTuple();
            HTuple hTuple11 = new HTuple();
            HTuple hTuple12 = hv_Select.Clone();
            HTuple hTuple13 = hv_Transition.Clone();
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HObject hObject3;
            HOperatorSet.GenEmptyObj(out hObject3);
            HObject hObject4;
            HOperatorSet.GenEmptyObj(out hObject4);
            HOperatorSet.GenEmptyObj(out hObject);
            HOperatorSet.GenEmptyObj(out hObject2);
            hv_ArcType = new HTuple();
            try
            {
                HTuple hTuple14;
                HTuple hTuple15;
                HOperatorSet.GetImageSize(ho_Image, out hTuple14, out hTuple15);
                ho_Regions.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Regions);
                hv_ResultRow = new HTuple();
                hv_ResultColumn = new HTuple();
                hObject3.Dispose();
                HOperatorSet.GenContourPolygonXld(out hObject3, hv_ROIRows, hv_ROICols);
                HTuple hTuple16;
                HTuple hTuple17;
                HTuple radius;
                HTuple startPhi;
                HTuple endPhi;
                HTuple pointOrder;
                HOperatorSet.FitCircleContourXld(hObject3, "algebraic", -1, 0, 0, 3, 2, out hTuple16, out hTuple17, out radius, out startPhi, out endPhi, out pointOrder);
                hObject4.Dispose();
                HOperatorSet.GenCircleContourXld(out hObject4, hTuple16, hTuple17, radius, startPhi, endPhi, pointOrder, 3);
                HTuple hTuple18;
                HTuple hTuple19;
                HOperatorSet.GetContourXld(hObject4, out hTuple18, out hTuple19);
                HTuple hTuple20;
                HOperatorSet.LengthXld(hObject4, out hTuple20);
                HTuple hTuple21;
                HOperatorSet.TupleLength(hTuple19, out hTuple21);
                if (new HTuple(hv_Elements.TupleLess(1)) != 0)
                {
                    hObject3.Dispose();
                    hObject4.Dispose();
                    hObject.Dispose();
                    hObject2.Dispose();
                }
                else
                {
                    HTuple hTuple22 = 0;
                    while (hTuple22.Continue(hv_Elements - 1, 1))
                    {
                        if (new HTuple(hTuple18.TupleSelect(0).TupleEqual(hTuple18.TupleSelect(hTuple21 - 1))) != 0)
                        {
                            HOperatorSet.TupleInt(1.0 * hTuple21 / (hv_Elements - 1) * hTuple22, out hTuple);
                            hv_ArcType = "circle";
                        }
                        else
                        {
                            HOperatorSet.TupleInt(1.0 * hTuple21 / (hv_Elements - 1) * hTuple22, out hTuple);
                            hv_ArcType = "arc";
                        }
                        if (new HTuple(hTuple.TupleGreaterEqual(hTuple21)) != 0)
                        {
                            hTuple = hTuple21 - 1;
                        }
                        hTuple2 = hTuple18.TupleSelect(hTuple);
                        hTuple3 = hTuple19.TupleSelect(hTuple);
                        if (new HTuple(new HTuple(new HTuple(hTuple2.TupleGreater(hTuple15 - 1)).TupleOr(new HTuple(hTuple2.TupleLess(0)))).TupleOr(new HTuple(hTuple3.TupleGreater(hTuple14 - 1)))).TupleOr(new HTuple(hTuple3.TupleLess(0))) == 0)
                        {
                            if (new HTuple(hv_Direct.TupleEqual("inner")) != 0)
                            {
                                HOperatorSet.TupleAtan2(-hTuple2 + hTuple16, hTuple3 - hTuple17, out hTuple4);
                                hTuple4 = new HTuple(180).TupleRad() + hTuple4;
                            }
                            else
                            {
                                HOperatorSet.TupleAtan2(-hTuple2 + hTuple16, hTuple3 - hTuple17, out hTuple4);
                            }
                            hObject.Dispose();
                            HOperatorSet.GenRectangle2(out hObject, hTuple2, hTuple3, hTuple4, hv_DetectHeight / 2, hv_DetectWidth / 2);
                            array[(int)(checked((IntPtr)num))] = ho_Regions.CopyObj(1, -1);
                            num += 1L;
                            ho_Regions.Dispose();
                            checked
                            {
                                HOperatorSet.ConcatObj(array[(int)((IntPtr)(unchecked(num - 1L)))], hObject, out ho_Regions);
                                array[(int)((IntPtr)(unchecked(num - 1L)))].Dispose();
                                num = 0L;
                                if (new HTuple(hTuple22.TupleEqual(0)) != 0)
                                {
                                    hv_Row = hTuple2 + hv_DetectHeight / 2 * (-hTuple4).TupleSin();
                                    hv_Row2 = hTuple2 - hv_DetectHeight / 2 * (-hTuple4).TupleSin();
                                    hv_Column = hTuple3 + hv_DetectHeight / 2 * (-hTuple4).TupleCos();
                                    hv_Column2 = hTuple3 - hv_DetectHeight / 2 * (-hTuple4).TupleCos();
                                    hObject2.Dispose();
                                    gen_arrow_contour_xld(out hObject2, hv_Row2, hv_Column2, hv_Row, hv_Column, 25, 25);
                                    array[(int)((IntPtr)num)] = ho_Regions.CopyObj(1, -1);
                                    unchecked
                                    {
                                        num += 1L;
                                        ho_Regions.Dispose();
                                    }
                                    HOperatorSet.ConcatObj(array[(int)((IntPtr)(unchecked(num - 1L)))], hObject2, out ho_Regions);
                                    array[(int)((IntPtr)(unchecked(num - 1L)))].Dispose();
                                    num = 0L;
                                }
                                HOperatorSet.GenMeasureRectangle2(hTuple2, hTuple3, hTuple4, hv_DetectHeight / 2, hv_DetectWidth / 2, hTuple14, hTuple15, "nearest_neighbor", out measureHandle);
                                if (new HTuple(hTuple13.TupleEqual("negative")) != 0)
                                {
                                    hTuple13 = "negative";
                                }
                                else if (new HTuple(hTuple13.TupleEqual("positive")) != 0)
                                {
                                    hTuple13 = "positive";//negative
                                }
                                else
                                {
                                    hTuple13 = "all";
                                }
                                if (new HTuple(hTuple12.TupleEqual("first")) != 0)
                                {
                                    hTuple12 = "first";
                                }
                                else if (new HTuple(hTuple12.TupleEqual("last")) != 0)
                                {
                                    hTuple12 = "last";
                                }
                                else
                                {
                                    hTuple12 = "all";
                                }
                                HOperatorSet.MeasurePos(ho_Image, measureHandle, hv_Sigma, hv_Threshold, hTuple13, hTuple12, out hTuple5, out hTuple6, out hTuple7, out hTuple8);
                                HOperatorSet.CloseMeasure(measureHandle);
                                t = 0;
                                t2 = 0;
                                hTuple9 = 0;
                                HOperatorSet.TupleLength(hTuple5, out hTuple10);
                                if (new HTuple(hTuple10.TupleLess(1)) == 0)
                                {
                                    hTuple11 = 0;
                                    while (hTuple11.Continue(hTuple10 - 1, 1))
                                    {
                                        if (new HTuple(hTuple7.TupleSelect(hTuple11).TupleAbs().TupleGreater(hTuple9)) != 0)
                                        {
                                            t = hTuple5.TupleSelect(hTuple11);
                                            t2 = hTuple6.TupleSelect(hTuple11);
                                            hTuple9 = hTuple7.TupleSelect(hTuple11).TupleAbs();
                                        }
                                        hTuple11 = hTuple11.TupleAdd(1);
                                    }
                                    if (new HTuple(hTuple9.TupleGreater(0)) != 0)
                                    {
                                        hv_ResultRow = hv_ResultRow.TupleConcat(t);
                                        hv_ResultColumn = hv_ResultColumn.TupleConcat(t2);
                                    }
                                }
                            }
                        }
                        hTuple22 = hTuple22.TupleAdd(1);
                    }
                    hObject3.Dispose();
                    hObject4.Dispose();
                    hObject.Dispose();
                    hObject2.Dispose();
                }
            }
            catch (HalconException ex)
            {
                hObject3.Dispose();
                hObject4.Dispose();
                hObject.Dispose();
                hObject2.Dispose();
                throw ex;
            }
        }
        public void spoke(HObject ho_Image, out HObject ho_Regions, HTuple hv_ROIRows, HTuple hv_ROICols,CircleRoiPara circleRoiPara,
            out HTuple hv_ResultRow, out HTuple hv_ResultColumn, out HTuple hv_ArcType)
        {
            HObject[] array = new HObject[20];
            long num = 0L;
            HObject hObject = null;
            HObject hObject2 = null;
            HTuple hTuple = new HTuple();
            HTuple hTuple2 = new HTuple();
            HTuple hTuple3 = new HTuple();
            HTuple hTuple4 = new HTuple();
            HTuple hv_Row = new HTuple();
            HTuple hv_Row2 = new HTuple();
            HTuple hv_Column = new HTuple();
            HTuple hv_Column2 = new HTuple();
            HTuple measureHandle = new HTuple();
            HTuple hTuple5 = new HTuple();
            HTuple hTuple6 = new HTuple();
            HTuple hTuple7 = new HTuple();
            HTuple hTuple8 = new HTuple();
            HTuple t = new HTuple();
            HTuple t2 = new HTuple();
            HTuple hTuple9 = new HTuple();
            HTuple hTuple10 = new HTuple();
            HTuple hTuple11 = new HTuple();
            HTuple hTuple12 = circleRoiPara.Select.Clone();
            HTuple hTuple13 = circleRoiPara.Transition.Clone();
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HObject hObject3;
            HOperatorSet.GenEmptyObj(out hObject3);
            HObject hObject4;
            HOperatorSet.GenEmptyObj(out hObject4);
            HOperatorSet.GenEmptyObj(out hObject);
            HOperatorSet.GenEmptyObj(out hObject2);
            hv_ArcType = new HTuple();
            try
            {
                HTuple hTuple14;
                HTuple hTuple15;
                HOperatorSet.GetImageSize(ho_Image, out hTuple14, out hTuple15);
                ho_Regions.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Regions);
                hv_ResultRow = new HTuple();
                hv_ResultColumn = new HTuple();
                hObject3.Dispose();
                HOperatorSet.GenContourPolygonXld(out hObject3, hv_ROIRows, hv_ROICols);
                HTuple hTuple16;
                HTuple hTuple17;
                HTuple radius;
                HTuple startPhi;
                HTuple endPhi;
                HTuple pointOrder;
                HOperatorSet.FitCircleContourXld(hObject3, "algebraic", -1, 0, 0, 3, 2, out hTuple16, out hTuple17, out radius, out startPhi, out endPhi, out pointOrder);
                hObject4.Dispose();
                HOperatorSet.GenCircleContourXld(out hObject4, hTuple16, hTuple17, radius, startPhi, endPhi, pointOrder, 3);
                HTuple hTuple18;
                HTuple hTuple19;
                HOperatorSet.GetContourXld(hObject4, out hTuple18, out hTuple19);
                HTuple hTuple20;
                HOperatorSet.LengthXld(hObject4, out hTuple20);
                HTuple hTuple21;
                HOperatorSet.TupleLength(hTuple19, out hTuple21);
                if (new HTuple(circleRoiPara.Elements.TupleLess(1)) != 0)
                {
                    hObject3.Dispose();
                    hObject4.Dispose();
                    hObject.Dispose();
                    hObject2.Dispose();
                }
                else
                {
                    HTuple hTuple22 = 0;
                    while (hTuple22.Continue(circleRoiPara.Elements - 1, 1))
                    {
                        if (new HTuple(hTuple18.TupleSelect(0).TupleEqual(hTuple18.TupleSelect(hTuple21 - 1))) != 0)
                        {
                            HOperatorSet.TupleInt(1.0 * hTuple21 / (circleRoiPara.Elements - 1) * hTuple22, out hTuple);
                            hv_ArcType = "circle";
                        }
                        else
                        {
                            HOperatorSet.TupleInt(1.0 * hTuple21 / (circleRoiPara.Elements - 1) * hTuple22, out hTuple);
                            hv_ArcType = "arc";
                        }
                        if (new HTuple(hTuple.TupleGreaterEqual(hTuple21)) != 0)
                        {
                            hTuple = hTuple21 - 1;
                        }
                        hTuple2 = hTuple18.TupleSelect(hTuple);
                        hTuple3 = hTuple19.TupleSelect(hTuple);
                        if (new HTuple(new HTuple(new HTuple(hTuple2.TupleGreater(hTuple15 - 1)).TupleOr(new HTuple(hTuple2.TupleLess(0)))).TupleOr(new HTuple(hTuple3.TupleGreater(hTuple14 - 1)))).TupleOr(new HTuple(hTuple3.TupleLess(0))) == 0)
                        {
                            if (new HTuple(circleRoiPara.Direct.TupleEqual("inner")) != 0)
                            {
                                HOperatorSet.TupleAtan2(-hTuple2 + hTuple16, hTuple3 - hTuple17, out hTuple4);
                                hTuple4 = new HTuple(180).TupleRad() + hTuple4;
                            }
                            else
                            {
                                HOperatorSet.TupleAtan2(-hTuple2 + hTuple16, hTuple3 - hTuple17, out hTuple4);
                            }
                            hObject.Dispose();
                            HOperatorSet.GenRectangle2(out hObject, hTuple2, hTuple3, hTuple4, circleRoiPara.DeteHeight / 2, circleRoiPara.DetectWidth / 2);
                            array[(int)(checked((IntPtr)num))] = ho_Regions.CopyObj(1, -1);
                            num += 1L;
                            ho_Regions.Dispose();
                            checked
                            {
                                HOperatorSet.ConcatObj(array[(int)((IntPtr)(unchecked(num - 1L)))], hObject, out ho_Regions);
                                array[(int)((IntPtr)(unchecked(num - 1L)))].Dispose();
                                num = 0L;
                                if (new HTuple(hTuple22.TupleEqual(0)) != 0)
                                {
                                    hv_Row = hTuple2 + circleRoiPara.DeteHeight / 2 * (-hTuple4).TupleSin();
                                    hv_Row2 = hTuple2 - circleRoiPara.DeteHeight / 2 * (-hTuple4).TupleSin();
                                    hv_Column = hTuple3 + circleRoiPara.DeteHeight / 2 * (-hTuple4).TupleCos();
                                    hv_Column2 = hTuple3 - circleRoiPara.DeteHeight / 2 * (-hTuple4).TupleCos();
                                    hObject2.Dispose();
                                    gen_arrow_contour_xld(out hObject2, hv_Row2, hv_Column2, hv_Row, hv_Column, 25, 25);
                                    array[(int)((IntPtr)num)] = ho_Regions.CopyObj(1, -1);
                                    unchecked
                                    {
                                        num += 1L;
                                        ho_Regions.Dispose();
                                    }
                                    HOperatorSet.ConcatObj(array[(int)((IntPtr)(unchecked(num - 1L)))], hObject2, out ho_Regions);
                                    array[(int)((IntPtr)(unchecked(num - 1L)))].Dispose();
                                    num = 0L;
                                }
                                HOperatorSet.GenMeasureRectangle2(hTuple2, hTuple3, hTuple4, circleRoiPara.DeteHeight / 2, circleRoiPara.DetectWidth / 2, hTuple14, hTuple15, "nearest_neighbor", out measureHandle);
                                if (new HTuple(hTuple13.TupleEqual("negative")) != 0)
                                {
                                    hTuple13 = "negative";
                                }
                                else if (new HTuple(hTuple13.TupleEqual("positive")) != 0)
                                {
                                    hTuple13 = "positive";//negative
                                }
                                else
                                {
                                    hTuple13 = "all";
                                }
                                if (new HTuple(hTuple12.TupleEqual("first")) != 0)
                                {
                                    hTuple12 = "first";
                                }
                                else if (new HTuple(hTuple12.TupleEqual("last")) != 0)
                                {
                                    hTuple12 = "last";
                                }
                                else
                                {
                                    hTuple12 = "all";
                                }
                                HOperatorSet.MeasurePos(ho_Image, measureHandle, circleRoiPara.Sigma, circleRoiPara.Threshold, hTuple13, hTuple12, out hTuple5, out hTuple6, out hTuple7, out hTuple8);
                                HOperatorSet.CloseMeasure(measureHandle);
                                t = 0;
                                t2 = 0;
                                hTuple9 = 0;
                                HOperatorSet.TupleLength(hTuple5, out hTuple10);
                                if (new HTuple(hTuple10.TupleLess(1)) == 0)
                                {
                                    hTuple11 = 0;
                                    while (hTuple11.Continue(hTuple10 - 1, 1))
                                    {
                                        if (new HTuple(hTuple7.TupleSelect(hTuple11).TupleAbs().TupleGreater(hTuple9)) != 0)
                                        {
                                            t = hTuple5.TupleSelect(hTuple11);
                                            t2 = hTuple6.TupleSelect(hTuple11);
                                            hTuple9 = hTuple7.TupleSelect(hTuple11).TupleAbs();
                                        }
                                        hTuple11 = hTuple11.TupleAdd(1);
                                    }
                                    if (new HTuple(hTuple9.TupleGreater(0)) != 0)
                                    {
                                        hv_ResultRow = hv_ResultRow.TupleConcat(t);
                                        hv_ResultColumn = hv_ResultColumn.TupleConcat(t2);
                                    }
                                }
                            }
                        }
                        hTuple22 = hTuple22.TupleAdd(1);
                    }
                    hObject3.Dispose();
                    hObject4.Dispose();
                    hObject.Dispose();
                    hObject2.Dispose();
                }
            }
            catch (HalconException ex)
            {
                hObject3.Dispose();
                hObject4.Dispose();
                hObject.Dispose();
                hObject2.Dispose();
                throw ex;
            }
        }

        public static void gen_arrow_contour_xld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
        {
            HObject[] array = new HObject[20];
            long num = 0L;
            HObject hObject = null;
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out hObject);
            try
            {
                ho_Arrow.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Arrow);
                HTuple hTuple;
                HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hTuple);
                HTuple hTuple2 = hTuple.TupleFind(0);
                if (new HTuple(hTuple2.TupleNotEqual(-1)) != 0)
                {
                    hTuple[hTuple2] = -1;
                }
                HTuple t = 1.0 * (hv_Row2 - hv_Row1) / hTuple;
                HTuple t2 = 1.0 * (hv_Column2 - hv_Column1) / hTuple;
                HTuple t3 = hv_HeadWidth / 2.0;
                HTuple hTuple3 = hv_Row1 + (hTuple - hv_HeadLength) * t + t3 * t2;
                HTuple hTuple4 = hv_Column1 + (hTuple - hv_HeadLength) * t2 - t3 * t;
                HTuple hTuple5 = hv_Row1 + (hTuple - hv_HeadLength) * t - t3 * t2;
                HTuple hTuple6 = hv_Column1 + (hTuple - hv_HeadLength) * t2 + t3 * t;
                HTuple hTuple7 = 0;
                while (hTuple7 <= new HTuple(hTuple.TupleLength()) - 1)
                {
                    if (new HTuple(hTuple.TupleSelect(hTuple7).TupleEqual(-1)) != 0)
                    {
                        hObject.Dispose();
                        HOperatorSet.GenContourPolygonXld(out hObject, hv_Row1.TupleSelect(hTuple7), hv_Column1.TupleSelect(hTuple7));
                    }
                    else
                    {
                        hObject.Dispose();
                        HOperatorSet.GenContourPolygonXld(out hObject, hv_Row1.TupleSelect(hTuple7).TupleConcat(hv_Row2.TupleSelect(hTuple7)).TupleConcat(hTuple3.TupleSelect(hTuple7)).TupleConcat(hv_Row2.TupleSelect(hTuple7)).TupleConcat(hTuple5.TupleSelect(hTuple7)).TupleConcat(hv_Row2.TupleSelect(hTuple7)), hv_Column1.TupleSelect(hTuple7).TupleConcat(hv_Column2.TupleSelect(hTuple7)).TupleConcat(hTuple4.TupleSelect(hTuple7)).TupleConcat(hv_Column2.TupleSelect(hTuple7)).TupleConcat(hTuple6.TupleSelect(hTuple7)).TupleConcat(hv_Column2.TupleSelect(hTuple7)));
                    }
                    array[(int)(checked((IntPtr)num))] = ho_Arrow.CopyObj(1, -1);
                    num += 1L;
                    ho_Arrow.Dispose();
                    checked
                    {
                        HOperatorSet.ConcatObj(array[(int)((IntPtr)(unchecked(num - 1L)))], hObject, out ho_Arrow);
                        array[(int)((IntPtr)(unchecked(num - 1L)))].Dispose();
                        num = 0L;
                    }
                    hTuple7++;
                }
                hObject.Dispose();
            }
            catch (HalconException ex)
            {
                hObject.Dispose();
                throw ex;
            }
        }
        public void pts_to_best_circle(out HObject ho_Circle, HTuple hv_Rows, HTuple hv_Cols, HTuple hv_ActiveNum, HTuple hv_ArcType, out HTuple hv_RowCenter, out HTuple hv_ColCenter, out HTuple hv_Radius)
        {
            HObject hObject = null;
            HTuple hTuple = new HTuple();
            HTuple endPhi = new HTuple();
            HTuple pointOrder = new HTuple();
            HTuple hTuple2 = new HTuple();
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out hObject);
            try
            {
                hv_RowCenter = 0;
                hv_ColCenter = 0;
                hv_Radius = 0;
                ho_Circle.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Circle);
                HTuple hTuple3;
                HOperatorSet.TupleLength(hv_Cols, out hTuple3);
                int a = new HTuple(hTuple3.TupleGreaterEqual(hv_ActiveNum)).TupleAnd(new HTuple(hv_ActiveNum.TupleGreater(2)));
                if (new HTuple(hTuple3.TupleGreaterEqual(hv_ActiveNum)).TupleAnd(new HTuple(hv_ActiveNum.TupleGreater(2))) != 0)
                {
                    hObject.Dispose();
                    HOperatorSet.GenContourPolygonXld(out hObject, hv_Rows, hv_Cols);
                    HOperatorSet.FitCircleContourXld(hObject, "geotukey", -1, 0, 0, 3, 2, out hv_RowCenter, out hv_ColCenter, out hv_Radius, out hTuple, out endPhi, out pointOrder);
                    HOperatorSet.TupleLength(hTuple, out hTuple2);
                    if (new HTuple(hTuple2.TupleLess(1)) != 0)
                    {
                        hObject.Dispose();
                        return;
                    }
                    if (new HTuple(hv_ArcType.TupleEqual("arc")) != 0)
                    {
                        ho_Circle.Dispose();
                        HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter, hv_Radius, hTuple, endPhi, pointOrder, 1);
                    }
                    else
                    {
                        ho_Circle.Dispose();
                        HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter, hv_Radius, 0, new HTuple(360).TupleRad(), pointOrder, 1);
                    }
                }
                hObject.Dispose();
            }
            catch (HalconException ex)
            {
                hObject.Dispose();
                throw ex;
            }
        }

        public void pts_to_best_circle2(out HObject ho_Circle, HTuple hv_Rows, HTuple hv_Cols,
      HTuple hv_ActiveNum, HTuple hv_ArcType, out HTuple hv_RowCenter, out HTuple hv_ColCenter,
      out HTuple hv_Radius, out HTuple hv_StartPhi, out HTuple hv_EndPhi, out HTuple hv_PointOrder,
      out HTuple hv_ArcAngle)
        {
            HObject ho_Contour = null;
            HTuple hv_Length = null, hv_Length1 = new HTuple();
            HTuple hv_CircleLength = new HTuple();
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            hv_StartPhi = new HTuple();
            hv_EndPhi = new HTuple();
            hv_PointOrder = new HTuple();
            hv_ArcAngle = new HTuple();
            try
            {
                //初始化
                hv_RowCenter = 0;
                hv_ColCenter = 0;
                hv_Radius = 0;
                //产生一个空的直线对象，用于保存拟合后的圆
                ho_Circle.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Circle);
                //计算边缘数量
                HOperatorSet.TupleLength(hv_Cols, out hv_Length);
                //当边缘数量不小于有效点数时进行拟合
                if ((int)((new HTuple(hv_Length.TupleGreaterEqual(hv_ActiveNum))).TupleAnd(
                    new HTuple(hv_ActiveNum.TupleGreater(2)))) != 0)
                {
                    //halcon的拟合是基于xld的，需要把边缘连接成xld
                    if ((int)(new HTuple(hv_ArcType.TupleEqual("circle"))) != 0)
                    {
                        //如果是闭合的圆，轮廓需要首尾相连
                        ho_Contour.Dispose();
                        HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows.TupleConcat(hv_Rows.TupleSelect(
                            0)), hv_Cols.TupleConcat(hv_Cols.TupleSelect(0)));
                    }
                    else
                    {
                        ho_Contour.Dispose();
                        HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows, hv_Cols);
                    }
                    //拟合圆。使用的算法是''geotukey''，其他算法请参考fit_circle_contour_xld的描述部分。
                    HOperatorSet.FitCircleContourXld(ho_Contour, "geotukey", -1, 0, 0, 3, 2,
                        out hv_RowCenter, out hv_ColCenter, out hv_Radius, out hv_StartPhi, out hv_EndPhi,
                        out hv_PointOrder);
                    //判断拟合结果是否有效：如果拟合成功，数组中元素的数量大于0
                    HOperatorSet.TupleLength(hv_StartPhi, out hv_Length1);
                    if ((int)(new HTuple(hv_Length1.TupleLess(1))) != 0)
                    {
                        ho_Contour.Dispose();

                        return;
                    }
                    //根据拟合结果，产生直线xld
                    if ((int)(new HTuple(hv_ArcType.TupleEqual("arc"))) != 0)
                    {
                        ho_Circle.Dispose();
                        HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter,
                            hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);

                        HOperatorSet.LengthXld(ho_Circle, out hv_CircleLength);
                        hv_ArcAngle = hv_EndPhi - hv_StartPhi;
                        if ((int)(new HTuple(hv_CircleLength.TupleGreater(((new HTuple(180)).TupleRad()
                            ) * hv_Radius))) != 0)
                        {
                            if ((int)(new HTuple(((hv_ArcAngle.TupleAbs())).TupleLess((new HTuple(180)).TupleRad()
                                ))) != 0)
                            {
                                if ((int)(new HTuple(hv_ArcAngle.TupleGreater(0))) != 0)
                                {
                                    hv_ArcAngle = ((new HTuple(360)).TupleRad()) - hv_ArcAngle;
                                }
                                else
                                {

                                    hv_ArcAngle = ((new HTuple(360)).TupleRad()) + hv_ArcAngle;
                                }
                            }
                        }
                        else
                        {
                            if ((int)(new HTuple(hv_CircleLength.TupleLess(((new HTuple(180)).TupleRad()
                                ) * hv_Radius))) != 0)
                            {
                                if ((int)(new HTuple(((hv_ArcAngle.TupleAbs())).TupleGreater((new HTuple(180)).TupleRad()
                                    ))) != 0)
                                {
                                    if ((int)(new HTuple(hv_ArcAngle.TupleGreater(0))) != 0)
                                    {
                                        hv_ArcAngle = hv_ArcAngle - ((new HTuple(360)).TupleRad());

                                    }
                                    else
                                    {
                                        hv_ArcAngle = ((new HTuple(360)).TupleRad()) + hv_ArcAngle;
                                    }
                                }
                            }

                        }

                    }
                    else
                    {
                        hv_StartPhi = 0;
                        hv_EndPhi = (new HTuple(360)).TupleRad();
                        hv_ArcAngle = (new HTuple(360)).TupleRad();
                        ho_Circle.Dispose();
                        HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter,
                            hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);
                    }
                }

                ho_Contour.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Contour.Dispose();

                throw HDevExpDefaultException;
            }
        }

        /// <summary>
        /// 标定
        /// </summary>
        /// <param name="hv_RRow"></param>
        /// <param name="hv_RCol"></param>
        /// <param name="hv_MachX"></param>
        /// <param name="hv_MachY"></param>
        /// <param name="file"></param>
        /// <param name="hvHomMat2D"></param>
        /// <returns></returns>
        public bool Calbration(HTuple hv_RRow, HTuple hv_RCol, HTuple hv_MachX, HTuple hv_MachY, string file, ref HTuple hvHomMat2D)
        {
            try
            {
                HOperatorSet.VectorToHomMat2d(hv_RRow, hv_RCol, hv_MachX, hv_MachY, out hvHomMat2D);
                HOperatorSet.WriteTuple(hvHomMat2D, file);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
       
        /// <summary>
        /// 拟合圆中心（旋转中心）
        /// </summary>
        /// <param name="hwWind"></param>
        /// <param name="mImage"></param>
        /// <param name="hvCRow"></param>
        /// <param name="hvCCol"></param>
        /// <returns></returns>
        public string RakeCircleRoCenet(HWindow hwWind, HObject mImage, HTuple hvCRow, HTuple hvCCol)
        {
            try
            {
                HObject ho_Circle;
                HObject ho_Cross;
                HTuple hv_RowCenter, hv_ColCenter, hv_Radius1;
                HOperatorSet.GenEmptyObj(out ho_Circle);
                HOperatorSet.GenEmptyObj(out ho_Cross);
                pts_to_best_circle(out ho_Circle, hvCRow, hvCCol, 6, "circle", out hv_RowCenter, out hv_ColCenter, out hv_Radius1);
                HOperatorSet.GenCircle(out ho_Circle, hv_RowCenter, hv_ColCenter, hv_Radius1);
                string roateCent = "Row=" + hv_RowCenter.D.ToString("0.00") + "  " + "Col=" + hv_ColCenter.D.ToString("0.00");
                ho_Cross.Dispose();
                HOperatorSet.GenCrossContourXld(out ho_Cross, hvCRow, hvCCol, 16, 0.785398);
                HOperatorSet.SetDraw(hwWind, "margin");
                HOperatorSet.DispObj(mImage, hwWind);
                ho_Circle.DispObj(hwWind);
                ho_Cross.DispObj(hwWind);
                ho_Cross.Dispose();
                ho_Circle.Dispose();
                return roateCent;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 二维码读取
        /// </summary>
        /// <param name="img">输入图像</param>
        /// <param name="_SymbolXLDs">轮廓</param>
        /// <param name="_DataCodeHandle">句柄</param>
        /// <param name="_Corde2DXLD">输出轮廓</param>
        /// <param name="_DecodedDataStrings">条码内容</param>
        /// <returns></returns>
        ///ToDo:二维码读取
        public void FindCorde2D(HImage img, HObject _SymbolXLDs, HTuple _DataCodeHandle, int _CordeNum, HTuple _ResultHandles, out HXLDCont _Corde2DXLD, out string _DecodedDataStrings)
        {
            string m_Corde2D = "";
            HObject m_SymbolXLDs = new HObject();
            HTuple m_ResultHandles = new HTuple();
            HTuple m_DecodedDataStrings = new HTuple();
            m_DecodedDataStrings = "";
            HOperatorSet.FindDataCode2d(img, out m_SymbolXLDs, _DataCodeHandle, "stop_after_result_num", _CordeNum, out m_ResultHandles, out m_DecodedDataStrings);
            //HOperatorSet.FindDataCode2d(img, out m_SymbolXLDs, _DataCodeHandle, new HTuple(), new HTuple(6), out m_ResultHandles, out m_DecodedDataStrings);
            _Corde2DXLD = new HXLDCont(m_SymbolXLDs);
            for (int i = 0; i < m_DecodedDataStrings.Length; ++i)
            {
                m_Corde2D += m_DecodedDataStrings[i] + "\r\n";
            }
            _DecodedDataStrings = m_Corde2D;
        }
    }
}
