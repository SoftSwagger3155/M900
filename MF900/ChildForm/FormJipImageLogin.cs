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
    public partial class FormJipImageLogin : Form
    {
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        public FormJipImageLogin()
        {
            InitializeComponent();
            SetControlTag();
        }
        private void SetControlTag()
        {
            x = this.Width;
            y = this.Height;
            AutoSizeControls.SetTag(this);
        }
        private void FormJipImageLogin_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            AutoSizeControls.SetControls(newx, newy, this);
        }
    }
}
