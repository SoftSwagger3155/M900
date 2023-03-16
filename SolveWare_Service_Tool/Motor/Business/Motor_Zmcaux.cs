using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Manager.Base.Abstract;
using SolveWare_Service_Tool.Dlls;
using SolveWare_Service_Tool.MasterDriver.Business;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public override bool Get_Alarm_Signal()
        {
            if (Simulation) return false;
            return (((IO_Status_Zmcaux)motor_Status & IO_Status_Zmcaux.告警信号输入) == IO_Status_Zmcaux.告警信号输入);
        }

        public override double Get_AnalogInputValue()
        {
            throw new NotImplementedException();
        }

        public override double Get_CurPulse()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override int Get_IO_sts()
        {
            return 0;
        }

        public override bool Get_NEL_Signal()
        {
            if (Simulation) return false;
            return (((IO_Status_Zmcaux)motor_Status & IO_Status_Zmcaux.反向硬限位) == IO_Status_Zmcaux.反向硬限位);
        }

        public override bool Get_Origin_Signal()
        {
            return false;
        }

        public override bool Get_PEL_Signal()
        {
            if (Simulation) return false;
            return (((IO_Status_Zmcaux)motor_Status & IO_Status_Zmcaux.正向硬限位) == IO_Status_Zmcaux.正向硬限位);
        }

        public override bool Get_ServoStatus()
        {
            throw new NotImplementedException();
        }

        public override bool HomeMove()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override bool ManualMoveTo(double pos, float slowFactor = 1)
        {
            throw new NotImplementedException();
        }

        public override bool MoveTo(double pos, bool BypassDangerCheck = false, float slowFactor = 1)
        {
            throw new NotImplementedException();
        }

        public override bool MoveToAndStopByIO(double pos, Func<bool> StopAction, bool BypassDangerCheck = false, float slowFactor = 1)
        {
            throw new NotImplementedException();
        }

        public override bool MoveToSafeObservedPos(double pos)
        {
            throw new NotImplementedException();
        }

        public override void Set_Servo(bool on)
        {
            throw new NotImplementedException();
        }

        int motor_Status = 0;
        int motor_OriginValue = 0;
        uint motor_OriginSignal = 0;
        int motor_Enable = 0;
        public override void StartStatusReading()
        {
            if (readStatusSource != null) return;
            readStatusSource = new CancellationTokenSource();
            
           

            Task task = new Task(() =>
            {
                while (!readStatusSource.IsCancellationRequested)
                {
                    float pfvalue = 0;
                    Dll_Zmcaux.ZAux_Direct_GetDatumIn(Handler, mtrTable.AxisNo, ref motor_OriginValue);
                    Dll_Zmcaux.ZAux_Direct_GetIn(Handler, mtrTable.AxisNo, ref motor_OriginSignal);  //轴原点
                    Dll_Zmcaux.ZAux_Direct_GetAxisStatus(Handler, mtrTable.AxisNo, ref motor_Status);  //轴状态
                    Dll_Zmcaux.ZAux_Direct_GetAxisEnable(Handler, mtrTable.AxisNo, ref motor_Enable);  //轴使能
                    Dll_Zmcaux.ZAux_Direct_GetDpos(Handler, mtrTable.AxisNo, ref pfvalue);

                    this.IsPosLimit = Get_PEL_Signal();
                    this.IsNegLimit = Get_NEL_Signal();
                    this.IsAlarm = Get_Alarm_Signal();
                    this.IsServoOn = motor_Enable == 1 ? true : false;
                    this.IsOrg = motor_OriginSignal== 1 ? true : false;
                    this.CurrentPhysicalPos = pfvalue;
                }

                cancelDoneFlag.Set();

            }, readStatusSource.Token, TaskCreationOptions.LongRunning);
            task.Start();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override bool WaitHomeDone()
        {
            throw new NotImplementedException();
        }

        public override bool WaitStop()
        {
            throw new NotImplementedException();
        }
    }
}
