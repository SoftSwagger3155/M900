using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Definition;
using SolveWare_Service_Tool.Motor.Data;
using SolveWare_Service_Tool.Motor.Definition;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MF900_SolveWare.Views.AxisMesForm
{
    public partial class Form_Axis_Configuration_Item_MtrSafe : Form, IView
    {
        public Form_Axis_Configuration_Item_MtrSafe()
        {
            InitializeComponent();
            Fillup_Combobox_Motor();
            Fillup_Combobox_IOType();
            Fillup_Combobox_Operand();
        }

        ConfigData_Motor configData;
        MtrSafe mtrSafe;
        public void Setup<TObj>(TObj obj)
        {
            this.configData = obj as ConfigData_Motor;
            this.mtrSafe = configData.MtrSafe;

            MakeDataGridView();
            MakeIODataGridView();
        }

        private const string Pos_Property_Name_IsSelected = "IsSelected";
        private const string Pos_Property_Name_MotorName = "MotorName";
        private const string Pos_Property_Name_Operand = "Operand";
        private const string Pos_Property_Name_Pos = "Pos";
        
        private const string IO_Property_Name_IOName = "IOName";
        private const string IO_Property_Name_TriggerMode = "TriggerMode";
        private const string IO_Property_Name_IsSelected = "IsSelected";
        private const string IO_Property_Name_IOType = "IOType";


        private void MakeDataGridView()
        {
            if(dgv_Pos_Content.Columns.Count > 0) dgv_Pos_Content.Columns.Clear();

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "选择";
            checkColumn.Name = Pos_Property_Name_IsSelected;
            checkColumn.TrueValue = true;
            checkColumn.FalseValue = false;
            checkColumn.DataPropertyName = Pos_Property_Name_IsSelected;
            checkColumn.Width = 150;
            checkColumn.Resizable = DataGridViewTriState.False;
            this.dgv_Pos_Content.Columns.Insert(0, checkColumn);

            DataGridViewTextBoxColumn combo_Selector_Motor_Column = new DataGridViewTextBoxColumn();
            combo_Selector_Motor_Column.HeaderText = "轴选择";
            combo_Selector_Motor_Column.Name = Pos_Property_Name_MotorName;
            combo_Selector_Motor_Column.DataPropertyName = Pos_Property_Name_MotorName;
            combo_Selector_Motor_Column.Width = 150;
            combo_Selector_Motor_Column.Resizable = DataGridViewTriState.False;
            combo_Selector_Motor_Column.ReadOnly = true;
            this.dgv_Pos_Content.Columns.Insert(1, combo_Selector_Motor_Column);

            DataGridViewTextBoxColumn combo_Selector_Operand_Column = new DataGridViewTextBoxColumn();
            combo_Selector_Operand_Column.HeaderText = "运算符";
            combo_Selector_Operand_Column.Name = Pos_Property_Name_Operand;
            combo_Selector_Operand_Column.DataPropertyName = Pos_Property_Name_Operand;
            combo_Selector_Operand_Column.Width = 150;
            combo_Selector_Operand_Column.Resizable = DataGridViewTriState.False;
            combo_Selector_Operand_Column.ReadOnly = true;
           this.dgv_Pos_Content.Columns.Insert(2, combo_Selector_Operand_Column);


            DataGridViewTextBoxColumn text_Column = new DataGridViewTextBoxColumn();
            text_Column.HeaderText = "位置";
            text_Column.Name = Pos_Property_Name_Pos;
            text_Column.DataPropertyName = Pos_Property_Name_Pos;
            text_Column.Width = 150;
            text_Column.Resizable = DataGridViewTriState.False;
            this.dgv_Pos_Content.Columns.Insert(3, text_Column);

            dgv_Pos_Content.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };
            dgv_Pos_Content.DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };
            dgv_Pos_Content.DataSource = mtrSafe.Data_Pos_Safetys;

        }
        private void MakeIODataGridView()
        {
            if (dgv_IO_Content.Columns.Count > 0) dgv_IO_Content.Columns.Clear();

            DataGridViewCheckBoxColumn SelectedColumn = new DataGridViewCheckBoxColumn();
            SelectedColumn.HeaderText = "选择";
            SelectedColumn.Name = IO_Property_Name_IsSelected;
            SelectedColumn.TrueValue = true;
            SelectedColumn.FalseValue = false;
            SelectedColumn.DataPropertyName = IO_Property_Name_IsSelected;
            SelectedColumn.Width = 150;
            SelectedColumn.Resizable = DataGridViewTriState.False;
            this.dgv_IO_Content.Columns.Insert(0, SelectedColumn);

            DataGridViewTextBoxColumn combo_Selector_IOName_Column = new DataGridViewTextBoxColumn();
            combo_Selector_IOName_Column.HeaderText = "输入输出";
            combo_Selector_IOName_Column.Name = IO_Property_Name_IOName;
            combo_Selector_IOName_Column.DataPropertyName = IO_Property_Name_IOName;
            combo_Selector_IOName_Column.Width = 150;
            combo_Selector_IOName_Column.Resizable = DataGridViewTriState.False;
            combo_Selector_IOName_Column.ReadOnly = true;    
            this.dgv_IO_Content.Columns.Insert(1, combo_Selector_IOName_Column);

            DataGridViewTextBoxColumn combo_Selector_IOType_Column = new DataGridViewTextBoxColumn();
           combo_Selector_IOType_Column.HeaderText = "型态";
           combo_Selector_IOType_Column.Name = IO_Property_Name_IOType;
           combo_Selector_IOType_Column.DataPropertyName = IO_Property_Name_IOType;
           combo_Selector_IOType_Column.Width = 150;
           combo_Selector_IOType_Column.Resizable = DataGridViewTriState.False;
            combo_Selector_IOType_Column.ReadOnly = true;
            this.dgv_IO_Content.Columns.Insert(2, combo_Selector_IOType_Column);

            DataGridViewComboBoxColumn combo_Selector_Operand_Column = new DataGridViewComboBoxColumn();
            combo_Selector_Operand_Column.HeaderText = "TriggerMode";
            combo_Selector_Operand_Column.Name = IO_Property_Name_TriggerMode;
            combo_Selector_Operand_Column.DataPropertyName = IO_Property_Name_TriggerMode;
            combo_Selector_Operand_Column.Width = 150;
            combo_Selector_Operand_Column.Resizable = DataGridViewTriState.False;
            combo_Selector_Operand_Column.DataSource = new List<string> { ConstantProperty.ON, ConstantProperty.OFF };
            this.dgv_IO_Content.Columns.Insert(3, combo_Selector_Operand_Column);


            dgv_IO_Content.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };
            dgv_IO_Content.DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };
            dgv_IO_Content.DataSource = mtrSafe.Data_IO_Safetys;
        }

        private void dgv_Content_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //不是序号列和标题列时才执行
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                //checkbox 勾上
                if ((bool)dgv_Pos_Content.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                {
                    //选中改为不选中
                    this.dgv_Pos_Content.Rows[e.RowIndex].Cells[0].Value = false;
                }
                else
                {
                    //不选中改为选中
                    this.dgv_Pos_Content.Rows[e.RowIndex].Cells[0].Value = true;
                }
            }
        }

        private void Fillup_Combobox_Motor()
        {
            this.cmb_Selector_Motor.Items.Clear();
            SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor).Get_All_Item_Name().ToList().ForEach(item => { this.cmb_Selector_Motor.Items.Add(item); });
        }
        private void Fillup_Combobox_Operand()
        {
            this.cmb_Selector_Operand.Items.Clear();
            Enum.GetNames(typeof(Safety_Operand)).ToList().ForEach(x=> this.cmb_Selector_Operand.Items.Add(x));
        }
        private void Fillup_Combobox_IOType()
        {
            this.cmb_Selector_IOType.Items.Clear();
            this.cmb_Selector_IOType.Items.Add(ConstantProperty.InPut);
            this.cmb_Selector_IOType.Items.Add(ConstantProperty.OutPut);
        }
        private void Fillup_Combobox_Inputs()
        {
            this.cmb_Selector_IO.Items.Clear();
            var provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.IO);
            var ips = provider.Get_All_Items().ToList().ConvertAll(x => x as IOBase).FindAll(x => x.IOType == IO_Type.Input);
            ips.ForEach(tool => this.cmb_Selector_IO.Items.Add(tool.Name));
        }
        private void Fillup_Combobox_Outputs()
        {
            this.cmb_Selector_IO.Items.Clear();
            var provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.IO);
            var ops = provider.Get_All_Items().ToList().ConvertAll(x => x as IOBase).FindAll(x => x.IOType == IO_Type.Output);
            ops.ForEach(tool => this.cmb_Selector_IO.Items.Add(tool.Name));
        }

        private void Iterate_DataGridView()
        {
            string msg = string.Empty;
            for (int i = 0; i < dgv_Pos_Content.Rows.Count; i++)
            {
                string chkName = dgv_Pos_Content.Rows[i].Cells[0].Value.ToString();
                string mtrName = dgv_Pos_Content.Rows[i].Cells[1].Value.ToString();
                string operandName = dgv_Pos_Content.Rows[i].Cells[2].Value.ToString();
                string textName = dgv_Pos_Content.Rows[i].Cells[3].Value.ToString();
                msg += $"选择 {chkName} 轴选择 {mtrName} 运算符 {operandName} 位置 {textName}\r\n";
            }

            SolveWare.Core.MMgr.Infohandler.LogMessage(msg, true);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            var deleteItems = this.mtrSafe.Data_Pos_Safetys.FindAll(x => x.IsSelected == true);
            if (deleteItems.Count == 0) return;
            deleteItems.ToList().ForEach(item => this.mtrSafe.Data_Pos_Safetys.Remove(item));

            dgv_Pos_Content.DataSource = null;
            dgv_Pos_Content.DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };
            MakeDataGridView();

            //dgv_Pos_Content.DataSource = this.mtrSafe.Data_Pos_Safetys;
        }

        //public List<DataSourceTest> list = new List<DataSourceTest>();
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_Selector_Motor.SelectedItem as string) ||
                string.IsNullOrEmpty(cmb_Selector_Operand.SelectedItem as string) ||
                string.IsNullOrEmpty(txb_Pos.Text)) return;

            Data_Pos_Safety data = new Data_Pos_Safety()
            {
                IsSelected = false,
                MotorName = cmb_Selector_Motor.SelectedItem as string,
                Operand = cmb_Selector_Operand.SelectedItem as string,
                Pos = double.Parse(txb_Pos.Text)
            };

            mtrSafe.Data_Pos_Safetys.Add(data);
            dgv_Pos_Content.Columns.Clear();
            MakeDataGridView();
        }

        //public List<DataSourceIOTest> iOTests = new List<DataSourceIOTest>();
        private void btn_IO_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_Selector_IOType.SelectedItem as string) ||
                string.IsNullOrEmpty(cmb_Selector_IO.SelectedItem as string)) return;

            Data_IO_Safety data = new Data_IO_Safety()
            {
                IsSelected = false,
                IOName = cmb_Selector_IO.SelectedItem as string,
                TriggerMode = ConstantProperty.ON,
                IOType = cmb_Selector_IOType.SelectedItem as string
            };

            mtrSafe.Data_IO_Safetys.Add(data);
            dgv_IO_Content.Columns.Clear();
          
            MakeIODataGridView();
        }

        private void cmb_Selector_IOType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string iOType = (sender as ComboBox).SelectedItem as string;
            switch (iOType)
            {
                case ConstantProperty.OutPut:
                    Fillup_Combobox_Outputs();
                    break;
                case ConstantProperty.InPut:
                    Fillup_Combobox_Inputs();
                    break;
            }
        }

        private void btn_Delete_IO_Click(object sender, EventArgs e)
        {
            var deleteItems = this.mtrSafe.Data_IO_Safetys.FindAll(x => x.IsSelected == true);
            if (deleteItems.Count == 0) return;

            deleteItems.ToList().ForEach(item => this.mtrSafe.Data_IO_Safetys.Remove(item));

            dgv_IO_Content.DataSource = null;
            dgv_IO_Content.DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };
            MakeIODataGridView();

            dgv_IO_Content.DataSource = mtrSafe.Data_IO_Safetys;
        }
    }

    public class DataSourceTest
    {
        public bool IsChecked { get; set; }
        public string MotorName { get; set; }
        public string Operand { get; set; }
        public double Pos { get; set; }
    }
    public class DataSourceIOTest
    {
        public bool IsChecked { get; set;}
        public string IOName { get; set; }
        public string TriggerMode { get; set; }
    }
}
