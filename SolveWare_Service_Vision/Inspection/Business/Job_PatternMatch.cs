using HalconDotNet;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.JobSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Inspection.Business
{
    public class Job_PatternMatch : JobFundamentalBase, IDataModulePair
    {
        public Data_InspectionKit jobParam;


        public override int Do_Job()
        {
            string errMsg = string.Empty;
            JobSheet_PatternMatch data = jobParam.JobSheet_PatternMatch_Data;
            HTuple hv_ModelID = new HTuple();

            try
            {
                do
                {
                    if (string.IsNullOrEmpty(data.ModelID))
                    {
                        errorCode = ErrorCodes.NoVisionPatternObject;
                        errMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                    //将模板照片读出来
                    string fileName = SystemPath.GetVisionPatternPath + $"{data.ModelID}.shm";
                    HOperatorSet.ReadShapeModel(fileName, out hv_ModelID);

                    //执行 Pattern Match 任务
                    errorCode = new Service_PatternMatch().Execute(data.Hv_WindowHandle, data.ModelID, data.Ho_HImage,
                                                                                                    data.AngleStart, data.AngleExtent, data.MinScale, data.MaxScale, data.MinScore, 
                                                                                                    data.NumMatches, data.MaxOverLap, data.SubPixel, data.NumLevels, data.Greediness);

                    if(errorCode != ErrorCodes.NoError)
                    {
                        errMsg += ErrorCodes.GetErrorDescription(errorCode);
                        break;
                    }

                } while (false);

            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            return ErrorCode;
        }
        public void Setup(IElement data)
        {
            this.jobParam = data as Data_InspectionKit;
        }
    }

    public class Service_PatternMatch
    {
        /// <summary>
        /// ModelID, 模板句柄
        /// AngleStart, 搜索时的起始角度
        /// AngleExtent, 搜索时的角度范围，必须与创建模板时的有交集
        /// MinScore, 最小匹配值，输出的匹配的得分Score 大于该值
        /// NumMatches, 定义要输出的匹配的最大个数
        /// MaxOverlap, 当找到的目标存在重叠时，且重叠大于该值时选择一个好的输出
        /// SubPixel, 计算精度的设置，五种模式，多选2，3
        /// NumLevels, 搜索时金字塔的层数
        /// Greediness : 贪婪度，搜索启发式，一般都设为0.9，越高速度快,容易出现找不到的情况
        /// </summary>
        /// <param name="hv_WindowHandle"></param>
        /// <param name="hv_ModelID"></param>
        /// <param name="ho_GrayImage"></param>
        /// <param name="angleStart"></param>
        /// <param name="angleExtent"></param>
        /// <param name="scaleMin"></param>
        /// <param name="scaleMax"></param>
        /// <param name="minScore"></param>
        /// <param name="numMatches"></param>
        /// <param name="maxOverLap"></param>
        /// <param name="subPixel"></param>
        /// <param name="numLevels"></param>
        /// <param name="greediness"></param>
        public int Execute(HTuple hv_WindowHandle, HTuple hv_ModelID, HObject ho_GrayImage,
                                      HTuple angleStart, HTuple angleExtent, HTuple scaleMin, HTuple scaleMax, HTuple minScore, HTuple numMatches, HTuple maxOverLap,
                                      HTuple subPixel, HTuple numLevels, HTuple greediness)
        {
            int errorCode = ErrorCodes.NoError;

            HTuple hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple();
            HTuple hv_Angle = new HTuple();
            HTuple hv_Scale = new HTuple();
            HTuple hv_Score = new HTuple();
            try
            {


                //hv_Row1.Dispose(); hv_Column1.Dispose(); hv_Angle.Dispose(); hv_Scale.Dispose(); hv_Score.Dispose();

                //HOperatorSet.FindScaledShapeModel(ho_GrayImage, hv_ModelID, -0.39, 0.78, 0.9,
                //    1.1, 0.5, 1, 0.5, "least_squares", 0, 0.9, out hv_Row1, out hv_Column1,
                //    out hv_Angle, out hv_Scale, out hv_Score);

                HOperatorSet.FindScaledShapeModel(ho_GrayImage, hv_ModelID, 
                                                                              angleStart, angleExtent, scaleMin, scaleMax, minScore, numMatches, maxOverLap, subPixel, numLevels, greediness, 
                                                                              out hv_Row1, out hv_Column1, out hv_Angle, out hv_Scale, out hv_Score);


                dev_display_shape_matching_results(hv_ModelID, "red", hv_Row1, hv_Column1,
                    hv_Angle, 1, 1, 0);
               
                set_display_font(hv_WindowHandle, 26, "mono", "true", "false");
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    disp_message(hv_WindowHandle, (("操作结果:" + hv_Row1) + new HTuple(",")) + hv_Column1,
                        "window", 10, 10, "green", "false");
                }
                HOperatorSet.DispCross(hv_WindowHandle, hv_Row1, hv_Column1, 36, 0);
               
            }
            catch (HalconException HDevExpDefaultException)
            {
                // ho_GrayImage.Dispose();

                hv_WindowHandle.Dispose();
                hv_ModelID.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Angle.Dispose();
                hv_Scale.Dispose();
                hv_Score.Dispose();

                errorCode = ErrorCodes.PatternMatchFailed;
            }
            //ho_GrayImage.Dispose();

            hv_WindowHandle.Dispose();
            hv_ModelID.Dispose();
            hv_Row1.Dispose();
            hv_Column1.Dispose();
            hv_Angle.Dispose();
            hv_Scale.Dispose();
            hv_Score.Dispose();


            return errorCode;
        }

        #region 公开方法
        // Procedures 
        // External procedures 
        // Chapter: Matching / Shape-Based
        // Short Description: Display the results of Shape-Based Matching. 
        public void dev_display_shape_matching_results(HTuple hv_ModelID, HTuple hv_Color,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_Angle, HTuple hv_ScaleR, HTuple hv_ScaleC,
            HTuple hv_Model)
        {



            // Local iconic variables 

            HObject ho_ModelContours = null, ho_ContoursAffinTrans = null;

            // Local control variables 

            HTuple hv_NumMatches = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Match = new HTuple(), hv_HomMat2DIdentity = new HTuple();
            HTuple hv_HomMat2DScale = new HTuple(), hv_HomMat2DRotate = new HTuple();
            HTuple hv_HomMat2DTranslate = new HTuple();
            HTuple hv_Model_COPY_INP_TMP = new HTuple(hv_Model);
            HTuple hv_ScaleC_COPY_INP_TMP = new HTuple(hv_ScaleC);
            HTuple hv_ScaleR_COPY_INP_TMP = new HTuple(hv_ScaleR);

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans);
            try
            {
                //This procedure displays the results of Shape-Based Matching.
                //
                hv_NumMatches.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumMatches = new HTuple(hv_Row.TupleLength()
                        );
                }
                if ((int)(new HTuple(hv_NumMatches.TupleGreater(0))) != 0)
                {
                    if ((int)(new HTuple((new HTuple(hv_ScaleR_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        {
                            HTuple ExpTmpOutVar_0;
                            HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleR_COPY_INP_TMP, out ExpTmpOutVar_0);
                            hv_ScaleR_COPY_INP_TMP.Dispose();
                            hv_ScaleR_COPY_INP_TMP = ExpTmpOutVar_0;
                        }
                    }
                    if ((int)(new HTuple((new HTuple(hv_ScaleC_COPY_INP_TMP.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        {
                            HTuple ExpTmpOutVar_0;
                            HOperatorSet.TupleGenConst(hv_NumMatches, hv_ScaleC_COPY_INP_TMP, out ExpTmpOutVar_0);
                            hv_ScaleC_COPY_INP_TMP.Dispose();
                            hv_ScaleC_COPY_INP_TMP = ExpTmpOutVar_0;
                        }
                    }
                    if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength())).TupleEqual(
                        0))) != 0)
                    {
                        hv_Model_COPY_INP_TMP.Dispose();
                        HOperatorSet.TupleGenConst(hv_NumMatches, 0, out hv_Model_COPY_INP_TMP);
                    }
                    else if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength()
                        )).TupleEqual(1))) != 0)
                    {
                        {
                            HTuple ExpTmpOutVar_0;
                            HOperatorSet.TupleGenConst(hv_NumMatches, hv_Model_COPY_INP_TMP, out ExpTmpOutVar_0);
                            hv_Model_COPY_INP_TMP.Dispose();
                            hv_Model_COPY_INP_TMP = ExpTmpOutVar_0;
                        }
                    }
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ModelID.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_ModelContours.Dispose();
                            HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelID.TupleSelect(
                                hv_Index), 1);
                        }
                        if (HDevWindowStack.IsOpen())
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.SetColor(HDevWindowStack.GetActive(), hv_Color.TupleSelect(
                                    hv_Index % (new HTuple(hv_Color.TupleLength()))));
                            }
                        }
                        HTuple end_val18 = hv_NumMatches - 1;
                        HTuple step_val18 = 1;
                        for (hv_Match = 0; hv_Match.Continue(end_val18, step_val18); hv_Match = hv_Match.TupleAdd(step_val18))
                        {
                            if ((int)(new HTuple(hv_Index.TupleEqual(hv_Model_COPY_INP_TMP.TupleSelect(
                                hv_Match)))) != 0)
                            {
                                hv_HomMat2DIdentity.Dispose();
                                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_HomMat2DScale.Dispose();
                                    HOperatorSet.HomMat2dScale(hv_HomMat2DIdentity, hv_ScaleR_COPY_INP_TMP.TupleSelect(
                                        hv_Match), hv_ScaleC_COPY_INP_TMP.TupleSelect(hv_Match), 0, 0,
                                        out hv_HomMat2DScale);
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_HomMat2DRotate.Dispose();
                                    HOperatorSet.HomMat2dRotate(hv_HomMat2DScale, hv_Angle.TupleSelect(
                                        hv_Match), 0, 0, out hv_HomMat2DRotate);
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_HomMat2DTranslate.Dispose();
                                    HOperatorSet.HomMat2dTranslate(hv_HomMat2DRotate, hv_Row.TupleSelect(
                                        hv_Match), hv_Column.TupleSelect(hv_Match), out hv_HomMat2DTranslate);
                                }
                                ho_ContoursAffinTrans.Dispose();
                                HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans,
                                    hv_HomMat2DTranslate);
                                if (HDevWindowStack.IsOpen())
                                {
                                    HOperatorSet.DispObj(ho_ContoursAffinTrans, HDevWindowStack.GetActive()
                                        );
                                }
                            }
                        }
                    }
                }
                ho_ModelContours.Dispose();
                ho_ContoursAffinTrans.Dispose();

                hv_Model_COPY_INP_TMP.Dispose();
                hv_ScaleC_COPY_INP_TMP.Dispose();
                hv_ScaleR_COPY_INP_TMP.Dispose();
                hv_NumMatches.Dispose();
                hv_Index.Dispose();
                hv_Match.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DScale.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_HomMat2DTranslate.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_ModelContours.Dispose();
                ho_ContoursAffinTrans.Dispose();

                hv_Model_COPY_INP_TMP.Dispose();
                hv_ScaleC_COPY_INP_TMP.Dispose();
                hv_ScaleR_COPY_INP_TMP.Dispose();
                hv_NumMatches.Dispose();
                hv_Index.Dispose();
                hv_Match.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DScale.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_HomMat2DTranslate.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Graphics / Text
        // Short Description: This procedure writes a text message. 
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_GenParamName = new HTuple(), hv_GenParamValue = new HTuple();
            HTuple hv_Color_COPY_INP_TMP = new HTuple(hv_Color);
            HTuple hv_Column_COPY_INP_TMP = new HTuple(hv_Column);
            HTuple hv_CoordSystem_COPY_INP_TMP = new HTuple(hv_CoordSystem);
            HTuple hv_Row_COPY_INP_TMP = new HTuple(hv_Row);

            // Initialize local and output iconic variables 
            try
            {
                //This procedure displays text in a graphics window.
                //
                //Input parameters:
                //WindowHandle: The WindowHandle of the graphics window, where
                //   the message should be displayed
                //String: A tuple of strings containing the text message to be displayed
                //CoordSystem: If set to 'window', the text position is given
                //   with respect to the window coordinate system.
                //   If set to 'image', image coordinates are used.
                //   (This may be useful in zoomed images.)
                //Row: The row coordinate of the desired text position
                //   A tuple of values is allowed to display text at different
                //   positions.
                //Column: The column coordinate of the desired text position
                //   A tuple of values is allowed to display text at different
                //   positions.
                //Color: defines the color of the text as string.
                //   If set to [], '' or 'auto' the currently set color is used.
                //   If a tuple of strings is passed, the colors are used cyclically...
                //   - if |Row| == |Column| == 1: for each new textline
                //   = else for each text position.
                //Box: If Box[0] is set to 'true', the text is written within an orange box.
                //     If set to' false', no box is displayed.
                //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
                //       the text is written in a box of that color.
                //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
                //       'true' -> display a shadow in a default color
                //       'false' -> display no shadow
                //       otherwise -> use given string as color string for the shadow color
                //
                //It is possible to display multiple text strings in a single call.
                //In this case, some restrictions apply:
                //- Multiple text positions can be defined by specifying a tuple
                //  with multiple Row and/or Column coordinates, i.e.:
                //  - |Row| == n, |Column| == n
                //  - |Row| == n, |Column| == 1
                //  - |Row| == 1, |Column| == n
                //- If |Row| == |Column| == 1,
                //  each element of String is display in a new textline.
                //- If multiple positions or specified, the number of Strings
                //  must match the number of positions, i.e.:
                //  - Either |String| == n (each string is displayed at the
                //                          corresponding position),
                //  - or     |String| == 1 (The string is displayed n times).
                //
                //
                //Convert the parameters for disp_text.
                if ((int)((new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
                {

                    hv_Color_COPY_INP_TMP.Dispose();
                    hv_Column_COPY_INP_TMP.Dispose();
                    hv_CoordSystem_COPY_INP_TMP.Dispose();
                    hv_Row_COPY_INP_TMP.Dispose();
                    hv_GenParamName.Dispose();
                    hv_GenParamValue.Dispose();

                    return;
                }
                if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
                {
                    hv_Row_COPY_INP_TMP.Dispose();
                    hv_Row_COPY_INP_TMP = 12;
                }
                if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
                {
                    hv_Column_COPY_INP_TMP.Dispose();
                    hv_Column_COPY_INP_TMP = 12;
                }
                //
                //Convert the parameter Box to generic parameters.
                hv_GenParamName.Dispose();
                hv_GenParamName = new HTuple();
                hv_GenParamValue.Dispose();
                hv_GenParamValue = new HTuple();
                if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(0))) != 0)
                {
                    if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleEqual("false"))) != 0)
                    {
                        //Display no box
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                    "box");
                                hv_GenParamName.Dispose();
                                hv_GenParamName = ExpTmpLocalVar_GenParamName;
                            }
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                    "false");
                                hv_GenParamValue.Dispose();
                                hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                            }
                        }
                    }
                    else if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleNotEqual(
                        "true"))) != 0)
                    {
                        //Set a color other than the default.
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                    "box_color");
                                hv_GenParamName.Dispose();
                                hv_GenParamName = ExpTmpLocalVar_GenParamName;
                            }
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                    hv_Box.TupleSelect(0));
                                hv_GenParamValue.Dispose();
                                hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                            }
                        }
                    }
                }
                if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(1))) != 0)
                {
                    if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleEqual("false"))) != 0)
                    {
                        //Display no shadow.
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                    "shadow");
                                hv_GenParamName.Dispose();
                                hv_GenParamName = ExpTmpLocalVar_GenParamName;
                            }
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                    "false");
                                hv_GenParamValue.Dispose();
                                hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                            }
                        }
                    }
                    else if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleNotEqual(
                        "true"))) != 0)
                    {
                        //Set a shadow color other than the default.
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                    "shadow_color");
                                hv_GenParamName.Dispose();
                                hv_GenParamName = ExpTmpLocalVar_GenParamName;
                            }
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                    hv_Box.TupleSelect(1));
                                hv_GenParamValue.Dispose();
                                hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                            }
                        }
                    }
                }
                //Restore default CoordSystem behavior.
                if ((int)(new HTuple(hv_CoordSystem_COPY_INP_TMP.TupleNotEqual("window"))) != 0)
                {
                    hv_CoordSystem_COPY_INP_TMP.Dispose();
                    hv_CoordSystem_COPY_INP_TMP = "image";
                }
                //
                if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(""))) != 0)
                {
                    //disp_text does not accept an empty string for Color.
                    hv_Color_COPY_INP_TMP.Dispose();
                    hv_Color_COPY_INP_TMP = new HTuple();
                }
                //
                HOperatorSet.DispText(hv_WindowHandle, hv_String, hv_CoordSystem_COPY_INP_TMP,
                    hv_Row_COPY_INP_TMP, hv_Column_COPY_INP_TMP, hv_Color_COPY_INP_TMP, hv_GenParamName,
                    hv_GenParamValue);

                hv_Color_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Color_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Graphics / Text
        // Short Description: Set font independent of OS 
        public void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font,
            HTuple hv_Bold, HTuple hv_Slant)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_OS = new HTuple(), hv_Fonts = new HTuple();
            HTuple hv_Style = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_AvailableFonts = new HTuple(), hv_Fdx = new HTuple();
            HTuple hv_Indices = new HTuple();
            HTuple hv_Font_COPY_INP_TMP = new HTuple(hv_Font);
            HTuple hv_Size_COPY_INP_TMP = new HTuple(hv_Size);

            // Initialize local and output iconic variables 
            try
            {
                //This procedure sets the text font of the current window with
                //the specified attributes.
                //
                //Input parameters:
                //WindowHandle: The graphics window for which the font will be set
                //Size: The font size. If Size=-1, the default of 16 is used.
                //Bold: If set to 'true', a bold font is used
                //Slant: If set to 'true', a slanted font is used
                //
                hv_OS.Dispose();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
                {
                    hv_Size_COPY_INP_TMP.Dispose();
                    hv_Size_COPY_INP_TMP = 16;
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    //Restore previous behaviour
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = ((1.13677 * hv_Size_COPY_INP_TMP)).TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = hv_Size_COPY_INP_TMP.TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Courier";
                    hv_Fonts[1] = "Courier 10 Pitch";
                    hv_Fonts[2] = "Courier New";
                    hv_Fonts[3] = "CourierNew";
                    hv_Fonts[4] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Consolas";
                    hv_Fonts[1] = "Menlo";
                    hv_Fonts[2] = "Courier";
                    hv_Fonts[3] = "Courier 10 Pitch";
                    hv_Fonts[4] = "FreeMono";
                    hv_Fonts[5] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Luxi Sans";
                    hv_Fonts[1] = "DejaVu Sans";
                    hv_Fonts[2] = "FreeSans";
                    hv_Fonts[3] = "Arial";
                    hv_Fonts[4] = "Liberation Sans";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Times New Roman";
                    hv_Fonts[1] = "Luxi Serif";
                    hv_Fonts[2] = "DejaVu Serif";
                    hv_Fonts[3] = "FreeSerif";
                    hv_Fonts[4] = "Utopia";
                    hv_Fonts[5] = "Liberation Serif";
                }
                else
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple(hv_Font_COPY_INP_TMP);
                }
                hv_Style.Dispose();
                hv_Style = "";
                if ((int)(new HTuple(hv_Bold.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Bold";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Bold.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Italic";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Slant.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Style.TupleEqual(""))) != 0)
                {
                    hv_Style.Dispose();
                    hv_Style = "Normal";
                }
                hv_AvailableFonts.Dispose();
                HOperatorSet.QueryFont(hv_WindowHandle, out hv_AvailableFonts);
                hv_Font_COPY_INP_TMP.Dispose();
                hv_Font_COPY_INP_TMP = "";
                for (hv_Fdx = 0; (int)hv_Fdx <= (int)((new HTuple(hv_Fonts.TupleLength())) - 1); hv_Fdx = (int)hv_Fdx + 1)
                {
                    hv_Indices.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Indices = hv_AvailableFonts.TupleFind(
                            hv_Fonts.TupleSelect(hv_Fdx));
                    }
                    if ((int)(new HTuple((new HTuple(hv_Indices.TupleLength())).TupleGreater(
                        0))) != 0)
                    {
                        if ((int)(new HTuple(((hv_Indices.TupleSelect(0))).TupleGreaterEqual(0))) != 0)
                        {
                            hv_Font_COPY_INP_TMP.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(
                                    hv_Fdx);
                            }
                            break;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(""))) != 0)
                {
                    throw new HalconException("Wrong value of control parameter Font");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Font = (((hv_Font_COPY_INP_TMP + "-") + hv_Style) + "-") + hv_Size_COPY_INP_TMP;
                        hv_Font_COPY_INP_TMP.Dispose();
                        hv_Font_COPY_INP_TMP = ExpTmpLocalVar_Font;
                    }
                }
                HOperatorSet.SetFont(hv_WindowHandle, hv_Font_COPY_INP_TMP);

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                throw HDevExpDefaultException;
            }
        }
        #endregion
    }
}
