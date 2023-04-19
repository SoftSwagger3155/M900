using SolveWare_Service_Core.General;
using SolveWare_Service_Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolveWare_Service_Core.Base.Interface;
using MF900_SolveWare.Offset.Data;
using MF900_SolveWare.Offset.Job;
using Sunny.UI;
using MF900_SolveWare.Resource;
using MF900_SolveWare.Views.AxisMesForm;

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_Offset_2 : Form,IView
    {
        public Form_Offset_2()
        {
            InitializeComponent();
            Fillup_Combobox_OffsetJob();
            Fillup_Combobox_InspectKit();
        }
        public Job_Offset OffsetJob { get; protected set; }
        public Data_Offset OffsetData { get; protected set; }

        public void Setup<TObj>(TObj obj)
        {
            OffsetJob = obj as Job_Offset;
            OffsetData = OffsetJob.Data;

            
        }

        #region 本地方法
        private void Fillup_Combobox_OffsetJob()
        {
            var jobs = SolveWare.Core.MMgr.Get_Identical_ReosurcBase_Job(ConstantProperty.ResourceKey_Offset).ToList();
            this.cmb_Selector_OffsetJob.Items.Clear();
            jobs.ForEach(job => { this.cmb_Selector_OffsetJob.Items.Add(job.Name); });
        }
        private void Fillup_Combobox_InspectKit()
        {
            var jobs = SolveWare.Core.MMgr.Get_Identical_ReosurcBase_Job(ConstantProperty.ResourceKey_Inspect);
            if (jobs.Count == 0) return;

            this.cmb_Selector_InspectKit.Items.Clear();
            jobs.ForEach(job => { this.cmb_Selector_InspectKit.Items.Add(job.Name); });
        }
        private void DataBinding_CheckBox()
        {
            try
            {
                if (OffsetData == null) return;

                this.chk_Enable_Inspect.Checked = OffsetData.Enable_InspectKit;
                this.ckb_Move_To_Center.Checked = OffsetData.Move_To_Center;
                this.ckb_Reverse_X.Checked = OffsetData.IsReverseX;
                this.ckb_Reverse_Y.Checked = OffsetData.IsReverseY;
            }
            catch (Exception)
            {

            }
        }
        public void DataBinding_StartPos_Info()
        {
            if (this.OffsetData == null) return;
            this.Invoke(new Action(() =>
            {
                lbl_Top_Module_TestPosX_Info.Text = $"{ResourceKey.Motor_Top_X} : {OffsetData.Start_Top_PosX} mm";
                lbl_Top_Module_TestPosY_Info.Text = $"{ResourceKey.Motor_Top_Y} : {OffsetData.Start_Top_PosY} mm";
                lbl_Top_Module_TestPosZ_Info.Text = $"{ResourceKey.Motor_Top_Z} : {OffsetData.Start_Top_PosZ} mm";
                lbl_Top_Module_TestPosT_Info.Text = $"{ResourceKey.Motor_Top_T} : {OffsetData.Start_Top_PosT} mm";

                lbl_Btm_Module_TestPosX_Info.Text = $"{ResourceKey.Motor_Btm_X} : {OffsetData.Start_Btm_PosX} mm";
                lbl_Btm_Module_TestPosY_Info.Text = $"{ResourceKey.Motor_Btm_Y} : {OffsetData.Start_Btm_PosY} mm";
                lbl_Btm_Module_TestPosZ_Info.Text = $"{ResourceKey.Motor_Btm_Z} : {OffsetData.Start_Btm_PosZ} mm";
                lbl_Btm_Module_TestPosT_Info.Text = $"{ResourceKey.Motor_Btm_T} : {OffsetData.Start_Btm_PosT} mm";
            }));
        }
        public void DataBinding_FirstPos_Info()
        {
            if (this.OffsetData == null) return;
            if (string.IsNullOrEmpty(OffsetData.First_MotorX) || string.IsNullOrEmpty(OffsetData.First_MotorY)) return;

            this.Invoke(new Action(() =>
            {
                lbl_First_Save_PosX_Info.Text = $"{OffsetData.First_MotorX} : {OffsetData.FirstPosX} mm";
                lbl_First_Save_PosY_Info.Text = $"{OffsetData.First_MotorY} : {OffsetData.FirstPosY} mm";
            }));
        }
        public void DataBinding_Second_Pos()
        {
            if (this.OffsetData == null) return;
            if (string.IsNullOrEmpty(OffsetData.Anchor_MotorX) ||
                string.IsNullOrEmpty(OffsetData.Anchor_MotorY) ||
                string.IsNullOrEmpty(OffsetData.Anchor_MotorZ) ||
                string.IsNullOrEmpty(OffsetData.Anchor_MotorT)) return;

            this.Invoke(new Action(() =>
            {
                lbl_Second_Save_PosX_Info.Text = $"{OffsetData.Anchor_MotorX} : {OffsetData.SecondPosX} mm";
                lbl_Second_Save_PosY_Info.Text = $"{OffsetData.Anchor_MotorY} : {OffsetData.SecondPosY} mm";
            }));
        }
        public void DataBinding_Offset()
        {
            if (this.OffsetData == null) return;
            this.Invoke(new Action(() =>
            {
                lbl_OffsetX_Info.Text = $"OffsetX {OffsetData.OffsetX} mm";
                lbl_OffsetY_Info.Text = $"OffsetY {OffsetData.OffsetY} mm";
            }));
        }
        public void DataBinding_InspectKit()
        {
            if (this.OffsetData == null) return;
            this.Invoke(new Action(() =>
            {
                lbl_InspectKit.Text = $"视觉 : {OffsetData.InspectKitName}";
            }));
        }
        public void DataBinding_Inspect_Pos()
        {
            if (this.OffsetData == null) return;
            if (string.IsNullOrEmpty(OffsetData.Anchor_MotorX) ||
                string.IsNullOrEmpty(OffsetData.Anchor_MotorY) ||
                string.IsNullOrEmpty(OffsetData.Anchor_MotorZ) ||
                string.IsNullOrEmpty(OffsetData.Anchor_MotorT)) return;

            this.Invoke(new Action(() =>
            {
                lbl_Second_Target_MotorX_Info.Text = $"{OffsetData.Anchor_MotorX} : {OffsetData.Inspect_PosX} mm";
                lbl_Second_Target_MotorY_Info.Text = $"{OffsetData.Anchor_MotorY} : {OffsetData.Inspect_PosY} mm";
                lbl_Second_Target_MotorZ_Info.Text = $"{OffsetData.Anchor_MotorZ} : {OffsetData.Inspect_PosZ} mm";
                lbl_Second_Target_MotorT_Info.Text = $"{OffsetData.Anchor_MotorT} : {OffsetData.Inspect_PosT} mm";
            }));
        }
        #endregion


        #region 事件
        private void cmb_Selector_OffsetJob_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string offset = (sender as ComboBox).SelectedItem as string;
            var job = SolveWare.Core.MMgr.Get_PairJob(offset);
            if (job != null) { 
                
                this.Setup(job);
                DataBinding_Offset();
                DataBinding_CheckBox();
                DataBinding_Inspect_Pos();
                DataBinding_Second_Pos();
                DataBinding_FirstPos_Info();
                DataBinding_StartPos_Info();
            }
        }

        private void btn_Safe_Prevention_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if(OffsetData == null)
                    {
                        msg += "无 Offset Data 资料";
                        break;
                    }

                    IView view = new Form_Safe_Protection();
                    view.Setup(OffsetData.Data_Safe_Module);
                    (view as Form).Show();

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Save_Start_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Save_Start_Pos();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

                    errorCode = OffsetJob.Save_First_Pos();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

                    DataBinding_FirstPos_Info();
                    DataBinding_StartPos_Info();

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Go_Start_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Go_Start_Pos();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        #endregion

        private void btn_Motor_General_Controller_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    IView view = new Form_Axis_General_Controller();
                    (view as Form).Show();

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                     OffsetJob.Save(true);
                    

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Do_Job_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Do_Job();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Clear_Offset_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    OffsetData.OffsetX = 0;
                    OffsetData.OffsetY = 0;

                    DataBinding_Offset();

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Save_Second_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Save_Second_Pos();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

                    DataBinding_Second_Pos();

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Go_Second_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.GoSecondPos();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Confirm_InspectKit_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    string insp = cmb_Selector_InspectKit.SelectedItem as string;
                    OffsetData.InspectKitName = insp;

                    DataBinding_InspectKit();

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Do_Inspect_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Do_Inspect();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;


                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Save_Inspect_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Save_Inspect_Pos();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

                    DataBinding_Inspect_Pos();

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Go_Inspect_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Go_Inspect_Pos();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;


                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Calculate_Offset_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Calculate_Offset();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

                    DataBinding_Offset();

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void Form_Offset_2_Load(object sender, EventArgs e)
        {
           DataBinding_CheckBox();
        }

        private void chk_Enable_Inspect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (OffsetData == null) return;
                OffsetData.Enable_InspectKit = (sender as CheckBox).Checked;
            }
            catch (Exception)
            {

            }
        }

        private void ckb_Move_To_Center_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (OffsetData == null) return;
                OffsetData.Move_To_Center = (sender as CheckBox).Checked;
            }
            catch (Exception)
            {

            }
        }

        private void ckb_Reverse_X_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (OffsetData == null) return;
                OffsetData.IsReverseX = (sender as CheckBox).Checked;
            }
            catch (Exception)
            {

            }
        }

        private void ckb_Reverse_Y_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (OffsetData == null) return;
                OffsetData.IsReverseY = (sender as CheckBox).Checked;
            }
            catch (Exception)
            {

            }
        }

        private void btn_Offset_Go_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Go_Offset();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Offset_Return_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    int errorCode = OffsetJob.Return_Offset();
                    if (errorCode.NotPass(ref msg, OffsetJob.ErrorMsg)) break;

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
