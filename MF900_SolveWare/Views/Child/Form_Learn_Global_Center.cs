using HalconDotNet;
using log4net.Core;
using MF900_SolveWare.Resource;
using MF900_SolveWare.Views.AxisMesForm;
using MF900_SolveWare.WorldCenter.Data;
using MF900_SolveWare.WorldCenter.Job;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Extension;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_Learn_Global_Center : Form, IView
    {
        public Form_Learn_Global_Center()
        {
            InitializeComponent();
            btn_Save_WorldCenter_Pos.Visible = true;
        }
        Job_GlobalWorldCenter job_GlobalWorld;
        Data_GlobalWorldCenter data_GlobalCenter;
        public void Setup<TObj>(TObj obj)
        {
            job_GlobalWorld = obj as Job_GlobalWorldCenter;
            data_GlobalCenter = job_GlobalWorld.Data;

            ckb_Top_Module_Move_To_Center_After_Top_Inspection.Checked = data_GlobalCenter.Top_Module_Move_To_Center;
            ckb_Btm_Module_Move_To_Center_After_Top_Inspection.Checked = data_GlobalCenter.Btm_Module_Move_To_Center;
            Fillup_Combobox_Inspection();     
        }

     

        private void Fillup_Combobox_Inspection()
        {
            var inspects = SolveWare.Core.MMgr.Get_Identical_ReosurcBase_Job(ConstantProperty.ResourceKey_Inspect).ToList();
            this.cmb_Selector_Btm_Module_InspectKit.Items.Clear();
            this.cmb_Selector_Top_Module_InspectKit.Items.Clear();

            if (inspects.Count == 0) return;
            inspects.ForEach(inspect => { this.cmb_Selector_Btm_Module_InspectKit.Items.Add(inspect.Name);  this.cmb_Selector_Top_Module_InspectKit.Items.Add(inspect.Name); });
            
        }


        private void btn_Motor_General_Controller_Click(object sender, EventArgs e)
        {
            IView view = new Form_Axis_General_Controller();
            (view as Form).Show();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                DateTime dt = DateTime.Now;
                data_GlobalCenter.SaveDate = $"{dt.ToLongDateString()} {dt.ToLongTimeString()}";
               
                job_GlobalWorld.Save();
                msg += "储存 成功";
                tssl_Save_Date.Text = $"储存时间: {data_GlobalCenter.SaveDate}";             
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
           SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Execute_Both_Module_Click(object sender, EventArgs e)
        {
           
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            { 
                Mission_Report context = new Mission_Report();
            Stopwatch sw = Stopwatch.StartNew();
            try
                {
                    do
                    {
                        if (SolveWare.Core.Is_Machine_Already_Homing() == false)
                        {
                            return context;
                        }
                        ReportStatus(Status_Stage.运行中);

                        sw.Restart();
                        context = job_GlobalWorld.Do_Job();
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }
                finally
                {
                    tssl_TimeSpent.Text = $"耗时: {sw.Elapsed.TotalSeconds} 秒";
                    Status_Stage stage = context.ErrorCode != ErrorCodes.NoError ? Status_Stage.失败 : Status_Stage.成功;
                    ReportStatus(stage);
                }
                return context;
            });
        }

        private void btn_Execute_Top_Moudle_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    do
                    {
                        if (SolveWare.Core.Is_Machine_Already_Homing() == false) return context;
                        ReportStatus(Status_Stage.运行中);

                        sw.Restart();
                        context = job_GlobalWorld.Go_Top_Module_Pos();
                        if (context.NotPass(true)) break;

                        context = job_GlobalWorld.Do_Top_Module_Inspect();
                        if (context.NotPass(true)) break;

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }

                finally
                {
                    tssl_TimeSpent.Text = $"耗时: {sw.Elapsed.TotalSeconds} 秒";
                    Status_Stage stage = context.ErrorCode != ErrorCodes.NoError ? Status_Stage.失败 : Status_Stage.成功;
                    ReportStatus(stage);
                }
                return context;
            });
        }

        private void btn_Execute_Btm_Moudle_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    do
                    {
                        if (SolveWare.Core.Is_Machine_Already_Homing() == false) return context;
                        ReportStatus(Status_Stage.运行中);

                        sw.Restart();
                        context = job_GlobalWorld.Go_Top_Module_Pos();
                        if (context.NotPass(true)) break;

                        context = job_GlobalWorld.Do_Top_Module_Inspect();
                        if (context.NotPass(true)) break;

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }
                finally
                {
                    tssl_TimeSpent.Text = $"耗时: {sw.Elapsed.TotalSeconds} 秒";
                    Status_Stage stage = context.ErrorCode != ErrorCodes.NoError ? Status_Stage.失败 : Status_Stage.成功;
                    ReportStatus(stage);
                }
                return context;
            });
        }

        private void btn_Top_Module_Update_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (this.data_GlobalCenter == null)
                    {
                        msg += "无 Data 物件";
                        break;
                    }

                    if (SolveWare.Core.Is_Machine_Already_Homing() == false) return;

                    data_GlobalCenter.Top_Module_PosX = Math.Round(ResourceKey.Motor_Top_X.GetUnitPos(), 3);
                    data_GlobalCenter.Top_Module_PosY = Math.Round(ResourceKey.Motor_Top_Y.GetUnitPos(), 3);
                    data_GlobalCenter.Top_Module_PosZ = Math.Round(ResourceKey.Motor_Top_Z.GetUnitPos(), 3);
                    data_GlobalCenter.Top_Module_PosT = Math.Round(ResourceKey.Motor_Top_T.GetUnitPos(), 3);

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            bool showMsg = !string.IsNullOrEmpty(msg);
            SolveWare.Core.MMgr.Infohandler.LogMessage(msg, showMsg);
        }

        private void btn_Btm_Module_Update_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (this.data_GlobalCenter == null)
                    {
                        msg += "无 Data 物件";
                        break;
                    }

                    if (SolveWare.Core.Is_Machine_Already_Homing() == false) return;

                    data_GlobalCenter.Btm_Module_PosX = Math.Round(ResourceKey.Motor_Btm_X.GetUnitPos(), 3);
                    data_GlobalCenter.Btm_Module_PosY = Math.Round(ResourceKey.Motor_Btm_Y.GetUnitPos(), 3);
                    data_GlobalCenter.Btm_Module_PosZ = Math.Round(ResourceKey.Motor_Btm_Z.GetUnitPos(), 3);
                    data_GlobalCenter.Btm_Module_PosT = Math.Round(ResourceKey.Motor_Btm_T.GetUnitPos(), 3);

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }

            bool showMsg = !string.IsNullOrEmpty(msg);
            SolveWare.Core.MMgr.Infohandler.LogMessage(msg, showMsg);
        }

        private void btn_Top_Module_Go_Pos_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    do
                    {                    
                        if (SolveWare.Core.Is_Machine_Already_Homing() == false) return context;
                        ReportStatus(Status_Stage.运行中);
                        sw.Restart();
                        context = job_GlobalWorld.Go_Top_Module_Pos();
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }

                return context; ;
            });
        }

        private void btn_Btm_Module_Go_Pos_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    do
                    {
                        if (SolveWare.Core.Is_Machine_Already_Homing() == false) return context;
                        ReportStatus(Status_Stage.运行中);

                        sw.Restart();
                        context = job_GlobalWorld.Go_Btm_Module_Pos();
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });
        }

        private void btn_Top_Module_Update_InspectKit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cmb_Selector_Top_Module_InspectKit.SelectedItem as string))
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage("请选择一个视觉物件", true);
                return;
            }

            this.data_GlobalCenter.Top_Module_InspectKit_Name = cmb_Selector_Top_Module_InspectKit.SelectedItem as string;
        }

        private void btn_Btm_Module_Update_InspectKit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmb_Selector_Btm_Module_InspectKit.SelectedItem as string))
            {
                SolveWare.Core.MMgr.Infohandler.LogMessage("请选择一个视觉物件", true);
                return;
            }

            this.data_GlobalCenter.Btm_Module_InspectKit_Name = cmb_Selector_Btm_Module_InspectKit.SelectedItem as string;
        }

        private void btn_Top_Module_Execute_InspectKit_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    do
                    {
                        ReportStatus(Status_Stage.运行中);

                        sw.Restart();
                        context = job_GlobalWorld.Do_Top_Module_Inspect();
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });
        }

        private void btn_Btm_Module_Execute_InspectKit_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    do
                    {
                        ReportStatus(Status_Stage.运行中);

                        sw.Restart();
                        context = job_GlobalWorld.Do_Btm_Module_Inspect();
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }

                return context;
            });
        }

        private void btn_Top_Update_WorldCenter_Click(object sender, EventArgs e)
        {
            if (SolveWare.Core.Is_Machine_Already_Homing() == false) return;

            data_GlobalCenter.Top_WorldCenter_PosX = Math.Round(ResourceKey.Motor_Top_X.GetUnitPos(), 3);
            data_GlobalCenter.Top_WorldCenter_PosY = Math.Round(ResourceKey.Motor_Top_Y.GetUnitPos(), 3);
            data_GlobalCenter.Top_WorldCenter_PosZ = Math.Round(ResourceKey.Motor_Top_Z.GetUnitPos(), 3);
            data_GlobalCenter.Top_WorldCenter_PosT = Math.Round(ResourceKey.Motor_Top_T.GetUnitPos(), 3);
        }

        private void btn_Btm_Update_WorldCenter_Click(object sender, EventArgs e)
        {
            if (SolveWare.Core.Is_Machine_Already_Homing() == false) return;

            data_GlobalCenter.Btm_WorldCenter_PosX = Math.Round(ResourceKey.Motor_Btm_X.GetUnitPos(), 3);
            data_GlobalCenter.Btm_WorldCenter_PosY = Math.Round(ResourceKey.Motor_Btm_Y.GetUnitPos(), 3);
            data_GlobalCenter.Btm_WorldCenter_PosZ = Math.Round(ResourceKey.Motor_Btm_Z.GetUnitPos(), 3);
            data_GlobalCenter.Btm_WorldCenter_PosT = Math.Round(ResourceKey.Motor_Btm_T.GetUnitPos(), 3);

        }
        private void ReportStatus(Status_Stage stage)
        {
            this.Invoke(new Action(() =>
            {
                switch (stage)
                {
                    case Status_Stage.空闲:
                        tssl_Top_Module_Status.Text = $"状态 : {stage}";
                        tssl_Top_Module_Status.BackColor = Color.LightGray;
                        break;
                    case Status_Stage.运行中:
                        tssl_Top_Module_Status.Text = $"状态 : {stage}";
                        tssl_Top_Module_Status.BackColor = Color.Orange;
                        break;
                    case Status_Stage.成功:
                        tssl_Top_Module_Status.Text = $"状态 : {stage}";
                        tssl_Top_Module_Status.BackColor = Color.Green;
                        break;
                    case Status_Stage.失败:
                        tssl_Top_Module_Status.Text = $"状态 : {stage}";
                        tssl_Top_Module_Status.BackColor = Color.IndianRed;
                        break;
                }
            }));
        }

        private void btn_Go_Top_WorldCenter_Pos_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    do
                    {
                        if (SolveWare.Core.Is_Machine_Already_Homing() == false) return context;
                        ReportStatus(Status_Stage.运行中);

                        sw.Restart();
                        context = job_GlobalWorld.Go_Top_WorldCenter_Pos();
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }
                finally
                {
                    tssl_TimeSpent.Text = $"耗时: {sw.Elapsed.TotalSeconds.ToString("F3")} 秒";
                    Status_Stage stage = context.ErrorCode != ErrorCodes.NoError ? Status_Stage.失败 : Status_Stage.成功;
                    ReportStatus(stage);
                }

                return context;
            });
        }

        private void btn_Go_Btm_WorldCenter_Pos_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Task_Requested_From_Client(() =>
            {
                Mission_Report context = new Mission_Report();
                Stopwatch sw = Stopwatch.StartNew();
                try
                {
                    do
                    {
                        if (SolveWare.Core.Is_Machine_Already_Homing() == false) return context;
                        ReportStatus(Status_Stage.运行中);

                        sw.Restart();
                        context = job_GlobalWorld.Go_Btm_WorldCenter_Pos();
                        context.NotPass(true);

                    } while (false);
                }
                catch (Exception ex)
                {
                    context.Window_Show_Not_Pass_Message(ErrorCodes.ActionFailed, ex.Message);
                }
                finally
                {
                    tssl_TimeSpent.Text = $"耗时: {sw.Elapsed.TotalSeconds.ToString("F3")} 秒";
                    Status_Stage stage = context.ErrorCode != ErrorCodes.NoError ? Status_Stage.失败 : Status_Stage.成功;
                    ReportStatus(stage);
                }

                return context;
            });
        }

        private void btn_Btm_Safe_Click(object sender, EventArgs e)
        {
            IView view = new Form_Safe_Protection();
            view.Setup(this.data_GlobalCenter.Data_Safe_Btm_Module);
            (view as Form).Show();
        }

        private void btn_Top_Safe_Click(object sender, EventArgs e)
        {
            IView view = new Form_Safe_Protection();
            view.Setup(this.data_GlobalCenter.Data_Safe_Top_Module);
            (view as Form).Show();
        }

        private void btn_Save_WorldCenter_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                job_GlobalWorld.Save_Pos();
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void Form_Learn_Global_Center_Load(object sender, EventArgs e)
        {
            StartListening();
        }

        private void Form_Learn_Global_Center_FormClosing(object sender, FormClosingEventArgs e)
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
                    if (this.data_GlobalCenter == null) { continue; }

                    DataBinding_lbl_Pos(data_GlobalCenter);
                    DataBinding_Obj(data_GlobalCenter);
                    Thread.Sleep(10);
                }
            }, cancelSource.Token);
        }
        private void StopListening()
        {
            if(cancelSource != null) cancelSource.Cancel();
        }

        private void DataBinding_lbl_Pos(Data_GlobalWorldCenter data)
        {
            this.Refresh_UI_Item(new[] { lbl_Top_MotorX_Info, lbl_Top_MotorY_Info, lbl_Top_MotorZ_Info, lbl_Top_MotorT_Info }, () =>
            {
                lbl_Top_MotorX_Info.Text = $"{ResourceKey.Motor_Top_X} {data.Top_Module_PosX} mm";
                lbl_Top_MotorY_Info.Text = $"{ResourceKey.Motor_Top_Y} {data.Top_Module_PosY} mm";
                lbl_Top_MotorZ_Info.Text = $"{ResourceKey.Motor_Top_Z} {data.Top_Module_PosZ} mm";
                lbl_Top_MotorT_Info.Text = $"{ResourceKey.Motor_Top_T} {data.Top_Module_PosT} Deg";
            });

            this.Refresh_UI_Item(new[] { lbl_Btm_MotorX_Info, lbl_Btm_MotorY_Info, lbl_Btm_MotorZ_Info, lbl_Btm_MotorT_Info }, () =>
            {
                lbl_Btm_MotorX_Info.Text = $"{ResourceKey.Motor_Btm_X} {data.Btm_Module_PosX} mm";
                lbl_Btm_MotorY_Info.Text = $"{ResourceKey.Motor_Btm_Y} {data.Btm_Module_PosY} mm";
                lbl_Btm_MotorZ_Info.Text = $"{ResourceKey.Motor_Btm_Z} {data.Btm_Module_PosZ} mm";
                lbl_Btm_MotorT_Info.Text = $"{ResourceKey.Motor_Btm_T} {data.Btm_Module_PosT} Deg";
            });

            this.Refresh_UI_Item(new[] { lbl_Top_WorldCenter_PosX, lbl_Top_WorldCenter_PosY, lbl_Top_WorldCenter_PosZ, lbl_Top_WorldCenter_PosT }, () =>
            {
                lbl_Top_WorldCenter_PosX.Text = $"{ResourceKey.Motor_Top_X} {data.Top_WorldCenter_PosX} mm";
                lbl_Top_WorldCenter_PosY.Text = $"{ResourceKey.Motor_Top_Y} {data.Top_WorldCenter_PosY} mm";
                lbl_Top_WorldCenter_PosZ.Text = $"{ResourceKey.Motor_Top_Z} {data.Top_WorldCenter_PosZ} mm";
                lbl_Top_WorldCenter_PosT.Text = $"{ResourceKey.Motor_Top_T} {data.Top_WorldCenter_PosT} Deg";
            });

            this.Refresh_UI_Item(new[] { lbl_Btm_WorldCenter_PosX, lbl_Btm_WorldCenter_PosY, lbl_Btm_WorldCenter_PosZ, lbl_Btm_WorldCenter_PosT }, () =>
            {
                lbl_Btm_WorldCenter_PosX.Text = $"{ResourceKey.Motor_Btm_X} {data.Btm_WorldCenter_PosX} mm";
                lbl_Btm_WorldCenter_PosY.Text = $"{ResourceKey.Motor_Btm_Y} {data.Btm_WorldCenter_PosY} mm";
                lbl_Btm_WorldCenter_PosZ.Text = $"{ResourceKey.Motor_Btm_Z} {data.Btm_WorldCenter_PosZ} mm";
                lbl_Btm_WorldCenter_PosT.Text = $"{ResourceKey.Motor_Btm_T} {data.Btm_WorldCenter_PosT} Deg";
            });        
        }
        private void DataBinding_Obj(Data_GlobalWorldCenter data)
        {
            tssl_Save_Date.Text = $"储存时间: {data.SaveDate}";

          
            this.Refresh_UI_Item(new[] { ckb_Top_Module_Move_To_Center_After_Top_Inspection, ckb_Btm_Module_Move_To_Center_After_Top_Inspection }, () =>
            {
                lbl_Top_InspectKit.BackColor = Color.LightBlue;
                lbl_Btm_InspectKit.BackColor = Color.LightBlue;
                lbl_Top_InspectKit.Text = data.Top_Module_InspectKit_Name;
                lbl_Btm_InspectKit.Text = data.Btm_Module_InspectKit_Name;
            });
        }

        private void ckb_Btm_Module_Move_To_Center_After_Top_Inspection_CheckedChanged(object sender, EventArgs e)
        {
            if (data_GlobalCenter == null) return;
            data_GlobalCenter.Top_Module_Move_To_Center = (sender as CheckBox).Checked;
        }

        private void ckb_Top_Module_Move_To_Center_After_Top_Inspection_CheckedChanged(object sender, EventArgs e)
        {
            if (data_GlobalCenter == null) return;
            data_GlobalCenter.Btm_Module_Move_To_Center = (sender as CheckBox).Checked;
        }
    }
}
