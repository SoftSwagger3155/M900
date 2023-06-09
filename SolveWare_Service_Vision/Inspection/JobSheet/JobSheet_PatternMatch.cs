﻿using HalconDotNet;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Utility.Heler.Converters;
using SolveWare_Service_Vision.Inspection.Base.Abstract;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using SolveWare_Service_Vision.Inspection.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolveWare_Service_Vision.Inspection.JobSheet
{
    [PairAttribute(typeof(Job_PatternMatch))]
    public class JobSheet_PatternMatch : JobSheetDataBase
    {
        public JobSheet_PatternMatch(): base()
        {
            
        }

        [Browsable(false)]
        public string ModelID { get; set; }
     
        [Category("模板 学习 参数")]
        [DisplayName("1. 开始角度-Angle Start")]
        public double AngleStart { get; set; }

        [Category("模板 学习 参数")]
        [DisplayName("2. 角度范围-Angle Extent")]
        public double AngleExtent { get; set; }

        [Category("模板 学习 参数")]
        [DisplayName("3. 最小规模-MinScale")]
        [TypeConverter(typeof(DoubleConverter_DropDown_ScaleMin))]
        public double MinScale { get; set; }

        [Category("模板 学习 参数")]
        [DisplayName("4. 最大规模-MaxScale")]
        [TypeConverter(typeof(DoubleConverter_DropDown_ScaleMax))]
        public double MaxScale { get; set; }

        [Category("模板 学习 参数")]
        [DisplayName("5. 设置模板优化-Optimization")]
        [TypeConverter(typeof(StringConverter_DropDown_Optimization))]
        public string Optimization { get; set; }

        [Category("模板 学习 参数")]
        [DisplayName("6. 匹配方法设置-Metric")]
        [TypeConverter(typeof(StringConverter_DropDown_Metric))]
        public string Metric { get; set; }

        [Category("模板 学习 参数")]
        [DisplayName("7. 对比度-Contrast")]
        [TypeConverter(typeof(IntConverter_DropDown_Contrast))]
        public int Contrast { get; set; } = 10;

        [Category("模板 学习 参数")]
        [DisplayName("8. 最小对比度-MinContrast")]
        [TypeConverter(typeof(IntConverter_DropDown_MinContrast))]
        public int MinContrast { get; set; } = 30;


        [Category("寻找 模板 参数")]
        [DisplayName("1. 最小匹配值-Min Scorce")]
        [TypeConverter(typeof(DoubleConverter_DropDown_MinScore))]
        public double MinScore { get; set; } = 0.8;

        [Category("寻找 模板 参数")]
        [DisplayName("2. 匹配最大个数-Num Matches")]
        [TypeConverter(typeof(IntConverter_DropDown_NumMatches))]
        public int NumMatches { get; set; } = 1;

        [Category("寻找 模板 参数")]
        [DisplayName("3. 金字塔层数-Num Levels")]
        //[TypeConverter(typeof(StringConverter_DropDown_NumLevels))]
        public int NumLevels { get; set; } = 0;

        [Category("寻找 模板 参数")]
        [DisplayName("4. 亚像素精度-SubPixel")]
        [TypeConverter(typeof(StringConverter_DropDown_SubPixel))]
        public string SubPixel { get; set; }

        [Category("寻找 模板 参数")]
        [DisplayName("5. 重迭最大个数-Max OverLap")]
        [TypeConverter(typeof(IntConverter_DropDown_MaxOverlap))]
        public double MaxOverLap { get; set; } = 0.5;

        [Category("寻找 模板 参数")]
        [DisplayName("6. 贪婪度-Greediness")]
        [TypeConverter(typeof(DoubleConverter_DropDown_Greediness))]
        public double Greediness { get; set; }


        [Category("Blob 参数")]
        [DisplayName("1. Threshold 阈值")]
        public int Threshold { get; set; }

        [Category("Blob 参数")]
        [DisplayName("2. Transition  转移方向")]
        [TypeConverter(typeof(StringConverter_DropDown_TransitionDirection))]
        public string TransitionDirection { get; set; } = "positive";

        [Category("Blob 参数")]
        [DisplayName("3. Vertical Measure Length 垂直切线")]
        [TypeConverter(typeof(IntConverter_DropDown_VerticalMeasureLength))]
        public int VerticalMeasureLength { get; set; } = 20;

        [Category("Blob 参数")]
        [DisplayName("4. Horizontal Measure Length 平行切线")]
        [TypeConverter(typeof(IntConverter_DropDown_HorizontalMeasureLength))]
        public int HorizontalMeasureLength { get; set; } = 5;

        [Category("Blob 参数")]
        [DisplayName("5. Measure Sigma 高斯滤波函数")]
        [TypeConverter(typeof(DoubleConverter_DropDown_MeasureSigma))]
        public double MeasureSigma { get; set; } = 1.0;

        [XmlIgnore]
        [Browsable(false)]
        public HTuple Hv_WindowHandle { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public HObject Ho_HImage { get; set; }


        [Browsable(false)]
        public double Radius_PatternMatch { get; set; }
    }
}
