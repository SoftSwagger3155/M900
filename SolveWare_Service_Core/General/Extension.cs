using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace SolveWare_Service_Core.General
{
    public static  class Extension
    {
        public static bool NotPass(ref this int errorCode)
        {
            bool notPass = true;
            if (errorCode != ErrorCodes.NoError)
            {
                notPass = true;
            }
            else if (errorCode == ErrorCodes.NoError)
            {
                notPass = false;
            }
            else if (SolveWare.Core.MMgr.IsStop)
            {
                errorCode = ErrorCodes.MachineStopCall;
                notPass = true;
            }


            return notPass;
        }
        public static bool NotPass(this int errorCode, ref string msg, string additionalMsg = "")
        {
            bool notPass = true;
            if (errorCode != ErrorCodes.NoError)
            {
                notPass = true;
            }
            else if (errorCode == ErrorCodes.NoError)
            {
                notPass = false;
                msg = string.Empty;
                return notPass;
            }
            else if (SolveWare.Core.MMgr.IsStop) 
            {
                errorCode = ErrorCodes.MachineStopCall; 
                notPass = true; 
            }

            msg = ErrorCodes.GetErrorDescription(errorCode)+ "\n" + additionalMsg;
            return notPass;
        }
        public static string ErrorMsg(this string function)
        {
            return $"功能 [{function}] Error:\n";
        }
        public static string ErrorMsg(this string function, string title)
        {
            return $"{title} 功能 [{function}] Error:\n";
        }
        public static string OkMsg(this string function)
        {
            return $"功能 [{function}] 成功\n";
        }
        public static string OkMsg(this string function, string title)
        {
            return $"{title} 功能 [{function}] 成功\n";
        }
    }
}
