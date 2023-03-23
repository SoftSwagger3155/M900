
namespace MF900
{
    partial class FormPanel
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Suspend = new Sunny.UI.UIButton();
            this.btn_SaveProgramPara = new Sunny.UI.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.uiPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(898, 464);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.tableLayoutPanel2);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.FillColor = System.Drawing.Color.White;
            this.uiPanel1.FillColor2 = System.Drawing.Color.White;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(4, 413);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.uiPanel1.Size = new System.Drawing.Size(890, 46);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel1.TabIndex = 2;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.Controls.Add(this.btn_Suspend, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_SaveProgramPara, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(886, 42);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btn_Suspend
            // 
            this.btn_Suspend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Suspend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Suspend.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Suspend.Location = new System.Drawing.Point(814, 3);
            this.btn_Suspend.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Suspend.Name = "btn_Suspend";
            this.btn_Suspend.Size = new System.Drawing.Size(69, 36);
            this.btn_Suspend.TabIndex = 0;
            this.btn_Suspend.Text = "OK";
            this.btn_Suspend.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Suspend.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_Suspend.Click += new System.EventHandler(this.btn_Suspend_Click);
            // 
            // btn_SaveProgramPara
            // 
            this.btn_SaveProgramPara.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveProgramPara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SaveProgramPara.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveProgramPara.Location = new System.Drawing.Point(739, 3);
            this.btn_SaveProgramPara.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_SaveProgramPara.Name = "btn_SaveProgramPara";
            this.btn_SaveProgramPara.Size = new System.Drawing.Size(69, 36);
            this.btn_SaveProgramPara.TabIndex = 1;
            this.btn_SaveProgramPara.Text = "保 存";
            this.btn_SaveProgramPara.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveProgramPara.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_SaveProgramPara.Click += new System.EventHandler(this.btn_SaveProgramPara_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(892, 402);
            this.panel1.TabIndex = 3;
            // 
            // FormPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 464);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPanel";
            this.Text = "FormPanel";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Sunny.UI.UIButton btn_Suspend;
        private Sunny.UI.UIPanel uiPanel1;
        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UIButton btn_SaveProgramPara;
    }
}