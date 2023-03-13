using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    [Serializable]
    public class DebugTableTopModel
    {
        public bool IsRetryOffset { get; set; }
        public RetryCondittion SetRetryCondition { get; set; }
        public List<RetryOffsetXYZ> RetryOffsetData { get; set; }
    }

    public class RetryOffsetXYZ
    {
        public CoordXYZ UpOffset { get; set; }
        public CoordXYZ DownOffset { get; set; }
        public int TestCount { get; set; }
    }
    public class RetryCondittion
    {
        public bool Open { get; set; }
        public bool Wng4 { get; set; }
        public bool C { get; set; }
        public bool Short { get; set; }
        public bool Error { get; set; }
        public bool Aux { get; set; }
    }
    public class CoordXYZ
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
