using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public enum DistinguishMethod
    {
        标记偏移,
        标记次数
    }
    [Serializable]
    public class MarkerSetModel
    {
        public bool ExecuteCheckMark { get; set; } 
        public bool MarkObject { get; set; } //标记对象
        public bool NgDghMark { get; set; }  //NG加以区别标记
        public Pofloat MarkOffsetXY { get; set; }  //标记偏移
        public DistinguishMethod DghMethod { get; set; }
        public MarkerOffset MarkerOffset { get; set; }
        public MarkerCount MarkerCount { get; set; }
    }
    public class MarkerOffset
    {
        public Pofloat Open { get; set; }
        public Pofloat Short { get; set; }
        public Pofloat OpenShort { get; set; }
        public Pofloat Aux { get; set; }
        public Pofloat Error { get; set; }
        public Pofloat Skip { get; set; }
    }
    public class MarkerCount
    {
        public int Open { get; set; }
        public int Short { get; set; }
        public int OpenShort { get; set; }
        public int Aux { get; set; }
        public int Error { get; set; }
        public int Skip { get; set; }
        public Point OffsetDistanceXY { get; set; }
    }
}
