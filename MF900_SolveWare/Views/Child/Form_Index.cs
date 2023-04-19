using MF900_SolveWare.Index.Data;
using MF900_SolveWare.Index.Job;
using MF900_SolveWare.Resource;
using MF900_SolveWare.Views.AxisMesForm;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Motor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_Index : Form, IView
    {
        public Form_Index()
        {
            InitializeComponent();
        }

        public Job_Index_Simulate job_Index { get; protected set; }
        public Data_Index indexData { get; protected set; }
        CancellationTokenSource cancelSource = null;
        AutoResetEvent stopFlag = new AutoResetEvent(false);

        public void Setup<TObj>(TObj obj)
        {
            job_Index = obj as Job_Index_Simulate;
            indexData = job_Index.Data;

            pGrid_Setup.SelectedObject = indexData.Data_Setup;
        }

        #region 本地方法 
        private void Start_Listening()
        {
            if (cancelSource != null) return;
            cancelSource = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        if (cancelSource.IsCancellationRequested) break;
                        if (indexData != null)
                        {
                            this.Invoke(new Action(() =>
                            {
                                if (lbl_TotalNo.InvokeRequired) this.lbl_TotalNo.Text = $"总产品数 : {indexData.Data_Setup.Total_Nos_Of_X * indexData.Data_Setup.Total_Nos_Of_Y}";
                                if (lbl_CurrentNumber.InvokeRequired) this.lbl_CurrentNumber.Text = $"当前目标 : {indexData.Data_Display.Current_No}";
                                if (lbl_CurrentRowColumn.InvokeRequired) this.lbl_CurrentRowColumn.Text = $"当前 Row {indexData.Data_Display.Current_Y} Column {indexData.Data_Display.Current_X}";
                            }));
                        }

                        if (cancelSource.IsCancellationRequested) break;
                        Thread.Sleep(50);
                    }
                }
                finally
                {
                      
                }
                stopFlag.Set();
            });

        }   
        public void Stop_Listening()
        {
            if(cancelSource == null) return;
            cancelSource.Cancel();
            stopFlag.WaitOne(100);
            cancelSource = null;
        }
        public void DataBinding_FirstPos()
        {
            lbl_Top_First_PosX_Info.Text = $"{ResourceKey.Motor_Top_X} {indexData.Data_FirstPos.Top_PosX} mm";
            lbl_Top_First_PosY_Info.Text = $"{ResourceKey.Motor_Top_Y} {indexData.Data_FirstPos.Top_PosY} mm";
            lbl_Top_First_PosZ_Info.Text = $"{ResourceKey.Motor_Top_Z} {indexData.Data_FirstPos.Top_PosZ} mm";
            lbl_Top_First_PosT_Info.Text = $"{ResourceKey.Motor_Top_T} {indexData.Data_FirstPos.Top_PosT} mm";

            lbl_Btm_First_PosX_Info.Text = $"{ResourceKey.Motor_Btm_X} {indexData.Data_FirstPos.Btm_PosX} mm";
            lbl_Btm_First_PosY_Info.Text = $"{ResourceKey.Motor_Btm_Y} {indexData.Data_FirstPos.Btm_PosY} mm";
            lbl_Btm_First_PosZ_Info.Text = $"{ResourceKey.Motor_Btm_Z} {indexData.Data_FirstPos.Btm_PosZ} mm";
            lbl_Btm_First_PosT_Info.Text = $"{ResourceKey.Motor_Btm_T} {indexData.Data_FirstPos.Btm_PosT} mm";
        }
        #endregion

        #region 事件
        private void btn_Go_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                string msg = string.Empty;
                int errorCode = ErrorCodes.NoError;
                try
                {
                    do
                    {
                        if (indexData == null) return;

                        if (string.IsNullOrEmpty(txb_NoToGo.Text))
                        {
                            msg += "请输入产品目标数";
                            break;
                        }
                        int noToGo = int.Parse(txb_NoToGo.Text);
                        errorCode = job_Index.Go(noToGo);

                        if (errorCode.NotPass(ref msg, job_Index.ErrorMsg)) break;

                    } while (false);
                }
                catch (Exception ex)
                {
                    msg += ex.Message;
                }
                SolveWare.Core.ShowMsg(msg, true);
            });
        }

        #endregion

        private void Form_Index_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop_Listening();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                stopFlag.Set();
                SolveWare.Core.MMgr.Stop();
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg, true);
        }

        private void Form_Index_Load(object sender, EventArgs e)
        {
            Start_Listening();
            DataBinding_FirstPos();
            
        }

        private void btn_Save_First_Pos_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                job_Index.Save_First_Pos();
                DataBinding_FirstPos();
            
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_General_Motor_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                IView view = new Form_Axis_General_Controller();
                (view as Form).Show();
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Save_Data_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                if (indexData == null) return;
                job_Index.Save(true);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Offset_Go_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (indexData == null) return;
                    int errorCode = job_Index.Go_Offset();
                    if (errorCode.NotPass(ref msg, job_Index.ErrorMsg)) break;

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
                    if (indexData == null) return;
                    int errorCode = job_Index.Return_Offset();
                    if (errorCode.NotPass(ref msg, job_Index.ErrorMsg)) break;

                } while (false);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Go_Action_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                string msg = string.Empty;
                try
                {
                    do
                    {
                        if (indexData == null) return;
                        if (string.IsNullOrEmpty(txb_NoToGo.Text))
                        {
                            msg += "请输入产品目标数";
                            break;
                        }
                        int noToGo = int.Parse(txb_NoToGo.Text);
                        int errorCode = job_Index.Go_Offset_Press(noToGo);
                        if (errorCode.NotPass(ref msg, job_Index.ErrorMsg)) break;

                    } while (false);

                }
                catch (Exception ex)
                {
                    msg += ex.Message;
                }
                SolveWare.Core.ShowMsg(msg);
            });
        }

        private void btn_Go_Previous_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (indexData == null) return;
                    if (string.IsNullOrEmpty(txb_NoToGo.Text))
                    {
                        msg += "请输入产品目标数";
                        break;
                    }
                    int noToGo = indexData.Data_Display.Current_No;
                    noToGo -= 1;

                    if (noToGo <= 0 || noToGo > indexData.Data_Setup.Total_Nos_Of_X * indexData.Data_Setup.Total_Nos_Of_Y)
                    {
                        msg += $"超出索引范围数量\r\n有效产品数为 X {indexData.Data_Setup.Total_Nos_Of_X} *  Y {indexData.Data_Setup.Total_Nos_Of_Y} = {indexData.Data_Setup.Total_Nos_Of_Y * indexData.Data_Setup.Total_Nos_Of_X}";
                        break;
                    }


                    int errorCode = job_Index.Go_Offset_Press(noToGo);
                    if (errorCode.NotPass(ref msg, job_Index.ErrorMsg)) break;

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        private void btn_Go_Next_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (indexData == null) return;
                    if (string.IsNullOrEmpty(txb_NoToGo.Text))
                    {
                        msg += "请输入产品目标数";
                        break;
                    }
                    int noToGo = indexData.Data_Display.Current_No;
                    noToGo -= 1;

                    if (noToGo <= 0 || noToGo > indexData.Data_Setup.Total_Nos_Of_X * indexData.Data_Setup.Total_Nos_Of_Y)
                    {
                        msg += $"超出索引范围数量\r\n有效产品数为 X {indexData.Data_Setup.Total_Nos_Of_X} *  Y {indexData.Data_Setup.Total_Nos_Of_Y} = {indexData.Data_Setup.Total_Nos_Of_Y * indexData.Data_Setup.Total_Nos_Of_X}";
                        break;
                    }


                    int errorCode =  job_Index.Go_Offset_Press(noToGo);
                    if(errorCode.NotPass(ref msg,job_Index.ErrorMsg)) break;

                } while (false);

            }
            catch (Exception ex)
            {
                msg += ex.Message;
            }
            SolveWare.Core.ShowMsg(msg);
        }

        Thread Action_Thread;
        AutoResetEvent StopFlag = new AutoResetEvent(false);
        private void btn_Go_Cycle_Click(object sender, EventArgs e)
        {
            if (Action_Thread != null) Action_Thread = null;
            

            Action_Thread = new Thread(Do_Auto_Cycle);
            Action_Thread.IsBackground = true;  
            Action_Thread.Start();
        }

        private void Do_Auto_Cycle()
        {
            string msg = string.Empty;
            try
            {
                do
                {
                    if (indexData == null) return;
                    int total = job_Index.Data.Data_Setup.Total_Nos_Of_X * job_Index.Data.Data_Setup.Total_Nos_Of_Y;

                    for (int i = 0; i < total; i++)
                    {
                        int no = i + 1;
                        int errorCode = job_Index.Go_Offset_Press(no);
                        if (errorCode.NotPass(ref msg, job_Index.ErrorMsg)) break;
                        if (stopFlag.WaitOne(10)) break;
                    }

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
