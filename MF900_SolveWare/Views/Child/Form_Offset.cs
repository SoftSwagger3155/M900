using MF900_SolveWare.Safe;
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

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_Offset : Form, IView
    {
        public Form_Offset()
        {
            InitializeComponent();
        }

        public void Setup<TObj>(TObj obj)
        {
           
        }

        private void btn_Top_Safe_Click(object sender, EventArgs e)
        {
            IView view = new Form_Safe_Protection();
            view.Setup(new Data_Safe());
            (view as Form).Show();
        }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }
        private void btn_Btm_Module_Safe_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                IView view = new Form_Safe_Protection();
                view.Setup(OffsetData.Data_Safe_Btm_Module);
                (view as Form).Show();
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }
        private void btn_Save_Test_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    string module = cmb_Selector_Test_Based_Module.SelectedItem as string;
                    if (string.IsNullOrEmpty(module)) {
                        msg += "请选择一个模具";
                        break;
                    }

                    Job_GlobalWorldCenter worldCenter = (Job_GlobalWorldCenter)SolveWare.Core.MMgr.Get_PairJob(ResourceKey.GlobalWorldCenter);


                    double offsetX = 0, offsetY = 0;

                    switch (module)
                    {
                        case TopModule:
                            offsetX = ResourceKey.Motor_Top_X.GetUnitPos() - worldCenter.Data.Top_WorldCenter_PosX;
                            offsetY = ResourceKey.Motor_Top_Y.GetUnitPos() - worldCenter.Data.Top_WorldCenter_PosY;

                            OffsetData.Test_Top_PosX = Math.Round(ResourceKey.Motor_Top_X.GetUnitPos(), 3);
                            OffsetData.Test_Top_PosY = Math.Round(ResourceKey.Motor_Top_Y.GetUnitPos(), 3);
                            OffsetData.Test_Top_PosZ = Math.Round(ResourceKey.Motor_Top_Z.GetUnitPos(), 3);
                            OffsetData.Test_Top_PosT = Math.Round(ResourceKey.Motor_Top_T.GetUnitPos(), 3);

                            OffsetData.Test_Btm_PosX = Math.Round(worldCenter.Data.Btm_WorldCenter_PosX + offsetX, 3);
                            OffsetData.Test_Btm_PosY = Math.Round(worldCenter.Data.Btm_WorldCenter_PosY + offsetY, 3);
                            OffsetData.Test_Btm_PosZ = Math.Round(ResourceKey.Motor_Btm_Z.GetUnitPos(), 3);
                            OffsetData.Test_Btm_PosT = Math.Round(ResourceKey.Motor_Btm_T.GetUnitPos(), 3);

                            break;

                        case BtmModule:
                            offsetX = ResourceKey.Motor_Btm_X.GetUnitPos() - worldCenter.Data.Btm_WorldCenter_PosX;
                            offsetY = ResourceKey.Motor_Btm_Y.GetUnitPos() - worldCenter.Data.Btm_WorldCenter_PosY;

                            OffsetData.Test_Top_PosX = Math.Round(worldCenter.Data.Top_WorldCenter_PosX + offsetX, 3);
                            OffsetData.Test_Top_PosY = Math.Round(worldCenter.Data.Top_WorldCenter_PosY + offsetY, 3);
                            OffsetData.Test_Top_PosZ = Math.Round(ResourceKey.Motor_Top_Z.GetUnitPos(), 3);
                            OffsetData.Test_Top_PosT = Math.Round(ResourceKey.Motor_Top_T.GetUnitPos(), 3);

                            OffsetData.Test_Btm_PosX = Math.Round(ResourceKey.Motor_Btm_X.GetUnitPos());
                            OffsetData.Test_Btm_PosY = Math.Round(ResourceKey.Motor_Btm_Y.GetUnitPos());
                            OffsetData.Test_Btm_PosZ = Math.Round(ResourceKey.Motor_Btm_Z.GetUnitPos(), 3);
                            OffsetData.Test_Btm_PosT = Math.Round(ResourceKey.Motor_Btm_T.GetUnitPos(), 3);
                            break;
                    }

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Go_Test_Pos_Click(object sender, EventArgs e)
        {

        }

        private void btn_Save_First_Pos_Click(object sender, EventArgs e)
        {

        }

        private void btn_Save_Target_Pos_Click(object sender, EventArgs e)
        {

        }

        private void btn_Go_Target_Pos_Click(object sender, EventArgs e)
        {

        }
        private void btn_Execute_Both_Module_Click(object sender, EventArgs e)
        {

        }
        private void btn_Motor_General_Controller_Click(object sender, EventArgs e)
        {

        }

        private void btn_Clear_Offset_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;

            try
            {
                do
                {
                   
                    this.OffsetData.OffsetX = 0;
                    this.OffsetData.OffsetY = 0;

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }

            SolveWare.Core.ShowMsg(msg);
        }

        #endregion

     
        private void cmb_Selector_First_MotorX_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmb_Selector_First_MotorY_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmb_Selector_Second_MotorX_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmb_Selector_Second_MotorY_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmb_Selector_Second_MotorZ_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmb_Selector_Second_MotorT_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
    }
}
