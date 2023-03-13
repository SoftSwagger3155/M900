using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class RoiLineData : RoiDataBase
    {
        public double StartRow { get; set; } = 0;
        public double StartColumn { get; set; } = 0;
        public double EndRow { get; set; } = 0;
        public double EndColumn { get; set; } = 0;
        public RoiLineData(double row1, double col1, double row2, double col2)
        {
            this.StartRow = row1;
            this.StartColumn = col1;
            this.EndRow = row2;
            this.EndColumn = col2;
        }
    }
}
