using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Abstract;
using SolveWare_Service_Tool.Dlls;
using SolveWare_Service_Tool.MasterDriver.Business;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Base.Interface;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Tool.Motor.Definition;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Motor.Business
{
 
    public class Motor_Zmcaux : AxisBase
    {
        IntPtr Handler;
        public Motor_Zmcaux(IElement configData, bool simulation) : base(configData)
        {
            SetSafeKeeper(new SafeKeeper());
            this.simulation = simulation;
        }

        public override bool DoAvoidDangerousPosAction()
        {
            return true;
        }

        public override bool Get_Alarm_Signal()
        {
            if (Simulation) return false;
            return (((IO_Status_Zmcaux)axis_Read_Status & IO_Status_Zmcaux.告警信号输入) == IO_Status_Zmcaux.告警信号输入);
        }

        public override double Get_AnalogInputValue()
        {
            return 0;
        }

        public override double Get_CurPulse()
        {
            if(!Simulation) return 0;   

            float pulse = 0f;
            Dll_Zmcaux.ZAux_Direct_GetMpos(Handler, mtrTable.AxisNo, ref pulse);
            return pulse;
        }

        public override double Get_CurUnitPos()
        {
            if (Simulation) return CurrentPhysicalPos;

            double position = 0;
            if (this.mtrTable.IsFormulaAxis)
            {
                double mm = CurrentPulse * mtrTable.UnitPerRevolution / mtrTable.PulsePerRevolution;
                position = FormulaCalc_AngleToUnit(mm);
            }
            else
            {
                float tempPos = 0;
                Dll_Zmcaux.ZAux_Direct_GetDpos(Handler, mtrTable.AxisNo, ref tempPos);
                position = tempPos; //CurrentPulse * mtrTable.UnitPerRevolution / mtrTable.PulsePerRevolution;
            }

            if (MtrTable.MotorRealDirectionState == DirectionState.Negative && MtrTable.MotorDisplayDirectionState == DirectionState.Positive ||
                MtrTable.MotorRealDirectionState == DirectionState.Positive && MtrTable.MotorDisplayDirectionState == DirectionState.Negative)
            {
                position *= -1;
            }

            return position;
        }

        public override bool Get_InPos_Signal()
        {
            ///正运动没有 In Pos 
            return true;
        }
        public override int Get_IO_sts()
        {
            return 0;
        }
        public override bool Get_NEL_Signal()
        {
            if (Simulation) return false;
            return (((IO_Status_Zmcaux)axis_Read_Status & IO_Status_Zmcaux.反向硬限位) == IO_Status_Zmcaux.反向硬限位);
        }
        public override bool Get_Origin_Signal()
        {
            if (Simulation) return true;

            uint org = 0;
            Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Home_IO, ref org);
            bool isOn = org == 1 ? true : false;
            return isOn;
        }
        public override bool Get_PEL_Signal()
        {
            if (Simulation) return false;
            return (((IO_Status_Zmcaux)axis_Read_Status & IO_Status_Zmcaux.正向硬限位) == IO_Status_Zmcaux.正向硬限位);
        }
        public override bool Get_ServoStatus()
        {
            if(Simulation) return IsServoOn;

            int status = 0;
            Dll_Zmcaux.ZAux_Direct_GetAxisEnable(Handler, mtrTable.AxisNo, ref status);
            
            bool isOn = status == 1 ? true : false;
            return isOn;
        }
        public override bool HomeMove()
        {
            //变数
            bool isHomeSuccessful = false;
            string sErr = string.Empty;
            uint homeStatus = 0;
            uint revStatus = 0;
            int homemode = 0;
            bool isTimeOut = false;
            TimeSpent = "0.000";
            Stopwatch sw = Stopwatch.StartNew();
            errorReport = string.Empty;

            //先检查安全问题
            if (IsProhibitToHome()) 
            {
                errorReport += "不安全复位";
                return isHomeSuccessful; 
            }

            //模拟状态
            if (Simulation)
            {
                sw.Restart();
                MtrTable.NewPos = 0;
                MoveTo(0);
                hasHome = true;
                isHomeSuccessful = true;
                TimeSpent = $"{sw.Elapsed.TotalSeconds.ToString("F3")}";
                return isHomeSuccessful;
            }

            //设置正负限位
            if(mtrTable.SetLimitModeBeforeHoming)
            {
                Dll_Zmcaux.ZAux_Direct_SetRevIn(Handler, mtrTable.AxisNo, mtrTable.Param_Rev_Limit); //设置负限位
                Dll_Zmcaux.ZAux_Direct_SetFwdIn(Handler, mtrTable.AxisNo, mtrTable.Param_Fwd_Limit); //设置正限位
                Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Rev_Limit, ref revStatus);
            }

            // 零点复位
            if(mtrTable.ZeroHoming)
            {
                return ZeroHoming();
            }

            //读取原点
            sw.Restart();
            Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Home_IO, ref homeStatus);

            //先往原点方向一直走，直到到达限位
            if (homeStatus != 0)
                Jog(false);

            //回零方式判断
            DateTime st = DateTime.Now;
            while (true) 
            {
                Thread.Sleep(1);
                Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Home_IO, ref homeStatus);
                if (homeStatus == 0)
                {
                    homemode = 4;
                    Stop();
                    break;
                }
                if (IsHomeTimeOut(st))
                {
                    isTimeOut = true;
                    break;
                }
                Thread.Sleep(10);
            }

            if (isTimeOut)
            {
                errorReport += "运动超时";
                Stop();
                hasHome = false;
                return isHomeSuccessful;
            }
 
            //执行回原点
            Dll_Zmcaux.ZAux_Direct_SetCreep(Handler, this.mtrTable.AxisNo, 1f);
            Dll_Zmcaux.ZAux_Direct_Single_Datum(Handler, mtrTable.AxisNo, homemode);
            
            //等待停止
           st = DateTime.Now;
           while (true)
            {
                if (!Get_MovingStatus()) break;
                if (IsHomeTimeOut(st))
                {
                    errorReport += "运动超时";
                    isTimeOut = true;
                    break;
                }
            }
           if(isTimeOut)
            {
                errorReport += "运动超时";
                Stop();
                hasHome = false;
                return isHomeSuccessful;
            }
            Thread.Sleep(50);

            if (HomeMoveTo(mtrTable.MoveOutGapAfterHoming) == false)
            {
                errorReport += $"复位后 位移{mtrTable.MoveOutGapAfterHoming} mm 失败";
                Stop();
                return isHomeSuccessful;
            }
            Thread.Sleep(50);

            Dll_Zmcaux.ZAux_Direct_SetMpos(Handler, mtrTable.AxisNo, 0.0f);  //重置编码器位置
            Dll_Zmcaux.ZAux_Direct_SetDpos(Handler, mtrTable.AxisNo, 0.0f);
            //Dll_Zmcaux.ZAux_BusCmd_Datum(Handler, (uint)mtrTable.AxisNo, 35);

            TimeSpent = $"{sw.Elapsed.TotalSeconds.ToString("F3")}";
            isHomeSuccessful = true;
            return isHomeSuccessful;
        }
        public override bool HomeMove(SpeedSeting speed)
        {
            bool isHomeSuccessful = false;
            string sErr = string.Empty;
            uint homeStatus = 0;
            uint revStatus = 0;
            int homemode = 0;
            TimeSpent = "0.000";
            Stopwatch sw = Stopwatch.StartNew();
            errorReport = string.Empty;

            if (IsProhibitToHome()) 
            {
                errorReport += "不安全复位";
                return false; 
            }

            isStopReq = false;

            if (Simulation)
            {
                sw.Restart();
                MtrTable.NewPos = 0;
                double DistanceToMove = 0 - currentPulse;
                double EstimateTimeTaken = 0.01 * (Math.Abs(DistanceToMove) / mtrSpeed.SpeedSettings[0].Max_Velocity);
                MoveTo(0);
                TimeSpent = $"{sw.Elapsed.TotalSeconds.ToString("F3")}";
                return true;
            }

            //目前不需要
            if (mtrTable.SetLimitModeBeforeHoming)
            {
                Dll_Zmcaux.ZAux_Direct_SetRevIn(Handler, mtrTable.AxisNo, mtrTable.Param_Rev_Limit); //设置负限位
                Dll_Zmcaux.ZAux_Direct_SetFwdIn(Handler, mtrTable.AxisNo, mtrTable.Param_Fwd_Limit); //设置正限位
                Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Rev_Limit, ref revStatus);
            }

            // 有此轴是零点复位
            if (mtrTable.ZeroHoming)
            {
                return ZeroHoming();
            }

            sw.Restart();
            Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Home_IO, ref homeStatus);

            //先往原点方向一直走，直到到达限位
            if (homeStatus != 0) //&& revStatus != 0)
            {
                this.Jog(false, speed);
            }

            sw.Restart();
            bool isTimeOut = false;
            //回零方式判断
            while (true)  //IsMoving(axis)  resetStop
            {
                Thread.Sleep(1);
                Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Home_IO, ref homeStatus);
                if (homeStatus == 0)
                {
                    homemode = 4;
                    Stop();
                    break;
                }
                if (sw.ElapsedMilliseconds >= mtrTable.HomeTimeOut)
                {
                    isTimeOut = true;
                    break;
                }
                Thread.Sleep(10);
            }

            if (isTimeOut )
            {
                errorReport += "运动超时";
                Stop();
                return isHomeSuccessful;
            }

            //执行回原点
            Dll_Zmcaux.ZAux_Direct_SetCreep(Handler, this.mtrTable.AxisNo, 1f);
            Dll_Zmcaux.ZAux_Direct_Single_Datum(Handler, mtrTable.AxisNo, homemode);
            //等待停止
            DateTime st = DateTime.Now;
            while (true)
            {
                if (!Get_MovingStatus()) break;
                if (IsHomeTimeOut(st))
                {
                    errorReport += "运动超时";
                    isTimeOut = true;
                    break;
                }
            }
            if (isTimeOut)
            {
                errorReport += "运动超时";
                Stop();
                hasHome = false;
                return isHomeSuccessful;
            }
            Thread.Sleep(50);

            if (HomeMoveTo(mtrTable.MoveOutGapAfterHoming) == false)
            {
                errorReport += $"复位后 位移 {mtrTable.MoveOutGapAfterHoming} mm 失败";
                Stop();
                return isHomeSuccessful;
            }
            Thread.Sleep(50);


            Dll_Zmcaux.ZAux_Direct_SetMpos(Handler, mtrTable.AxisNo, 0.0f);  //重置编码器位置
            Dll_Zmcaux.ZAux_Direct_SetDpos(Handler, mtrTable.AxisNo, 0.0f);
            //Dll_Zmcaux.ZAux_BusCmd_Datum(Handler, (uint)mtrTable.AxisNo, 35);

            TimeSpent = $"{sw.Elapsed.TotalSeconds.ToString("F3")}";
            isHomeSuccessful = true;
            return isHomeSuccessful;
        }
        private bool ZeroHoming()
        {
            //TODO - Zero Homing 填空
            return this.MoveTo(0);
        }
        public override bool Init()
        {          
            if(!this.simulation)
            {
                var master = (SolveWare.Core.MMgr as MainManagerBase).MasterDriver as MasterDriverManager;
                if (master.CardInfo.Dic_CardHandler.Count == 0 && !this.simulation) return false;
                if (master.CardInfo.Dic_CardHandler.Count == 0 && this.simulation) return true;

                Handler = master.CardInfo.Dic_CardHandler[mtrTable.CardNo];
                Dll_Zmcaux.ZAux_Direct_SetUnits(Handler, mtrTable.AxisNo, (float)mtrTable.PulsePerRevolution / (float)mtrTable.UnitPerRevolution);
            }

            Set_Servo(true);
            return true;
        }
        public override void Jog(bool isPositive)
        {
            if (isMoving) return;
            
            //变量
            int error = 0;
            float minVel = 0;
            float maxVel = 0;
            float acc = 0;
            float dec = 0;
            int dir = 0;

            try
            {
                do
                {
                    if (mtrTable.IsConvertedToMMperSec)
                    {
                        Conver_To_Jog_MMPerSec(ref minVel, ref maxVel, ref acc, ref dec);
                    }
                   else
                    {
                        var jogMode = this.mtrSpeed.SpeedSettings.FirstOrDefault(x => x.Name == ConstantProperty.SpeedSetting_Jog);
                        minVel = (float)jogMode.Min_Velocity;
                        maxVel= (float)jogMode.Max_Velocity;
                        acc = (float)jogMode.Acceleration;
                        dec = (float)jogMode.Deceleration;
                    }
                    error = SetSpeedParameters(minVel, maxVel, acc, dec, false) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                    if (error != ErrorCodes.NoError) break;
                    //设置方向并运动
                    Dll_Zmcaux.ZAux_Direct_SetUnits(Handler, mtrTable.AxisNo, (float)mtrTable.PulsePerRevolution / (float)mtrTable.UnitPerRevolution);
                    error = Dll_Zmcaux.ZAux_Direct_Single_Vmove(Handler, mtrTable.AxisNo, dir = isPositive ? 1 : -1);
                
                } while (false);
            }
            catch
            {
                error = ErrorCodes.MotionFunctionError;
            }
            if(error != ErrorCodes.NoError) SolveWare.Core.MMgr.Infohandler.LogMessage($"{Name} Jog 失败");
        }
        public override void Jog(bool isPositive, SpeedSeting speed)

        {
            if (isMoving) return;

            //变量
            int error = 0;
            float minVel = 0;
            float maxVel = 0;
            float acc = 0;
            float dec = 0;
            int dir = 0;

            try
            {
                do
                {
                    if (mtrTable.IsConvertedToMMperSec)
                    {
                        Conver_To_Jog_MMPerSec(ref minVel, ref maxVel, ref acc, ref dec);
                    }
                    else
                    {
                        minVel = (float)speed.Min_Velocity;
                        maxVel = (float)speed.Max_Velocity;
                        acc = (float)speed.Acceleration;
                        dec = (float)speed.Deceleration;
                    }

                    error = SetSpeedParameters(minVel, maxVel, acc, dec, false) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                    if (error != ErrorCodes.NoError) break;
                    //设置方向并运动
                    Dll_Zmcaux.ZAux_Direct_SetUnits(Handler, mtrTable.AxisNo, (float)mtrTable.PulsePerRevolution / (float)mtrTable.UnitPerRevolution);
                    error = Dll_Zmcaux.ZAux_Direct_Single_Vmove(Handler, mtrTable.AxisNo, dir = isPositive ? 1 : -1);

                } while (false);
            }
            catch
            {
                error = ErrorCodes.MotionFunctionError;
            }
            if (error != ErrorCodes.NoError) SolveWare.Core.MMgr.Infohandler.LogMessage($"{Name} Jog 失败");
        }


        public override bool ManualMoveTo(double pos)
        {
            return true;
        }

        public override bool MoveRelative(double distance, bool BypassDangerCheck = false)
        {
            double currPoints = Get_CurUnitPos();
            return MoveTo(currPoints + distance, BypassDangerCheck);
        }
        public override bool MoveRelative(double distance, SpeedSeting speed, bool BypassDangerCheck = false)
        {
            double currPoints = Get_CurUnitPos();
            return MoveTo(currPoints + distance, speed, BypassDangerCheck);     
        }
        public override bool HomeMoveTo(double pos, bool BypassDangerCheck = false)
        {
            bool isMoveSuccessful = false;
            DateTime st = DateTime.Now;

            double tempPos = 0;
            //公式轴执行方式
            if (this.MtrTable.IsFormulaAxis)
            {
                tempPos = FormulaCalc_UnitToAngle(pos);
                pos = tempPos;
            }

            //安全检查
            if (IsProhibitToMove()) return false;
            if (IsDangerousToMove)
            {
                if (DoAvoidDangerousPosAction() == false) return false;
            }
            if (IsZoneSafeToGo(pos) == false) return false;

            //获取速度资料
            var homeMode = this.mtrSpeed.SpeedSettings.FirstOrDefault(x => x.Name == ConstantProperty.SpeedSetting_Home);

            //模拟方式
            double distanceToMove = pos - Get_CurUnitPos();
            double estimateTimeTaken = 0.01 * (Math.Abs(distanceToMove) / homeMode.Max_Velocity);
            pos = Math.Round(pos, 5, MidpointRounding.AwayFromZero);
            DateTime commmandStartTime = DateTime.Now;
            MtrTable.NewPos = pos;
            if (Simulation)
            {
                TimeSpan ts = estimateTimeTaken < 0.5 ?
                                           TimeSpan.FromSeconds(0.5) :
                                           TimeSpan.FromSeconds(estimateTimeTaken);


                TimeSpan ts2 = DateTime.Now - commmandStartTime;
                double temppos = MtrTable.CurPos;
                double tempfliction = 1;
                while (ts2.TotalMilliseconds < ts.TotalMilliseconds)
                {
                    ts2 = DateTime.Now - commmandStartTime;
                    
                    MtrTable.CurPos = temppos + tempfliction * distanceToMove;
                    Thread.Sleep(5);
                    if (isStopReq) break;
                }
                if (!isStopReq)
                {
                    MtrTable.CurPos = MtrTable.NewPos;
                    CurrentPhysicalPos = MtrTable.CurPos;
                }


                return true;
            }

            //实际执行
            double factor = MtrTable.MotorRealDirectionState == DirectionState.Negative && MtrTable.MotorDisplayDirectionState == DirectionState.Positive ||
                                      MtrTable.MotorRealDirectionState == DirectionState.Positive && MtrTable.MotorDisplayDirectionState == DirectionState.Negative ? -1 : 1;


            pos *= factor;
            //设置速度参数
            SetSpeedParameters((float)homeMode.Min_Velocity, (float)homeMode.Max_Velocity, (float)homeMode.Acceleration, (float)homeMode.Deceleration, true);
            //绝对位置
          
            Dll_Zmcaux.ZAux_Direct_SetUnits(Handler, mtrTable.AxisNo, (float)mtrTable.PulsePerRevolution / (float)mtrTable.UnitPerRevolution);
            //绝对位置
            Dll_Zmcaux.ZAux_Direct_Single_MoveAbs(Handler, mtrTable.AxisNo, (float)pos);
            
            while(true)
            {
                if (!Get_MovingStatus()) break;
                if (IsMoveTimeOut(st))
                {
                    Stop();
                    return isMoveSuccessful;
                }
            }

            isMoveSuccessful = true;
            return isMoveSuccessful;
        }
        public override bool HomeMoveTo(double pos, SpeedSeting speed, bool BypassDangerCheck = false)
        {
            //变量
            bool isMoveSuccessful = false;
            DateTime st = DateTime.Now;
            double tempPos = 0;

            //公式轴执行方式
            if (this.MtrTable.IsFormulaAxis)
            {
                tempPos = FormulaCalc_UnitToAngle(pos);
                pos = tempPos;
            }

            //安全检查
            if (IsProhibitToMove()) return false;
            if (IsDangerousToMove)
            {
                if (DoAvoidDangerousPosAction() == false) return false;
            }
            if (IsZoneSafeToGo(pos) == false) return false;

            
            //模拟方式
            double distanceToMove = pos - Get_CurUnitPos();
            double estimateTimeTaken = 0.01 * (Math.Abs(distanceToMove) / speed.Max_Velocity);
            pos = Math.Round(pos, 5, MidpointRounding.AwayFromZero);
            DateTime commmandStartTime = DateTime.Now;
            MtrTable.NewPos = pos;
            if (Simulation)
            {
                TimeSpan ts = estimateTimeTaken < 0.5 ?
                                           TimeSpan.FromSeconds(0.5) :
                                           TimeSpan.FromSeconds(estimateTimeTaken);


                TimeSpan ts2 = DateTime.Now - commmandStartTime;
                double temppos = MtrTable.CurPos;
                double tempfliction = 1;
                while (ts2.TotalMilliseconds < ts.TotalMilliseconds)
                {
                    ts2 = DateTime.Now - commmandStartTime;

                    MtrTable.CurPos = temppos + tempfliction * distanceToMove;
                    Thread.Sleep(5);
                    if (isStopReq) break;
                }
                if (!isStopReq)
                {
                    MtrTable.CurPos = MtrTable.NewPos;
                    CurrentPhysicalPos = MtrTable.CurPos;
                }

                return true;
            }

            //实际执行
            double factor = MtrTable.MotorRealDirectionState == DirectionState.Negative && MtrTable.MotorDisplayDirectionState == DirectionState.Positive ||
                                      MtrTable.MotorRealDirectionState == DirectionState.Positive && MtrTable.MotorDisplayDirectionState == DirectionState.Negative ? -1 : 1;


            //double targetPos = pos / mtrTable.UnitPerRevolution * mtrTable.PulsePerRevolution;
            pos *= factor;
            //设置速度参数
            SetSpeedParameters((float)speed.Min_Velocity, (float)speed.Max_Velocity, (float)speed.Acceleration, (float)speed.Deceleration, true);
            //绝对位置
            Dll_Zmcaux.ZAux_Direct_SetUnits(Handler, mtrTable.AxisNo, (float)mtrTable.PulsePerRevolution / (float)mtrTable.UnitPerRevolution);
            Dll_Zmcaux.ZAux_Direct_Single_MoveAbs(Handler, mtrTable.AxisNo, (float)pos);
            while (true)
            {
                if (!Get_MovingStatus()) break;
                if (IsMoveTimeOut(st))
                {
                    Stop();
                    return isMoveSuccessful;
                }
            }

            isMoveSuccessful = true;
            return isMoveSuccessful;
        }
        
        public override bool MoveTo(double pos, bool BypassDangerCheck = false)
        {
            //变量
            bool isMoveSuccessful = false;
            DateTime st = DateTime.Now;
            double tempPos = 0;
            Stopwatch sw = Stopwatch.StartNew();
            errorReport = string.Empty;
            
            //公式轴执行方式
            if (this.MtrTable.IsFormulaAxis)
            {
                tempPos = FormulaCalc_UnitToAngle(pos);
                pos = tempPos;
            }

            //安全检查
            if (IsProhibitToMove()) {

                errorReport += "不安全运动";
                return false;
            }
            if (IsDangerousToMove)
            {
                if (DoAvoidDangerousPosAction() == false) {
                    errorReport += "不安全运动";
                    return false;
                }
            }
            if (IsZoneSafeToGo(pos) == false)
            {
                errorReport += "超出限位距离";
                return false;
            }


            //获取速度
            float minVel = 0;
            float maxVel = 0;
            float acc = 0;
            float dec = 0;
            //转算速度参数
            if (mtrTable.IsConvertedToMMperSec)
            {
                Conver_To_Jog_MMPerSec(ref minVel, ref maxVel, ref acc, ref dec);
            }
            else
            {
                var jogMode = this.mtrSpeed.SpeedSettings.FirstOrDefault(x => x.Name == ConstantProperty.SpeedSetting_Jog);
                minVel = (float)jogMode.Min_Velocity;
                maxVel = (float)jogMode.Max_Velocity;
                acc = (float)jogMode.Acceleration;
                dec = (float)jogMode.Deceleration;
            }

         
            double distanceToMove = pos - Get_CurUnitPos();
            double estimateTimeTaken = 0.01 * (Math.Abs(distanceToMove) / maxVel);
            pos = Math.Round(pos, 5, MidpointRounding.AwayFromZero);
            DateTime commmandStartTime = DateTime.Now;

            MtrTable.NewPos = pos;
            
            if (Simulation)
            {
                TimeSpan ts = estimateTimeTaken < 0.5 ?
                                           TimeSpan.FromSeconds(0.5) :
                                           TimeSpan.FromSeconds(estimateTimeTaken);


                TimeSpan ts2 = DateTime.Now - commmandStartTime;
                double temppos = MtrTable.CurPos;
                double tempfliction = 1;
                while (ts2.TotalMilliseconds < ts.TotalMilliseconds)
                {
                    ts2 = DateTime.Now - commmandStartTime;

                    MtrTable.CurPos = temppos + tempfliction * distanceToMove;
                    Thread.Sleep(20);
                    if (isStopReq) break;
                }
                if (!isStopReq)
                {
                    MtrTable.CurPos = MtrTable.NewPos;
                    CurrentPhysicalPos = MtrTable.CurPos;
                }

                TimeSpent = $"{sw.Elapsed.TotalSeconds.ToString("F3")}";
                return true;
            }


            double factor = MtrTable.MotorRealDirectionState == DirectionState.Negative && MtrTable.MotorDisplayDirectionState == DirectionState.Positive ||
                                      MtrTable.MotorRealDirectionState == DirectionState.Positive && MtrTable.MotorDisplayDirectionState == DirectionState.Negative ? -1 : 1;

            //double targetPos = pos / mtrTable.UnitPerRevolution * mtrTable.PulsePerRevolution;
            pos *= factor;
            //设置速度参数
            SetSpeedParameters(minVel, maxVel, (float)acc, (float)dec,false);
            //绝对位置
            sw.Restart();
            Dll_Zmcaux.ZAux_Direct_SetUnits(Handler, mtrTable.AxisNo, (float)mtrTable.PulsePerRevolution / (float)mtrTable.UnitPerRevolution);
            Dll_Zmcaux.ZAux_Direct_Single_MoveAbs(Handler, mtrTable.AxisNo, (float)pos);

            //等待停止
            bool isTimeOut = false;
            st = DateTime.Now;
            while (true)
            {
                if (Simulation) break;
                if (!Get_MovingStatus()) break;
                if (IsHomeTimeOut(st))
                {
                    errorReport += "运动超时";
                    isTimeOut = true;
                    break;
                }
            }
            if (isTimeOut)
            {
                errorReport += "运动超时";
                Stop();
                return isMoveSuccessful;
            }

            TimeSpent = (sw.ElapsedMilliseconds / 1000).ToString("F3");
            isMoveSuccessful = true;
            return isMoveSuccessful;
        }
        public override bool MoveTo(double pos, SpeedSeting speed, bool BypassDangerCheck = false)
        {
            //变量
            bool isMoveSuccessful = false;
            DateTime st = DateTime.Now;
            double tempPos = 0;
            Stopwatch sw = Stopwatch.StartNew();
            errorReport = string.Empty;

            //公式轴执行方式
            if (this.MtrTable.IsFormulaAxis)
            {
                tempPos = FormulaCalc_UnitToAngle(pos);
                pos = tempPos;
            }

            //安全检查
            if (IsProhibitToMove())
            {

                errorReport += "不安全运动";
                return false;
            }
            if (IsDangerousToMove)
            {
                if (DoAvoidDangerousPosAction() == false)
                {
                    errorReport += "不安全运动";
                    return false;
                }
            }
            if (IsZoneSafeToGo(pos) == false)
            {
                errorReport += "超出限位距离";
                return false;
            }

            //获取速度
            float minVel = 0;
            float maxVel = 0;
            float acc = 0;
            float dec = 0;
            //转算速度参数
            if (mtrTable.IsConvertedToMMperSec)
            {
                Conver_To_Jog_MMPerSec(ref minVel, ref maxVel, ref acc, ref dec);
            }
            else
            {
                minVel = (float)speed.Min_Velocity;
                maxVel = (float)speed.Max_Velocity;
                acc = (float)speed.Acceleration;
                dec = (float)speed.Deceleration;
            }


            double distanceToMove = pos - Get_CurUnitPos();
            double estimateTimeTaken = 0.01 * (Math.Abs(distanceToMove) / maxVel);
            pos = Math.Round(pos, 5, MidpointRounding.AwayFromZero);
            DateTime commmandStartTime = DateTime.Now;

            MtrTable.NewPos = pos;

            if (Simulation)
            {
                sw.Restart();
                TimeSpan ts = estimateTimeTaken < 0.5 ?
                                           TimeSpan.FromSeconds(0.5) :
                                           TimeSpan.FromSeconds(estimateTimeTaken);


                TimeSpan ts2 = DateTime.Now - commmandStartTime;
                double temppos = MtrTable.CurPos;
                double tempfliction = 1;
                while (ts2.TotalMilliseconds < ts.TotalMilliseconds)
                {
                    ts2 = DateTime.Now - commmandStartTime;

                    MtrTable.CurPos = temppos + tempfliction * distanceToMove;
                    Thread.Sleep(5);
                    if (isStopReq) break;
                }
                if (!isStopReq)
                {
                    MtrTable.CurPos = MtrTable.NewPos;
                    CurrentPhysicalPos = MtrTable.CurPos;
                }

                TimeSpent = $"{sw.Elapsed.TotalSeconds.ToString("F3")}";
                return true;
            }


            double factor = MtrTable.MotorRealDirectionState == DirectionState.Negative && MtrTable.MotorDisplayDirectionState == DirectionState.Positive ||
                                      MtrTable.MotorRealDirectionState == DirectionState.Positive && MtrTable.MotorDisplayDirectionState == DirectionState.Negative ? -1 : 1;

            //double targetPos = pos / mtrTable.UnitPerRevolution * mtrTable.PulsePerRevolution;
            pos *= factor;
            //设置速度参数
            SetSpeedParameters(minVel, maxVel, (float)acc, (float)dec, false);
            //绝对位置
            sw.Restart();
            Dll_Zmcaux.ZAux_Direct_SetUnits(Handler, mtrTable.AxisNo, (float)mtrTable.PulsePerRevolution / (float)mtrTable.UnitPerRevolution);
            Dll_Zmcaux.ZAux_Direct_Single_MoveAbs(Handler, mtrTable.AxisNo, (float)pos);


            //等待停止
            bool isTimeOut = false;
            st = DateTime.Now;
            while (true)
            {
                if (Simulation) break;
                if (!Get_MovingStatus()) break;
                if (IsHomeTimeOut(st))
                {
                    errorReport += "运动超时";
                    isTimeOut = true;
                    break;
                }
            }
            if (isTimeOut)
            {
                errorReport += "运动超时";
                Stop();
                return isMoveSuccessful;
            }

            TimeSpent = $"{sw.Elapsed.TotalSeconds.ToString("F3")}";
            isMoveSuccessful = true;
            return isMoveSuccessful;
        }
        public override bool MoveToAndStopByIO(double pos, Func<bool> StopAction, bool BypassDangerCheck = false)
        {
            return true;
        }
        public override bool MoveToSafeObservedPos(double pos)
        {
            throw new NotImplementedException();
        }
        public override void Set_Servo(bool on)
        {
            if(this.Simulation)
            {
                this.IsServoOn = on;
                return;
            }

            int val = 0;
            int errorCode = ErrorCodes.NoError;

            if (this.mtrTable.ServoOn_Logic == 0)
                val = on ? 1 : 0;
            else
                val = on ? 0 : 1;
            errorCode = Dll_Zmcaux.ZAux_Direct_SetAxisEnable(Handler, mtrTable.AxisNo, val);

            if (errorCode != ErrorCodes.NoError) { SolveWare.Core.MMgr.Infohandler.LogMessage($"{Name} Set Servo 失败"); }
        }
      
        public override void Stop()
        {
            if(simulation)
            {
                this.IsMoving = false;
                return;
            }

            /*         
          0 （缺省）取消当前运动
          1 取消缓冲的运动
          2 取消当前运动和缓冲运动
          3 立即中断脉冲发送
           */

           int errorCode = ErrorCodes.NoError;
            do
            {
                errorCode = Dll_Zmcaux.ZAux_Direct_Single_Cancel(Handler, mtrTable.AxisNo, 2);
                if (errorCode != ErrorCodes.NoError) break;
                
                errorCode = Dll_Zmcaux.ZAux_Direct_Single_Cancel(Handler, mtrTable.AxisNo, 3);
                if (errorCode != ErrorCodes.NoError) break;

            } while (false);

            if (errorCode != ErrorCodes.NoError) SolveWare.Core.MMgr.Infohandler.LogMessage($"{Name} Stop 功能失败", isError: true);
        }
        public override void SetZero(uint homeMode = 35)
        {
            //TODO - Motor_Zmcaux 设为零点
            Dll_Zmcaux.ZAux_BusCmd_Datum(Handler, (uint)this.mtrTable.AxisNo, homeMode);
        }
        public override Motor_Wait_Kind WaitHomeDone()
        {
            // 判断是否满足初始化条件
            int error = 0;
            uint homestate = 0;
            Motor_Wait_Kind waitKind = Motor_Wait_Kind.Moving;
            DateTime st = DateTime.Now;

            while(true)
            {
                if (WaitStop() == Motor_Wait_Kind.Success)  //!IsMoving(axis)
                    error = Dll_Zmcaux.ZAux_Direct_GetHomeStatus(Handler, mtrTable.AxisNo, ref homestate);

                TimeSpan ts = DateTime.Now - st;
                waitKind = homestate == 0 ? Motor_Wait_Kind.Success : Motor_Wait_Kind.Moving;
                waitKind = ts.TotalMilliseconds > mtrTable.HomeTimeOut ? Motor_Wait_Kind.Fail : Motor_Wait_Kind.Moving;

                if (waitKind == Motor_Wait_Kind.Success || waitKind == Motor_Wait_Kind.Fail) break;
            } 

            if(waitKind == Motor_Wait_Kind.Fail)
                SolveWare.Core.MMgr.Infohandler.LogMessage("ZAux_Direct_GetHomeStatus", isError: true);

            return waitKind;
        }     
        public override Motor_Wait_Kind WaitStop()
        {
            /// <param name="piValue">运动状态反馈值 0-运动中 -1 停止</param>
            
            int response = 0;
            Motor_Wait_Kind waitKind = Motor_Wait_Kind.Moving;
            DateTime st = DateTime.Now;
            while (true)
            {
                //Dll_Zmcaux.ZAux_Direct_GetIfIdle(Handler, mtrTable.AxisNo, ref response);
                TimeSpan ts = DateTime.Now - st;
                Dll_Zmcaux.ZAux_Direct_GetIfIdle(Handler, mtrTable.AxisNo, ref response);
                waitKind = response == -1 ? Motor_Wait_Kind.Success : Motor_Wait_Kind.Moving;

                if (waitKind == Motor_Wait_Kind.Moving)
                {
                    waitKind = ts.TotalMilliseconds > mtrTable.MotionTimeOut ? Motor_Wait_Kind.Fail : Motor_Wait_Kind.Moving;
                }



                if (waitKind == Motor_Wait_Kind.Success || waitKind == Motor_Wait_Kind.Fail) break;
                Thread.Sleep(10);
            }
            //Task task = new Task(() =>
            //{


            //});
            //task.Start();
            //task.Wait(5);

            return waitKind;
        }
        public  float GetMpos()
        {
            //判断是否满足初始化条件


            float pos = 0;
            int error = 0;

            try
            {
                error = Dll_Zmcaux.ZAux_Direct_GetMpos(Handler, mtrTable.AxisNo, ref pos);               
            }
            catch (Exception)
            {
                error = ErrorCodes.MotorMoveError;
            }
            
            if(error != ErrorCodes.NoError) SolveWare.Core.MMgr.Infohandler.LogMessage("无法获得 ZAux_Direct_GetMpos", isError: true);

            return pos;
        }    
        private bool SetSpeedParameters(float minVel, float maxVel, float acc, float dec, bool homeCreep)
        {
            int errorCode = ErrorCodes.NoError;
            bool isSetOK = false;
            do
            {
                //设置最小速度
                errorCode = Dll_Zmcaux.ZAux_Direct_SetLspeed(Handler, mtrTable.AxisNo, minVel);
                if (errorCode != ErrorCodes.NoError) break;

                //设置运行速度
                errorCode = Dll_Zmcaux.ZAux_Direct_SetSpeed(Handler, mtrTable.AxisNo, maxVel);
                if (errorCode != ErrorCodes.NoError) break;

                //设置加速度
                errorCode = Dll_Zmcaux.ZAux_Direct_SetAccel(Handler, mtrTable.AxisNo, acc);
                if (errorCode != ErrorCodes.NoError) break;

                //设置减速度
                errorCode = Dll_Zmcaux.ZAux_Direct_SetDecel(Handler, mtrTable.AxisNo, dec);
                if (errorCode != ErrorCodes.NoError) break;

                //设置S曲线
                errorCode = Dll_Zmcaux.ZAux_Direct_SetSramp(Handler, mtrTable.AxisNo, 0.2f);
                if (errorCode != ErrorCodes.NoError) break;

               if(homeCreep)
                {
                    errorCode = Dll_Zmcaux.ZAux_Direct_SetCreep(Handler, mtrTable.AxisNo, mtrTable.HomeCreepSpeedRate);
                    if (errorCode != ErrorCodes.NoError) break;
                }


            } while (false);

            isSetOK = errorCode == ErrorCodes.NoError;
            return isSetOK;
        }
        private bool IsZoneSafeToGo(double pos)
        {
            if(pos < mtrTable.MinDistance_SoftLimit || pos > mtrTable.MaxDistance_SoftLimit)
            {
                return false;
            }

            //if (this.IsInZone == false) return true;

            //var foundZone = this.MtrSafe.ZoneSafeDatas.ToList().Find(x => x.IsInZone == true);
            //if (pos < foundZone.AllowableMinPos || pos > foundZone.AllowableMaxPos) return false;

            return true;
        }

        public override bool Get_MovingStatus()
        {
            if (Simulation)
            {
                return IsMoving;
            }

            int pValue = 0;
            Dll_Zmcaux.ZAux_Direct_GetIfIdle(Handler, mtrTable.AxisNo, ref pValue);
            return pValue == 0;
        }
    }
}
