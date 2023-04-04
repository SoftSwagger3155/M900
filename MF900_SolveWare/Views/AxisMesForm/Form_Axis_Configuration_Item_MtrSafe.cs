﻿using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
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
            MakeDataGridView();
            Fillup_Combobox_Motor();
            Fillup_Combobox_Operand();
        }

        private string checkColumnName = "IsChecked";
        private string combo_Selector_Motor_Column_Name = "MotorName";
        private string combo_Selector_Operand_Column_Name = "Operand";
        private string text_Column_Name = "Pos";

        private void MakeDataGridView()
        {
            if(dgv_Content.Columns.Count > 0) dgv_Content.Columns.Clear();

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "选择";
            checkColumn.Name = checkColumnName;
            checkColumn.TrueValue = true;
            checkColumn.FalseValue = false;
            checkColumn.DataPropertyName = checkColumnName;
            checkColumn.Width = 150;
            checkColumn.Resizable = DataGridViewTriState.False;
            this.dgv_Content.Columns.Insert(0, checkColumn);

            DataGridViewTextBoxColumn combo_Selector_Motor_Column = new DataGridViewTextBoxColumn();
            combo_Selector_Motor_Column.HeaderText = "轴选择";
            combo_Selector_Motor_Column.Name = combo_Selector_Motor_Column_Name;
            combo_Selector_Motor_Column.DataPropertyName = combo_Selector_Motor_Column_Name;
            combo_Selector_Motor_Column.Width = 150;
            combo_Selector_Motor_Column.Resizable = DataGridViewTriState.False;
             this.dgv_Content.Columns.Insert(1, combo_Selector_Motor_Column);

            DataGridViewTextBoxColumn combo_Selector_Operand_Column = new DataGridViewTextBoxColumn();
            combo_Selector_Operand_Column.HeaderText = "运算符";
            combo_Selector_Operand_Column.Name = combo_Selector_Operand_Column_Name;
            combo_Selector_Operand_Column.DataPropertyName = combo_Selector_Operand_Column_Name;
            combo_Selector_Operand_Column.Width = 150;
            combo_Selector_Operand_Column.Resizable = DataGridViewTriState.False;
           this.dgv_Content.Columns.Insert(2, combo_Selector_Operand_Column);


            DataGridViewTextBoxColumn text_Column = new DataGridViewTextBoxColumn();
            text_Column.HeaderText = "位置";
            text_Column.Name = text_Column_Name;
            text_Column.DataPropertyName = text_Column_Name;
            text_Column.Width = 150;
            text_Column.Resizable = DataGridViewTriState.False;
            this.dgv_Content.Columns.Insert(3, text_Column);

            
        }

        private void dgv_Content_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //不是序号列和标题列时才执行
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                //checkbox 勾上
                if ((bool)dgv_Content.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                {
                    //选中改为不选中
                    this.dgv_Content.Rows[e.RowIndex].Cells[0].Value = false;
                }
                else
                {
                    //不选中改为选中
                    this.dgv_Content.Rows[e.RowIndex].Cells[0].Value = true;
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

        private void Iterate_DataGridView()
        {
            string msg = string.Empty;
            for (int i = 0; i < dgv_Content.Rows.Count; i++)
            {
                string chkName = dgv_Content.Rows[i].Cells[0].Value.ToString();
                string mtrName = dgv_Content.Rows[i].Cells[1].Value.ToString();
                string operandName = dgv_Content.Rows[i].Cells[2].Value.ToString();
                string textName = dgv_Content.Rows[i].Cells[3].Value.ToString();
                msg += $"选择 {chkName} 轴选择 {mtrName} 运算符 {operandName} 位置 {textName}\r\n";
            }

            SolveWare.Core.MMgr.Infohandler.LogMessage(msg, true);
        }

        public void Setup<TObj>(TObj obj)
        {
            
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            var deleteItems = this.list.FindAll(x => x.IsChecked == true);    
            deleteItems.ToList().ForEach(item => this.list.Remove(item));

            dgv_Content.DataSource = null;
            dgv_Content.DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };
            MakeDataGridView();

            dgv_Content.DataSource = list;
        }

        public List<DataSourceTest> list = new List<DataSourceTest>();
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_Selector_Motor.SelectedItem as string) ||
                string.IsNullOrEmpty(cmb_Selector_Operand.SelectedItem as string) ||
                string.IsNullOrEmpty(txb_Pos.Text)) return;

            DataSourceTest data = new DataSourceTest()
            {
                IsChecked = false,
                MotorName = cmb_Selector_Motor.SelectedItem as string,
                Operand = (string)cmb_Selector_Operand.SelectedItem,
                Pos = double.Parse(txb_Pos.Text)
            };

            list.Add(data);
            dgv_Content.Columns.Clear();
            MakeDataGridView();
            dgv_Content.DataSource = list;
            dgv_Content.DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };
        }
    }

    public class DataSourceTest
    {
        public bool IsChecked { get; set; }
        public string MotorName { get; set; }
        public string Operand { get; set; }
        public double Pos { get; set; }
    }
}