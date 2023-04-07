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

        private void btn_Clear_Click(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

        }

        private void btn_Execute_Both_Module_Click(object sender, EventArgs e)
        {

        }

        private void btn_Execute_Top_Moudle_Click(object sender, EventArgs e)
        {

        }

        private void btn_Execute_Btm_Moudle_Click(object sender, EventArgs e)
        {

        }

        private void btn_Top_Module_Update_Pos_Click(object sender, EventArgs e)
        {

        }

        private void btn_Btm_Module_Update_Pos_Click(object sender, EventArgs e)
        {

        }

        private void btn_Top_Module_Go_Pos_Click(object sender, EventArgs e)
        {

        }

        private void btn_Btm_Module_Go_Pos_Click(object sender, EventArgs e)
        {

        }

        private void btn_Top_Module_Update_InspectKit_Click(object sender, EventArgs e)
        {

        }

        private void btn_Btm_Module_Update_InspectKit_Click(object sender, EventArgs e)
        {

        }

        private void btn_Top_Module_Execute_InspectKit_Click(object sender, EventArgs e)
        {

        }

        private void btn_Btm_Module_Execute_InspectKit_Click(object sender, EventArgs e)
        {

        }
    }
}
