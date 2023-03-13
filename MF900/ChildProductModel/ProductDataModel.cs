using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    [Serializable]
    public class ProductDataModel
    {
        public bool UseNowProduct { get; set; }  //利用当前的程序
        public bool SubtendFaceDisop { get; set; }  //对向面处理
        public bool RegionFaceDisop { get; set; }  //区面处理
        public Point ProductXY { get; set; }  //产品数
        public Pofloat CqtFaceStepXY { get; set; }  //顺向面处理节距
        public Pofloat BwdFaceSkewingXY { get; set; }//逆向面处理偏移
        public Pofloat ReferencePositionXY { get; set; }//工件台面基准位置
        public Point RegionCount { get; set; }
        public Pofloat RegionSetp { get; set; }
        public Pofloat CkeckFrontRefPosXY { get; set; } //检查基准位置正面
        public Pofloat ChekBackSideRfePosXY { get; set; }  //检查基准位置反面
        public Pofloat PosMarkTableXY1 { get; set; }  //表1
        public Pofloat PosMarkTableXY2 { get; set; }  //表2
        public Pofloat BackSideXY1 { get; set; }  //背面1
        public Pofloat BackSideXY2 { get; set; }  //背面1
        
    }
}
