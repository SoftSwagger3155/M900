using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Utility.Extension;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.AxisMesForm
{
    public partial class Form_Axis_Configuration : Form, IView
    {
        private AxisBase axis;

        public Form_Axis_Configuration()
        {
            InitializeComponent();
            Fillup_Combobox_Motor();
        }

        public void Setup<TObj>(TObj obj)
        {
            
        }

        private void Fillup_Combobox_Motor()
        {
            var motors = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor).Get_All_Item_Name().ToList();
            if (motors.Count == 0) return;

            this.cmb_Selector_Motor.Items.Clear();
            motors.ForEach(x => { this.cmb_Selector_Motor.Items.Add(x); });

            this.cmb_Selector_Motor.SelectionChangeCommitted -= Cmb_Selector_Motor_SelectionChangeCommitted;
            this.cmb_Selector_Motor.SelectionChangeCommitted += Cmb_Selector_Motor_SelectionChangeCommitted;

            if(cmb_Selector_Motor.Items.Count > 0) {
                cmb_Selector_Motor.SelectedItem = cmb_Selector_Motor.Items[0];
                string mtr = cmb_Selector_Motor.SelectedItem as string;
                Fillup_TabControl(mtr);
            }
        }

        private void Cmb_Selector_Motor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string mtr = (string)(sender as ComboBox).SelectedItem;
            Fillup_TabControl(mtr);
        }
        private void Fillup_TabControl(string mtr)
        {
            string errMsg = string.Empty;
            try
            {
                axis = mtr.GetAxisBase();

                if (axis == null) return;
                MakePropertyGrid(axis);

            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            if (string.IsNullOrEmpty(errMsg) == false) { SolveWare.Core.MMgr.Infohandler.LogMessage(errMsg, true); }
        }


        IView form_Speed = null;
        IView form_Safety = null;
        private void MakePropertyGrid(AxisBase axis)
        {
            PropertyGrid pGrid = null;
            string errMsg = string.Empty;
            try
            {
                pGrid = new PropertyGrid();
                StylePropertyGrid(ref pGrid);
                axis.MtrTable.Name = axis.Name;
                pGrid.SelectedObject = axis.MtrTable;
                tab_MtrTable.Controls.Clear();
                tab_MtrTable.Controls.Add(pGrid);

                pGrid = new PropertyGrid();
                StylePropertyGrid(ref pGrid);
                pGrid.SelectedObject = axis.MtrConfig;
                tab_MtrConfig.Controls.Clear();
                tab_MtrConfig.Controls.Add(pGrid);

                form_Speed = new Form_Axis_Configuration_Item_MtrSpeed();
                form_Speed.Setup(this.axis.ConfigData);
                (form_Speed as Form).TopLevel = false;
                (form_Speed as Form).Visible = true;
                (form_Speed as Form).Dock = DockStyle.Fill;
                tab_MtrSpeed.Controls.Clear();
                tab_MtrSpeed.Controls.Add(form_Speed as Form);

                form_Safety = new Form_Axis_Configuration_Item_MtrSafe();
                (form_Safety as Form).TopLevel = false;
                (form_Safety as Form).Visible = true;
                (form_Safety as Form).Dock = DockStyle.Fill;
                tab_MtrSafety.Controls.Clear();
                tab_MtrSafety.Controls.Add(form_Safety as Form);

            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            if(string.IsNullOrEmpty(errMsg) == false) SolveWare.Core.MMgr.Infohandler.LogMessage($"{errMsg}", true);    
        }

        private void StylePropertyGrid(ref PropertyGrid pGrid)
        {
            pGrid.Height = this.tab_MtrTable.Height - 20;
            pGrid.Width = this.tab_MtrTable.Width - 20;
            pGrid.Location = new Point(5, 20);
            pGrid.Dock = DockStyle.Fill;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            try
            {
                do
                {
                    if (this.axis == null)
                    {
                        errMsg += "请选择一个马达轴";
                        break;
                    }
                    var provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor);
                    provider.SaveSingleData(axis.ConfigData);

                } while (false);
            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }

            if (string.IsNullOrEmpty(errMsg) == false) SolveWare.Core.MMgr.Infohandler.LogMessage($"{errMsg}", true);
        }

        private void Form_Axis_Configuration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (form_Speed != null) (form_Speed as Form).Close();
        }
    }
}
