using SolveWare_Service_Core.Base.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public partial class FormJipImageLogin : Form, IView
    {
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        public FormJipImageLogin()
        {
            InitializeComponent();
        }

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        private void FormJipImageLogin_Resize(object sender, EventArgs e)
        {
          
        }
    }
}
