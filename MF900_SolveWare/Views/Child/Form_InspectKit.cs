using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.General;
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
        public void Setup<TObj>(TObj obj)
        {
            this.job_Inspect = obj as Job_Inspect;
            this.data_Inspect = job_Inspect.Data;
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
            if (data_Inspect == null)
            {
                tView_Content.Nodes.Add(NodeName_BrightNess);
                tView_Content.Nodes.Add(NodeName_Lighting);
                tView_Content.Nodes.Add(NodeName_PatternMatch);
                tView_Content.Nodes.Add(NodeName_Blob);
            }
            else
            {
                Convert_Data_To_TreeView();
            }         
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
                        if (lighting.Is_IO_Controlled)
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
           if(data_Inspect.JobSheet_PatternMatch_Data != null)
            {
                tView_Content.Nodes[SheetNo_PatternMatch].Nodes.Add($"开始角度 {data_Inspect.JobSheet_PatternMatch_Data.AngleStart} ," +
                                                                                                              $"角度范围 {data_Inspect.JobSheet_PatternMatch_Data.AngleExtent} ," +
                                                                                                              $"最小匹配值 {data_Inspect.JobSheet_PatternMatch_Data.MinScore} ," +
                                                                                                              $"最小规模 {data_Inspect.JobSheet_PatternMatch_Data.MinScale} ," +
                                                                                                              $"最大规模 {data_Inspect.JobSheet_PatternMatch_Data.MaxScale} ," +
                                                                                                              $"匹配最大个数 {data_Inspect.JobSheet_PatternMatch_Data.NumMatches} ," +
                                                                                                              $"金字塔层数 {data_Inspect.JobSheet_PatternMatch_Data.NumLevels}  ," +
                                                                                                              $"亚像素精度 {data_Inspect.JobSheet_PatternMatch_Data.SubPixel}  ," +
                                                                                                              $"重迭最大个数{data_Inspect.JobSheet_PatternMatch_Data.MaxOverLap} ," +
                                                                                                              $"贪婪度 {data_Inspect.JobSheet_PatternMatch_Data.Greediness}" );
            }
           if(data_Inspect.JobSheet_Blob_Data != null)
            {
                tView_Content.Nodes[SheetNo_Blob].Nodes.Add($"阈值{data_Inspect.JobSheet_Blob_Data.Threshold} 尺度 {data_Inspect.JobSheet_Blob_Data.HorizontalMeasureLength}");
            }                    
        }

        private void TView_Content_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string nodeName = (sender as TreeView).SelectedNode.Text;
            
            switch(nodeName)
            {
                case NodeName_BrightNess:
                    IView view_BrightNess = new Form_InspectKit_Brightness();
                    view_BrightNess.Setup(data_Inspect);
                    (view_BrightNess as Form).ShowDialog();
                    break;
                case NodeName_Lighting:
                    IView view_Lighting = new Form_InspectKit_Lighting();
                    view_Lighting.Setup(data_Inspect);
                    (view_Lighting as Form).ShowDialog();
                    break;
                case NodeName_PatternMatch:
                    IView view_PatternMatch = new Form_InspectKit_PatternMatch();
                    view_PatternMatch.Setup(data_Inspect);
                    (view_PatternMatch as Form).ShowDialog();
                    break;
                case NodeName_Blob:
                    break;
                
                default:
                    DialogResult result = MessageBox.Show($"是否删除\r\n{nodeName}", "提问", MessageBoxButtons.YesNoCancel);
                    break;

            }
            this.tView_Content.ExpandAll();
        }

        private void cmb_Selector_InspectKit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string jobName = (sender as ComboBox).SelectedItem as string;
                var job = SolveWare.Core.MMgr.Get_PairJob(jobName);
                
                if (job == null) return;
                this.Setup(job);

            }
            catch (Exception ex)
            {
                SolveWare.Core.ShowMsg(ex.Message);
            }
        }
    }
}
