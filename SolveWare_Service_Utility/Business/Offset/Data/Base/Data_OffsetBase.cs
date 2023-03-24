using SolveWare_Service_Core.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Business.Offset.Data.Base
{
    public class Data_OffsetBase : ElementBase
    {
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public bool IsReverseX { get; set; }
        public bool IsReverseY { get; set; }
        public double ManualOffsetX { get; set; }
        public double ManualOffsetY { get; set; }
    }
}
