using MF900_SolveWare.Business;
using SolveWare_Service_Core;
using SolveWare_Service_Tool.MasterDriver.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SolveWare.Core.SetMMgr(new MainManager());
            SolveWare.Core.MMgr.Setup();
            SolveWare.Core.MMgr.AssignMasterDriver(new MasterDriverManager());
            SolveWare.Core.MMgr.Initialize();


            Application.Run(new MainForm());
        }
    }
}
