using SolveWare_Service_Core;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MF900_SolveWare.Views
{
    public static class ViewHelper
    {
        public static void SetStatus(this Control lbl, JobStatus status)
        {
            switch (status)
            {
                case JobStatus.Unknown:
                    lbl.Text = "未知";
                    lbl.BackColor = Color.Yellow;
                    break;
                case JobStatus.Fail:
                    lbl.Text = "失败";
                    lbl.BackColor = Color.IndianRed;
                    break;
                case JobStatus.Done:
                    lbl.Text = "成功";
                    lbl.BackColor = Color.Green;
                    break;
            }
        }
        public static void GetSpentTime(this Control lbl, Stopwatch sWatch)
        {
            if (!sWatch.IsRunning) return;
            lbl.Text = $"{(sWatch.ElapsedMilliseconds / 1000).ToString("F3")}";
            sWatch.Stop();
        }
        public static void DoButtonTask(this bool action)
        {
            Task.Run(() =>
            {
                if (SolveWare.Core.MMgr.Status == Machine_Status.Busy) return;
                if (SolveWare.Core.MMgr.Status == Machine_Status.Initialising) return;
                if (SolveWare.Core.MMgr.Status == Machine_Status.SingleCycle) return;
                if (SolveWare.Core.MMgr.Status == Machine_Status.Auto) return;

                string errMsg = string.Empty;
                SolveWare.Core.MMgr.SetStatus(Machine_Status.Busy);
                errMsg = action ? ErrorCodes.GetErrorDescription(ErrorCodes.ActionFailed) : string.Empty;
                if (errMsg == string.Empty) SolveWare.Core.MMgr.SetStatus(Machine_Status.Idle);
                else
                {
                    SolveWare.Core.MMgr.SetStatus(Machine_Status.Error);
                    SolveWare.Core.MMgr.Infohandler.LogMessage(errMsg, true, true);
                }

            });
        }
    }
}
