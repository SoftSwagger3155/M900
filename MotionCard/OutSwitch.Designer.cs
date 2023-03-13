
namespace MotionCard
{
    partial class OutSwitch
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
            this.label1 = new System.Windows.Forms.Label();
            this.xktToggle1 = new XKTControl.xktToggle();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.xktToggle1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(93, 56);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "信号名称";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xktToggle1
            // 
            this.xktToggle1.Checked = false;
            this.xktToggle1.CircleDiameter = 10;
            this.xktToggle1.CirclePosition = 10;
            this.xktToggle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xktToggle1.FalseColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.xktToggle1.FalseText = "关闭";
            this.xktToggle1.Location = new System.Drawing.Point(3, 22);
            this.xktToggle1.Name = "xktToggle1";
            this.xktToggle1.Size = new System.Drawing.Size(87, 31);
            this.xktToggle1.SliderColor = System.Drawing.Color.White;
            this.xktToggle1.Switchtype = XKTControl.xktToggle.SwitchType.Rectangle;
            this.xktToggle1.TabIndex = 1;
            this.xktToggle1.TrueColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(119)))), ((int)(((byte)(232)))));
            this.xktToggle1.TrueText = "打开";
            this.xktToggle1.CheckedChanged += new System.EventHandler(this.xktToggle1_CheckedChanged);
            // 
            // OutSwitch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OutSwitch";
            this.Size = new System.Drawing.Size(93, 56);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private XKTControl.xktToggle xktToggle1;
    }
}
