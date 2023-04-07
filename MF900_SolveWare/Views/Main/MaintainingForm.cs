using MF900_SolveWare.Views.AxisMesForm;
using MF900_SolveWare.Views.Child;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Utility.Extension;
using Sunny.UI;
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
    public partial class MaintainingForm : Form, IView
    {
        private IODebugForm ioDebug;
        private AxisDebugForm axisDebug;
        public MaintainingForm()
        {
            InitializeComponent();          
        }

       
        public void Setup<TData>(TData data)
        {
            
        }

        private void btn_IoShow_Click(object sender, EventArgs e)
        {
            ioDebug = new IODebugForm();
            ioDebug.Show();
        }

        private void btn_AxisDebugShow_Click(object sender, EventArgs e)
        {
            Form_Axis_Configuration form = new Form_Axis_Configuration();
            form.Show();
            //axisDebug = new AxisDebugForm();
            //axisDebug.Show();
        }

        private void btn_InspectKit_Click(object sender, EventArgs e)
        {
            Form_InspectKit form_InspectKit = new Form_InspectKit();
            form_InspectKit.Show();
        }

        private void btn_All_Motor_Click(object sender, EventArgs e)
        {
             IView view = new Form_Axis_General_Controller();
            (view as Form).Show();

        }

        private void btn_Gold_Center_Click(object sender, EventArgs e)
        {
            IView view = new Form_Learn_Global_Center();
            (view as Form).Show();
        }

        private void btn_Offset_Click(object sender, EventArgs e)
        {
            IView view = new Form_Offset();
            (view as Form).Show();
        }

        private void btn_MMperPixel_Click(object sender, EventArgs e)
        {
            IView view = new Form_MMperPixel();
            (view as Form).Show();
        }
    }
}
