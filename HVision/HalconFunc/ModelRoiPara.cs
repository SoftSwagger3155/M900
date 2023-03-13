using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class ModelRoiPara
    {
        public HTuple StartAngle { get; set; }
        public HTuple EndAngle { get; set; }
        public HTuple MinScore { get; set; }
        public HTuple NumMatches { get; set; }
        public HTuple MaxOverlap { get; set; }
        public HTuple SubPixel { get; set; }
        public HTuple NumLevels { get; set; } = 0;
        public HTuple Greediness { get; set; }
    }
}
