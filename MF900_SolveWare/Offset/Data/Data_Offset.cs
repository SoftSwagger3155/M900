using MF900_SolveWare.Business;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Utility.Business.Offset.Base.Abstract;
using SolveWare_Service_Utility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Offset.Data
{
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_Offset)]
    public class Data_Offset: Data_OffsetBase
    {
        public Data_Offset()
        {
            Data_Protections = new MtrSafe();
        }

        #region 第一位置
        public double FirstPosX { get;set; }
        public double FirstPosY { get;set; }
        public double FirstPosZ { get; set; }
        public double FirstPosT { get; set; }
        #endregion

        #region 第二位置
        public double SecondPosX { get; set; }
        public double SecondPosY { get; set; }
        public double SecondPosZ { get; set; }
        public double SecondPosT { get; set; }
        #endregion

        public string InspectKitName { get; set; }
        public MtrSafe Data_Protections { get; set; }

    }

}
