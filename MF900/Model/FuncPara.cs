using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    [Serializable]
    public class FuncPara
    {
        public string RunModelSelect { get; set; }
        public string ProgramExecution { get; set; }
        public string WorkHolding { get; set; }
        public bool ChekResultMark { get; set; }
        public bool ChekFinishMark { get; set; }
        public bool InterleafControl { get; set; }
        public string ProvideOrOut { get; set; }
        /// <summary>
        /// 电气检查错误忽视
        /// </summary>
        public bool ElectrCkeckErrorOverlook { get; set; }
        /// <summary>
        /// 数据代码错误忽视
        /// </summary>
        public bool DataCodeCheckErrorOverlook { get; set; }
        /// <summary>
        /// 图像错误忽视
        /// </summary>
        public bool ImageErrorOverlook { get; set; }
        /// <summary>
        /// 图像异常按当前坐标检查
        /// </summary>
        public bool ImageErrorNowPointsCheck { get; set; }
        public float VelPrecent { get; set; }
        public float AccPrecent { get; set; }
    }
   
}
