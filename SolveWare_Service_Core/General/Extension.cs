using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Core.General
{
    public static  class Extension
    {
        public static bool NotPass(ref this int errorCode)
        {
            if (errorCode != ErrorCodes.NoError) return true;
            else if (errorCode == ErrorCodes.NoError) return false;
            else if (errorCode == ErrorCodes.NoError && SolveWare.Core.MMgr.IsStop) { errorCode = ErrorCodes.MachineStopCall; return true; }

            return false;
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
