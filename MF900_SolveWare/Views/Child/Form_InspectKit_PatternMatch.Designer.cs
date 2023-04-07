namespace MF900_SolveWare.Views.Child
{
    partial class Form_InspectKit_PatternMatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_InspectKit_PatternMatch));
            this.pGrid_PatternMatch = new System.Windows.Forms.PropertyGrid();
            this.hWindow_Model = new HalconDotNet.HWindowControl();
            this.hWindow_Live = new HalconDotNet.HWindowControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.gpb_Operation = new System.Windows.Forms.GroupBox();
            this.btn_Inspect = new System.Windows.Forms.Button();
            this.btn_LearnPattern = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_TimeSpent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.gpb_Operation.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pGrid_PatternMatch
            // 
            this.pGrid_PatternMatch.Location = new System.Drawing.Point(547, 401);
            this.pGrid_PatternMatch.Name = "pGrid_PatternMatch";
            this.pGrid_PatternMatch.Size = new System.Drawing.Size(517, 400);
            this.pGrid_PatternMatch.TabIndex = 0;
            // 
            // hWindow_Model
            // 
            this.hWindow_Model.BackColor = System.Drawing.Color.Black;
            this.hWindow_Model.BorderColor = System.Drawing.Color.Black;
            this.hWindow_Model.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindow_Model.Location = new System.Drawing.Point(547, 12);
            this.hWindow_Model.Name = "hWindow_Model";
            this.hWindow_Model.Size = new System.Drawing.Size(517, 358);
            this.hWindow_Model.TabIndex = 1;
            this.hWindow_Model.WindowSize = new System.Drawing.Size(517, 358);
            // 
            // hWindow_Live
            // 
            this.hWindow_Live.BackColor = System.Drawing.Color.Black;
            this.hWindow_Live.BorderColor = System.Drawing.Color.Black;
            this.hWindow_Live.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindow_Live.Location = new System.Drawing.Point(15, 12);
            this.hWindow_Live.Name = "hWindow_Live";
            this.hWindow_Live.Size = new System.Drawing.Size(517, 358);
            this.hWindow_Live.TabIndex = 2;
            this.hWindow_Live.WindowSize = new System.Drawing.Size(517, 358);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolStrip1.Location = new System.Drawing.Point(15, 373);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1101, 60);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // gpb_Operation
            // 
            this.gpb_Operation.Controls.Add(this.btn_Inspect);
            this.gpb_Operation.Controls.Add(this.btn_LearnPattern);
            this.gpb_Operation.Location = new System.Drawing.Point(15, 423);
            this.gpb_Operation.Name = "gpb_Operation";
            this.gpb_Operation.Size = new System.Drawing.Size(517, 383);
            this.gpb_Operation.TabIndex = 4;
            this.gpb_Operation.TabStop = false;
            this.gpb_Operation.Text = "操作";
            // 
            // btn_Inspect
            // 
            this.btn_Inspect.Location = new System.Drawing.Point(23, 86);
            this.btn_Inspect.Name = "btn_Inspect";
            this.btn_Inspect.Size = new System.Drawing.Size(92, 45);
            this.btn_Inspect.TabIndex = 1;
            this.btn_Inspect.Text = "视觉测试";
            this.btn_Inspect.UseVisualStyleBackColor = true;
            // 
            // btn_LearnPattern
            // 
            this.btn_LearnPattern.Location = new System.Drawing.Point(23, 35);
            this.btn_LearnPattern.Name = "btn_LearnPattern";
            this.btn_LearnPattern.Size = new System.Drawing.Size(92, 45);
            this.btn_LearnPattern.TabIndex = 0;
            this.btn_LearnPattern.Text = "模板学习";
            this.btn_LearnPattern.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.lbl_TimeSpent,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 862);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1076, 26);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(89, 20);
            this.toolStripStatusLabel2.Text = "灰度值: 255";
            // 
            // lbl_TimeSpent
            // 
            this.lbl_TimeSpent.Name = "lbl_TimeSpent";
            this.lbl_TimeSpent.Size = new System.Drawing.Size(89, 20);
            this.lbl_TimeSpent.Text = "耗时: 100秒";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(74, 20);
            this.toolStripStatusLabel1.Text = "状态: Idle";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(227, 20);
            this.toolStripStatusLabel3.Text = "像素座标 Row 100 Column 100";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 57);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(29, 57);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(29, 57);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(29, 57);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(29, 57);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(29, 57);
            this.toolStripButton6.Text = "toolStripButton6";
            // 
            // Form_InspectKit_PatternMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 888);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gpb_Operation);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.hWindow_Live);
            this.Controls.Add(this.hWindow_Model);
            this.Controls.Add(this.pGrid_PatternMatch);
            this.Name = "Form_InspectKit_PatternMatch";
            this.Text = "视觉模板";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gpb_Operation.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pGrid_PatternMatch;
        private HalconDotNet.HWindowControl hWindow_Model;
        private HalconDotNet.HWindowControl hWindow_Live;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.GroupBox gpb_Operation;
        private System.Windows.Forms.Button btn_Inspect;
        private System.Windows.Forms.Button btn_LearnPattern;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_TimeSpent;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    }
}