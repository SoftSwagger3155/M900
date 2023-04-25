using MF900_SolveWare.MMperPixel.Data;
using MF900_SolveWare.MMperPixel.Job;
using MF900_SolveWare.Resource;
using MF900_SolveWare.Views.AxisMesForm;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Core.General;
using SolveWare_Service_Utility.Extension;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_MMperPixel : Form,IView
    {
        public Form_MMperPixel()
        {
            InitializeComponent();
            Fillup_Combobox_Camera();
        }

        Job_MMperPixel job = null;
        Data_MMperPixel data = null;    

        public void Setup<TObj>(TObj obj)
        {
            this.job = obj as Job_MMperPixel;
            this.data = job.Data;
            DataBinding_CheckBox();
        }

        private void btn_General_Motor_Click(object sender, EventArgs e)
        {
            IView view = new Form_Axis_General_Controller();
            view.Show();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

        }

        private void btn_Update_Pos_Click(object sender, EventArgs e)
        {

        }

        private void Form_MMperPixel_Load(object sender, EventArgs e)
        {
            StartListening();
        }

        private void Form_MMperPixel_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListening();
        }

        CancellationTokenSource cancelSource = null;
        private void StartListening()
        {
            if(cancelSource == null) { cancelSource = new CancellationTokenSource(); }
            Task.Run(() =>
            {
                while (!cancelSource.IsCancellationRequested)
                {
                    DataBinding_Label_Info();
                    Thread.Sleep(10);
                }
            });
        }
        private void StopListening() 
        {
            if(cancelSource != null) { cancelSource.Cancel(); }
        }

        private void DataBinding_Label_Info()
        {
            if (this.data == null) return;
            this.Refresh_UI_Item(new[] {lbl_MotorX_Info, lbl_MotorY_Info, lbl_MotorZ_Info, lbl_MotorT_Info}, () =>
            {
                string mtrX = this.job.Module == Data_MMperPixel.TopModule ? ResourceKey.Motor_Top_X : ResourceKey.Motor_Btm_X;
                string mtrY = this.job.Module == Data_MMperPixel.TopModule ? ResourceKey.Motor_Top_Y : ResourceKey.Motor_Btm_Y;
                string mtrZ = this.job.Module == Data_MMperPixel.TopModule ? ResourceKey.Motor_Top_Z : ResourceKey.Motor_Btm_Z;
                string mtrT = this.job.Module == Data_MMperPixel.TopModule ? ResourceKey.Motor_Top_T : ResourceKey.Motor_Btm_T;

                lbl_MotorX_Info.Text = $"{mtrX} {data.PosX} mm";
                lbl_MotorY_Info.Text = $"{mtrY} {data.PosY} mm";
                lbl_MotorZ_Info.Text = $"{mtrZ} {data.PosZ} mm";
                lbl_MotorT_Info.Text = $"{mtrT} {data.PosT} Deg";
            });
            this.Refresh_UI_Item(new[] { lbl_MotorX_Info, lbl_MotorY_Info, lbl_MotorZ_Info, lbl_MotorT_Info }, () =>
            {
                lbl_MMperPixel_X.Text = $"马达X 像素比 {data.MMperPixel_X} um";
                lbl_MMperPixel_Y.Text = $"马达Y 像素比 {data.MMperPixel_Y} um";
                lbl_MMperPixel_Average.Text = $"平均像素比 {data.MMperPixel_Average} um";
            });
        }
        private void DataBinding_CheckBox()
        {
            if (data == null) return;
            this.ckb_Motor_X.Checked = data.Enable_MotorX;
            this.ckb_Motor_Y.Checked = data.Enable_MotorY;
        }
        private void Fillup_Combobox_Camera()
        {
            this.cmb_Selector_Camera.Items.Clear();
            var jobs = SolveWare.Core.MMgr.Get_Identical_ReosurcBase_Job(ConstantProperty.ResourceKey_MMperPixel);
            jobs.ForEach(x=> this.cmb_Selector_Camera.Items.Add(x.Name));

        }

        private void cmb_Selector_Camera_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string jobName = (sender as ComboBox).SelectedItem as string;    
            var job = SolveWare.Core.MMgr.Get_PairJob(jobName);
            this.Setup(job);
        }
    }
}
