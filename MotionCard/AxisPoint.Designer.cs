
namespace MotionCard
{
    partial class AxisPoint
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
            this.lbl_PointName = new System.Windows.Forms.Label();
            this.lbl_unit = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_PointsNum = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_PointName
            // 
            this.lbl_PointName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PointName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PointName.Location = new System.Drawing.Point(4, 1);
            this.lbl_PointName.Name = "lbl_PointName";
            this.lbl_PointName.Size = new System.Drawing.Size(77, 23);
            this.lbl_PointName.TabIndex = 0;
            this.lbl_PointName.Text = "名称";
            this.lbl_PointName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_unit
            // 
            this.lbl_unit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_unit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_unit.Location = new System.Drawing.Point(131, 1);
            this.lbl_unit.Name = "lbl_unit";
            this.lbl_unit.Size = new System.Drawing.Size(16, 23);
            this.lbl_unit.TabIndex = 0;
            this.lbl_unit.Text = "mm";
            this.lbl_unit.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_unit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_PointName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_PointsNum, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(151, 25);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txt_PointsNum
            // 
            this.txt_PointsNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_PointsNum.Enabled = false;
            this.txt_PointsNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_PointsNum.Location = new System.Drawing.Point(84, 2);
            this.txt_PointsNum.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.txt_PointsNum.Multiline = true;
            this.txt_PointsNum.Name = "txt_PointsNum";
            this.txt_PointsNum.Size = new System.Drawing.Size(44, 22);
            this.txt_PointsNum.TabIndex = 1;
            this.txt_PointsNum.Text = "0";
            this.txt_PointsNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AxisPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AxisPoint";
            this.Size = new System.Drawing.Size(151, 25);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_PointName;
        private System.Windows.Forms.Label lbl_unit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txt_PointsNum;
    }
}
