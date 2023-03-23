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

namespace MF900_SolveWare
{
    public partial class FormJigOpations : Form, IView
    {
        public FormJigOpations()
        {
            InitializeComponent();
        }

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            //气缸松开
            
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
