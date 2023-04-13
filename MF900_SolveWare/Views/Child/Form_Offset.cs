using MF900_SolveWare.Safe;
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

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_Offset : Form, IView
    {
        public Form_Offset()
        {
            InitializeComponent();
        }

        public void Setup<TObj>(TObj obj)
        {
           
        }

        private void btn_Top_Safe_Click(object sender, EventArgs e)
        {
            IView view = new Form_Safe_Protection();
            view.Setup(new Data_Safe());
            (view as Form).Show();
        }
    }
}
