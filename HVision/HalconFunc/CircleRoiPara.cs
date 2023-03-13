using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVision
{
    public class CircleRoiPara
    {
        public HTuple Elements { get; set; }
        public HTuple DeteHeight { get; set; }
        public HTuple DetectWidth { get; set; }
        public HTuple Sigma { get; set; } = 1;
        public HTuple Threshold { get; set; }
        public HTuple Transition { get; set; }
        public HTuple Select { get; set; }
        public HTuple Direct { get; set; }


    }
}
