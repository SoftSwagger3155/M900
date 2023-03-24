
namespace SolveWare_Service_Vision
{
    partial class UserHWControls
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_GrayValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Location = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_OpenImage = new System.Windows.Forms.ToolStripButton();
            this.tsb_Savemage = new System.Windows.Forms.ToolStripButton();
            this.tsb_AutoImage = new System.Windows.Forms.ToolStripButton();
            this.tsb_IsShowCrros = new System.Windows.Forms.ToolStripButton();
            this.tsb_DrawCircle = new System.Windows.Forms.ToolStripButton();
            this.tsb_DrawRectangle = new System.Windows.Forms.ToolStripButton();
            this.tsb_DrawLines = new System.Windows.Forms.ToolStripButton();
            this.tsb_ClearHwindow = new System.Windows.Forms.ToolStripButton();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.hWindowControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 353);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_GrayValue,
            this.tssl_Location});
            this.statusStrip1.Location = new System.Drawing.Point(1, 330);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(318, 23);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_GrayValue
            // 
            //this.tssl_GrayValue.Image = global::SolveWare_Service_Vision.Properties.Resources.ColorChanne;
            this.tssl_GrayValue.Name = "tssl_GrayValue";
            this.tssl_GrayValue.Size = new System.Drawing.Size(45, 18);
            this.tssl_GrayValue.Text = "0,0";
            // 
            // tssl_Location
            // 
            //this.tssl_Location.Image = Properties.Resources.VisionLocation;
            this.tssl_Location.Name = "tssl_Location";
            this.tssl_Location.Size = new System.Drawing.Size(45, 18);
            this.tssl_Location.Text = "0,0";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OpenImage,
            this.tsb_Savemage,
            this.tsb_AutoImage,
            this.tsb_IsShowCrros,
            this.tsb_DrawCircle,
            this.tsb_DrawRectangle,
            this.tsb_DrawLines,
            this.tsb_ClearHwindow});
            this.toolStrip1.Location = new System.Drawing.Point(1, 307);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(318, 23);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_OpenImage
            // 
            this.tsb_OpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.tsb_OpenImage.Image = global::HVision.Properties.Resources.OpenFile;
            this.tsb_OpenImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OpenImage.Name = "tsb_OpenImage";
            this.tsb_OpenImage.Size = new System.Drawing.Size(24, 20);
            this.tsb_OpenImage.Text = "打开图片";
            //this.tsb_OpenImage.Click += new System.EventHandler(this.tsb_OpenImage_Click);
            // 
            // tsb_Savemage
            // 
            this.tsb_Savemage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.tsb_Savemage.Image = global::HVision.Properties.Resources.Save;
            this.tsb_Savemage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Savemage.Name = "tsb_Savemage";
            this.tsb_Savemage.Size = new System.Drawing.Size(24, 20);
            this.tsb_Savemage.Text = "保存图片";
            //this.tsb_Savemage.Click += new System.EventHandler(this.tsb_Savemage_Click);
            // 
            // tsb_AutoImage
            // 
            this.tsb_AutoImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.tsb_AutoImage.Image = global::HVision.Properties.Resources.Auto;
            this.tsb_AutoImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AutoImage.Name = "tsb_AutoImage";
            this.tsb_AutoImage.Size = new System.Drawing.Size(24, 20);
            this.tsb_AutoImage.Text = "图片自适应";
            //this.tsb_AutoImage.Click += new System.EventHandler(this.tsb_AutoImage_Click);
            // 
            // tsb_IsShowCrros
            // 
            this.tsb_IsShowCrros.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.tsb_IsShowCrros.Image = global::HVision.Properties.Resources.Cross;
            this.tsb_IsShowCrros.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_IsShowCrros.Name = "tsb_IsShowCrros";
            this.tsb_IsShowCrros.Size = new System.Drawing.Size(24, 20);
            this.tsb_IsShowCrros.Text = "十字架";
            //this.tsb_IsShowCrros.Click += new System.EventHandler(this.tsb_IsShowCrros_Click);
            // 
            // tsb_DrawCircle
            // 
            this.tsb_DrawCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.tsb_DrawCircle.Image = global::HVision.Properties.Resources.Circle;
            this.tsb_DrawCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DrawCircle.Name = "tsb_DrawCircle";
            this.tsb_DrawCircle.Size = new System.Drawing.Size(24, 20);
            this.tsb_DrawCircle.Text = "圆";
            // 
            // tsb_DrawRectangle
            // 
            this.tsb_DrawRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_DrawRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DrawRectangle.Name = "tsb_DrawRectangle";
            this.tsb_DrawRectangle.Size = new System.Drawing.Size(24, 20);
            this.tsb_DrawRectangle.Text = "矩形";
            // 
            // tsb_DrawLines
            // 
            this.tsb_DrawLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_DrawLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DrawLines.Name = "tsb_DrawLines";
            this.tsb_DrawLines.Size = new System.Drawing.Size(24, 20);
            this.tsb_DrawLines.Text = "直线";
            // 
            // tsb_ClearHwindow
            // 
            this.tsb_ClearHwindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_ClearHwindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ClearHwindow.Name = "tsb_ClearHwindow";
            this.tsb_ClearHwindow.Size = new System.Drawing.Size(24, 20);
            this.tsb_ClearHwindow.Text = "清除ROI";
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(4, 3);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(312, 301);
            this.hWindowControl1.TabIndex = 2;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(312, 301);
            //this.hWindowControl1.HMouseMove += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseMove);
            //this.hWindowControl1.HMouseDown += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseDown);
            //this.hWindowControl1.HMouseUp += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseUp);
            //this.hWindowControl1.HMouseWheel += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseWheel);
            // 
            // UserHWControls
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserHWControls";
            this.Size = new System.Drawing.Size(320, 353);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_OpenImage;
        private System.Windows.Forms.ToolStripButton tsb_Savemage;
        private System.Windows.Forms.ToolStripButton tsb_AutoImage;
        private System.Windows.Forms.ToolStripButton tsb_IsShowCrros;
        private System.Windows.Forms.ToolStripStatusLabel tssl_GrayValue;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Location;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.ToolStripButton tsb_DrawCircle;
        private System.Windows.Forms.ToolStripButton tsb_ClearHwindow;
        private System.Windows.Forms.ToolStripButton tsb_DrawRectangle;
        private System.Windows.Forms.ToolStripButton tsb_DrawLines;
    }
}
