using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionCard
{
    public enum CardType
    {
        正运动,
        固高
    }
    public abstract class MotionBase
    {
        //IO信号
        public const int ioInOn = 0;
        public const int ioInOff = 1;
        public const int ioOutOn = 0;
        public const int ioOutOff = 1;

        public bool resetStop = false;
        /// <summary>
        /// 卡类型
        /// </summary>
        private CardType cardTypeSelect;
        public CardType CardTypeSelect
        {
            get { return cardTypeSelect;}
            set { cardTypeSelect = value;}
        }
        public abstract OperationResult InitCard();
        public abstract OperationResult CloseCard();
        public abstract void SetPulseEquival(Dictionary<float, int> dicPulse);
        public abstract OperationResult VMove(short axis, float vel, bool dir, float velMin, float acc, float dec, float sramp);
        public abstract OperationResult MoveRelative(short axis, float vel, float distance, float velMin, float acc, float dec, float sramp);
        public abstract OperationResult MoveAbs(short axis, float vel, float pos, float velMin, float acc, float dec, float sramp);
        public abstract OperationResult LockVMove(short axis, int ioNum, float vel, bool dir, float velMin, float acc, float dec, float sramp);
        public abstract OperationResult LockMoveRelative(short axis, int ioNum, float vel, float distance, float velMin, float acc, float dec, float sramp);
        public abstract OperationResult LockMoveAbs(short axis, int ioNum, float vel, float pos, float velMin, float acc, float dec, float sramp);
        public abstract OperationResult MoveAbs(short axis, float pos, AxisSpeed axisSpeed, float percentum, float sramp);
        public abstract bool ZeroAxis(short axis, float vel, float creep, int homeio, int fwd_In, int rev_In ,float distance, float velMin, float acc, float dec, float sramp);
        public abstract bool ZeroAxis2(AxisPara axisPara, float vel, float creep, float distance, float velMin, float acc, float dec, float sramp);
        public abstract bool AxisGoHome(short axis, float vel);
        public abstract OperationResult DirectZeroAxis(short axis, float vel, float creep, int homeio, float velMin, float acc, float dec, int homemode);
        public abstract void ZeroAxisHome(uint axis);
        public abstract OperationResult WaitStop(short axis);
        public abstract bool WaitInPlace(short axis, float point);
        public abstract bool WaitInSignal(int signNum, double waitTime);
        public abstract OperationResult WaitHomeStop(short axis);
        public abstract OperationResult StopAxis(short axis);
        public abstract OperationResult StopMoreAxis(int[] axisList);
        public abstract OperationResult StopAllAxis(int model);
        public abstract float GetVel(short axis);
        public abstract float GetMpos(short axis);
        public abstract float GetPos(short axis);
        public abstract float GetMoveBufferPos(int axis);
        public abstract OperationResult ZeroPos(short axis);
        public abstract bool IsMoving(short axis);
        public abstract void GetInSignal(int ioNum, ref bool inSignal);
        public abstract void GetOutSignal(int ioNum, ref bool inSignal);
        public abstract void SetOutSignal(int ioNum, UInt32 ioValue);
        public abstract void GetPlanValue(short axis, ref float planValue);
        public abstract void GetEncoderValue(short axis, ref float encoderValue);
        public abstract string SetAxisEnable(int axis, int value);
        public abstract bool SetAllAxisEnable(int axisNum, int value);
        public abstract string GetDriveTorque(UInt32 axis, ref int torqueValue);
        public abstract void GetAxisStatus(int axis, ref AxisStatus axisStatus);
        public abstract void GetAxisStatus2(int axis, int fwd, int rev, ref AxisStatus axisStatus);
        public abstract void ClearAlarm(UInt32 axis, int model);
        public abstract bool WaitTime(double startMinute, double startSecond, double waitTime);
        public abstract bool WaitTime(DateTime recordTime, double waitTime);
        public abstract float ReadAbsSupeed(short axis);
    }

    /// <summary>
    /// 轴状态
    /// </summary>
    public struct AxisStatus
    {
        /// <summary>
        /// 负限位
        /// </summary>
        public int minusLimit;
        /// <summary>
        /// 正限位
        /// </summary>
        public int positLimit;
        /// <summary>
        /// 原点
        /// </summary>
        public int origin;
        /// <summary>
        /// 伺服报警
        /// </summary>
        public int servoError;
        /// <summary>
        /// 轴使能
        /// </summary>
        public int AxisEnadle;
    }
    /// <summary>
    /// 轴运行速度
    /// </summary>
    public struct AxisSpeed
    {
        public float vel;
        public float minVel;
        public float acc;
        public float dec;
    }
    public struct AxisPara
    {
        public short AxisNum { get; set; }
        public short homeIo { get; set; }
        public short Fwd { get; set; }
        public short Rev { get; set; }
    }

    public struct AxisPoints
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float R { get; set; }
    }
}
