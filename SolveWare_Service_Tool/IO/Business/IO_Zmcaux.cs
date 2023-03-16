using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Dlls;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Data;
using SolveWare_Service_Tool.IO.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.IO.Business
{
    public class IO_Zmcaux : IOBase
    {
        public IO_Zmcaux(IElement data) : base(data)
        {
            
        }

        public override void Off()
        {
            int errorCode = ErrorCodes.NoError;
            string exMSg = string.Empty;
            int triggerMode = configData.Logic_Op == 0 ? 1 : 0;

            try
            {
                errorCode = Dll_Zmcaux.ZAux_Direct_SetOp((IntPtr)configData.CardNo, configData.Bit, (uint)triggerMode);
            }
            catch (Exception ex)
            {
                errorCode = ErrorCodes.IOFunctionError;
                exMSg = ex.Message;
            }

            if (errorCode != ErrorCodes.NoError) SolveWare.Core.MMgr.Infohandler.LogMessage($"IOCard No: {configData.CardNo} IO: {configData.Name} OFF 功能 失效\r\n{exMSg}");
        }

        public override void On()
        {
            int errorCode = ErrorCodes.NoError;
            string exMSg = string.Empty;
            int triggerMode = configData.Logic_Op == 0 ? 0 : 1;

            try
            {
                errorCode = Dll_Zmcaux.ZAux_Direct_SetOp((IntPtr)configData.CardNo, configData.Bit, (uint)triggerMode);
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
                if (configData.Simulation)
                {
                    Status = IO_Status.Off;
                    return;
                }

                //0：低电平，1：高电平
                int errorCode = ErrorCodes.NoError;
                uint ioStatus = 0;

                if (this.IOType == IO_Type.Input)
                    errorCode = Dll_Zmcaux.ZAux_Direct_GetIn((IntPtr)configData.CardNo, configData.Bit, ref ioStatus);
                else if (this.IOType ==  IO_Type.Output)
                    errorCode = Dll_Zmcaux.ZAux_Direct_GetOp((IntPtr)configData.CardNo, configData.Bit, ref ioStatus);


                if (configData.Logic_Read == 0)
                    Status = ioStatus == 0 ? IO_Status.Off : IO_Status.On;
                else
                    Status = ioStatus == 0 ? IO_Status.On : IO_Status.Off;

            }
            catch (Exception ex)
            {

            }
        }
    }
}
