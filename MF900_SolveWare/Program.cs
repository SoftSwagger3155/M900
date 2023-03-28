using MF900_SolveWare.Business;
using MF900_SolveWare.Resource;
using SolveWare_Service_Core;
using SolveWare_Service_Tool.MasterDriver.Business;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision;
using Sunny.UI.Win32;
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

            //UserHWControls control = new UserHWControls();
            //control.Setup(ResourceKey.Top_Camera.GetCamera());
            //Form form = new Form();
            //form.Controls.Add(control);
            //Application.Run(form);
            Application.Run(new MainForm());
        }
    }
}
