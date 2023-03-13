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
    public partial class FormJigOpations : Form
    {
        public FormJigOpations()
        {
            InitializeComponent();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            //气缸松开
            MotionCommons.AirClamp(false);
        }
        //工件手臂释放
        private void uiButton2_Click(object sender, EventArgs e)
        {

        }
        //工件手臂固定
        private void uiButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
