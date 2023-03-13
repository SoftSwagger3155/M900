
namespace MF900
{
    partial class CameraHWControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraHWControls));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radb_DownCamera = new Sunny.UI.UIRadioButton();
            this.radb_UpCamera = new Sunny.UI.UIRadioButton();
            this.userHWControls1 = new HVision.UserHWControls();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.userHWControls1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(414, 474);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radb_DownCamera);
            this.panel1.Controls.Add(this.radb_UpCamera);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 45);
            this.panel1.TabIndex = 0;
            // 
            // radb_DownCamera
            // 
            this.radb_DownCamera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radb_DownCamera.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radb_DownCamera.Location = new System.Drawing.Point(183, 9);
            this.radb_DownCamera.MinimumSize = new System.Drawing.Size(1, 1);
            this.radb_DownCamera.Name = "radb_DownCamera";
            this.radb_DownCamera.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.radb_DownCamera.Size = new System.Drawing.Size(87, 29);
            this.radb_DownCamera.TabIndex = 0;
            this.radb_DownCamera.Text = "下相机";
            this.radb_DownCamera.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.radb_DownCamera.CheckedChanged += new System.EventHandler(this.radb_DownCamera_CheckedChanged);
            // 
            // radb_UpCamera
            // 
            this.radb_UpCamera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radb_UpCamera.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radb_UpCamera.Location = new System.Drawing.Point(33, 9);
            this.radb_UpCamera.MinimumSize = new System.Drawing.Size(1, 1);
            this.radb_UpCamera.Name = "radb_UpCamera";
            this.radb_UpCamera.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.radb_UpCamera.Size = new System.Drawing.Size(87, 29);
            this.radb_UpCamera.TabIndex = 0;
            this.radb_UpCamera.Text = "上相机";
            this.radb_UpCamera.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.radb_UpCamera.CheckedChanged += new System.EventHandler(this.radb_UpCamera_CheckedChanged);
            // 
            // userHWControls1
            // 
            this.userHWControls1.BackColor = System.Drawing.Color.White;
            this.userHWControls1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userHWControls1.IsShowCross = true;
            this.userHWControls1.Location = new System.Drawing.Point(3, 54);
            this.userHWControls1.M_HObjects = ((System.Collections.Generic.List<HalconDotNet.HObject>)(resources.GetObject("userHWControls1.M_HObjects")));
            this.userHWControls1.m_RoiManage = null;
            this.userHWControls1.Name = "userHWControls1";
            this.userHWControls1.roiCircle = null;
            this.userHWControls1.roiLine = null;
            this.userHWControls1.roiRectangle = null;
            this.userHWControls1.Size = new System.Drawing.Size(408, 417);
            this.userHWControls1.TabIndex = 1;
            // 
            // CameraHWControls
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CameraHWControls";
            this.Size = new System.Drawing.Size(414, 474);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sunny.UI.UIRadioButton radb_UpCamera;
        private Sunny.UI.UIRadioButton radb_DownCamera;
        private System.Windows.Forms.Panel panel1;
        private HVision.UserHWControls userHWControls1;
    }
}
