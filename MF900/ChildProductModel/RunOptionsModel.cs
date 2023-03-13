using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    [Serializable]
    public class RunOptionsModel
    {
        public bool UseEVJigRises { get; set; }
        /// <summary>
        /// 使用辅助板
        /// </summary>
        public bool UseAidBoard { get; set; }
        /// <summary>
        /// 仅在基准校正及倾斜校正时进行背面的图像获取
        /// </summary>
        public bool CorrectionGetImage { get; set; }  //
        /// <summary>
        /// 电气检查时不获取图像
        /// </summary>
        public bool ElectricCheckNotGetImage { get; set; }  //
        public bool ExceptAutoNgReCheck { get; set; }
        /// <summary>
        /// 获取全部图像后进行电气检查
        /// </summary>
        public bool GetAllImageCheckElectric { get; set; }  //
        /// <summary>
        /// 控制于工件接触的时机
        /// </summary>
        public bool WorkpieceContactTiming { get; set; }  //
        /// <summary>
        /// 电气检查时将下治具作为支撑板使用
        /// </summary>
        public bool ElectricCheckOnDownJigAsMounting { get; set; }  //
        /// <summary>
        /// 调整自动运行中时的接触水平
        /// </summary>
        public bool DebugAutoRunLevel { get; set; }  //
        /// <summary>
        /// 自动NG再检查有效时，使用最初的电气检查时的标记定位
        /// </summary>
        public bool UseInitialElectricCheckerMarker { get; set; }  //
        /// <summary>
        /// 对每列实施基准倾斜补正
        /// </summary>
        public bool EachColumnCorrection { get; set; }  //
        public bool CommenstationAutoNgReCheck { get; set; }
        public bool RegistOfUpperJig { get; set; }
        public bool AdjustZLevelAutoByConduct { get; set; }

    }
}
