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
        public static void SetStatus(this ToolStripStatusLabel lbl, JobStatus status)
        {
            switch (status)
            {
                case JobStatus.Unknown:
                    lbl.Text = "状态 : 未知";
                    lbl.BackColor = Color.Yellow;
                    break;
                case JobStatus.Fail:
                    lbl.Text = "状态 : 失败";
                    lbl.BackColor = Color.Indigo;
                    break;
                case JobStatus.Done:
                    lbl.Text = "状态 : 成功";
                    lbl.BackColor = Color.Green;
                    break;
            }
        }
        public static void GetSpentTime(this ToolStripStatusLabel lbl, Stopwatch sWatch)
        {
            if (!sWatch.IsRunning) return;
            lbl.Text = $"耗时 : {(sWatch.ElapsedMilliseconds / 1000).ToString("F3")} 秒";
            sWatch.Restart();
        }
    }
}
