using MF900_SolveWare.Business;
using MF900_SolveWare.Safe;
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
using System.Xml.Serialization;

namespace MF900_SolveWare.Offset.Data
{
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_Offset)]
    public class Data_Offset: Data_OffsetBase
    {
        [XmlIgnore]
        public const string TopModule = "上模具";
        [XmlIgnore]
        public const string BtmModule = "下模具";

        public Data_Offset()
        {
            Data_Safe_Module = new Data_Safe();
            //Data_Safe_Btm_Module = new Data_Safe();
        }

        #region 第一位置
        public string First_MotorX { get; set; }
        public string First_MotorY { get; set; }
        public double FirstPosX { get;set; }
        public double FirstPosY { get;set; }
        public double FirstPosZ { get; set; }
        public double FirstPosT { get; set; }
        #endregion

        #region 第二位置
        public string Second_MotorX { get; set; }
        public string Second_MotorY { get; set; }
        public double SecondPosX { get; set; }
        public double SecondPosY { get; set; }
        public double SecondPosZ { get; set; }
        public double SecondPosT { get; set; }
        #endregion

        #region 视觉标定点
        public string Inspect_MotorX { get; set; }
        public string Inspect_MotorY { get; set; }
        public string Inspect_MotorZ { get; set; }
        public string Inspect_MotorT { get; set; }

        public double Inspect_PosX { get; set; }
        public double Inspect_PosY { get; set; }
        public double Inspect_PosZ { get; set; }
        public double Inspect_PosT { get; set; }
        #endregion

        #region 开始位置
        public double Start_Top_PosX { get; set; }
        public double Start_Top_PosY { get; set; }
        public double Start_Top_PosZ { get; set; }
        public double Start_Top_PosT { get; set; }

        public double Start_Btm_PosX { get; set; }
        public double Start_Btm_PosY { get; set; }
        public double Start_Btm_PosZ { get; set; }
        public double Start_Btm_PosT { get; set; }
        #endregion

        #region Anchor马达
        public string Anchor_MotorX { get; set; }
        public string Anchor_MotorY { get; set; }
        public string Anchor_MotorZ { get; set; }
        public string Anchor_MotorT { get; set; }
        #endregion'

        public string Start_Based_Module { get; set; }
        public bool Move_To_Center { get; set; }
        public bool Enable_InspectKit { get; set; }
        public string InspectKitName { get; set; }
        public Data_Safe Data_Safe_Module { get; set; }
        //public Data_Safe Data_Safe_Btm_Module { get; set; }

    }

}
