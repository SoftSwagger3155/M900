using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Utility.Extension;
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
    public partial class Form_Axis_Configuration_Item_MtrSpeed : Form, IView
    {
        public Form_Axis_Configuration_Item_MtrSpeed()
        {
            InitializeComponent();
        }

        ConfigData_Motor configData;
        AxisBase axis = null;
        public void Setup<TObj>(TObj obj)
        {
            this.configData = obj as ConfigData_Motor;
            axis = configData.MtrTable.Name.GetAxisBase();

            if (axis == null) return;

            Fillup_Combobox_SpeedSetting();
            this.cmb_Selector_SpeedSetting.SelectionChangeCommitted -= Cmb_Selector_SpeedSetting_SelectionChangeCommitted;
            this.cmb_Selector_SpeedSetting.SelectionChangeCommitted += Cmb_Selector_SpeedSetting_SelectionChangeCommitted;
            axis.PropertyChanged -= Axis_PropertyChanged;
            axis.PropertyChanged += Axis_PropertyChanged;
        }

        private void Cmb_Selector_SpeedSetting_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectedTarget = (sender as ComboBox).SelectedItem as string;
            if (selectedTarget == null) return;

            List<string> temp = new List<string>();
            cmb_Selector_Copy.Items.Clear();
            axis.ConfigData.MtrSpeed.SpeedSettings.ForEach(x => { if (x.Name != selectedTarget) cmb_Selector_Copy.Items.Add(x.Name); });

            var targetObj = axis.ConfigData.MtrSpeed.SpeedSettings.FirstOrDefault(x=> x.Name == selectedTarget);
            this.pGrid_Speed.SelectedObject = targetObj;

        }

        private void Axis_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(axis.CurrentPhysicalPos))
            {
                this.Invoke(new Action(() =>
                {
                    this.lbl_Info_Motor.Text = $"{this.configData.MtrTable.Name} {axis.CurrentPhysicalPos} mm";
                }));
            }
        }

        private void Fillup_Combobox_SpeedSetting()
        {
            if(this.axis == null) return;
          
            this.cmb_Selector_SpeedSetting.Items.Clear();
            axis.ConfigData.MtrSpeed.SpeedSettings.ForEach(s => { this.cmb_Selector_SpeedSetting.Items.Add(s.Name); });
        }

        
    }
}
