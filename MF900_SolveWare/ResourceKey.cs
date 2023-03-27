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
        public const string Op_TowerLight_Green = "塔灯-绿";
        public const string Op_TowerLight_Red = "塔灯-红";
        public const string Op_TowerLight_Yellow = "塔灯-黄";
       

        //Motor
        public const string Motor_Top_X = "上马达X";
        public const string Motor_Top_Y = "上马达Y";
        public const string Motor_Top_Z = "上马达Y";
        public const string Motor_Top_T = "上治具T";
        public const string Motor_Btm_X = "下马达X";
        public const string Motor_Btm_Y = "下马达Y";
        public const string Motor_Btm_Z = "下马达Z";
        public const string Motor_Btm_T = "下治具T";
        public const string Motor_Table = "平台";

        
        //Camera
        public const string Top_Camera = "上相机";
        public const string Btm_Camera = "下相机";


        //视觉Data
        public const string InspectKit_Top_Camera_Btm_Prober_Mark_Point = "视觉-上相机-下治具-Mark点";
        public const string InspectKit_Top_Camera_Git_Hole = "视觉-上相机圆孔";
        public const string InspectKit_Top_Camera_Mark_Point = "视觉-上相机PCB-Mark点";
        public const string InspectKit_Btm_Camera_Top_Prober_Mark_Point = "视觉-下相机-上治具-Mark点";
        public const string InspectKit_Btm_Camera_Git_Hole = "视觉-下相机圆孔";
        public const string InspectKit_Btm_Camera_Mark_Point = "视觉-下相机PCB-Mark点";


        //OffsetData
        public const string OffsetData_Top_Camera_Top_Prober = "OffsetData-上相机-上治具";
        public const string OffsetData_Btm_Camera_Btm_Prober = "OffsetData-下相机-下治具";
        public const string OffsetData_Top_Camera_Btm_Pin = "OffsetData-上相机-下顶针";

        
        //MMperPixel
        public const string MMperPixel_TopCamera = "MMperPixel-上相机";
        public const string MMperPixel_BtmCamera = "MMperPixel-下相机";


        //Pos
        public const string Pos_TopCameraInspectGitHole = "Pos-上相机-定位圆孔";
        public const string Pos_BtmCameraInspectGitHole = "Pos-下相机-定位圆孔";
    }
}
