using SolveWare_Service_Core.Base.Abstract;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Dlls;
using SolveWare_Service_Tool.Motor.Base.Interface;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Tool.Motor.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Motor.Base.Abstract
{
    public abstract class AxisBase : ToolElementBase
    {
        #region ctor
        public AxisBase(IElement configData)
        {
            ConfigData_Motor config = (configData as ConfigData_Motor);

            this.mtrTable = config.MtrTable;
            this.mtrConfig = config.MtrConfig;
            this.mtrMisc = config.MtrMisc;
            this.mtrSafe = config.MtrSafe;
            this.mtrSpeed = config.MtrSpeed;
            this.simulation = config.Simulation;
            this.Name = mtrTable.Name;
            if (this.Id == 0) Id = IdentityGenerator.IG.GetIdentity();

            //StartStatusReading();
        }
        #endregion

        public void SetSafeKeeper(ISafeKeeper keeper)
        {
            this.SafeKeeper = keeper;
        }

        protected string name;
        protected MtrTable mtrTable = null;
        protected MtrConfig mtrConfig;
        protected MtrSpeed mtrSpeed;
        protected MtrMisc mtrMisc;
        protected MtrSafe mtrSafe;
        protected CancellationTokenSource readStatusSource;
        protected bool simulation;
        protected bool hasHome;
        protected AutoResetEvent cancelDoneFlag = new AutoResetEvent(false);

        protected bool isServoOn;
        protected bool isInPosition;
        protected bool isAlarm;
        protected bool isOrg;
        protected bool isPosLimit;
        protected bool isNegLimit;
        protected bool isProhibitActivated = false;
        protected bool isMoving;
        protected bool isStopReq = false;
        protected double currentPulse;
        protected double currentPhysicalPos = -999.999;
        protected double analogInputValue;
        protected string interlockWaringMsg;
        protected  int axis_Read_Status = 0;
        //Safe Keeper
        protected bool isDangerousToMove;
        protected bool isInZone;
        protected bool isSafeToMoveInZone;
        protected bool isIgnoreDanger;

        public bool IsIgnoreDanger
        {
            get => isIgnoreDanger;
            set => UpdateProper(ref isIgnoreDanger, value);
        }
        public bool IsDangerousToMove
        {
            get => isDangerousToMove;
            set
            {
                isDangerousToMove = value;
                OnPropertyChanged(nameof(IsDangerousToMove));
            }
        }
        public bool IsInZone
        {
            get => isInZone;
            set => UpdateProper(ref isInZone, value);
        }
        public bool IsSafeToMoveInZone
        {
            get => isSafeToMoveInZone;
            set => UpdateProper(ref isSafeToMoveInZone, value);
        }
        public ISafeKeeper SafeKeeper { get; private set; }

        public MtrTable MtrTable
        {
            get => mtrTable;
            set
            {
                mtrTable = value;
                this.Name = mtrTable.Name;
                OnPropertyChanged(nameof(MtrTable));
            }
        }
        public MtrConfig MtrConfig
        {
            get => mtrConfig;
            set => UpdateProper(ref mtrConfig, value);
        }
        public MtrMisc MtrMisc
        {
            get => mtrMisc;
            set => UpdateProper(ref mtrMisc, value);
        }
        public MtrSafe MtrSafe
        {
            get => mtrSafe;
            set => UpdateProper(ref mtrSafe, value);
        }
        public MtrSpeed MtrSpeed
        {
            get => mtrSpeed;
            set => UpdateProper(ref mtrSpeed, value);
        }
        public bool Simulation
        {
            get => simulation;
            set => UpdateProper(ref simulation, value);
        }
        public bool IsServoOn
        {
            get
            {
                return this.isServoOn;
            }
            protected set
            {
                isServoOn = value;
                UpdateProper(ref isServoOn, value);
            }
        }
        public bool IsInPosition
        {
            get => isInPosition;
            set => UpdateProper(ref isInPosition, value);
        }
        public bool IsAlarm
        {
            get => isAlarm;
            set => UpdateProper(ref isAlarm, value);
        }
        public bool IsOrg
        {
            get => isOrg;
            set => UpdateProper(ref isOrg, value);
        }
        public bool IsPosLimit
        {
            get => isPosLimit;
            set => UpdateProper(ref isPosLimit, value);
        }
        public bool IsNegLimit
        {
            get => isNegLimit;
            set => UpdateProper(ref isNegLimit, value);
        }
        public bool HasHome
        {
            get => hasHome;
            private set => UpdateProper(ref hasHome, value);
        }
        public double CurrentPulse
        {
            get => currentPulse;
            set => UpdateProper(ref currentPulse, value);
        }
        public double CurrentPhysicalPos
        {
            get => currentPhysicalPos;
            set
            {
                currentPhysicalPos = value;
                OnPropertyChanged(nameof(CurrentPhysicalPos));
                OnPropertyChanged(nameof(Description));
                UpdateDynamicStatus();
            }
        }
        public double AnalogInputValue
        {
            get => analogInputValue;
            set => UpdateProper(ref analogInputValue, value);
        }
        public string InterlockWaringMsg
        {
            get => interlockWaringMsg;
            set => UpdateProper(ref interlockWaringMsg, value);
        }
        public long Id { get; set; }

        public string Description
        {
            get;
            set;
        }

        private string dynamicContent;
        public string DynamicContent
        {
            get => dynamicContent;
            set => UpdateProper(ref dynamicContent, value);
        }
        public bool IsMoving
        {
            get => isMoving;
        }
        private void UpdateDynamicStatus()
        {
            string unit = MtrTable.IsMM ? "mm" : "Deg";
            DynamicContent = $"{Name} {CurrentPhysicalPos.ToString("F4")} {unit}";
            OnPropertyChanged(nameof(DynamicContent));
        }
        //公用部份
        public abstract void StartStatusReading();
        public void StopStatusReading()
        {
            if (readStatusSource == null) return;
            readStatusSource.Cancel();
            cancelDoneFlag.WaitOne();
            readStatusSource = null;
        }
        public bool IsProhibitToHome()
        {
            string sErr = string.Empty;
            this.isProhibitActivated = false;
            bool result = false;

            if (mtrTable.pIsInhibitToHome == null) return false;

            if (mtrTable.pIsInhibitToHome(ref sErr))
            {
                this.isProhibitActivated = true;
                this.InterlockWaringMsg = sErr;
                result = true;
            }

            return result;
        }
        public bool IsProhibitToMove()
        {


            this.InterlockWaringMsg = "";
            this.isProhibitActivated = false;
            bool result = false;
            string sErr = string.Empty;


            if (mtrTable.pIsInhibitToMove == null) return false;
            if (mtrTable.pIsInhibitToMove(ref sErr))
            {
                this.isProhibitActivated = true;
                this.InterlockWaringMsg = sErr;
                return true;
            }

            return result;
        }
        public bool InPositionCheck(double targetPos)
        {
            bool isInPosition = false;
            double curPos = Simulation ? MtrTable.CurPos : Get_CurUnitPos();
            double realOffset = Math.Abs(curPos) - Math.Abs(targetPos);

            isInPosition = Math.Abs(realOffset) > mtrTable.AcceptableInPositionOffset;

            return isInPosition;
        }

        //重写部份
        public abstract bool Init();
        public abstract bool Get_PEL_Signal();
        public abstract bool Get_NEL_Signal();
        public abstract bool Get_InPos_Signal();
        public abstract bool Get_Alarm_Signal();
        public abstract bool Get_Origin_Signal();
        public abstract double Get_CurPulse();
        public abstract double Get_CurUnitPos();
        public abstract double Get_AnalogInputValue();
        public abstract bool Get_ServoStatus();
        public abstract bool MoveRelative(double distance, MtrSpeed mtrSpeed, bool BypassDangerCheck = false);
        public abstract bool MoveTo(double pos, bool BypassDangerCheck = false);
        public abstract bool MoveTo(double pos, MtrSpeed mtrSpeed,  bool BypassDangerCheck = false);
        public abstract bool ManualMoveTo(double pos);
        public abstract void Stop();
        public abstract bool HomeMove();
        public abstract bool MoveToSafeObservedPos(double pos);
        public abstract bool MoveToAndStopByIO(double pos, Func<bool> StopAction, bool BypassDangerCheck = false);
        public abstract Motor_Wait_Kind WaitStop();
        public abstract Motor_Wait_Kind WaitHomeDone();

        public abstract void Jog(bool isPositive);
        public abstract int Get_IO_sts();
        public abstract void Set_Servo(bool on);

        public void Conver_To_Home_MMPerSec(ref float startVel, ref float maxVel, ref double acc, ref double dec)
        {
            double speedRatio = 100 / mtrSpeed.SpeedRate_Home;
      

            double unitPerSec = mtrTable.PulsePerRevolution / mtrTable.UnitPerRevolution;
            double acc_Unit = mtrSpeed.Home_Acceleration * unitPerSec * speedRatio;
            double dec_Unit = mtrSpeed.Home_Deceleration * unitPerSec * speedRatio;

            startVel = (float)(unitPerSec * mtrSpeed.Home_Min_Velocity);
            maxVel = (float)(unitPerSec * mtrSpeed.Home_Max_Velocity * speedRatio);

            double factor = maxVel == startVel ? 1 : maxVel - startVel;
            acc = factor / acc_Unit;
            dec = factor / dec_Unit;
        }

        public void Conver_To_Jog_MMPerSec(ref float startVel, ref float maxVel, ref double acc, ref double dec)
        {
            double speedRatio = 100 / mtrSpeed.SpeedRate_Jog;


            double unitPerSec = mtrTable.PulsePerRevolution / mtrTable.UnitPerRevolution;
            double acc_Unit = mtrSpeed.Jog_Acceleration * unitPerSec * speedRatio;
            double dec_Unit = mtrSpeed.Jog_Deceleration * unitPerSec * speedRatio;

            startVel = (float)(unitPerSec * mtrSpeed.Jog_Min_Velocity);
            maxVel = (float)(unitPerSec * mtrSpeed.Jog_Max_Velocity * speedRatio);

            double factor = maxVel == startVel ? 1 : maxVel - startVel;
            acc = factor / acc_Unit;
            dec = factor / dec_Unit;
        }

        public double FormulaCalc_AngleToUnit(double Angle)
        {
            //"InsideAngDeg=180-(-1*Angle);
            /* 电机旋转角度转三角形内角 */
            //AngArc=InsideAngDeg*Math.PI/180;
            /* 三角形内角转弧度 */
            //L1=2.5;  
            /*旋转臂长度*/
            //L2 =55; /*连杆臂长度*/
            //InitLen=L2-L1;  
            /* 初始连杆长度,归零用 */
            /*核心计算函数*/
            //a =1; b=-2*L1*Math.cos(AngArc);
            //c =L1*L1-L2*L2;\nDistance=(-b+Math.sqrt(b*b-4*a*c))/2-InitLen; 
            /*算出位置*/
            //-1*Distance  /* 向下运动为负数 */"
            Angle *= -1;
            double InsideAngDeg = 180 - (-1 * Angle);
            double AngArc = InsideAngDeg * Math.PI / 180;
            double L1 = 2.5;
            double L2 = 55;
            double InitLen = L2 - L1;
            double a = 1;
            double b = -2 * L1 * Math.Cos(AngArc);
            double c = L1 * L1 - L2 * L2;
            double Distance = (-b + Math.Sqrt(b * b - 4 * a * c)) / 2 - InitLen;

            double reulst = Distance * 1;
            return reulst;

        }
        public double FormulaCalc_UnitToAngle(double Unit)
        {

            //"Distance=-1*Unit 
            /* 向下运动为负数 */
            //L1 =2.5;  
            /*旋转臂长度*/
            //L2 =55; 
            /*连杆臂长度*/
            //InitLen =L2-L1;  
            /* 初始连杆长度,归零用 */
            //L3 =Distance+InitLen;  
            /*三角形可变边长*/
            //if (Unit>=0)
            //{0}v
            //else if(L3>L1+L2){-180}
            //else{/*核心计算函数 计算出三角形内角 */
            //InsideAngArc =Math.acos((L2*L2-L1*L1-L3*L3)/(-2*L1*L3));
            //InsideAngDeg = InsideAngArc * 180 / Math.PI;/* 
            //三角形内角转角度 */(-1)*(180-InsideAngDeg); /*算出角度*/\n}"

            Unit *= -1;
            double Distance = -1 * Unit;
            double L1 = 2.5;
            double L2 = 55;
            double InitLen = L2 - L1;
            double L3 = Distance + InitLen;
            double InsideAngArc = 0;
            double InsideAngDeg = 0;

            if (Unit >= 0)
            {
                return 0;
            }
            else if (L3 > L1 + L2)
            {
                return -180;
            }
            else
            {
                InsideAngArc = Math.Acos((L2 * L2 - L1 * L1 - L3 * L3) / (-2 * L1 * L3));
                InsideAngDeg = InsideAngArc * 180 / Math.PI;
            }
            double result = (1) * (180 - InsideAngDeg);
            return result;
        }
        public bool IsMoveTimeOut(DateTime st)
        {
            TimeSpan ts = DateTime.Now - st;
            return ts.TotalMilliseconds > mtrTable.MotionTimeOut;
        }
        public bool IsHomeTimeOut(DateTime st)
        {
            TimeSpan ts = DateTime.Now - st;
            return ts.TotalMilliseconds > mtrTable.HomeTimeOut;
        }


        public abstract bool DoAvoidDangerousPosAction();

        public void Setup(IElement configData)
        {

        }

      
    }
}
