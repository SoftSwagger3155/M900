using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    public enum IonFan
    {
        无效,
        仅夹层纸,
        仅工作,
        每次
    }
    [Serializable]
    public class HandPosModel
    {
        public IonFan SelectEffect { get; set; }
        public bool CoveyHandDown { get; set; }
        public bool WorkPoeceAdsorb { get; set; }
        public bool CoveyHandUp { get; set; }
        public bool AdsorbWait { get; set; }
        public bool CoveyHandShake { get; set; }
    }
}
