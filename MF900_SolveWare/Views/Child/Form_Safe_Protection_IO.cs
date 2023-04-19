using MF900_SolveWare.Safe;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Definition;
using SolveWare_Service_Tool.Motor.Data;
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

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_Safe_Protection_IO : Form, IView
    {
        public Form_Safe_Protection_IO()
        {
            InitializeComponent();
        }

        DetailData_Safe_IO data;
        public void Setup<TObj>(TObj obj)
        {
            data = obj as DetailData_Safe_IO;
        }

        private void Form_Safe_Protection_IO_Load(object sender, EventArgs e)
        {
            Fillup_Combobox_IOtype();
            Fillup_Combobox_IO();
            Fillup_Combobox_TriggerMode();
            DataBinding();
        }

        private void Fillup_Combobox_IOtype()
        {
            this.cmb_Selector_IOType.Items.Clear();
            this.cmb_Selector_IOType.Items.AddRange(new string[] { ConstantProperty.InPut, ConstantProperty.OutPut });
        }
        private void Fillup_Combobox_TriggerMode()
        {
            this.cmb_Selector_TriggerMode.Items.Clear();
            this.cmb_Selector_TriggerMode.Items.AddRange(new string[] { ConstantProperty.ON, ConstantProperty.OFF });
        }
        private void Fillup_Combobox_IO()
        {
            this.cmb_Selector_IO.Items.Clear();
            IO_Type targetType = cmb_Selector_IOType.SelectedItem as string == ConstantProperty.InPut ? IO_Type.Input : IO_Type.Output;
            var iOs = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.IO).Get_All_Items().ToList().FindAll(x => (x as IOBase).IOType == targetType);
            List<string> names = new List<string>();
            iOs.ForEach(x => names.Add((x as IOBase).Name));
            this.cmb_Selector_IO.Items.AddRange(names.ToArray());
        }
        private void DataBinding()
        {
            ckb_Selected.DataBindings.Add(nameof(ckb_Selected.Checked), data, nameof(data.IsSelected));
            txb_Priority.DataBindings.Add(nameof(txb_Priority.Text), data, nameof(data.Priority));
            txb_DelayTime.DataBindings.Add(nameof(txb_DelayTime.Text), data, nameof(data.DelayTime));
            cmb_Selector_IO.DataBindings.Add(nameof(cmb_Selector_IO.SelectedItem), data, nameof(data.IOName));
            cmb_Selector_IOType.DataBindings.Add(nameof(cmb_Selector_IOType.SelectedItem), data, nameof(data.IOType));
            cmb_Selector_TriggerMode.DataBindings.Add(nameof(cmb_Selector_TriggerMode.SelectedItem), data, nameof(data.TriggerMode));
        }

        private void cmb_Selector_IOType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string type = (sender as ComboBox).SelectedItem as string;
            if (string.IsNullOrEmpty(type)) return;
            data.IOType = type;
            Fillup_Combobox_IO();
        }

        private void cmb_Selector_TriggerMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string triggerMode = (sender as ComboBox).SelectedItem as string;
            if (string.IsNullOrEmpty(triggerMode)) return;
            data.TriggerMode = triggerMode;
        }

        private void txb_Priority_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_Priority.Text)) { return; }
            data.Priority = int.Parse(txb_Priority.Text);
        }

        private void txb_DelayTime_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_DelayTime.Text)) { return; }
            data.DelayTime = int.Parse(txb_DelayTime.Text);
        }

        private void cmb_Selector_IO_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string iO = (sender as ComboBox).SelectedItem as string;
            if (string.IsNullOrEmpty(iO)) return;
            data.IOName = iO;
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            string msg = string.Empty; 
            try
            {
                do
                {

                    if (string.IsNullOrEmpty(data.IOName) ||
                       string.IsNullOrEmpty(data.TriggerMode) ||
                       string.IsNullOrEmpty(data.IOType)) { msg += ErrorCodes.GetErrorDescription(ErrorCodes.NoRelevantData);  break; }

                    int errorCode = data.IOName.IOFunction(data.TriggerMode);
                    if(errorCode.NotPass(ref msg)) { break; }

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }
    }
}
