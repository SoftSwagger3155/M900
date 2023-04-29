namespace MF900_SolveWare.Views.AxisMesForm
{
    partial class Form_Axis_Simple_Controller_Horizontal
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
            this.lbl_Motor_Name = new System.Windows.Forms.Label();
            this.lbl_Tag_CurrentPhysicalPos = new System.Windows.Forms.Label();
            this.btn_Jog_Positive = new System.Windows.Forms.Button();
            this.btn_Jog_Negative = new System.Windows.Forms.Button();
            this.txb_AbsolutePos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Go_Absolute = new System.Windows.Forms.Button();
            this.btn_Go_Relative_Positive = new System.Windows.Forms.Button();
            this.txb_RelativePos = new System.Windows.Forms.TextBox();
            this.lbl_Lmt_Negative = new System.Windows.Forms.Label();
            this.lbl_Lmt_Positive = new System.Windows.Forms.Label();
            this.ckb_Servo_Switch = new System.Windows.Forms.CheckBox();
            this.btn_Go_Relative_Negative = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_VelPctSelect = new System.Windows.Forms.ComboBox();
            this.ckb_Is_Jog_Monitoring = new System.Windows.Forms.CheckBox();
            this.lbl_CurrentPhysicalPos = new System.Windows.Forms.Label();
            this.btn_Home = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_RunVel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Motor_Name
            // 
            this.lbl_Motor_Name.AutoSize = true;
            this.lbl_Motor_Name.Location = new System.Drawing.Point(7, 16);
            this.lbl_Motor_Name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Motor_Name.Name = "lbl_Motor_Name";
            this.lbl_Motor_Name.Size = new System.Drawing.Size(53, 12);
            this.lbl_Motor_Name.TabIndex = 0;
            this.lbl_Motor_Name.Text = "上马达 X";
            // 
            // lbl_Tag_CurrentPhysicalPos
            // 
            this.lbl_Tag_CurrentPhysicalPos.AutoSize = true;
            this.lbl_Tag_CurrentPhysicalPos.Location = new System.Drawing.Point(66, 16);
            this.lbl_Tag_CurrentPhysicalPos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Tag_CurrentPhysicalPos.Name = "lbl_Tag_CurrentPhysicalPos";
            this.lbl_Tag_CurrentPhysicalPos.Size = new System.Drawing.Size(47, 12);
            this.lbl_Tag_CurrentPhysicalPos.TabIndex = 1;
            this.lbl_Tag_CurrentPhysicalPos.Text = "位置/mm";
            // 
            // btn_Jog_Positive
            // 
            this.btn_Jog_Positive.Location = new System.Drawing.Point(373, 10);
            this.btn_Jog_Positive.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Jog_Positive.Name = "btn_Jog_Positive";
            this.btn_Jog_Positive.Size = new System.Drawing.Size(56, 24);
            this.btn_Jog_Positive.TabIndex = 2;
            this.btn_Jog_Positive.Text = "Jog +";
            this.btn_Jog_Positive.UseVisualStyleBackColor = true;
            this.btn_Jog_Positive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Positive_MouseDown);
            this.btn_Jog_Positive.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Positive_MouseUp);
            // 
            // btn_Jog_Negative
            // 
            this.btn_Jog_Negative.Location = new System.Drawing.Point(432, 10);
            this.btn_Jog_Negative.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Jog_Negative.Name = "btn_Jog_Negative";
            this.btn_Jog_Negative.Size = new System.Drawing.Size(56, 24);
            this.btn_Jog_Negative.TabIndex = 3;
            this.btn_Jog_Negative.Text = "Jog -";
            this.btn_Jog_Negative.UseVisualStyleBackColor = true;
            this.btn_Jog_Negative.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Negative_MouseDown);
            this.btn_Jog_Negative.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Negative_MouseUp);
            // 
            // txb_AbsolutePos
            // 
            this.txb_AbsolutePos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_AbsolutePos.Location = new System.Drawing.Point(564, 12);
            this.txb_AbsolutePos.Margin = new System.Windows.Forms.Padding(2);
            this.txb_AbsolutePos.Name = "txb_AbsolutePos";
            this.txb_AbsolutePos.Size = new System.Drawing.Size(76, 21);
            this.txb_AbsolutePos.TabIndex = 4;
            this.txb_AbsolutePos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(492, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "绝对位置/mm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(693, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "相对位置/mm";
            // 
            // btn_Go_Absolute
            // 
            this.btn_Go_Absolute.Location = new System.Drawing.Point(644, 10);
            this.btn_Go_Absolute.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Go_Absolute.Name = "btn_Go_Absolute";
            this.btn_Go_Absolute.Size = new System.Drawing.Size(44, 24);
            this.btn_Go_Absolute.TabIndex = 7;
            this.btn_Go_Absolute.Text = "Go";
            this.btn_Go_Absolute.UseVisualStyleBackColor = true;
            this.btn_Go_Absolute.Click += new System.EventHandler(this.btn_Go_Absolute_Click);
            // 
            // btn_Go_Relative_Positive
            // 
            this.btn_Go_Relative_Positive.Location = new System.Drawing.Point(846, 10);
            this.btn_Go_Relative_Positive.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Go_Relative_Positive.Name = "btn_Go_Relative_Positive";
            this.btn_Go_Relative_Positive.Size = new System.Drawing.Size(44, 24);
            this.btn_Go_Relative_Positive.TabIndex = 9;
            this.btn_Go_Relative_Positive.Text = "Go +";
            this.btn_Go_Relative_Positive.UseVisualStyleBackColor = true;
            this.btn_Go_Relative_Positive.Click += new System.EventHandler(this.btn_Go_Relative_Positive_Click);
            // 
            // txb_RelativePos
            // 
            this.txb_RelativePos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_RelativePos.Location = new System.Drawing.Point(766, 12);
            this.txb_RelativePos.Margin = new System.Windows.Forms.Padding(2);
            this.txb_RelativePos.Name = "txb_RelativePos";
            this.txb_RelativePos.Size = new System.Drawing.Size(76, 21);
            this.txb_RelativePos.TabIndex = 8;
            this.txb_RelativePos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Lmt_Negative
            // 
            this.lbl_Lmt_Negative.AutoSize = true;
            this.lbl_Lmt_Negative.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_Lmt_Negative.Location = new System.Drawing.Point(1066, 6);
            this.lbl_Lmt_Negative.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Lmt_Negative.Name = "lbl_Lmt_Negative";
            this.lbl_Lmt_Negative.Size = new System.Drawing.Size(35, 12);
            this.lbl_Lmt_Negative.TabIndex = 10;
            this.lbl_Lmt_Negative.Text = "lmt -";
            // 
            // lbl_Lmt_Positive
            // 
            this.lbl_Lmt_Positive.AutoSize = true;
            this.lbl_Lmt_Positive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_Lmt_Positive.Location = new System.Drawing.Point(1066, 22);
            this.lbl_Lmt_Positive.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Lmt_Positive.Name = "lbl_Lmt_Positive";
            this.lbl_Lmt_Positive.Size = new System.Drawing.Size(35, 12);
            this.lbl_Lmt_Positive.TabIndex = 11;
            this.lbl_Lmt_Positive.Text = "lmt +";
            // 
            // ckb_Servo_Switch
            // 
            this.ckb_Servo_Switch.AutoSize = true;
            this.ckb_Servo_Switch.Location = new System.Drawing.Point(991, 14);
            this.ckb_Servo_Switch.Margin = new System.Windows.Forms.Padding(2);
            this.ckb_Servo_Switch.Name = "ckb_Servo_Switch";
            this.ckb_Servo_Switch.Size = new System.Drawing.Size(72, 16);
            this.ckb_Servo_Switch.TabIndex = 12;
            this.ckb_Servo_Switch.Text = "伺服开关";
            this.ckb_Servo_Switch.UseVisualStyleBackColor = true;
            // 
            // btn_Go_Relative_Negative
            // 
            this.btn_Go_Relative_Negative.Location = new System.Drawing.Point(892, 10);
            this.btn_Go_Relative_Negative.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Go_Relative_Negative.Name = "btn_Go_Relative_Negative";
            this.btn_Go_Relative_Negative.Size = new System.Drawing.Size(44, 24);
            this.btn_Go_Relative_Negative.TabIndex = 13;
            this.btn_Go_Relative_Negative.Text = "Go -";
            this.btn_Go_Relative_Negative.UseVisualStyleBackColor = true;
            this.btn_Go_Relative_Negative.Click += new System.EventHandler(this.btn_Go_Relative_Negative_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmb_VelPctSelect);
            this.panel1.Controls.Add(this.ckb_Is_Jog_Monitoring);
            this.panel1.Controls.Add(this.lbl_CurrentPhysicalPos);
            this.panel1.Controls.Add(this.btn_Home);
            this.panel1.Controls.Add(this.btn_Go_Absolute);
            this.panel1.Controls.Add(this.btn_Go_Relative_Negative);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl_Motor_Name);
            this.panel1.Controls.Add(this.ckb_Servo_Switch);
            this.panel1.Controls.Add(this.lbl_Tag_CurrentPhysicalPos);
            this.panel1.Controls.Add(this.lbl_Lmt_Positive);
            this.panel1.Controls.Add(this.btn_Jog_Positive);
            this.panel1.Controls.Add(this.lbl_Lmt_Negative);
            this.panel1.Controls.Add(this.btn_Jog_Negative);
            this.panel1.Controls.Add(this.btn_Go_Relative_Positive);
            this.panel1.Controls.Add(this.txb_AbsolutePos);
            this.panel1.Controls.Add(this.txb_RelativePos);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbl_RunVel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1182, 46);
            this.panel1.TabIndex = 14;
            // 
            // cmb_VelPctSelect
            // 
            this.cmb_VelPctSelect.FormattingEnabled = true;
            this.cmb_VelPctSelect.Items.AddRange(new object[] {
            "0.01",
            "0.1",
            "0.3",
            "0.5",
            "0.7",
            "0.8",
            "0.9",
            "1"});
            this.cmb_VelPctSelect.Location = new System.Drawing.Point(307, 12);
            this.cmb_VelPctSelect.Name = "cmb_VelPctSelect";
            this.cmb_VelPctSelect.Size = new System.Drawing.Size(48, 20);
            this.cmb_VelPctSelect.TabIndex = 17;
            // 
            // ckb_Is_Jog_Monitoring
            // 
            this.ckb_Is_Jog_Monitoring.AutoSize = true;
            this.ckb_Is_Jog_Monitoring.Location = new System.Drawing.Point(170, 14);
            this.ckb_Is_Jog_Monitoring.Margin = new System.Windows.Forms.Padding(2);
            this.ckb_Is_Jog_Monitoring.Name = "ckb_Is_Jog_Monitoring";
            this.ckb_Is_Jog_Monitoring.Size = new System.Drawing.Size(72, 16);
            this.ckb_Is_Jog_Monitoring.TabIndex = 16;
            this.ckb_Is_Jog_Monitoring.Text = "安全监视";
            this.ckb_Is_Jog_Monitoring.UseVisualStyleBackColor = true;
            this.ckb_Is_Jog_Monitoring.CheckedChanged += new System.EventHandler(this.ckb_Is_Jog_Monitoring_CheckedChanged);
            // 
            // lbl_CurrentPhysicalPos
            // 
            this.lbl_CurrentPhysicalPos.AutoSize = true;
            this.lbl_CurrentPhysicalPos.Location = new System.Drawing.Point(116, 16);
            this.lbl_CurrentPhysicalPos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_CurrentPhysicalPos.Name = "lbl_CurrentPhysicalPos";
            this.lbl_CurrentPhysicalPos.Size = new System.Drawing.Size(47, 12);
            this.lbl_CurrentPhysicalPos.TabIndex = 15;
            this.lbl_CurrentPhysicalPos.Text = "000.000";
            // 
            // btn_Home
            // 
            this.btn_Home.Location = new System.Drawing.Point(943, 10);
            this.btn_Home.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Home.Name = "btn_Home";
            this.btn_Home.Size = new System.Drawing.Size(44, 24);
            this.btn_Home.TabIndex = 14;
            this.btn_Home.Text = "复位";
            this.btn_Home.UseVisualStyleBackColor = true;
            this.btn_Home.Click += new System.EventHandler(this.btn_Home_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "速度比:";
            // 
            // lbl_RunVel
            // 
            this.lbl_RunVel.AutoSize = true;
            this.lbl_RunVel.Location = new System.Drawing.Point(1127, 22);
            this.lbl_RunVel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_RunVel.Name = "lbl_RunVel";
            this.lbl_RunVel.Size = new System.Drawing.Size(11, 12);
            this.lbl_RunVel.TabIndex = 6;
            this.lbl_RunVel.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1117, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "运行速度:";
            // 
            // Form_Axis_Simple_Controller_Horizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1182, 46);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_Axis_Simple_Controller_Horizontal";
            this.Text = "Form_Axis_Simple_Controller_Horizontal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Axis_Simple_Controller_Horizontal_FormClosing);
            this.Load += new System.EventHandler(this.Form_Axis_Simple_Controller_Horizontal_Load);
            this.Shown += new System.EventHandler(this.Form_Axis_Simple_Controller_Horizontal_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Motor_Name;
        private System.Windows.Forms.Label lbl_Tag_CurrentPhysicalPos;
        private System.Windows.Forms.Button btn_Jog_Positive;
        private System.Windows.Forms.Button btn_Jog_Negative;
        private System.Windows.Forms.TextBox txb_AbsolutePos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Go_Absolute;
        private System.Windows.Forms.Button btn_Go_Relative_Positive;
        private System.Windows.Forms.TextBox txb_RelativePos;
        private System.Windows.Forms.Label lbl_Lmt_Negative;
        private System.Windows.Forms.Label lbl_Lmt_Positive;
        private System.Windows.Forms.CheckBox ckb_Servo_Switch;
        private System.Windows.Forms.Button btn_Go_Relative_Negative;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Home;
        private System.Windows.Forms.Label lbl_CurrentPhysicalPos;
        private System.Windows.Forms.CheckBox ckb_Is_Jog_Monitoring;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_VelPctSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_RunVel;
    }
}