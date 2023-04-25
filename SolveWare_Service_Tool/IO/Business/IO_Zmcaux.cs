using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Core.Manager.Base.Abstract;
using SolveWare_Service_Tool.Dlls;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Data;
using SolveWare_Service_Tool.IO.Definition;
using SolveWare_Service_Tool.MasterDriver.Business;
using SolveWare_Service_Tool.Motor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.IO.Business
{
    public class IO_Zmcaux : IOBase
    {
        IntPtr Handler;
        public IO_Zmcaux(IElement data, bool simulation) : base(data)
        {
            this.Simulation = simulation;
        }

        public override bool Init()
        {
            this.IOType = this.configData.IOType;

            if(this.Simulation == false)
            {

                var master = (SolveWare.Core.MMgr as MainManagerBase).MasterDriver as MasterDriverManager;
                if (master.CardInfo.Dic_CardHandler.Count == 0 && !configData.Simulation) return false;
                if (master.CardInfo.Dic_CardHandler.Count == 0 && configData.Simulation) return true;

                Handler = master.CardInfo.Dic_CardHandler[(this.configData as ConfigData_IO).CardNo];
            }
            if(this.IOType == IO_Type.Output) { this.Off(); }
            return true;
        }

        CancellationTokenSource cancelSource = null;
        AutoResetEvent cancelFlag = new AutoResetEvent(false);

        public override void Off()
        {             
            try
            {
                if (this.Simulation)
                {
                    this.Status = IO_Status.Off;
                    return;
                }
                // 0: 关 1: 开
                if(configData.IsDualChannel)
                {
                    //int triggerMode_1st = configData.Data_DualChannel.TriggerMode_First.ToUpper() == ConstantProperty.ON ? 1 : 0;
                    //int triggerMode_2nd = configData.Data_DualChannel.TriggerMode_Second.ToUpper() == ConstantProperty.ON ? 1 : 0;

                    Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Data_DualChannel.Bit_First, (uint)0);
                    Thread.Sleep(50);
                    Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Data_DualChannel.Bit_Second, (uint)1);
                }
                if (configData.IsBuzzer)
                {
                    if (cancelSource == null) return;
                    if (configData.Data_Buzzer.Is_Interval_Buzzing)
                    {
                        cancelSource.Cancel();
                    }
                    else
                    {
                        int errorCode = Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Bit, (uint)0);
                        if (errorCode != ErrorCodes.NoError) { SolveWare.Core.ShowMsg(ErrorCodes.GetErrorDescription(ErrorCodes.IOFunctionError)); }
                    }
                }
                else
                {
                    int errorCode = Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Bit, (uint)0);
                    if(errorCode != ErrorCodes.NoError) { SolveWare.Core.ShowMsg(ErrorCodes.GetErrorDescription(ErrorCodes.IOFunctionError)); }
                }
            }
            catch (Exception ex)
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage($"IOCard No: {configData.CardNo} IO: {configData.Name} OFF 功能 失效\r\n{ex.Message}");
            }    
        }

        public override void On()
        {
            int errorCode = ErrorCodes.NoError;
            string exMSg = string.Empty;

            try
            {
                if (this.Simulation)
                {
                    this.Status = IO_Status.On;
                    return;
                }

                if(configData.IsBuzzer) 
                {
                    if (configData.Data_Buzzer.Is_Interval_Buzzing)
                    {
                        cancelSource = new CancellationTokenSource();
                        Task.Run(() =>
                        {
                            while (true)
                            {
                                errorCode = Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Bit, (uint)1);
                                if (cancelSource.IsCancellationRequested) break;
                                Thread.Sleep(configData.Data_Buzzer.Interval_ms);
                                errorCode = Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Bit, (uint)0);
                                if (cancelSource.IsCancellationRequested) break;
                                Thread.Sleep(configData.Data_Buzzer.Interval_ms);
                            }
                            errorCode = Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Bit, (uint)0);
                        });
                    }
                    else
                    {
                        errorCode = Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Bit, (uint)1);
                    }
                }
                else if (configData.IsDualChannel)
                {
                    //int triggerMode_1st = configData.Data_DualChannel.TriggerMode_First.ToUpper() == ConstantProperty.ON ? 1 : 0;
                    //int triggerMode_2nd = configData.Data_DualChannel.TriggerMode_Second.ToUpper() == ConstantProperty.ON ? 1 : 0;

                    errorCode = Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Data_DualChannel.Bit_Second, (uint)0);
                    Thread.Sleep(50);
                    errorCode = Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Data_DualChannel.Bit_First, (uint)1);
                }
                else if(!configData.IsDualChannel && !configData.IsBuzzer) 
                {
                    errorCode = Dll_Zmcaux.ZAux_Direct_SetOp(Handler, configData.Bit, (uint)1);
                }               
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.IOFunctionError;
                exMSg = ex.Message;
            }

            if (errorCode != ErrorCodes.NoError) SolveWare.Core.MMgr.Infohandler.LogMessage($"IOCard No: {configData.CardNo} IO: {configData.Name} ON 功能 失效\r\n{exMSg}");
        }

        public override void UpdateStatus()
        {
            try
            {
                if (this.Simulation)
                {
                    this.Status = Status;
                    return; 
                }

                //0：低电平，1：高电平
                int errorCode = ErrorCodes.NoError;
                uint ioStatus = 0;

                if (this.IOType == IO_Type.Input)
                {
                    errorCode = Dll_Zmcaux.ZAux_Direct_GetIn(Handler, configData.Bit, ref ioStatus);
                }
                else if (this.IOType ==  IO_Type.Output)
                {
                    if (configData.IsDualChannel)
                    {
                        errorCode = Dll_Zmcaux.ZAux_Direct_GetOp(Handler, configData.Data_DualChannel.Bit_First, ref ioStatus);
                    }
                    else
                    {
                        errorCode = Dll_Zmcaux.ZAux_Direct_GetOp(Handler, configData.Bit, ref ioStatus);
                    }
                    
                }


                Status = ioStatus == 0 ? IO_Status.Off : IO_Status.On;

            }
            catch (Exception ex)
            {
                Status = IO_Status.Off;
            }
        }
    }
}
