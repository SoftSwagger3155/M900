namespace MF900_SolveWare.Views.Child
{
    partial class Form_InspectKit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_InspectKit));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("上相机 曝光时间 100 us  增益 10");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("相机亮度设定", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("上相机 光源 OP 开");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("下相机 光源 OP 关");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("光源设定", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Inspect = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_Selector_InspectKit = new System.Windows.Forms.ComboBox();
            this.gpb_Operation = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.tView_Content = new System.Windows.Forms.TreeView();
            this.pGrid_Parameters = new System.Windows.Forms.PropertyGrid();
            this.gpb_PatternMatch = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hWindow_Pattern_Template = new HalconDotNet.HWindowControl();
            this.button3 = new System.Windows.Forms.Button();
            this.ctrl_Camera = new SolveWare_Service_Vision.UserHWControl();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpb_Operation.SuspendLayout();
            this.gpb_PatternMatch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(85, 139);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(106, 45);
            this.btn_Save.TabIndex = 6;
            this.btn_Save.Text = "储存资料";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Inspect
            // 
            this.btn_Inspect.Location = new System.Drawing.Point(197, 24);
            this.btn_Inspect.Name = "btn_Inspect";
            this.btn_Inspect.Size = new System.Drawing.Size(106, 45);
            this.btn_Inspect.TabIndex = 5;
            this.btn_Inspect.Text = "视觉测试";
            this.btn_Inspect.UseVisualStyleBackColor = true;
            this.btn_Inspect.Click += new System.EventHandler(this.btn_Inspect_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 714);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1894, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.Lime;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(130, 20);
            this.toolStripStatusLabel3.Text = "测试结果: Good";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Margin = new System.Windows.Forms.Padding(10, 4, 0, 2);
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(231, 20);
            this.toolStripStatusLabel5.Text = "储存日期 : 2023/00/00 00:00:00";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(16, 978);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1291, 33);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 30);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(29, 30);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(29, 30);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_Selector_InspectKit);
            this.groupBox2.Location = new System.Drawing.Point(14, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(682, 87);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择器";
            // 
            // cmb_Selector_InspectKit
            // 
            this.cmb_Selector_InspectKit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_InspectKit.FormattingEnabled = true;
            this.cmb_Selector_InspectKit.Location = new System.Drawing.Point(23, 29);
            this.cmb_Selector_InspectKit.Name = "cmb_Selector_InspectKit";
            this.cmb_Selector_InspectKit.Size = new System.Drawing.Size(357, 28);
            this.cmb_Selector_InspectKit.TabIndex = 0;
            this.cmb_Selector_InspectKit.SelectionChangeCommitted += new System.EventHandler(this.cmb_Selector_InspectKit_SelectionChangeCommitted);
            // 
            // gpb_Operation
            // 
            this.gpb_Operation.Controls.Add(this.button4);
            this.gpb_Operation.Controls.Add(this.button3);
            this.gpb_Operation.Controls.Add(this.btn_Save);
            this.gpb_Operation.Controls.Add(this.btn_Inspect);
            this.gpb_Operation.Location = new System.Drawing.Point(1288, 503);
            this.gpb_Operation.Name = "gpb_Operation";
            this.gpb_Operation.Size = new System.Drawing.Size(499, 203);
            this.gpb_Operation.TabIndex = 8;
            this.gpb_Operation.TabStop = false;
            this.gpb_Operation.Text = "操作";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(85, 75);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 45);
            this.button4.TabIndex = 10;
            this.button4.Text = "删除模板";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tView_Content
            // 
            this.tView_Content.BackColor = System.Drawing.SystemColors.Control;
            this.tView_Content.HideSelection = false;
            this.tView_Content.Location = new System.Drawing.Point(1288, 296);
            this.tView_Content.Name = "tView_Content";
            treeNode1.Name = "节点5";
            treeNode1.Text = "上相机 曝光时间 100 us  增益 10";
            treeNode2.Name = "节点0";
            treeNode2.Text = "相机亮度设定";
            treeNode3.Name = "节点6";
            treeNode3.Text = "上相机 光源 OP 开";
            treeNode4.Name = "节点7";
            treeNode4.Text = "下相机 光源 OP 关";
            treeNode5.Name = "节点1";
            treeNode5.Text = "光源设定";
            this.tView_Content.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode5});
            this.tView_Content.ShowNodeToolTips = true;
            this.tView_Content.Size = new System.Drawing.Size(499, 194);
            this.tView_Content.TabIndex = 3;
            // 
            // pGrid_Parameters
            // 
            this.pGrid_Parameters.Location = new System.Drawing.Point(15, 24);
            this.pGrid_Parameters.Name = "pGrid_Parameters";
            this.pGrid_Parameters.Size = new System.Drawing.Size(535, 650);
            this.pGrid_Parameters.TabIndex = 10;
            // 
            // gpb_PatternMatch
            // 
            this.gpb_PatternMatch.Controls.Add(this.pGrid_Parameters);
            this.gpb_PatternMatch.Location = new System.Drawing.Point(707, 14);
            this.gpb_PatternMatch.Name = "gpb_PatternMatch";
            this.gpb_PatternMatch.Size = new System.Drawing.Size(562, 694);
            this.gpb_PatternMatch.TabIndex = 11;
            this.gpb_PatternMatch.TabStop = false;
            this.gpb_PatternMatch.Text = "模板参数";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hWindow_Pattern_Template);
            this.groupBox1.Location = new System.Drawing.Point(1287, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 276);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "学羽模板";
            // 
            // hWindow_Pattern_Template
            // 
            this.hWindow_Pattern_Template.BackColor = System.Drawing.Color.Black;
            this.hWindow_Pattern_Template.BorderColor = System.Drawing.Color.Black;
            this.hWindow_Pattern_Template.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindow_Pattern_Template.Location = new System.Drawing.Point(15, 23);
            this.hWindow_Pattern_Template.Name = "hWindow_Pattern_Template";
            this.hWindow_Pattern_Template.Size = new System.Drawing.Size(471, 240);
            this.hWindow_Pattern_Template.TabIndex = 0;
            this.hWindow_Pattern_Template.WindowSize = new System.Drawing.Size(471, 240);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(85, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 45);
            this.button3.TabIndex = 9;
            this.button3.Text = "模板学习";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ctrl_Camera
            // 
            this.ctrl_Camera.BackColor = System.Drawing.Color.White;
            this.ctrl_Camera.Location = new System.Drawing.Point(12, 106);
            this.ctrl_Camera.Name = "ctrl_Camera";
            this.ctrl_Camera.Size = new System.Drawing.Size(689, 599);
            this.ctrl_Camera.TabIndex = 9;
            // 
            // Form_InspectKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1894, 740);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpb_PatternMatch);
            this.Controls.Add(this.ctrl_Camera);
            this.Controls.Add(this.tView_Content);
            this.Controls.Add(this.gpb_Operation);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form_InspectKit";
            this.Text = "视觉";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.gpb_Operation.ResumeLayout(false);
            this.gpb_PatternMatch.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Inspect;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmb_Selector_InspectKit;
        private System.Windows.Forms.GroupBox gpb_Operation;
        private System.Windows.Forms.TreeView tView_Content;
        private System.Windows.Forms.Button button4;
        private SolveWare_Service_Vision.UserHWControl ctrl_Camera;
        private System.Windows.Forms.PropertyGrid pGrid_Parameters;
        private System.Windows.Forms.GroupBox gpb_PatternMatch;
        private System.Windows.Forms.GroupBox groupBox1;
        private HalconDotNet.HWindowControl hWindow_Pattern_Template;
        private System.Windows.Forms.Button button3;
    }
}