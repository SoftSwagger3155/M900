﻿using MF900_SolveWare.Resource;
using SolveWare_Service_Core;
using SolveWare_Service_Tool.Motor.Base.Abstract;
using SolveWare_Service_Tool.Motor.Business;
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

namespace MF900_SolveWare
{
    public partial class AxisDebugForm : UIForm
    {
        Dictionary<string, AxisBase> keyAxis;

        public AxisDebugForm()
        {
            InitializeComponent();
            //List<string> names = SolveWare.Core.MMgr.Get_Single_Tool_Resource(SolveWare_Service_Core.Definition.Tool_Resource_Kind.Motor).Get_All_Item_Name().ToList();         
            keyAxis = new Dictionary<string, AxisBase>()
            {
                {ResourceKey.Motor_Top_X, ResourceKey.Motor_Top_X.GetAxisBase() },
                {ResourceKey.Motor_Top_Y, ResourceKey.Motor_Top_Y.GetAxisBase() },
                {ResourceKey.Motor_Top_Z, ResourceKey.Motor_Top_Z.GetAxisBase() },
                {ResourceKey.Motor_Top_T, ResourceKey.Motor_Top_T.GetAxisBase() },
                {ResourceKey.Motor_Btm_X, ResourceKey.Motor_Btm_X.GetAxisBase() },
                {ResourceKey.Motor_Btm_Y, ResourceKey.Motor_Btm_Y.GetAxisBase() },
                {ResourceKey.Motor_Btm_Z, ResourceKey.Motor_Btm_Z.GetAxisBase() },
                {ResourceKey.Motor_Btm_T, ResourceKey.Motor_Btm_T.GetAxisBase() },
                {ResourceKey.Motor_Table, ResourceKey.Motor_Table.GetAxisBase() },
            };

            InitalAxisMes();
            InitalAxisVel();
            InitalAxisJop();
            ResourceKey.Op_TowerLight_Green.GetIOBase();

        }
        /// <summary>
        /// 双缓冲
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #region InitalAxis
        private void InitalAxisMes()
        {
            var axisArry = keyAxis.Keys.ToArray();
            for (int i = 0; i < 4; i++)
            {
                uiDataGridView4.Rows.Add(new object[] {axisArry[i], keyAxis[axisArry[i]].MtrTable.AxisNo,keyAxis[axisArry[i]].MtrTable.Param_Home_IO,
                    keyAxis[axisArry[i]].MtrTable.Param_Fwd_Limit,keyAxis[axisArry[i]].MtrTable.Param_Rev_Limit});
            }
            for (int i = 4; i < 8; i++)
            {
                uiDataGridView2.Rows.Add(new object[] {axisArry[i], keyAxis[axisArry[i]].MtrTable.AxisNo,keyAxis[axisArry[i]].MtrTable.Param_Home_IO,
                    keyAxis[axisArry[i]].MtrTable.Param_Fwd_Limit,keyAxis[axisArry[i]].MtrTable.Param_Rev_Limit});
            }
            uiDataGridView8.Rows.Add(new object[] {axisArry[8], keyAxis[axisArry[8]].MtrTable.AxisNo,keyAxis[axisArry[8]].MtrTable.Param_Home_IO,
                    keyAxis[axisArry[8]].MtrTable.Param_Fwd_Limit,keyAxis[axisArry[8]].MtrTable.Param_Rev_Limit});
            uiDataGridView8.Rows[0].Height = 30;
        }
        private void InitalAxisVel()
        {
            var axisArry = keyAxis.Keys.ToArray();
            for (int i = 0; i < 4; i++)
            {
                uiDataGridView1.Rows.Add(new object[] {axisArry[i], keyAxis[axisArry[i]].MtrSpeed.Jog_Max_Velocity,
                keyAxis[axisArry[i]].MtrSpeed.Jog_Min_Velocity,keyAxis[axisArry[i]].MtrSpeed.Jog_Acceleration,keyAxis[axisArry[i]].MtrSpeed.Jog_Deceleration});
            }
            for (int i = 4; i < 8; i++)
            {
                uiDataGridView5.Rows.Add(new object[] {axisArry[i], keyAxis[axisArry[i]].MtrSpeed.Jog_Max_Velocity,
                keyAxis[axisArry[i]].MtrSpeed.Jog_Min_Velocity,keyAxis[axisArry[i]].MtrSpeed.Jog_Acceleration,keyAxis[axisArry[i]].MtrSpeed.Jog_Deceleration});
            }
            uiDataGridView6.Rows.Add(new object[] {axisArry[8], keyAxis[axisArry[8]].MtrSpeed.Jog_Max_Velocity,
                keyAxis[axisArry[8]].MtrSpeed.Jog_Min_Velocity,keyAxis[axisArry[8]].MtrSpeed.Jog_Acceleration,keyAxis[axisArry[8]].MtrSpeed.Jog_Deceleration});
            
        }
        private void InitalAxisJop()
        {
            var axisArry = keyAxis.Keys.ToArray();
            for (int i = 0; i < 4; i++)
            {
                uiDataGridView7.Rows.Add(new object[] { axisArry[i], 0, "移动", "10", "左", "右" });
                uiDataGridView7.Rows[i].Height = 35;
            }
            for (int i = 4; i < 8; i++)
            {
                uiDataGridView3.Rows.Add(new object[] { axisArry[i], 0, "移动", "10", "左", "右" });
                uiDataGridView3.Rows[i - 4].Height = 35;
            }
        }
        #endregion

