using SolveWare_Service_Core.Definition;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
