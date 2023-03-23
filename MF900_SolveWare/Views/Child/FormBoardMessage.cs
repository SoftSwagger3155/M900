using SolveWare_Service_Core.Base.Interface;
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
    public partial class FormBoardMessage : Form, IView
    {
        public static FormBoardMessage form;
        public FormBoardMessage()
        {
            InitializeComponent();
            form = this;
            //GenDgvTable.GenDataGridTable(dgv_BoardMessage, ProgramParamMange.ProductDataPara.ProductXY.Y * ProgramParamMange.ProductDataPara.RegionCount.Y,
            //    ProgramParamMange.ProductDataPara.ProductXY.X * ProgramParamMange.ProductDataPara.RegionCount.X);
            //GenDgvTable.SetDataGridViewMes(dgv_BoardMessage,ProgramParamMange.BoardMessagePara.OneBoardSelect);
            ReadBoardMessage();
        }
       
        //左后
        private void uiRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //GenDgvTable.SetDataGridViewMes(dgv_BoardMessage, OneBoard.左后);
        }
        //右后
        private void uiRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //GenDgvTable.SetDataGridViewMes(dgv_BoardMessage, OneBoard.右后);
        }
        //左前
        private void uiRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //GenDgvTable.SetDataGridViewMes(dgv_BoardMessage, OneBoard.左前);
        }
        //右前
        private void uiRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //GenDgvTable.SetDataGridViewMes(dgv_BoardMessage, OneBoard.右前);
        }

        private void dgv_BoardMessage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            //GenDgvTable.SetDataGriaTableColor(dgv_BoardMessage, Color.White);
            uiTextBox3.Text = dgv_BoardMessage[col, row].Value.ToString();
            //跟随显示
            //for (int i = 0; i < ProgramParamMange.UpJipDataPara.JipStepXY.Y; i++)
            //{
            //    for (int j = 0; j < ProgramParamMange.UpJipDataPara.JipStepXY.X; j++)
            //    {
            //        dgv_BoardMessage[col + i, row + j].Style.BackColor = Color.FromArgb(80, 120, 255);
            //    }
            //}
        }

        public static void SaveBoardMessage()
        {
            //OneBoard theOneBoard =OneBoard.左后;
            //if (form.uiRadioButton1.Checked)
            //    theOneBoard = OneBoard.左后;
            //else if(form.uiRadioButton2.Checked)
            //    theOneBoard = OneBoard.右后;
            //else if (form.uiRadioButton3.Checked)
            //    theOneBoard = OneBoard.左前;
            //else if (form.uiRadioButton4.Checked)
            //    theOneBoard = OneBoard.右前;
            //ProgramParamMange.BoardMessagePara = new BoardMessageModel()
            //{
            //    OneBoardSelect = theOneBoard,
            //    Priority = form.uiRadioButton5.Checked ? "X" : "Y"
            //};
            //SerializeHelper.SerializeXml<BoardMessageModel>(ProgramParamMange.BoardMessagePara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\BoardMessageModel.xml");
        }
        private void ReadBoardMessage()
        {
            //switch (ProgramParamMange.BoardMessagePara.OneBoardSelect)
            //{
            //    case OneBoard.左后:
            //        uiRadioButton1.Checked = true;
            //        break;
            //    case OneBoard.右后:
            //        uiRadioButton2.Checked = true;
            //        break;
            //    case OneBoard.左前:
            //        uiRadioButton3.Checked = true;
            //        break;
            //    case OneBoard.右前:
            //        uiRadioButton4.Checked = true;
            //        break;
            //    default:
            //        break;
            //}
        }

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }
    }
}
