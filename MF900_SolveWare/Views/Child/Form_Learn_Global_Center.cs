using MF900_SolveWare.Views.AxisMesForm;
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
    public partial class Form_Learn_Global_Center : Form, IView
    {
        public Form_Learn_Global_Center()
        {
            InitializeComponent();
        }

        public void Setup<TObj>(TObj obj)
        {
            
        }

        private void btn_Motor_General_Controller_Click(object sender, EventArgs e)
        {
            IView view = new Form_Axis_General_Controller();
            (view as Form).Show();
        }
    }
}
