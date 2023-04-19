namespace MF900_SolveWare.Views.AxisMesForm
{
    partial class Form_Axis_Configuration
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.cmb_Selector_Motor = new System.Windows.Forms.ComboBox();
            this.tab_Control = new System.Windows.Forms.TabControl();
            this.tab_MtrTable = new System.Windows.Forms.TabPage();
            this.tab_MtrConfig = new System.Windows.Forms.TabPage();
            this.tab_MtrSpeed = new System.Windows.Forms.TabPage();
            this.tab_MtrSafety = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.tab_Control.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Save);
            this.groupBox1.Controls.Add(this.cmb_Selector_Motor);
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(191, 156);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "功能表";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(99, 95);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(87, 41);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "储存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // cmb_Selector_Motor
            // 
            this.cmb_Selector_Motor.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Motor.FormattingEnabled = true;
            this.cmb_Selector_Motor.Location = new System.Drawing.Point(5, 38);
            this.cmb_Selector_Motor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Selector_Motor.Name = "cmb_Selector_Motor";
            this.cmb_Selector_Motor.Size = new System.Drawing.Size(179, 35);
            this.cmb_Selector_Motor.TabIndex = 0;
            // 
            // tab_Control
            // 
            this.tab_Control.Controls.Add(this.tab_MtrTable);
            this.tab_Control.Controls.Add(this.tab_MtrConfig);
            this.tab_Control.Controls.Add(this.tab_MtrSpeed);
            this.tab_Control.Controls.Add(this.tab_MtrSafety);
            this.tab_Control.Location = new System.Drawing.Point(219, 19);
            this.tab_Control.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_Control.Name = "tab_Control";
            this.tab_Control.SelectedIndex = 0;
            this.tab_Control.Size = new System.Drawing.Size(1032, 700);
            this.tab_Control.TabIndex = 1;
            // 
            // tab_MtrTable
            // 
            this.tab_MtrTable.Location = new System.Drawing.Point(4, 25);
            this.tab_MtrTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_MtrTable.Name = "tab_MtrTable";
            this.tab_MtrTable.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_MtrTable.Size = new System.Drawing.Size(1024, 671);
            this.tab_MtrTable.TabIndex = 0;
            this.tab_MtrTable.Text = "功能表";
            this.tab_MtrTable.UseVisualStyleBackColor = true;
            // 
            // tab_MtrConfig
            // 
            this.tab_MtrConfig.Location = new System.Drawing.Point(4, 25);
            this.tab_MtrConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_MtrConfig.Name = "tab_MtrConfig";
            this.tab_MtrConfig.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_MtrConfig.Size = new System.Drawing.Size(1024, 671);
            this.tab_MtrConfig.TabIndex = 1;
            this.tab_MtrConfig.Text = "讯号";
            this.tab_MtrConfig.UseVisualStyleBackColor = true;
            // 
            // tab_MtrSpeed
            // 
            this.tab_MtrSpeed.Location = new System.Drawing.Point(4, 25);
            this.tab_MtrSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_MtrSpeed.Name = "tab_MtrSpeed";
            this.tab_MtrSpeed.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_MtrSpeed.Size = new System.Drawing.Size(1024, 671);
            this.tab_MtrSpeed.TabIndex = 2;
            this.tab_MtrSpeed.Text = "速度设定";
            this.tab_MtrSpeed.UseVisualStyleBackColor = true;
            // 
            // tab_MtrSafety
            // 
            this.tab_MtrSafety.Location = new System.Drawing.Point(4, 25);
            this.tab_MtrSafety.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_MtrSafety.Name = "tab_MtrSafety";
            this.tab_MtrSafety.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_MtrSafety.Size = new System.Drawing.Size(1024, 671);
            this.tab_MtrSafety.TabIndex = 3;
            this.tab_MtrSafety.Text = "安全防护";
            this.tab_MtrSafety.UseVisualStyleBackColor = true;
            // 
            // Form_Axis_Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 752);
            this.Controls.Add(this.tab_Control);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form_Axis_Configuration";
            this.Text = "Form_Axis_Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Axis_Configuration_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.tab_Control.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_Selector_Motor;
        private System.Windows.Forms.TabControl tab_Control;
        private System.Windows.Forms.TabPage tab_MtrTable;
        private System.Windows.Forms.TabPage tab_MtrConfig;
        private System.Windows.Forms.TabPage tab_MtrSpeed;
        private System.Windows.Forms.TabPage tab_MtrSafety;
        private System.Windows.Forms.Button btn_Save;
    }
}