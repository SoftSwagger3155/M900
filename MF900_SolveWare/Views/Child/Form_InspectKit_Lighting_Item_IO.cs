using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision.Inspection.JobSheet;
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
    public partial class Form_InspectKit_Lighting_Item_IO : Form, IView
    {
        public Form_InspectKit_Lighting_Item_IO()
        {
            InitializeComponent();
            Fillup_Combobox_TriggerMode();
        }

        JobSheet_Lighting lighting;

        public void Setup<TObj>(TObj obj)
        {
            lighting = obj as JobSheet_Lighting;
            MakeUI();
        }
        private void Fillup_Combobox_TriggerMode()
        {
            this.cmb_Selector_TriggerMode.Items.Clear();
            this.cmb_Selector_TriggerMode.Items.AddRange(new[] { ConstantProperty.ON , ConstantProperty.OFF});
        }

        private void MakeUI()
        {
            if (lighting == null) return;

            this.cmb_Selector_TriggerMode.SelectedItem = lighting.TriggerMode.ToUpper();
            this.lbl_Tag.Text = $"物件 : {lighting.IO_Name}";
        }

        private void cmb_Selector_TriggerMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if(lighting == null) return;    
                lighting.TriggerMode = (sender as ComboBox).SelectedItem as string;
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);  
            }
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            try
            {
                if (lighting == null) return;

                switch(lighting.TriggerMode)
                {
                    case ConstantProperty.ON :
                        lighting.IO_Name.GetIOBase().On();
                        break;

                    case ConstantProperty.OFF :
                        lighting.IO_Name.GetIOBase().Off();
                        break;
                }

            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void ckb_Selected_CheckedChanged(object sender, EventArgs e)
        {
            if (lighting != null) 
            {
                lighting.IsSelected = (sender as CheckBox).Checked;
            }
        }

        private void Form_InspectKit_Lighting_Item_IO_Load(object sender, EventArgs e)
        {
            if(lighting != null) { this.ckb_Selected.Checked = lighting.IsSelected; }
        }
    }
}
