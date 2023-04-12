using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Common.Motion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.MMperPixel.Data
{
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_MMperPixel)]
    public class Data_MMperPixel : ElementBase
    {
        //下拉选单去选
        public string Name_Data_Inspection { get; set; }
        public Data_Motion Data_Inspection_Pos { get; set; }
        public double MovePitch { get; set; }

    }
}
