using MF900_SolveWare.Resource;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MF900_SolveWare.Safe
{
    public class Data_Safe
    {
        public List<SafeDetailDataBase> SafeDetailDatas { get; set; }
        public Data_Safe()
        {
            SafeDetailDatas = new List<SafeDetailDataBase>();
        }
    }


    [XmlInclude(typeof(DetailData_Safe_IO))]
    [XmlInclude(typeof(DetailData_Safe_Pos))]
    public class SafeDetailDataBase: DetailDataElementBase
    {

    }

    public class DetailData_Safe_Pos: SafeDetailDataBase
    {
        public string MotorName { get; set; } = ResourceKey.Motor_Top_Z;
        public double Pos { get; set; } = 0;
    }

    public class DetailData_Safe_IO : SafeDetailDataBase
    {
        public string IOName { get; set; } = ResourceKey.OP_Buzzer;
        public string IOType { get; set; } = ConstantProperty.OutPut;
        public string TriggerMode { get; set; } = ConstantProperty.OFF;
        public int DelayTime { get; set; } = 10;
    }
}
