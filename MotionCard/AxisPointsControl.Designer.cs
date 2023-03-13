
namespace MotionCard
{
    partial class AxisPointsControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_PointsNum = new Sunny.UI.UITextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_PointsNum);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(56, 29);
            this.panel1.TabIndex = 0;
            // 
            // txt_PointsNum
            // 
            this.txt_PointsNum.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_PointsNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_PointsNum.Enabled = false;
            this.txt_PointsNum.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_PointsNum.Location = new System.Drawing.Point(0, 0);
            this.txt_PointsNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_PointsNum.MinimumSize = new System.Drawing.Size(1, 16);
            this.txt_PointsNum.Name = "txt_PointsNum";
            this.txt_PointsNum.ShowText = false;
            this.txt_PointsNum.Size = new System.Drawing.Size(56, 29);
            this.txt_PointsNum.TabIndex = 0;
            this.txt_PointsNum.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_PointsNum.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // AxisPointsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Name = "AxisPointsControl";
            this.Size = new System.Drawing.Size(56, 29);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UITextBox txt_PointsNum;
    }
}
