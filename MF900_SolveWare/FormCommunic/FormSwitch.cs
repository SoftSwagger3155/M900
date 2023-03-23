using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare
{
    public class FormSwitch
    {
        public static void SwitchForm(Form childForm, Panel panel)
        {
            Form form = null;
            try
            {
                //判断容器中是否有其他的窗体
                foreach (Control item in panel.Controls)
                {
                    if (item is Form)
                    {
                        ((Form)item).Visible = false;
                    }
                }
                if (childForm != null)
                {
                    form = childForm as Form;
                    //嵌入新窗体
                    form.TopLevel = false; //将子窗体设置成非顶级控件
                    form.Parent = panel;
                    form.Dock = DockStyle.Fill;   //随着容S器大小自动调整窗体大小
                    form.Show();
                    form.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体切换错误:" + ex.Message);
            }
        }

    }
}
