﻿using log4net.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static bool NotPass(ref this Mission_Report mReport, bool showMsg = false)
        {
            bool notPass = true;
            string msg = string.Empty;
            if (mReport.ErrorCode != ErrorCodes.NoError)
            {
                notPass = true;
                msg = mReport.Message;
            }
            else if (mReport.ErrorCode == ErrorCodes.NoError)
            {
                notPass = false;
            }
            else if (SolveWare.Core.MMgr.IsStop)
            {
                mReport.ErrorCode = ErrorCodes.MachineStopCall;
                notPass = true;
                msg = mReport.Message;
            }

            if(notPass && showMsg) 
                SolveWare.Core.ShowMsg(msg, true);

            return notPass;
        }
        public static void Window_Show_Not_Pass_Message(ref this Mission_Report mReport, int erroCode, string msg)
        {
            mReport.Set(erroCode, msg);
            mReport.NotPass(true);
        }
        public static Mission_Report Converto_Mission_Report(this Data_Mission_Report[] datas)
        {
            Mission_Report context = new Mission_Report();
            foreach (var data in datas)
            {
                context.Message += data.Context.Message;
            }

            if (context.Message != string.Empty)
            {
                context.ErrorCode = ErrorCodes.ActionFailed;
            }

            return context;
        }
        public static Mission_Report Converto_Mission_Report(this List<Task> tasks)
        {
            Mission_Report context = new Mission_Report();
            foreach (var task in tasks)
            {
                Data_Mission_Report data = task.AsyncState as Data_Mission_Report;
                if (string.IsNullOrEmpty(data.Context.Message)) continue;
                context.Message += data.Context.Message;
            }

            if (string.IsNullOrEmpty(context.Message) == false)
            {
                context.ErrorCode = ErrorCodes.ActionFailed;
            }
            else
            {
                context.ErrorCode = ErrorCodes.NoError;
            }

            return context;
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
