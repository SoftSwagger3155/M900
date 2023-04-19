using MF900_SolveWare.Safe;
using SolveWare_Service_Core.Attributes;
using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.WorldCenter.Data
{
    [ResourceBaseAttribute(ConstantProperty.ResourceKey_WorldCenter)]
    public class Data_GlobalWorldCenter: ElementBase
    {
        public Data_GlobalWorldCenter()
        {
            Data_Safe_Top_Module = new Data_Safe();
            Data_Safe_Btm_Module = new Data_Safe();
        }

        #region 上模具位置
        private double top_Module_PosX;
        public double Top_Module_PosX
        {
            get => top_Module_PosX;
            set=> UpdateProper(ref  top_Module_PosX, value);
        }

        private double top_Module_PosY;
        public double Top_Module_PosY
        {
            get => top_Module_PosY;
            set => UpdateProper(ref top_Module_PosY, value);
        }

        private double top_Module_PosZ;
        public double Top_Module_PosZ
        {
            get => top_Module_PosZ;
            set => UpdateProper(ref top_Module_PosZ, value);
        }

        private double top_Module_PosT;
        public double Top_Module_PosT
        {
            get => top_Module_PosT;
            set => UpdateProper(ref top_Module_PosT, value);
        }
        #endregion

        #region 上模具视觉
        private string top_Module_InspectKit_Name;
        public string Top_Module_InspectKit_Name
        {
            get=> top_Module_InspectKit_Name;
            set=> UpdateProper(ref top_Module_InspectKit_Name, value);
        }

        private bool top_Module_Move_To_Center = true;
        public bool Top_Module_Move_To_Center
        {
            get => top_Module_Move_To_Center;
            set=> UpdateProper(ref top_Module_Move_To_Center, value);
        }
        #endregion

        #region 下模具位置
        private double btm_Module_PosX;
        public double Btm_Module_PosX
        {
            get => btm_Module_PosX;
            set => UpdateProper(ref btm_Module_PosX, value);
        }

        private double btm_Module_PosY;
        public double Btm_Module_PosY
        {
            get => btm_Module_PosY;
            set => UpdateProper(ref btm_Module_PosY, value);
        }

        private double btm_Module_PosZ;
        public double Btm_Module_PosZ
        {
            get => btm_Module_PosZ;
            set => UpdateProper(ref btm_Module_PosZ, value);
        }

        private double btm_Module_PosT;
        public double Btm_Module_PosT
        {
            get => btm_Module_PosT;
            set => UpdateProper(ref btm_Module_PosT, value);
        }
        #endregion

        #region 下模具视觉
        private string btm_Module_InspectKit_Name;
        public string Btm_Module_InspectKit_Name
        {
            get => btm_Module_InspectKit_Name;
            set => UpdateProper(ref btm_Module_InspectKit_Name, value);
        }

        private bool btm_Module_Move_To_Center = true;
        public bool Btm_Module_Move_To_Center
        {
            get => btm_Module_Move_To_Center;
            set => UpdateProper(ref btm_Module_Move_To_Center, value);
        }
        #endregion

        #region 上模具世异中心
        private double top_WorldCenter_PosX;
        public double Top_WorldCenter_PosX
        {
            get => top_WorldCenter_PosX;
            set => UpdateProper(ref top_WorldCenter_PosX, value);
        }
        private double top_WorldCenter_PosY;
        public double Top_WorldCenter_PosY
        {
            get => top_WorldCenter_PosY;
            set => UpdateProper(ref top_WorldCenter_PosY, value);
        }
        private double top_WorldCenter_PosZ;
        public double Top_WorldCenter_PosZ
        {
            get => top_WorldCenter_PosZ;
            set => UpdateProper(ref top_WorldCenter_PosZ, value);
        }
        private double top_WorldCenter_PosT;
        public double Top_WorldCenter_PosT
        {
            get => top_WorldCenter_PosT;
            set => UpdateProper(ref top_WorldCenter_PosT, value);
        }
        #endregion

        #region 下模具世界中心
        private double btm_WorldCenter_PosX;
        public double Btm_WorldCenter_PosX
        {
            get => btm_WorldCenter_PosX;
            set => UpdateProper(ref btm_WorldCenter_PosX, value);
        }
        private double btm_WorldCenter_PosY;
        public double Btm_WorldCenter_PosY
        {
            get => btm_WorldCenter_PosY;
            set => UpdateProper(ref btm_WorldCenter_PosY, value);
        }
        private double btm_WorldCenter_PosZ;
        public double Btm_WorldCenter_PosZ
        {
            get => btm_WorldCenter_PosZ;
            set => UpdateProper(ref btm_WorldCenter_PosZ, value);
        }
        private double btm_WorldCenter_PosT;
        public double Btm_WorldCenter_PosT
        {
            get => btm_WorldCenter_PosT;
            set => UpdateProper(ref btm_WorldCenter_PosT, value);
        }
        #endregion

        #region 安全措施
        public Data_Safe Data_Safe_Top_Module { get; set; }
        public Data_Safe Data_Safe_Btm_Module { get; set; }
        #endregion
    }
}
