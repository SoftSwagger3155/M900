using Microsoft.SqlServer.Server;
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
        //OutPut_IO
        public const string Op_TowerLight_Red = "塔灯-红";
        public const string Op_TowerLight_Yellow = "塔灯-黄";
        public const string Op_TowerLight_Green = "塔灯-绿";
        public const string OP_Buzzer = "蜂鸣器";
        public const string Op_StartButton = "启动按钮";
        public const string Op_ZeroButton = "归零按钮";
        public const string Op_StopButton = "停止按钮";
        public const string Op_ResetButton = "复位按钮";
        public const string Op_LeftX_RailTighten = "左X向导轨固定气缸紧";
        public const string Op_LeftX_RailLoosen = "左X向导轨固定气缸松";
        public const string Op_RightX_RailTighten = "右X向导轨固定气缸紧";
        public const string Op_RightX_RailLoosen = "右X向导轨固定气缸松";
        public const string Op_Y_RailTighten = "Y向导轨固定气缸紧";
        public const string Op_Y_RailLoosen = "Y向导轨固定气缸松";
        public const string Op_PCB_Clamp = "软板夹紧气缸紧";
        public const string Op_PCB_Loosen = "软板夹紧气缸松";
        public const string Op_X_StretchAirOn = "X向拉伸气缸伸";
        public const string Op_X_StretchAirOff = "X向拉伸气缸缩";
        public const string Op_Y_StretchAirOn = "Y向拉伸气缸伸";
        public const string Op_Y_StretchAirOff = "Y向拉伸气缸缩";
        public const string Op_MarkeInkBoxOn = "打标墨盒盖开";
        public const string Op_MarkeInkBoxOff = "打标墨盒盖关";
        public const string Op_UpMarkeOn = "上模打标气缸_伸";
        public const string Op_DownMarkOn = "下模打标气缸_伸";
        public const string Op_UpJipClampAirOn = "上模治具夹紧气缸紧";
        public const string Op_UpJipClampAirOff = "上模治具夹紧气缸松";
        public const string Op_DownJipClampAirOn = "下模治具夹紧气缸紧";
        public const string Op_DownJipClampAirOff = "下模治具夹紧气缸松";



        //Input_I0
        public const string In_Start = "启动";
        public const string In_Zero = "归零";
        public const string In_Stop = "停止";
        public const string In_Reset = "复位";
        public const string In_E_Stop = "急停1";
        public const string In_MoveFornt = "前";
        public const string In_MoveBack = "后";
        public const string In_MoveLeft = "左";
        public const string In_MoveRight = "右";
        public const string In_TopJigInductor = "上模治具到位感应";
        public const string In_BtmJigInductor = "下模治具到位感应";
        public const string In_ForntBanner = "正前方安全光栅";
        public const string In_TopBanner = "上方安全光栅";
        public const string In_XStretchAir1 = "X向拉伸气缸1";
        public const string In_XStretchAir2 = "X向拉伸气缸2";
        public const string In_YStretchAir1 = "Y向拉伸气缸1";
        public const string In_YStretchAir2 = "Y向拉伸气缸2";
        public const string In_TopMarkeInkycapOut = "上模打标墨盒盖气缸伸";
        public const string In_TopMarkeAirOut = "上模打标气缸伸";
        public const string In_TopMarkeAirRetract = "上模打标气缸缩";
        public const string In_BtmPropUpAirOut = "下模打标气缸伸";
        public const string In_BtmPropUpAirRetract = "下模打标气缸缩";


        //Motor
        public const string Motor_Top_X = "上马达X";
        public const string Motor_Top_Y = "上马达Y";
        public const string Motor_Top_Z = "上马达Z";
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
        public const string Pos_WorldCenter_TopCamera = "位置-世界中心-上相机";
        public const string Pos_WorldCenter_BtmCamera = "位置-世界中心-下相机";
        public const string Pos_Top_StandbyPos = "Pos-上预备位置";
        public const string Pos_Btm_StandbyPos = "Pos-下预备位置";

        //Data
        public const string Data_GlobalWorldCenter = "世界中心资料";
    }
}
