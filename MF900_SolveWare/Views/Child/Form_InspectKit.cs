using HalconDotNet;
using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
using SolveWare_Service_Tool.Camera.Base.Abstract;
using SolveWare_Service_Utility.Extension;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.Business;
using SolveWare_Service_Vision.Inspection.JobSheet;
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
    public partial class Form_InspectKit : Form, IView
    {
       
        const int SheetNo_Brightness = 0;
        const int SheetNo_Lighting = 1;
        const int SheetNo_PatternMatch = 2;
        const int SheetNo_Blob = 3;
        const string NodeName_BrightNess = "相机亮度设定";
        const string NodeName_Lighting = "光源设定";
        const string NodeName_PatternMatch = "模板";
        const string NodeName_Blob = "Blob";

        

        public Form_InspectKit()
        {
            InitializeComponent();
            Fillup_Combobox_Inspect();
            MakeTreeView(); 
        }

        Job_Inspect job_Inspect;
        Data_Inspection data_Inspect;
        CameraMediaBase camera;
        public void Setup<TObj>(TObj obj)
        {
            this.job_Inspect = obj as Job_Inspect;
            this.data_Inspect = job_Inspect.Data;
            this.camera = data_Inspect.CameraName.GetCamera();
            this.ctrl_Camera.Setup(camera);
            this.pGrid_Parameters.SelectedObject = data_Inspect.JobSheet_PatternMatch_Data;

        }
        private void Fillup_Combobox_Inspect()
        {
            this.cmb_Selector_InspectKit.Items.Clear();
            var jobs = SolveWare.Core.MMgr.Get_Identical_ReosurcBase_Job(ConstantProperty.ResourceKey_Inspect);
            jobs.ForEach(job=> cmb_Selector_InspectKit.Items.Add(job.Name));
        }
        private void MakeTreeView()
        {
            tView_Content.Nodes.Clear();
            tView_Content.Nodes.Add(NodeName_BrightNess);
            tView_Content.Nodes.Add(NodeName_Lighting);
            if (data_Inspect != null) Convert_Data_To_TreeView();


            tView_Content.ExpandAll();

            tView_Content.MouseDoubleClick -= TView_Content_MouseDoubleClick;
            tView_Content.MouseDoubleClick += TView_Content_MouseDoubleClick;
        }

        private void Convert_Data_To_TreeView()
        {
            //曝光
           if(data_Inspect.JobSheet_Brightness_Data != null)
            {
                tView_Content.Nodes[SheetNo_Brightness].Nodes.Add($"相机 曝光时间 {data_Inspect.JobSheet_Brightness_Data.ExposureTime} us, 增益 {data_Inspect.JobSheet_Brightness_Data.Gain}");
            }
           if(data_Inspect.JobSheet_Lighting_Datas != null )
            {
                if(data_Inspect.JobSheet_Lighting_Datas.Count > 0)
                {
                    foreach (var lighting in data_Inspect.JobSheet_Lighting_Datas)
                    {
                        if (!lighting.Is_IO_Controlled)
                        {
                            tView_Content.Nodes[SheetNo_Lighting].Nodes.Add($"光源 {lighting.Lighting_Name} 光度 {lighting.Intensity}");
                        }
                        else
                        {
                            tView_Content.Nodes[SheetNo_Lighting].Nodes.Add($"物件 {lighting.IO_Name}, 模式 {lighting.TriggerMode}");
                        }
                    }
                }
            }                 
        }

        private void TView_Content_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(data_Inspect == null)
            {
                SolveWare.Core.ShowMsg("请选择一个视觉物件");
                return;
            }

            if ((sender as TreeView).SelectedNode == null) return;

            string nodeName = (sender as TreeView).SelectedNode.Text;
            
            switch(nodeName)
            {
                case NodeName_BrightNess:
                    ShowDialog_JobSheet(new Form_InspectKit_Brightness());
                    break;
                case NodeName_Lighting:
                    ShowDialog_JobSheet(new Form_InspectKit_Lighting());
                    break;
                case NodeName_PatternMatch:
                    ShowDialog_JobSheet(new Form_InspectKit_PatternMatch());
                    break;
                case NodeName_Blob:
                    ShowDialog_JobSheet(new Form_InspectKit_PatternMatch());
                    break;
                
                default:
                    TreeNode treeNode = (sender as TreeView).SelectedNode;
                    if (tView_Content.Nodes[SheetNo_Brightness].Nodes.Contains(treeNode)) ShowDialog_JobSheet(new Form_InspectKit_Brightness());
                    else if (tView_Content.Nodes[SheetNo_Lighting].Nodes.Contains(treeNode)) ShowDialog_JobSheet(new Form_InspectKit_Lighting());
                    else if (tView_Content.Nodes[SheetNo_PatternMatch].Nodes.Contains(treeNode)) ShowDialog_JobSheet(new Form_InspectKit_PatternMatch());
                    else if (tView_Content.Nodes[SheetNo_Blob].Nodes.Contains(treeNode)) ShowDialog_JobSheet(new Form_InspectKit_PatternMatch());
                    break;

            }
            MakeTreeView();
            this.tView_Content.ExpandAll();
        }
        private void ShowDialog_JobSheet(IView form)
        {      
            form.Setup(data_Inspect);
            (form as Form).Show();
        }

        private void cmb_Selector_InspectKit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string jobName = (sender as ComboBox).SelectedItem as string;
                var job = SolveWare.Core.MMgr.Get_PairJob(jobName);
                
                if (job == null) return;
                this.Setup(job);
                MakeTreeView();

            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.job_Inspect == null)
                {
                    SolveWare.Core.ShowMsg("请选择一个视觉物件");
                    return;
                }

                this.job_Inspect.Save(true);
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    if (this.ctrl_Camera.Controller == null) return;
                    Mission_Report context  = this.ctrl_Camera.Controller.Learn_Pattern(this.job_Inspect.Data);

                    if (this.ctrl_Camera.Controller == null) return;
                    HImage obj = this.ctrl_Camera.Controller.Load_Model();
                    this.ctrl_Camera.Controller.Adapt_Window_And_Attach(obj, this.hWindow_Pattern_Template);
                }
                catch (Exception ex)
                {
                    SolveWare.Core.ShowMsg(ex.Message);
                }
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ctrl_Camera.Controller == null) return;
                HObject obj = this.ctrl_Camera.Controller.Load_Model();
                this.hWindow_Pattern_Template.HalconWindow.DispObj(obj);
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }

        private void btn_Inspect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ctrl_Camera.Controller == null) return;
                this.ctrl_Camera.Controller.Find_Pattern();
            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }
    }
}
