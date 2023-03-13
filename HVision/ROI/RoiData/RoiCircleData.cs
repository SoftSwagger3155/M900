using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class RoiCircleData: RoiDataBase
    {
        public double Row { get; set; } = 0;
        public double Column { get; set; } = 0;
        public double Radius { get; set; } = 0;
        
        public RoiCircleData(double row, double col, double radius) 
        {
            this.Row = row;
            this.Column = col;
            this.Radius = radius;
        }

    }
}
