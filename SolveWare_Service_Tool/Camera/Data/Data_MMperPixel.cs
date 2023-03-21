using SolveWare_Service_Core.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Camera.Data
{
    public class Data_MMperPixel: ElementBase
    {
        public double MMperPixel_X { get; set; }
        public bool IsReverseX { get; set; }
        public double MMperPixel_Y { get; set; }
        public bool IsReverseY { get; set; }
        public double Average_MMperPixel { get; set; }
        public Point InspectionPos { get; set; }
        public double MoveGap { get; set; }
    }
}
