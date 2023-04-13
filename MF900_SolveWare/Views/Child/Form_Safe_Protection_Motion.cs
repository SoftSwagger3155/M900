using MF900_SolveWare.Safe;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
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
    public partial class Form_Safe_Protection_Motion : Form, IView
    {
        public Form_Safe_Protection_Motion()
        {
            InitializeComponent();
        }

        DetailData_Safe_Pos data;
        public void Setup<TObj>(TObj obj)
        {
           data = obj as DetailData_Safe_Pos;
        }

        private void Form_Safe_Protection_Motion_Load(object sender, EventArgs e)
        {
            Fillup_Combobox_Motor();
            DataBinding();
        }
        private void Fillup_Combobox_Motor()
        {
            this.cmb_Selector_Motor.Items.Clear();
            var motors = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor).Get_All_Item_Name().ToArray();
            this.cmb_Selector_Motor.Items.AddRange(motors);
        }
        private void DataBinding()
        {
            ckb_Selected.DataBindings.Add(nameof(ckb_Selected.Checked), data, nameof(data.IsSelected));
            txb_Priority.DataBindings.Add(nameof(txb_Priority.Text), data, nameof(data.Priority));
            txb_Pos.DataBindings.Add(nameof(txb_Pos.Text), data, nameof(data.Pos));
            cmb_Selector_Motor.DataBindings.Add(nameof(cmb_Selector_Motor.SelectedItem), data, nameof(data.MotorName));
        }

        private void txb_Priority_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txb_Priority.Text)) { return; }
            data.Priority = int.Parse(txb_Priority.Text);   
        }

        private void txb_Pos_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_Pos.Text)) { return; }
            data.Pos = Math.Round(double.Parse(txb_Pos.Text), 3);
        }

        private void cmb_Selector_Motor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string mtr = (sender as ComboBox).SelectedItem as string;
            data.MotorName = mtr;
        }
    }
}
