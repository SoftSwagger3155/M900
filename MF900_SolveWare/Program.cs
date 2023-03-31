using MF900_SolveWare.Business;
using MF900_SolveWare.Resource;
using SolveWare_Service_Core;
using SolveWare_Service_Tool.MasterDriver.Business;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision;
using SolveWare_Service_Vision.Controller.Base.Abstract;
using SolveWare_Service_Vision.Controller.Base.Interface;
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

            //SolveWare_Service_Vision.UserHWControl control = new SolveWare_Service_Vision.UserHWControl();        
            //control.Setup(ResourceKey.Top_Camera.GetCamera());
            //Form form = new Form();
            //form.FormClosing += (s, e) =>
            //{
            //    ResourceKey.Top_Camera.GetCamera().CloseCamera();
            //};
            //form.Controls.Add(control);
            //Application.Run(form);
            Application.Run(new MainForm());
        }
    }
}
