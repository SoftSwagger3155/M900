using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Vision.Data;
using SolveWare_Service_Vision.Inspection.JobSheet;
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
        Data_InspectionKit dataKit;
        const int SheetNo_Brightness = 0;
        const int SheetNo_Lighting = 1;
        const int SheetNo_PatternMatch = 2;
        const int SheetNo_Blob = 3;
        const string NodeName_BrightNess = "相机亮度设定";
        const string NodeName_Lighting = "光源设定";
        const string NodeName_PatternMatch = "模板";
        const string NodeName_Blob = "Blob";

        static string leftArrow = "";
        static string rightArrow = "";
        static string upArrow = "";
        static string downArrow = "";
        static string leftTurn = "";
        static string rightTurn = "";

        public Form_InspectKit()
        {
            InitializeComponent();
            MakeTreeView(); 
        }

        public void Setup<TObj>(TObj obj)
        {
            this.dataKit = obj as Data_InspectionKit;
        }

        private void MakeTreeView()
        {
            tView_Content.Nodes.Clear();
            tView_Content.Nodes.Add(NodeName_BrightNess);
            tView_Content.Nodes.Add(NodeName_Lighting);
            tView_Content.Nodes.Add(NodeName_PatternMatch);
            tView_Content.Nodes.Add(NodeName_Blob);

            tView_Content.Nodes[SheetNo_Brightness].Nodes.Add("相机 曝光时间 100 us 增益 10");
            tView_Content.Nodes[SheetNo_Lighting].Nodes.Add("上相机 光源 OP 开");
            tView_Content.Nodes[SheetNo_Lighting].Nodes.Add("下相机 光源 OP 关");
            tView_Content.Nodes[SheetNo_PatternMatch].Nodes.Add("模板学习: 开始角度 0 结束角度 20 金字塔层数 8 容许度 80%");
            tView_Content.Nodes[SheetNo_Blob].Nodes.Add("Blob 阈值 25 尺度 5");
            tView_Content.ExpandAll();



            tView_Content.MouseDoubleClick -= TView_Content_MouseDoubleClick;
            tView_Content.MouseDoubleClick += TView_Content_MouseDoubleClick;
        }

        private void TView_Content_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string nodeName = (sender as TreeView).SelectedNode.Text;
            
            switch(nodeName)
            {
                case NodeName_BrightNess:
                    IView view_BrightNess = new Form_InspectKit_Brightness();
                    view_BrightNess.Setup(dataKit);
                    (view_BrightNess as Form).ShowDialog();
                    break;
                case NodeName_Lighting:
                    IView view_Lighting = new Form_InspectKit_Lighting();
                    view_Lighting.Setup(dataKit);
                    (view_Lighting as Form).ShowDialog();
                    break;
                case NodeName_PatternMatch:
                    IView view_PatternMatch = new Form_InspectKit_PatternMatch();
                    view_PatternMatch.Setup(dataKit);
                    (view_PatternMatch as Form).ShowDialog();
                    break;
                case NodeName_Blob:
                    break;
                
                default:
                    DialogResult result = MessageBox.Show($"是否删除\r\n{nodeName}", "提问", MessageBoxButtons.YesNoCancel);
                    break;

            }

        }

    }
}
