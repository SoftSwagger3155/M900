using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    [Serializable]
    public class ParaFliePath
    {
        public static string SystemParaPath { get; set; } = Application.StartupPath + "\\Parameter\\";
        public static string AxisParaPath { get; set; } = Application.StartupPath + "\\Parameter\\AxisPara\\";
        public static string ProductPath { get; set; } = Application.StartupPath + "\\Parameter\\Products\\";
    }
}
