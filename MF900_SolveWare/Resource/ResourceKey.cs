using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900_SolveWare.Resource
{
    public class ResourceKey
    {
        //IO
        public const string TowerLight_Green = "塔灯-绿";
        public const string TowerLight_Red = "塔灯-红";
        public const string TowerLight_Yellow = "塔灯-黄";


        //Motor
        public const string Motor_Top_X = "上马达X";
        public const string Motor_Top_Y = "上马达Y";
        public const string Motor_Top_Z = "上马达Y";
        public const string Motor_Top_T = "上治具T";
        public const string Motor_Btm_X = "下马达X";
        public const string Motor_Btm_Y = "下马达Y";
        public const string Motor_Btm_Z = "下马达Z";
        public const string Motor_Btm_T = "下治具T";

        //Camera
        public const string Top_Camera = "上相机";
        public const string Btm_Camera = "下相机";


        //视觉Data
        public const string Top_Camera_Btm_Prober_Mark_Point_InspectKit = "上相机 下治具 Mark点 视觉";
        public const string Top_Camera_Git_Hole_InspectKit = "上相机圆孔视觉";
        public const string Top_Camera_Mark_Point_InspectKit = "上相机Pcb Mark点 视觉";
        public const string Btm_Camera_Top_Prober_Mark_Point_InspectKit = "下相机 上治具 Mark点 视觉";
        public const string Btm_Camera_Git_Hole_InspectKit = "下相机圆孔视觉";
        public const string Btm_Camera_Mark_Point_InspectKit = "下相机Pcb Mark点 视觉";


        //OffsetData
        public const string Top_Camera_Top_Prober_OffsetData = "上相机-上治具 Offset Data";
        public const string Btm_Camera_Btm_Prober_OffsetData = "下相机-下治具 Offset Data";
        public const string Btm_Camera_Btm_Pin_OffsetData = "下相机-下顶针 Offset Data";
    }
}
