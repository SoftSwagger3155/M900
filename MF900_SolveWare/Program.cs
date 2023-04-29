using MF900_SolveWare.Business;
using MF900_SolveWare.Resource;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Tool.MasterDriver.Business;
using SolveWare_Service_Tool.Motor.Base.Abstract;
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
            SolveWare.Core.MMgr.AssignFSM();

            //Stop Motor
            var motors = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor).Get_All_Items().ToList();
            motors.ForEach(x => (x as AxisBase).Stop());

           
            Application.Run(new MainForm());
        }
    }
}
