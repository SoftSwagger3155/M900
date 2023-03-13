using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool result = false;
            Application.EnableVisualStyles();
            Mutex mutex = new Mutex(true, Application.ProductName, out result);
            if (!result)
            {
                MessageBox.Show("不能打开多个软件");
                return;
            }
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
