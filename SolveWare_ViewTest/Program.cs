using HVision;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Camera.Base.Interface;
using SolveWare_Service_Tool.Camera.Business;
using SolveWare_Service_Tool.Camera.Data;
using SolveWare_Service_Vision;
using SolveWare_Service_Vision.View.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolveWare_ViewTest
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


            IView testView = new SolveWare_Service_Vision.UserHWControls();
            ConfigData_Camera data = new ConfigData_Camera { IsSimulation = true };
            ICamera camera = new Camera_Basler(data);
            testView.Setup(camera);
            Form form = new Form();
            form.Controls.Add(testView as Control);
            Application.Run(form);

            //UserHWControls userForm = new UserHWControls();
            //Form form = new Form();
            //form.Controls.Add(userForm);
            //Application.Run(form);
        }
    }
}
