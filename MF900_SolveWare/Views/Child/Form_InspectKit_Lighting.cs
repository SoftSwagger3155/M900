using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Definition;
using SolveWare_Service_Vision.Data;
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

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_InspectKit_Lighting : Form, IView
    {
        const string Selector_Kind_IO = "输出 IO";
        const string Selector_Kind_Source = "光源";

        public Form_InspectKit_Lighting()
        {
            InitializeComponent();
            Fillup_Combobox_Kind();
            cmb_Selector_Kind.SelectionChangeCommitted -= Cmb_Selector_Kind_SelectionChangeCommitted;
            cmb_Selector_Kind.SelectionChangeCommitted += Cmb_Selector_Kind_SelectionChangeCommitted;

            IView form_IO = new Form_InspectKit_Lighting_Item_IO();
            IView form_Source = new Form_InspectKit_Lighting_Item_LightSource();

            StyleForm(ref form_IO);
            StyleForm(ref form_Source);

            gpb_Content.Controls.Clear();
            gpb_Content.Controls.Add((Form)form_IO);
            gpb_Content.Controls.Add((Form)form_Source);
        }

        Data_InspectionKit dataKit;
        public void Setup<TObj>(TObj obj)
        {
            dataKit = obj as Data_InspectionKit;
        }

        private void Cmb_Selector_Kind_SelectionChangeCommitted(object sender, EventArgs e)
        {
           string tag = (sender as ComboBox).SelectedItem as string;

            switch (tag)
            {
                case Selector_Kind_IO:
                    Fillup_Combobox_Item_IO();
                    break; 
                case Selector_Kind_Source:
                    Fillup_Combobox_Item_LightSource();
                    break;
            }
        }
        private void Fillup_Combobox_Item_IO()
        {
            cmb_Selector_Item.Text = string.Empty;
            cmb_Selector_Item.Items.Clear();
            var provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.IO);
          
            if (provider == null) return;
            var oPs = provider.Get_All_Items().ToList().FindAll(x => (x as IOBase).IOType == IO_Type.Output);
            oPs.ForEach(x => { cmb_Selector_Item.Items.Add((x as IElement).Name);});
        }
        private void Fillup_Combobox_Item_LightSource()
        {
            cmb_Selector_Item.Text = string.Empty;
            cmb_Selector_Item.Items.Clear();
            var provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Lighting);
            if (provider == null) return;

            var lightNames = provider.Get_All_Item_Name();
            lightNames.ForEach(x => cmb_Selector_Item.Items.Add(x));
        }
        private void Fillup_Combobox_Kind()
        {
            cmb_Selector_Kind.Items.Clear();
            cmb_Selector_Kind.Items.Add(Selector_Kind_IO);
            cmb_Selector_Kind.Items.Add(Selector_Kind_Source);
        }

        int pitch = 45;
        private void StyleForm(ref IView form)
        {
            (form as Form).TopLevel = false;
            (form as Form).Visible = true;
            (form as Form).Width = 600;
            (form as Form).Height = 40;
            //(form as Form).Location = new Point(5, (20 + ((count - 1) * pitch)));
            (form as Form).Dock = DockStyle.Top;
            (form as Form).FormBorderStyle = FormBorderStyle.None;
        }
    }
}
