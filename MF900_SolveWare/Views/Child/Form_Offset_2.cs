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
using Sunny.UI.Win32;
using SolveWare_Service_Utility.Extension;
using System.Threading;

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
            DataBinding_CheckBox();
            
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
            this.Refresh_UI_Item(new[] { lbl_Top_Module_TestPosX_Info, lbl_Top_Module_TestPosY_Info, lbl_Top_Module_TestPosZ_Info, lbl_Top_Module_TestPosT_Info,
                                                          lbl_Btm_Module_TestPosX_Info, lbl_Btm_Module_TestPosY_Info, lbl_Btm_Module_TestPosZ_Info, lbl_Btm_Module_TestPosT_Info}, () =>
            {
                if (this.OffsetData == null) return;
                lbl_Top_Module_TestPosX_Info.Text = $"{ResourceKey.Motor_Top_X} : {OffsetData.Start_Top_PosX} mm";
                lbl_Top_Module_TestPosY_Info.Text = $"{ResourceKey.Motor_Top_Y} : {OffsetData.Start_Top_PosY} mm";
                lbl_Top_Module_TestPosZ_Info.Text = $"{ResourceKey.Motor_Top_Z} : {OffsetData.Start_Top_PosZ} mm";
                lbl_Top_Module_TestPosT_Info.Text = $"{ResourceKey.Motor_Top_T} : {OffsetData.Start_Top_PosT} Deg";

                lbl_Btm_Module_TestPosX_Info.Text = $"{ResourceKey.Motor_Btm_X} : {OffsetData.Start_Btm_PosX} mm";
                lbl_Btm_Module_TestPosY_Info.Text = $"{ResourceKey.Motor_Btm_Y} : {OffsetData.Start_Btm_PosY} mm";
                lbl_Btm_Module_TestPosZ_Info.Text = $"{ResourceKey.Motor_Btm_Z} : {OffsetData.Start_Btm_PosZ} mm";
                lbl_Btm_Module_TestPosT_Info.Text = $"{ResourceKey.Motor_Btm_T} : {OffsetData.Start_Btm_PosT} Deg";
            });
        }
        public void DataBinding_FirstPos_Info()
        {
            this.Refresh_UI_Item(new[] { lbl_First_Save_PosX_Info, lbl_First_Save_PosY_Info }, () =>
            {
                if (this.OffsetData == null) return;
                if (string.IsNullOrEmpty(OffsetData.First_MotorX) || string.IsNullOrEmpty(OffsetData.First_MotorY)) return;

                lbl_First_Save_PosX_Info.Text = $"{OffsetData.First_MotorX} : {OffsetData.FirstPosX} mm";
                lbl_First_Save_PosY_Info.Text = $"{OffsetData.First_MotorY} : {OffsetData.FirstPosY} mm";
            });
        }
        public void DataBinding_Second_Pos()
        {
            this.Refresh_UI_Item(new[] { lbl_Second_Save_PosX_Info, lbl_Second_Save_PosY_Info }, () =>
            {
                if (this.OffsetData == null) return;
                if (string.IsNullOrEmpty(OffsetData.Anchor_MotorX) ||
                    string.IsNullOrEmpty(OffsetData.Anchor_MotorY) ||
                    string.IsNullOrEmpty(OffsetData.Anchor_MotorZ) ||
                    string.IsNullOrEmpty(OffsetData.Anchor_MotorT)) return;

                lbl_Second_Save_PosX_Info.Text = $"{OffsetData.Anchor_MotorX} : {OffsetData.SecondPosX} mm";
                lbl_Second_Save_PosY_Info.Text = $"{OffsetData.Anchor_MotorY} : {OffsetData.SecondPosY} mm";
            });
           
        }
        public void DataBinding_Offset()
        {
            this.Refresh_UI_Item(new[] { lbl_OffsetX_Info, lbl_OffsetY_Info }, () =>
            {
                if (this.OffsetData == null) return;
                lbl_OffsetX_Info.Text = $"OffsetX {OffsetData.OffsetX} mm";
                lbl_OffsetY_Info.Text = $"OffsetY {OffsetData.OffsetY} mm";
            });       
        }
        public void DataBinding_InspectKit()
        {
            this.Refresh_UI_Item(new[] { lbl_InspectKit }, () =>
            {
                if (this.OffsetData == null) return;
                lbl_InspectKit.Text = $"视觉 : {OffsetData.InspectKitName}";
            });

          
        }
        public void DataBinding_Inspect_Pos()
        {
            this.Refresh_UI_Item(new[] { lbl_Second_Target_MotorX_Info, lbl_Second_Target_MotorY_Info, lbl_Second_Target_MotorZ_Info, lbl_Second_Target_MotorT_Info }, () =>
            {
                if (this.OffsetData == null) return;
                if (string.IsNullOrEmpty(OffsetData.Anchor_MotorX) ||
                    string.IsNullOrEmpty(OffsetData.Anchor_MotorY) ||
                    string.IsNullOrEmpty(OffsetData.Anchor_MotorZ) ||
                    string.IsNullOrEmpty(OffsetData.Anchor_MotorT)) return;

                lbl_Second_Target_MotorX_Info.Text = $"{OffsetData.Anchor_MotorX} : {OffsetData.Inspect_PosX} mm";
                lbl_Second_Target_MotorY_Info.Text = $"{OffsetData.Anchor_MotorY} : {OffsetData.Inspect_PosY} mm";
                lbl_Second_Target_MotorZ_Info.Text = $"{OffsetData.Anchor_MotorZ} : {OffsetData.Inspect_PosZ} mm";
                lbl_Second_Target_MotorT_Info.Text = $"{OffsetData.Anchor_MotorT} : {OffsetData.Inspect_PosT} Deg";
            });
        }
        #endregion


        #region 事件
        private void cmb_Selector_OffsetJob_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string offset = (sender as ComboBox).SelectedItem as string;
            var job = SolveWare.Core.MMgr.Get_PairJob(offset);
            if (job != null) { 
                
                this.Setup(job);   
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
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        context.Set(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                        if (context.NotPass(true)) break;
                    }

                    context = OffsetJob.Save_Start_Pos();
                    if (context.NotPass()) break;

                    context = OffsetJob.Save_First_Pos();
                    if (context.NotPass()) break;

                } while (false);

            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
            }
        }

        private void btn_Go_Start_Pos_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Set(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                            if (context.NotPass(true)) break;
                        }

                        context = OffsetJob.Go_Start_Pos();
                        if (context.NotPass(true)) break;

                    } while (false);

                }
                catch (Exception ex)
                {
                    context.Set(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });
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
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Set(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                            if(context.NotPass()) break;
                        }

                        context = OffsetJob.Do_Job();
                        if (context.NotPass()) break;

                    } while (false);

                }
                catch (Exception ex)
                {
                    context.Set(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });    
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

                    Mission_Report context = OffsetJob.Save_Second_Pos();
                    if (context.NotPass()) break;

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
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Set(ErrorCodes.NoMotorObject, "请选择一个 Offset物件");
                            if(context.NotPass(true)) break;    
                            break;
                        }

                        context = OffsetJob.GoSecondPos();
                        if (context.NotPass(true)) break;

                    } while (false);

                }
                catch (Exception ex)
                {
                   context.Set(ErrorCodes.ActionFailed, ex.Message);
                    if (context.NotPass(true)) ;
                }
                return context;
            });
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
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Set(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                            context.NotPass(true);  
                            break;
                        }

                        context = OffsetJob.Do_Inspect();
                        if (context.NotPass()) break;


                    } while (false);

                }
                catch (Exception ex)
                {
                    context.Set(ErrorCodes.ActionFailed, ex.Message);
                    context.NotPass(true);
                }
                return context;
            });
        }

        private void btn_Save_Inspect_Pos_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Set(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                            context.NotPass(true);
                            break;
                        }

                        context = OffsetJob.Save_Inspect_Pos();
                        if (context.NotPass(true)) break;

                    } while (false);

                }
                catch (Exception ex)
                {
                    context.Set(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });
        }

        private void btn_Go_Inspect_Pos_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Set(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                            context.NotPass(true);
                            break;
                        }

                        context = OffsetJob.Go_Inspect_Pos();
                        if (context.NotPass(true)) break;


                    } while (false);

                }
                catch (Exception ex)
                {
                    context.Set(ErrorCodes.ActionFailed, ex.Message);
                    context.NotPass(true);
                }

                return context;
            });
        }

        private void btn_Calculate_Offset_Click(object sender, EventArgs e)
        {
            Mission_Report context = new Mission_Report();
            try
            {
                do
                {
                    if (OffsetData == null)
                    {
                        context.Set(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                        context.NotPass(true);  
                        break;
                    }

                    context = OffsetJob.Calculate_Offset();
                    if (context.NotPass(true)) break;

                } while (false);

            }
            catch (Exception ex)
            {
                context.Set(ErrorCodes.ActionFailed, ex.Message);
                context.NotPass(true);
            }
        }

        private void Form_Offset_2_Load(object sender, EventArgs e)
        {
           StartListening();
        }
        private void Form_Offset_2_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListening();
        }

        CancellationTokenSource cancelSource = null;

        private void StartListening()
        {
            if(cancelSource == null) cancelSource = new CancellationTokenSource();
            Task.Run(() =>
            {
                while (!cancelSource.IsCancellationRequested) 
                {
                    DataBinding_Offset();
                    DataBinding_Inspect_Pos();
                    DataBinding_Second_Pos();
                    DataBinding_FirstPos_Info();
                    DataBinding_StartPos_Info();
                    Thread.Sleep(10);
                }
            });
        }
        private void StopListening() 
        {
            if(cancelSource != null) cancelSource.Cancel(); 
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
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Set(ErrorCodes.NoRelevantObject, "请选择一个 Offset物件");
                            context.NotPass(true);
                            break;
                        }

                        context = OffsetJob.Go_Offset();
                        if (context.NotPass(true)) break;

                    } while (false);

                }
                catch (Exception ex)
                {
                    context.Set(ErrorCodes.ActionFailed, ex.Message);
                    context.NotPass(true);
                }
                return context;
            });
        }

        private void btn_Offset_Return_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                try
                {
                    do
                    {
                        if (OffsetData == null)
                        {
                            context.Window_Show_Not_Pass_Message(ErrorCodes.NoRelevantData, "请选择一个 Offset物件");
                            break;
                        }

                        context = OffsetJob.Return_Offset();
                        context.NotPass(true);

                    } while (false);

                }
                catch (Exception ex)
                {
                   context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed,ex.Message);
                }

                return context;
            });

        }

        
    }
}
