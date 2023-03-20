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
    [PairAttribute(typeof(Inspect_PatternMatch))]
    public class JobSheet_PatternMatch : ElementBase, IJobSheetData
    {
        public JobSheet_PatternMatch(): base()
        {
            
        }

        public string ModelID { get; set; }
     
        [Category("Pattern Match 参数")]
        [DisplayName("开始角度-Angle Start")]
        public double AngleStart { get; set; }

        [Category("Pattern Match 参数")]
        [DisplayName("角度范围-Angle Extent")]
        public double AngleExtent { get; set; }

        [Category("Pattern Match 参数")]
        [DisplayName("最小匹配值-Min Scorce")]
        public double MinScore { get; set; }

        [Category("Pattern Match 参数")]
        [DisplayName("匹配最大个数-Num Matches")]
        public double NumMatches { get; set; }

        [Category("Pattern Match 参数")]
        [DisplayName("金字塔层数-Num Levels")]
        public double NumLevels { get; set; }

        [Category("Pattern Match 参数")]
        [DisplayName("亚像素精度-SubPixel")]
        public double SubPixel { get; set; }

        [Category("Pattern Match 参数")]
        [DisplayName("重迭最大个数-Max OverLap")]
        public double MaxOverLap { get; set; }
    }
}
