using MF900_SolveWare.Business;
using MF900_SolveWare.Offset.Data;
using MF900_SolveWare.Offset.Job;
using MF900_SolveWare.Resource;
using MF900_SolveWare.Safe;
using MF900_SolveWare.Views.AxisMesForm;
using MF900_SolveWare.WorldCenter.Job;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Common;
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
    public partial class Form_Offset : Form, IView
    {
        public Form_Offset()
        {
            InitializeComponent();
            Fillup_Combobox_Motor();
            Fillup_Combobox_Selector();
            Fillup_Combobox_InspectKit();
            Fillup_Combobox_Start_Module();
        }

       
        public Job_Offset OffsetJob { get; protected set; }
        public Data_Offset OffsetData { get; protected set; }

        #region 本地方法
        public void Setup<TObj>(TObj obj)
        {
            OffsetJob = obj as Job_Offset;
            OffsetData = OffsetJob.Data;
        }
        private void Fillup_Combobox_Selector()
        {
            var jobs= SolveWare.Core.MMgr.Get_Identical_ReosurcBase_Job(ConstantProperty.ResourceKey_Offset).ToList();
            this.cmb_Selector_Offset.Items.Clear();
            jobs.ForEach(job => { this.cmb_Selector_Offset.Items.Add(job.Name); });
        }
        private void Fillup_Combobox_Start_Module()
        {
            this.cmb_Selector_Test_Based_Module.Items.Clear();
            this.cmb_Selector_Test_Based_Module.Items.AddRange(new string[] { Data_Offset.TopModule, Data_Offset.BtmModule });
        }
        private void Fillup_Combobox_Motor()
        {
            this.cmb_Selector_First_MotorX.Items.Clear();
            this.cmb_Selector_First_MotorY.Items.Clear();
            this.cmb_Selector_Second_MotorX.Items.Clear();
            this.cmb_Selector_Second_MotorY.Items.Clear();
            this.cmb_Selector_Second_MotorZ.Items.Clear();
            this.cmb_Selector_Second_MotorT.Items.Clear();

            var mtrs = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor).Get_All_Item_Name();
            this.cmb_Selector_First_MotorX.Items.AddRange(mtrs.ToArray());
            this.cmb_Selector_First_MotorY.Items.AddRange(mtrs.ToArray());
            this.cmb_Selector_Second_MotorX.Items.AddRange(mtrs.ToArray());
            this.cmb_Selector_Second_MotorY.Items.AddRange(mtrs.ToArray());
            this.cmb_Selector_Second_MotorZ.Items.AddRange(mtrs.ToArray());
            this.cmb_Selector_Second_MotorT.Items.AddRange(mtrs.ToArray());
        }
        public void Fillup_Combobox_InspectKit()
        {
            var jobs = SolveWare.Core.MMgr.Get_Identical_ReosurcBase_Job(ConstantProperty.ResourceKey_Inspect);
            if (jobs.Count == 0) return;

            this.cmb_Selector_InspectKit.Items.Clear();
            jobs.ForEach(job => { this.cmb_Selector_InspectKit.Items.Add(job.Name); });
        }
        public void DataBinding_StartPos_Info()
        {
            if(this.OffsetData == null) return;
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
        public void DataBinding_Inspect_Pos()
        {
            if (this.OffsetData == null) return;
            if (string.IsNullOrEmpty(OffsetData.Inspect_MotorX) ||
                string.IsNullOrEmpty(OffsetData.Inspect_MotorY) || 
                string.IsNullOrEmpty(OffsetData.Inspect_MotorZ) || 
                string.IsNullOrEmpty(OffsetData.Inspect_MotorT)) return;

            this.Invoke(new Action(() =>
            {
                lbl_Second_Target_MotorX_Info.Text = $"{OffsetData.Inspect_MotorX} : {OffsetData.Inspect_PosX} mm";
                lbl_Second_Target_MotorY_Info.Text = $"{OffsetData.Inspect_MotorY} : {OffsetData.Inspect_PosY} mm";
                lbl_Second_Target_MotorZ_Info.Text = $"{OffsetData.Inspect_MotorZ} : {OffsetData.Inspect_PosZ} mm";
                lbl_Second_Target_MotorT_Info.Text = $"{OffsetData.Inspect_MotorT} : {OffsetData.Inspect_PosT} mm";
            }));
        }
        public void DataBinding_Offset()
        {
            if(this.OffsetData == null) return;
            this.Invoke(new Action(() =>
            {
                lbl_OffsetX_Info.Text = $"{OffsetData.OffsetX} mm";
                lbl_OffsetY_Info.Text = $"{OffsetData.OffsetY} mm";
            }));
        }
        #endregion

        #region 事件
        private void cmb_Selector_Offset_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string offset = (sender as ComboBox).SelectedItem as string;
            var job = SolveWare.Core.MMgr.Get_PairJob(offset);
            if (job != null) { this.Setup(job); }
        }
        private void cmb_Selector_Test_Based_Module_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.OffsetJob == null) return;
            OffsetJob.Data.Start_Based_Module = cmb_Selector_Test_Based_Module.SelectedItem as string;

        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    DateTime dt = DateTime.Now;
                    if (OffsetData == null)
                    {
                        msg += "请选择一个 Offset物件";
                        break;
                    }

                    OffsetData.SaveDate = $"{dt.ToLongDateString()} {dt.ToLongTimeString()}";

                    OffsetJob.Save();
                    msg += "储存 成功";
                    tssl_Save_Date.Text = $"储存时间: {OffsetData.SaveDate}";

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }
        private void btn_First_Save_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
               
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }
        private void btn_First_Go_Pos_Click(object sender, EventArgs e)
        {

        }
        private void btn_Top_Module_Safe_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                IView view = new Form_Safe_Protection();
                view.Setup(OffsetData.Data_Safe_Module);
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
               // view.Setup(OffsetData.Data_Safe_Btm_Module);
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
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                        break;
                    }

                    string module = cmb_Selector_Test_Based_Module.SelectedItem as string;
                    if (string.IsNullOrEmpty(module)) {
                        context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantData, "请选择一个模具");
                        break;
                    }

                    context = OffsetJob.Save_Start_Pos();
                    if (context.NotPass(true)) break;

                    DataBinding_StartPos_Info();

                } while (false);

            }
            catch (Exception ex)
            {
                context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
            }
        }
        private void btn_Go_Test_Pos_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickActionTask(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                            break;
                        }
                        context = OffsetJob.Go_Start_Pos();
                        if (context.NotPass(true)) break;

                    } while (false);

                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }
                return context;
            });
        }
        private void btn_Save_First_Pos_Click(object sender, EventArgs e)
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
                    if(string.IsNullOrEmpty(cmb_Selector_First_MotorX.SelectedItem as string) ||
                       string.IsNullOrEmpty(cmb_Selector_First_MotorY.SelectedItem as string))
                    {
                        msg += "请选择 第一位置的 X Y 马达";
                        break;
                    }

                    string mtrX = cmb_Selector_First_MotorX.SelectedItem as string;
                    string mtrY = cmb_Selector_First_MotorY.SelectedItem as string;

                    OffsetData.First_MotorX = mtrX;
                    OffsetData.First_MotorY = mtrY;
                    OffsetData.FirstPosX = Math.Round(mtrX.GetUnitPos(), 3);
                    OffsetData.FirstPosY = Math.Round(mtrY.GetUnitPos(), 3);

                    DataBinding_FirstPos_Info();

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg); 
        }
        private void btn_Save_Target_Pos_Click(object sender, EventArgs e)
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
                    if (string.IsNullOrEmpty(cmb_Selector_Second_MotorX.SelectedItem as string) ||
                        string.IsNullOrEmpty(cmb_Selector_Second_MotorY.SelectedItem as string) ||
                        string.IsNullOrEmpty(cmb_Selector_Second_MotorZ.SelectedItem as string) ||
                        string.IsNullOrEmpty(cmb_Selector_Second_MotorT.SelectedItem as string))
                    {
                        msg += "请选择 第二位置的 X Y Z T马达";
                        break;
                    }

                    string mtrX = cmb_Selector_Second_MotorX.SelectedItem as string;
                    string mtrY = cmb_Selector_Second_MotorY.SelectedItem as string;
                    string mtrZ = cmb_Selector_Second_MotorZ.SelectedItem as string;
                    string mtrT = cmb_Selector_Second_MotorT.SelectedItem as string;

                    OffsetData.Inspect_MotorX = mtrX;
                    OffsetData.Inspect_MotorY = mtrY;
                    OffsetData.Inspect_MotorZ = mtrZ;
                    OffsetData.Inspect_MotorT = mtrT;
                    OffsetData.Inspect_PosX = Math.Round(mtrX.GetUnitPos(), 3);
                    OffsetData.Inspect_PosY = Math.Round(mtrY.GetUnitPos(), 3);
                    OffsetData.Inspect_PosZ = Math.Round(mtrX.GetUnitPos(), 3);
                    OffsetData.Inspect_PosT = Math.Round(mtrY.GetUnitPos(), 3);

                    DataBinding_Inspect_Pos();

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }
        private void btn_Go_Target_Pos_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.DoButtonClickActionTask(() =>
            {
               Mission_Report context = new Mission_Report();

                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                            break;
                        }
                        if (string.IsNullOrEmpty(OffsetData.Inspect_MotorX) ||
                            string.IsNullOrEmpty(OffsetData.Inspect_MotorY) ||
                            string.IsNullOrEmpty(OffsetData.Inspect_MotorZ) ||
                            string.IsNullOrEmpty(OffsetData.Inspect_MotorT))
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantData, "无 Offset Data 视觉 X Y Z T马达 资讯");
                            break;
                        }

                        context = OffsetJob.Do_Safe_Prevention();
                        if (context.NotPass(true)) break;

                        context = MotionHelper.Move_Multiple_Motors(
                            new Info_Motion { Motor_Name = OffsetData.Inspect_MotorX, Pos = OffsetData.Inspect_PosX },
                            new Info_Motion { Motor_Name = OffsetData.Inspect_MotorY, Pos = OffsetData.Inspect_PosY },
                            new Info_Motion { Motor_Name = OffsetData.Inspect_MotorT, Pos = OffsetData.Inspect_PosT }
                            );
                        if (context.NotPass(true)) break;

                        context = MotionHelper.Move_Motor(
                            new Info_Motion { Motor_Name = OffsetData.Inspect_MotorZ, Pos = OffsetData.Inspect_PosZ });
                        if (context.NotPass(true)) break;

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }
                return context;
            });
        }
        private void btn_Execute_Both_Module_Click(object sender, EventArgs e)
        {

        }
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

                    this.OffsetData.OffsetX = 0;
                    this.OffsetData.OffsetY = 0;

                    DataBinding_Offset();

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }

            SolveWare.Core.ShowMsg(msg);
        }


        #endregion
    }
}
