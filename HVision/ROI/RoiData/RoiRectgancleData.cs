using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class RoiRectgancleData:RoiDataBase
    {
        public double Row1 { get; set; } = 0;
        public double Column1 { get; set; } = 0;
        public double Row2 { get; set; } = 0;
        public double Column2 { get; set; } = 0;
        public RoiRectgancleData(double row1, double col1, double row2, double col2)
        {
            this.Row1 = row1;
            this.Column1 = col1;
            this.Row2 = row2;
            this.Column2 = col2;
        }
    }
}
