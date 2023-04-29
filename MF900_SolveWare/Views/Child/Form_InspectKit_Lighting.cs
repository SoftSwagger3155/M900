using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Definition;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.JobSheet;
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
        }

        Data_Inspection dataKit;
        public void Setup<TObj>(TObj obj)
        {
            dataKit = obj as Data_Inspection;
            MakeUI();
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
            (form as Form).Height = 45;
            //(form as Form).Location = new Point(5, (20 + ((count - 1) * pitch)));
            (form as Form).Dock = DockStyle.Top;
            (form as Form).FormBorderStyle = FormBorderStyle.None;
        }
        private void MakeUI()
        {
            if (dataKit == null) return;
            if (dataKit.JobSheet_Lighting_Datas.Count == 0)
            {
                gpb_Content.Controls.Clear();
                return;
            }

            List<IView> views = new List<IView>();

            foreach (var lighting in dataKit.JobSheet_Lighting_Datas)
            {
                IView view = null;
                if (lighting.Is_IO_Controlled) view = new Form_InspectKit_Lighting_Item_IO();
                else view = new Form_InspectKit_Lighting_Item_LightSource();
                view.Setup(lighting);
                StyleForm(ref view);
                views.Add(view);
            }

            views.Reverse();
            gpb_Content.Controls.Clear();
            views.ForEach(view => { gpb_Content.Controls.Add(view as Form); }); 


        }


        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string tag = cmb_Selector_Kind.SelectedItem as string;
                JobSheet_Lighting lighting = new JobSheet_Lighting();
                switch (tag)
                {
                    case Selector_Kind_IO:
                        lighting.Is_IO_Controlled = true;
                        lighting.IO_Name = cmb_Selector_Item.SelectedItem as string;
                        break;

                    case Selector_Kind_Source:
                        lighting.Is_IO_Controlled = false;
                        lighting.Lighting_Name = cmb_Selector_Item.SelectedItem as string;
                        break;
                }

                dataKit.JobSheet_Lighting_Datas.Add(lighting);
                
                MakeUI();
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }


        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataKit == null) return;
                if (this.dataKit.JobSheet_Lighting_Datas.Count == 0) return;

                List<JobSheet_Lighting> tempDatas = new List<JobSheet_Lighting> ();
                this.dataKit.JobSheet_Lighting_Datas.ForEach(data => { if (!data.IsSelected) tempDatas.Add(data); });

                this.dataKit.JobSheet_Lighting_Datas.Clear ();
                this.dataKit.JobSheet_Lighting_Datas.AddRange(tempDatas.ToArray ());

                MakeUI();
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }
    }
}
