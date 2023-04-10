using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Tool.Motor.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolveWare_Service_Tool.Motor.Data
{
    public delegate bool BrakeDel(bool set);
    public delegate bool MotionDel();
    public delegate bool MotionDel2(ref string sErr);
    public delegate bool MotionInterlockDel(double currentPos, double newPos);

    public class MtrTable : ElementBase
    {
        //马达名称
        //[Category("Axis Table")]
        //[DisplayName("Name  马达名称")]
        //[Description("Axis Name")]
        //public string Name { get; set; }

        [Category("Axis Table")]
        [DisplayName("CardNo  控制卡号")]
        [Description("Card #No")]
        public short CardNo { get; set; }

        [Category("Axis Table")]
        [DisplayName("AxisNo  马达轴号")]
        [Description("Axis #No")]
        public short AxisNo { get; set; }

        [Category("Axis Table")]
        [DisplayName("Home IO 零点讯号参数")]
        [Description("Param Home IO")]
        public int Param_Home_IO { get; set; }

        [Category("Axis Table")]
        [DisplayName("Fwd Limit 正限位讯号参数")]
        [Description("Param Fwd Limit")]
        public int Param_Fwd_Limit { get; set; }

        [Category("Axis Table")]
        [DisplayName("Rev Limit 负限位讯号参数")]
        [Description("Param Rev Limit")]
        public int Param_Rev_Limit { get; set; }

        [Category("Axis Table")]
        [DisplayName("Servo On Logic 马达使能")]
        [Description("Servo On Logic")]
        public short ServoOn_Logic { get; set; }

        [Category("Axis Table")]
        [DisplayName("Unit Per Revolution (mm : 圈数)")]
        [Description("revolution")]
        public double UnitPerRevolution { get; set; }

        [Category("Axis Table")]
        [DisplayName("Pulse Per Revolution (脉冲 : 圈数)")]
        [Description("Pulse")]
        public double PulsePerRevolution { get; set; }

        [Category("Axis Table")]
        [DisplayName("PulseFactor")]
        [Description("PulseFactor")]
        public double PulseFactor { get; set; }

        [Category("Axis Table")]
        [DisplayName("IsConvertedToMMperSec 是否转换MM per Sec")]
        [Description("IsConvertedToMMperSec")]
        public bool IsConvertedToMMperSec { get; set; }

        [Category("Axis Table")]
        [DisplayName("Is Formula Axis (公式轴)")]
        [Description("Is Formula Axis")]
        public bool IsFormulaAxis { get; set; }

        [Category("Axis Table")]
        [DisplayName("Decimal Digit 小数点位数")]
        [Description("Decimal Digit")]
        public int Decimal_Digit { get; set; }

        //马达控制器
        [Category("Motor Master Driver")]
        [Description("Motor Master Driver  马达控制器")]
        [DisplayName("MotorMaster")]
        public Master_Driver_Motor MotorMasterDriver { get; set; }


        //马达数值
        [Category("Motor Step Unit")]
        [DisplayName("MM 马达显示单位")]
        [Description("Motor Step Unit")]
        public bool IsMM { get; set; }


        //In Position Check
        [Category("In Position Check")]
        [DisplayName("Acceptable Offset | 容许范围")]
        [Description("Direction")]
        public double AcceptableInPositionOffset { get; set; }


        //移动方向
        [Category("Direction")]
        [DisplayName("Direction 马达实际前进状态")]
        [Description("Direction")]
        public DirectionState MotorRealDirectionState { get; set; }

        [Category("Direction")]
        [DisplayName("Direction 马达显示前进状态")]
        [Description("Direction")]
        public DirectionState MotorDisplayDirectionState { get; set; }

        [Category("Direction")]
        [DisplayName("HomeDirection 马达实际复位方向")]
        [Description("Home Direction")]
        public DirectionState MotorHomeDirectionState { get; set; }


      

        //状态读取间隔
        [Category("Status Read Timing")]
        [DisplayName("Timing 状态读取时间间隔")]
        [Description("Timing")]
        public int StatusReadTiming { get; set; }


        //自定义 复位设定
        [Category("Home Setting")]
        [Description("Custom Home Type (0:NEL/1:PEL/2:MID)")]
        [DisplayName("CustomHomeType")]
        public CustomHomeType CustomHomeType { get; set; }

        [Category("Home Setting")]
        [DisplayName("Custome Home StepSize")]
        [Description("Custom Home Type (0:NEL/1:PEL/2:MID)")]
        public double CustomHomeStepSize { get; set; }

        [Category("Home Setting")]
        [DisplayName("OnBoardHome 使用板卡复位")]
        [Description("On board home (0:No/1:Yes)")]
        public short OnBoardHome { get; set; }

        [Category("Home Setting")]
        [DisplayName("Zero Homing 零点复位")]
        [Description("Zero Homing")]
        public bool ZeroHoming { get; set; }

        [Category("Home Setting")]
        [DisplayName("Set Limit Mode 复位前设定限位功能")]
        [Description("Set Limit Mode Before Homing")]
        public bool SetLimitModeBeforeHoming { get; set; }

        [Category("Home Setting")]
        [DisplayName("MoveOutGapAfterHoming 复位后离开原点距离")]
        [Description("Move Out Gap After Homing")]
        public double MoveOutGapAfterHoming { get; set; } = 20;

        [Category("Home Setting")]
        [DisplayName("HomeCreepSpeedRate 复位爬升速度")]
        [Description("Home Creep Speed Rate")]
        public float HomeCreepSpeedRate { get; set; } = 0.5f;



        //超时设定
        [Category("Time Out")]
        [DisplayName("Motion TimeOut 移动超时")]
        [Description("Motion Time Out in mili-second")]
        public int MotionTimeOut { get; set; }

        [Category("Time Out")]
        [DisplayName("Home TimeOut 复位超时")]
        [Description("Home Time Out in mili-second")]
        public int HomeTimeOut { get; set; }


        //软限位
        [Category("Soft Limit")]
        [DisplayName("Enable Soft Limit 启用软限位")]
        [Description("Enable Soft Limit")]
        public bool Enable_SoftLimit { get; set; }

        [Category("Soft Limit")]
        [DisplayName("Max Distance 最大软限位")]
        [Description("Enable Soft Limit")]
        public double MaxDistance_SoftLimit { get; set; }

        [Category("Soft Limit")]
        [DisplayName("Min Soft Limit 最小软限位")]
        [Description("Enable Soft Limit")]
        public double MinDistance_SoftLimit { get; set; }


        [XmlIgnore]
        public MotionDel2 pIsInhibitToHome = null;
        [XmlIgnore]
        public MotionDel2 pIsInhibitToMove = null;
        [XmlIgnore]
        public double NewPos;
        [XmlIgnore]
        public double CurPos = 2105.0;


        #region ctor
        public MtrTable()
        {
            PulsePerRevolution = 1000;
            UnitPerRevolution = 1;
            OnBoardHome = 1;
            StatusReadTiming = 1;
        }
        #endregion
    }
}
