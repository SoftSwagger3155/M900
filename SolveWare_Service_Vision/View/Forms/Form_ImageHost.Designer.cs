namespace SolveWare_Service_Vision.View.Forms
{
    partial class Form_ImageHost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ImageHost));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
<<<<<<< HEAD
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
=======
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_OpenImage = new System.Windows.Forms.ToolStripButton();
            this.tsb_Savemage = new System.Windows.Forms.ToolStripButton();
            this.tsb_AutoImage = new System.Windows.Forms.ToolStripButton();
            this.tsb_IsShowCrros = new System.Windows.Forms.ToolStripButton();
            this.tsb_DrawCircle = new System.Windows.Forms.ToolStripButton();
            this.tsb_DrawRectangle = new System.Windows.Forms.ToolStripButton();
            this.tsb_DrawLines = new System.Windows.Forms.ToolStripButton();
            this.tsb_ClearHwindow = new System.Windows.Forms.ToolStripButton();
            this.tsb_Play = new System.Windows.Forms.ToolStripButton();
            this.tsb_Stop = new System.Windows.Forms.ToolStripButton();
            this.tsb_GrabOneImage = new System.Windows.Forms.ToolStripButton();
            this.tssl_GrayValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Location = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_CameraCapabilityInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
>>>>>>> f13614b6d60c63a69de90ab4ffdfbf48a575aeac
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 365);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(360, 35);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 30);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(5, 335);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(350, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.hWindowControl1, "hWindowControl1");
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
<<<<<<< HEAD
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(8, 8);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(344, 324);
            this.hWindowControl1.TabIndex = 3;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(344, 324);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.hWindowControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.17507F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.824926F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 365);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Form_ImageHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(360, 400);
=======
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 1080, 1920);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.WindowSize = new System.Drawing.Size(604, 467);
            this.hWindowControl1.HMouseMove += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseMove);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.hWindowControl1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OpenImage,
            this.tsb_Savemage,
            this.tsb_AutoImage,
            this.tsb_IsShowCrros,
            this.tsb_DrawCircle,
            this.tsb_DrawRectangle,
            this.tsb_DrawLines,
            this.tsb_ClearHwindow,
            this.tsb_Play,
            this.tsb_Stop,
            this.tsb_GrabOneImage});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsb_OpenImage
            // 
            resources.ApplyResources(this.tsb_OpenImage, "tsb_OpenImage");
            this.tsb_OpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_OpenImage.Name = "tsb_OpenImage";
            this.tsb_OpenImage.Click += new System.EventHandler(this.tsb_OpenImage_Click);
            // 
            // tsb_Savemage
            // 
            this.tsb_Savemage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_Savemage, "tsb_Savemage");
            this.tsb_Savemage.Name = "tsb_Savemage";
            // 
            // tsb_AutoImage
            // 
            this.tsb_AutoImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_AutoImage, "tsb_AutoImage");
            this.tsb_AutoImage.Name = "tsb_AutoImage";
            this.tsb_AutoImage.Click += new System.EventHandler(this.tsb_AutoImage_Click_1);
            // 
            // tsb_IsShowCrros
            // 
            this.tsb_IsShowCrros.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_IsShowCrros, "tsb_IsShowCrros");
            this.tsb_IsShowCrros.Name = "tsb_IsShowCrros";
            this.tsb_IsShowCrros.Click += new System.EventHandler(this.tsb_IsShowCrros_Click);
            // 
            // tsb_DrawCircle
            // 
            this.tsb_DrawCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_DrawCircle, "tsb_DrawCircle");
            this.tsb_DrawCircle.Name = "tsb_DrawCircle";
            this.tsb_DrawCircle.Click += new System.EventHandler(this.tsb_DrawCircle_Click);
            // 
            // tsb_DrawRectangle
            // 
            this.tsb_DrawRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_DrawRectangle, "tsb_DrawRectangle");
            this.tsb_DrawRectangle.Name = "tsb_DrawRectangle";
            this.tsb_DrawRectangle.Click += new System.EventHandler(this.tsb_DrawRectangle_Click);
            // 
            // tsb_DrawLines
            // 
            this.tsb_DrawLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_DrawLines, "tsb_DrawLines");
            this.tsb_DrawLines.Name = "tsb_DrawLines";
            this.tsb_DrawLines.Click += new System.EventHandler(this.tsb_DrawLines_Click);
            // 
            // tsb_ClearHwindow
            // 
            this.tsb_ClearHwindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_ClearHwindow, "tsb_ClearHwindow");
            this.tsb_ClearHwindow.Name = "tsb_ClearHwindow";
            this.tsb_ClearHwindow.Click += new System.EventHandler(this.tsb_ClearHwindow_Click);
            // 
            // tsb_Play
            // 
            this.tsb_Play.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_Play, "tsb_Play");
            this.tsb_Play.Name = "tsb_Play";
            this.tsb_Play.Click += new System.EventHandler(this.tsb_Play_Click);
            // 
            // tsb_Stop
            // 
            this.tsb_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_Stop, "tsb_Stop");
            this.tsb_Stop.Name = "tsb_Stop";
            this.tsb_Stop.Click += new System.EventHandler(this.tsb_Stop_Click);
            // 
            // tsb_GrabOneImage
            // 
            this.tsb_GrabOneImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb_GrabOneImage, "tsb_GrabOneImage");
            this.tsb_GrabOneImage.Name = "tsb_GrabOneImage";
            this.tsb_GrabOneImage.Click += new System.EventHandler(this.tsb_GrabOneImage_Click);
            // 
            // tssl_GrayValue
            // 
            resources.ApplyResources(this.tssl_GrayValue, "tssl_GrayValue");
            this.tssl_GrayValue.Name = "tssl_GrayValue";
            // 
            // tssl_Location
            // 
            resources.ApplyResources(this.tssl_Location, "tssl_Location");
            this.tssl_Location.Name = "tssl_Location";
            // 
            // tssl_CameraCapabilityInfo
            // 
            resources.ApplyResources(this.tssl_CameraCapabilityInfo, "tssl_CameraCapabilityInfo");
            this.tssl_CameraCapabilityInfo.Name = "tssl_CameraCapabilityInfo";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_GrayValue,
            this.tssl_Location,
            this.tssl_CameraCapabilityInfo});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            // 
            // Form_ImageHost
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
>>>>>>> f13614b6d60c63a69de90ab4ffdfbf48a575aeac
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form_ImageHost";
<<<<<<< HEAD
            this.Text = "Form_ImageHost";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
=======
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
>>>>>>> f13614b6d60c63a69de90ab4ffdfbf48a575aeac
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
<<<<<<< HEAD
=======
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_OpenImage;
        private System.Windows.Forms.ToolStripButton tsb_Savemage;
        private System.Windows.Forms.ToolStripButton tsb_AutoImage;
        private System.Windows.Forms.ToolStripButton tsb_IsShowCrros;
        private System.Windows.Forms.ToolStripButton tsb_DrawCircle;
        private System.Windows.Forms.ToolStripButton tsb_DrawRectangle;
        private System.Windows.Forms.ToolStripButton tsb_DrawLines;
        private System.Windows.Forms.ToolStripButton tsb_ClearHwindow;
        private System.Windows.Forms.ToolStripButton tsb_Play;
        private System.Windows.Forms.ToolStripButton tsb_Stop;
        private System.Windows.Forms.ToolStripButton tsb_GrabOneImage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_GrayValue;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Location;
        private System.Windows.Forms.ToolStripStatusLabel tssl_CameraCapabilityInfo;
>>>>>>> f13614b6d60c63a69de90ab4ffdfbf48a575aeac
    }
}