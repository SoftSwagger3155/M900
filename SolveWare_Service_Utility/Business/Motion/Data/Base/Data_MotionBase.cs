using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolveWare_Service_Utility.Business.Motion.Data.Base
{
    public abstract  class Data_MotionBase: ElementBase
    {
    }

    public class Motion_DetailData : DetailDataElementBase
    {
     
        double pos = 0;
        string axisName;
        string slowDownType = ConstantProperty.NormalSpeed;
        double slowDownGap = 0;
        double slowDownSpeedRate = 100;

        
        public double Pos
        {
            get => pos;
            set
            {
                UpdateProper(ref pos, value);
                OnPropertyChanged(nameof(Content));
            }
        }
        public string AxisName
        {
            get => axisName;
            set
            {
                UpdateProper(ref axisName, value);
                OnPropertyChanged(nameof(Content));
            }
        }
        public double SlowDownGap
        {
            get => slowDownGap;
            set
            {
                UpdateProper(ref slowDownGap, value);
                OnPropertyChanged(nameof(Content));
            }
        }
        public double SlowDownSpeedRate
        {
            get => slowDownSpeedRate;
            set
            {
                UpdateProper(ref slowDownSpeedRate, value);
                OnPropertyChanged(nameof(Content));
            }
        }

        public string SlowDownType
        {
            get => slowDownType;
            set => UpdateProperAction(ref slowDownType, value, ac: UpdateContent);
        }



        public override void UpdateContent()
        {
           this.Content = $"顺序 {Priority} 马达{AxisName} 位置 {Pos} mm  缓速间距 {SlowDownGap} 缓速比率 {SlowDownSpeedRate}";
        }

    }
}
