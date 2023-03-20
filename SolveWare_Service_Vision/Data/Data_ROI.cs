using SolveWare_Service_Core.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Vision.Data
{
    public class Data_ROI: ElementBase
    {
        public double Region_Row_1 { get; set; }
        public double Region_Column_1 { get; set; }
        public double Region_Row_2 { get; set; }
        public double Region_Column_2 { get; set; }
        public double Angle { get; set; }
        public double Radius { get; set; }
        public double Mid_Row { get; set; }
        public double Mid_Column { get; set; }
        public string ROI_Kind { get; set; }
    }
}
