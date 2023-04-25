using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Common.Motion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MF900_SolveWare.MMperPixel.Data
{
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_MMperPixel)]
    public class Data_MMperPixel : ElementBase
    {
        [XmlIgnore]
        public const string TopModule = "上模具";
        [XmlIgnore]
        public const string BtmModule = "下模具";

        //下拉选单去选
        public string Name_Data_Inspection { get; set; }
        public string MotorX { get; set; }
        public string MotorY { get; set; }
        public string MotorZ { get; set; }
        public string MotorT { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }
        public double PosT { get; set; }
        public double MMperPixel_X { get; set; }
        public double MMperPixel_Y { get; set; }
        public double MMperPixel_Average { get; set; }
        public double MovePitch { get; set; }
        public bool Enable_MotorX { get; set; }
        public bool Enable_MotorY { get; set; }
        public bool IsReverseX { get; set; }
        public bool IsReverseY { get; set; }

    }
}