        #region TopAxis_Home
        //X_Home
        private void uiButton1_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Top_X.GetAxisBase().HomeMove();
        }
        //Y_Home
        private void uiButton2_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Top_Y.GetAxisBase().HomeMove();
        }
        //Z_Home
        private void uiButton3_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Top_Z.GetAxisBase().HomeMove();
        }
        //T_Home
        private void uiButton4_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Top_T.GetAxisBase().HomeMove();
        }

        #endregion

        #region BtmAxis_Home
        //X_Home
        private void uiButton6_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Btm_X.GetAxisBase().HomeMove();
        }
        //Y_Home
        private void uiButton8_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Btm_Y.GetAxisBase().HomeMove();
        }
        //Z_Home
        private void uiButton7_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Btm_Z.GetAxisBase().HomeMove();
        }
        //T_Home
        private void uiButton5_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Btm_T.GetAxisBase().HomeMove();
        }

        #endregion

        #region TopAxis and BtmAxis Jop
        //TopAxis_Jop
        private void uiDataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //需加正则判断输入的值是否为double

            TopAndBtmAxisMove(uiDataGridView7, e.RowIndex, e.ColumnIndex);
        }

        //BtmAxis_Jop
        private void uiDataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //需加正则判断输入的值是否为double

            TopAndBtmAxisMove(uiDataGridView3, e.RowIndex, e.ColumnIndex);
        }
        public void TopAndBtmAxisMove(DataGridView dataGridView,int row,int col)
        {
            switch (col)
            {
                case 2:
                    keyAxis[dataGridView[0, row].Value.ToString()].MoveTo(double.Parse(dataGridView[1, row].Value.ToString()));
                    break;
                case 4:
                    keyAxis[dataGridView[0, row].Value.ToString()].Jog(true);
                    break;
                case 5:
                    keyAxis[dataGridView[0, row].Value.ToString()].Jog(false);
                    break;
            }
        }
        #endregion

        #region TableOpation
        //Home
        private void uiButton11_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Table.GetAxisBase().HomeMove();
        }
        //Jop+
        private void uiButton12_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Table.GetAxisBase().Jog(true);
        }
        //Jop-
        private void uiButton10_Click(object sender, EventArgs e)
        {
            ResourceKey.Motor_Table.GetAxisBase().Jog(false);
        }
        //Move
        private void uiButton9_Click(object sender, EventArgs e)
        {
            //需加正则判断
            ResourceKey.Motor_Table.GetAxisBase().MoveTo(double.Parse(uiTextBox1.Text));
        }

        #endregion
    }
}
