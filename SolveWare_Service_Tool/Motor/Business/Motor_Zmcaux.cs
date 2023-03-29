using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Abstract;
using SolveWare_Service_Tool.Dlls;
using SolveWare_Service_Tool.MasterDriver.Business;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Tool.Motor.Definition;
using System;
using System.Collections.Generic;
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
        public Motor_Zmcaux(IElement configData) : base(configData)
        {
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
            return 0;
        }

        public override double Get_CurUnitPos()
        {
            double position = 0;

            if (Simulation) return CurrentPhysicalPos;


            if (this.mtrTable.IsFormulaAxis)
            {
                double mm = CurrentPulse * mtrTable.UnitPerRevolution / mtrTable.PulsePerRevolution;
                position = FormulaCalc_AngleToUnit(mm);
            }
            else
            {
                position = CurrentPulse * mtrTable.UnitPerRevolution / mtrTable.PulsePerRevolution;
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
            return false;
        }
        public override bool Get_PEL_Signal()
        {
            if (Simulation) return false;
            return (((IO_Status_Zmcaux)axis_Read_Status & IO_Status_Zmcaux.正向硬限位) == IO_Status_Zmcaux.正向硬限位);
        }
        public override bool Get_ServoStatus()
        {
            return true;
        }

        public override bool HomeMove()
        {
            bool isHomeSuccessful = false;
            string sErr = string.Empty;
            uint homeStatus = 0;
            uint revStatus = 0;
            int homemode = 0;
            DateTime st = DateTime.Now;

            if (IsProhibitToHome()) { return false; }


            //HasHome = false;
            isStopReq = false;
            Set_Servo(false);
            Thread.Sleep(100);
            Set_Servo(true);


            isStopReq = false;

            if (Simulation)
            {
                MtrTable.NewPos = 0;
                double DistanceToMove = 0 - currentPulse;
                double EstimateTimeTaken = 0.01 * (Math.Abs(DistanceToMove) / mtrSpeed.Home_Max_Velocity);
                MoveTo(0);
                return true;
            }
            Dll_Zmcaux.ZAux_Direct_SetRevIn(Handler, mtrTable.AxisNo, mtrTable.Param_Rev_Limit); //设置负限位
            Dll_Zmcaux.ZAux_Direct_SetFwdIn(Handler, mtrTable.AxisNo, mtrTable.Param_Fwd_Limit); //设置正限位
            Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Home_IO, ref homeStatus);
            Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Rev_Limit, ref revStatus);

            //先往原点方向一直走，直到到达限位
            if (homeStatus != 0 && revStatus != 0)              
                if (MoveTo(-3000) == false) return false;

            //回零方式判断
            while (true)  //IsMoving(axis)  resetStop
            {
                Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Home_IO, ref homeStatus);
                Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.Param_Rev_Limit, ref revStatus);
                if (homeStatus == 0)
                {
                    homemode = 4;
                    Stop();
                    break;
                }
                else if (revStatus == 0)
                {
                    homemode = 3;
                    Stop();
                    //System.Windows.Forms.MessageBox.Show("负限位信号触发");
                    break;
                }
                Thread.Sleep(10);
            }

            //等待停止
            if( WaitStop() == Motor_Wait_Kind.Fail)
                return false;

            //设置HomeIO
            Dll_Zmcaux.ZAux_Direct_SetDatumIn(Handler, mtrTable.AxisNo, mtrTable.Param_Home_IO);

            //执行回原点
            Dll_Zmcaux.ZAux_Direct_Single_Datum(Handler, mtrTable.AxisNo, homemode);
            //等待停止
            if (WaitStop() == Motor_Wait_Kind.Fail)
                return false;
            Thread.Sleep(50);

            if (MoveTo(20) == false) return false;
            if (WaitStop() == Motor_Wait_Kind.Fail)
                return false;
            Thread.Sleep(20);

            //设置HomeIO
            Dll_Zmcaux.ZAux_Direct_SetDatumIn(Handler, mtrTable.AxisNo, mtrTable.Param_Home_IO);

            //执行回原点
            Dll_Zmcaux.ZAux_Direct_Single_Datum(Handler, mtrTable.AxisNo, homemode);
            //等待停止
            if (WaitStop() == Motor_Wait_Kind.Fail)
                return false;
            Thread.Sleep(100);
            Dll_Zmcaux.ZAux_Direct_SetMpos(Handler, mtrTable.AxisNo, 0.0f);  //重置编码器位置
            Dll_Zmcaux.ZAux_BusCmd_Datum(Handler, (uint)mtrTable.AxisNo, 35);

            return isHomeSuccessful;
        }
        public override bool Init()
        {
            var master = (SolveWare.Core.MMgr as MainManagerBase).MasterDriver as MasterDriverManager;
            Handler = master.CardInfo.Dic_CardHandler[mtrTable.CardNo];
            Dll_Zmcaux.ZAux_Direct_SetUnits(Handler, mtrTable.AxisNo, (float)mtrTable.PulsePerRevolution);
            return true;
        }
        public override void Jog(bool isPositive)
        {
            if (isMoving) return;
            
            //变量
            int error = 0;
            float minVel = 0;
            float maxVel = 0;
            double acc = 0;
            double dec = 0;
            int dir = 0;

            try
            {
                do
                {
                    Conver_To_Jog_MMPerSec(ref minVel, ref maxVel, ref acc, ref dec);
                    error = SetSpeedParameters(minVel, maxVel, (float)acc, (float)dec) ? ErrorCodes.NoError : ErrorCodes.MotorMoveError;
                    if (error != ErrorCodes.NoError) break;
                    //设置方向并运动
                    error = Dll_Zmcaux.ZAux_Direct_Single_Vmove(Handler, mtrTable.AxisNo, dir = isPositive ? 1 : -1);
                
                } while (false);
            }
            catch
            {
                error = ErrorCodes.MotionFunctionError;
            }
            if(error != ErrorCodes.NoError) SolveWare.Core.MMgr.Infohandler.LogMessage($"{Name} Jog 失败");
        }

        public override bool ManualMoveTo(double pos)
        {
            return true;
        }

        public override bool MoveRelative(double distance, MtrSpeed mtrSpeed, bool BypassDangerCheck = false)
        {
            double currPoints = Get_CurUnitPos();
            MoveTo(currPoints + distance, mtrSpeed, BypassDangerCheck);
            return true;
        }
        public override bool MoveTo(double pos, MtrSpeed mtrSpeed, bool BypassDangerCheck = false)
        {
            bool isMoveSuccessful = false;
            DateTime st = DateTime.Now;

            double tempPos = 0;
            if (this.MtrTable.IsFormulaAxis)
            {
                tempPos = FormulaCalc_UnitToAngle(pos);
                pos = tempPos;
            }

            if (IsProhibitToMove()) return false;
            if (IsDangerousToMove)
            {
                if (DoAvoidDangerousPosAction() == false) return false;
                //if (IsDangerousToMove) return false;
            }
            if (IsZoneSafeToGo(pos) == false) return false;


            //float minVel = 0;
            //float maxVel = 0;
            //double acc = 0;
            //double dec = 0;
            ////转算速度参数
            //Conver_To_Jog_MMPerSec(ref minVel, ref maxVel, ref acc, ref dec);


            double distanceToMove = pos - Get_CurUnitPos();
            double estimateTimeTaken = 0.01 * (Math.Abs(distanceToMove) / mtrSpeed.Jog_Max_Velocity);
            pos = Math.Round(pos, 5, MidpointRounding.AwayFromZero);
            DateTime commmandStartTime = DateTime.Now;

            MtrTable.NewPos = pos;
            double targetPos = pos / mtrTable.UnitPerRevolution * mtrTable.PulsePerRevolution;
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
                    //tempfliction = ts2.TotalMilliseconds / ts.TotalMilliseconds;
                    //if (tempfliction > 1)
                    //    tempfliction = 1;

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


            double factor = MtrTable.MotorRealDirectionState == DirectionState.Negative && MtrTable.MotorDisplayDirectionState == DirectionState.Positive ||
                                     MtrTable.MotorRealDirectionState == DirectionState.Positive && MtrTable.MotorDisplayDirectionState == DirectionState.Negative ? -1 : 1;

            targetPos *= factor;
            //设置速度参数
            SetSpeedParameters((float)mtrSpeed.Jog_Min_Velocity, (float)mtrSpeed.Jog_Max_Velocity, (float)mtrSpeed.Jog_Acceleration, (float)mtrSpeed.Jog_Deceleration);
            //绝对位置
            Dll_Zmcaux.ZAux_Direct_Single_MoveAbs(Handler, mtrTable.AxisNo, (float)pos);

            isMoveSuccessful = WaitStop() == Motor_Wait_Kind.Success;
            return isMoveSuccessful;
        }
        public override bool MoveTo(double pos, bool BypassDangerCheck = false)
        {
            bool isMoveSuccessful = false;
            DateTime st = DateTime.Now;

            double tempPos = 0;
            if (this.MtrTable.IsFormulaAxis)
            {
                tempPos = FormulaCalc_UnitToAngle(pos);
                pos = tempPos;
            }

            if (IsProhibitToMove()) return false;
            if (IsDangerousToMove)
            {
                if (DoAvoidDangerousPosAction() == false) return false;
                //if (IsDangerousToMove) return false;
            }
            if (IsZoneSafeToGo(pos) == false) return false;


            float minVel = 0;
            float maxVel = 0;
            double acc = 0;
            double dec = 0;
            //转算速度参数
            Conver_To_Jog_MMPerSec(ref minVel, ref maxVel, ref acc, ref dec);

         
            double distanceToMove = pos - Get_CurUnitPos();
            double estimateTimeTaken = 0.01 * (Math.Abs(distanceToMove) / maxVel);
            pos = Math.Round(pos, 5, MidpointRounding.AwayFromZero);
            DateTime commmandStartTime = DateTime.Now;

            MtrTable.NewPos = pos;
            double targetPos = pos / mtrTable.UnitPerRevolution * mtrTable.PulsePerRevolution;
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
                    //tempfliction = ts2.TotalMilliseconds / ts.TotalMilliseconds;
                    //if (tempfliction > 1)
                    //    tempfliction = 1;

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


            double factor = MtrTable.MotorRealDirectionState == DirectionState.Negative && MtrTable.MotorDisplayDirectionState == DirectionState.Positive ||
                                     MtrTable.MotorRealDirectionState == DirectionState.Positive && MtrTable.MotorDisplayDirectionState == DirectionState.Negative ? -1 : 1;

            targetPos *= factor;
            //设置速度参数
            SetSpeedParameters(minVel, maxVel, (float)acc, (float)dec);
            //绝对位置
            Dll_Zmcaux.ZAux_Direct_Single_MoveAbs(Handler, mtrTable.AxisNo, (float)pos);

            isMoveSuccessful = WaitStop() == Motor_Wait_Kind.Success;
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
        public override void StartStatusReading()
        {
            if(this.Simulation) { return; }
            if (readStatusSource != null) return;
            readStatusSource = new CancellationTokenSource();
      
            int motor_OriginValue = 0;
            int motor_Enable = 0;
            int motor_Idle = 0;
            uint motor_OriginSignal = 0;
            float pulse = 0;


            Task task = new Task(() =>
            {
                while (!readStatusSource.IsCancellationRequested)
                {
                    float pfvalue = 0;
                    Dll_Zmcaux.ZAux_Direct_GetDatumIn(Handler, mtrTable.AxisNo, ref motor_OriginValue);
                    Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.AxisNo, ref motor_OriginSignal);  //轴原点
                    Dll_Zmcaux.ZAux_Direct_GetAxisStatus(Handler, mtrTable.AxisNo, ref axis_Read_Status);  //轴状态
                    Dll_Zmcaux.ZAux_Direct_GetAxisEnable(Handler, mtrTable.AxisNo, ref motor_Enable);  //轴使能
                    Dll_Zmcaux.ZAux_Direct_GetIfIdle(Handler, mtrTable.AxisNo, ref motor_Idle); //  <param name="piValue">运动状态反馈值 0-运动中 -1 停止</param>
                    Dll_Zmcaux.ZAux_Direct_GetDpos(Handler, mtrTable.AxisNo, ref pfvalue);
                    Dll_Zmcaux.ZAux_Direct_GetMpos(Handler, mtrTable.AxisNo, ref pulse);

                    this.IsPosLimit = Get_PEL_Signal();
                    this.IsNegLimit = Get_NEL_Signal();
                    this.IsAlarm = Get_Alarm_Signal();
                    this.IsServoOn = motor_Enable == 1 ? true : false;
                    this.IsOrg = motor_OriginSignal== 1 ? true : false;
                    this.CurrentPhysicalPos = pfvalue;
                    this.CurrentPulse = pulse;
                    this.isMoving = motor_Idle == 0 ? true : false;
                    Thread.Sleep(mtrTable.StatusReadTiming);
                }

                cancelDoneFlag.Set();

            }, readStatusSource.Token, TaskCreationOptions.LongRunning);
            task.Start();
        }
        public override void Stop()
        {
            if(simulation)
            {
                this.isMoving = false;
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
        public override Motor_Wait_Kind WaitHomeDone()
        {
            // 判断是否满足初始化条件
            int error = 0;
            uint homestate = 0;
            Motor_Wait_Kind waitKind = Motor_Wait_Kind.UnKnown;
            DateTime st = DateTime.Now;

            while(true)
            {
                if (WaitStop() == Motor_Wait_Kind.Success)  //!IsMoving(axis)
                    error = Dll_Zmcaux.ZAux_Direct_GetHomeStatus(Handler, mtrTable.AxisNo, ref homestate);

                TimeSpan ts = DateTime.Now - st;
                waitKind = homestate == 0 ? Motor_Wait_Kind.Success : Motor_Wait_Kind.UnKnown;
                waitKind = ts.TotalMilliseconds > mtrTable.HomeTimeOut ? Motor_Wait_Kind.Fail : Motor_Wait_Kind.UnKnown;

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
            Motor_Wait_Kind waitKind = Motor_Wait_Kind.UnKnown;
            DateTime st = DateTime.Now;
                       
            Task task = new Task(() =>
            {
                while(true)
                {
                    //Dll_Zmcaux.ZAux_Direct_GetIfIdle(Handler, mtrTable.AxisNo, ref response);
                    TimeSpan ts = DateTime.Now - st;
                    waitKind = !isMoving? Motor_Wait_Kind.Success : Motor_Wait_Kind.UnKnown;
                    waitKind = ts.TotalMilliseconds > mtrTable.MotionTimeOut ? Motor_Wait_Kind.Fail: Motor_Wait_Kind.UnKnown;

                    if (waitKind == Motor_Wait_Kind.Success || waitKind == Motor_Wait_Kind.Fail) break;
                }

            });
            task.Start();
            task.Wait(5);

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
        private bool SetSpeedParameters(float minVel, float maxVel, float acc, float dec)
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

            } while (false);

            isSetOK = errorCode == ErrorCodes.NoError;
            return isSetOK;
        }
        private bool IsZoneSafeToGo(double pos)
        {
            if (this.IsInZone == false) return true;

            var foundZone = this.MtrSafe.ZoneSafeDatas.ToList().Find(x => x.IsInZone == true);
            if (pos < foundZone.AllowableMinPos || pos > foundZone.AllowableMaxPos) return false;

            return true;
        }

    }
}
