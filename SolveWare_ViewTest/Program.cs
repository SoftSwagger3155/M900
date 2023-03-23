using HVision;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Camera.Business;
using SolveWare_Service_Tool.Camera.Data;
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

            IView testView = new Form_ImageHost();
            testView.Setup(new Camera_Basler(new ConfigData_Camera()));
            Application.Run(testView as Form);

            //UserHWControls userForm = new UserHWControls();
            //Form form = new Form();
            //form.Controls.Add(userForm);
            //Application.Run(form);
        }
    }
}
