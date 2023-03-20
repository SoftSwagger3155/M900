using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Vision.Inspection.Base.Interface;
using SolveWare_Service_Vision.Inspection.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Data
{
    [PairAttribute(typeof(Inspect_Blob))]
    public class JobSheet_Blob : ElementBase, IJobSheetData
    {
        [Category("Blob 参数")]
        [DisplayName("Threshold 阈值")]
        public int Threshold { get; set; }

        [Category("Blob 参数")]
        [DisplayName("Vertical Measure Length 垂直切线")]
        public int VerticalMeasureLength { get; set; }

        [Category("Blob 参数")]
        [DisplayName("Horizontal Measure Length 平行切线")]
        public int HorizontalMeasureLength { get; set; }

        [Category("Blob 参数")]
        [DisplayName("Measure Sigma 高斯滤波函数")]
        public int MeasureSigma { get; set; }
    }
}
