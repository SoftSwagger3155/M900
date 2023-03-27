using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Utility.Common.Motion
{
    public class Data_Motion: ElementBase
    {
        public IList<DetailData_Motion> DetailDatas { get; set; }
        public Data_Motion()
        {
            DetailDatas = new List<DetailData_Motion>();
        }
    }

    public class DetailData_Motion : DetailDataElementBase
    {

        bool enableSlowDown=false;
        string axisName;
        string slowDownType = ConstantProperty.NormalSpeed;
        double pos = 0;
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

        public bool EnableSlowDown
        {
            get => enableSlowDown;
            set => UpdateProperAction(ref enableSlowDown, value, ac: UpdateContent);
        }



        public override void UpdateContent()
        {
            this.Content = $"顺序 {Priority} 马达{AxisName} 位置 {Pos} mm" ;
            this.Content += enableSlowDown ? $" 缓速间距 {SlowDownGap} 缓速比率 {SlowDownSpeedRate}" : string.Empty ;
        }

    }
}
