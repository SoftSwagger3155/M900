using MotionCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    public class DeviceBaseParaModel
    {
        public Pofloat BasePointXY { get; set; }
        /// <summary>
        /// 上模避让位
        /// </summary>
        public AxisPoints UpAvoidPoints { get; set; }
        /// <summary>
        /// 下模避让位
        /// </summary>
        public AxisPoints DownAvoidPoints { get; set; }
        /// <summary>
        /// 平台高度
        /// </summary>
        public float PlatformHeight { get; set; }
    }
}
