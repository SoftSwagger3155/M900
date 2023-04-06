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
            this.lbl_CurrentPhysicalPos = new System.Windows.Forms.Label();
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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Motor_Name
            // 
            this.lbl_Motor_Name.AutoSize = true;
            this.lbl_Motor_Name.Location = new System.Drawing.Point(9, 17);
            this.lbl_Motor_Name.Name = "lbl_Motor_Name";
            this.lbl_Motor_Name.Size = new System.Drawing.Size(68, 15);
            this.lbl_Motor_Name.TabIndex = 0;
            this.lbl_Motor_Name.Text = "上马达 X";
            // 
            // lbl_CurrentPhysicalPos
            // 
            this.lbl_CurrentPhysicalPos.AutoSize = true;
            this.lbl_CurrentPhysicalPos.Location = new System.Drawing.Point(96, 17);
            this.lbl_CurrentPhysicalPos.Name = "lbl_CurrentPhysicalPos";
            this.lbl_CurrentPhysicalPos.Size = new System.Drawing.Size(163, 15);
            this.lbl_CurrentPhysicalPos.TabIndex = 1;
            this.lbl_CurrentPhysicalPos.Text = "当前位置: 200.000 mm";
            // 
            // btn_Jog_Positive
            // 
            this.btn_Jog_Positive.Location = new System.Drawing.Point(296, 10);
            this.btn_Jog_Positive.Name = "btn_Jog_Positive";
            this.btn_Jog_Positive.Size = new System.Drawing.Size(75, 30);
            this.btn_Jog_Positive.TabIndex = 2;
            this.btn_Jog_Positive.Text = "Jog +";
            this.btn_Jog_Positive.UseVisualStyleBackColor = true;
            this.btn_Jog_Positive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Positive_MouseDown);
            this.btn_Jog_Positive.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Positive_MouseUp);
            // 
            // btn_Jog_Negative
            // 
            this.btn_Jog_Negative.Location = new System.Drawing.Point(375, 10);
            this.btn_Jog_Negative.Name = "btn_Jog_Negative";
            this.btn_Jog_Negative.Size = new System.Drawing.Size(75, 30);
            this.btn_Jog_Negative.TabIndex = 3;
            this.btn_Jog_Negative.Text = "Jog -";
            this.btn_Jog_Negative.UseVisualStyleBackColor = true;
            this.btn_Jog_Negative.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Negative_MouseDown);
            this.btn_Jog_Negative.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Negative_MouseUp);
            // 
            // txb_AbsolutePos
            // 
            this.txb_AbsolutePos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_AbsolutePos.Location = new System.Drawing.Point(565, 12);
            this.txb_AbsolutePos.Name = "txb_AbsolutePos";
            this.txb_AbsolutePos.Size = new System.Drawing.Size(100, 25);
            this.txb_AbsolutePos.TabIndex = 4;
            this.txb_AbsolutePos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(469, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "绝对位置/mm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(761, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "相对位置/mm";
            // 
            // btn_Go_Absolute
            // 
            this.btn_Go_Absolute.Location = new System.Drawing.Point(671, 9);
            this.btn_Go_Absolute.Name = "btn_Go_Absolute";
            this.btn_Go_Absolute.Size = new System.Drawing.Size(58, 30);
            this.btn_Go_Absolute.TabIndex = 7;
            this.btn_Go_Absolute.Text = "Go";
            this.btn_Go_Absolute.UseVisualStyleBackColor = true;
            this.btn_Go_Absolute.Click += new System.EventHandler(this.btn_Go_Absolute_Click);
            // 
            // btn_Go_Relative_Positive
            // 
            this.btn_Go_Relative_Positive.Location = new System.Drawing.Point(965, 10);
            this.btn_Go_Relative_Positive.Name = "btn_Go_Relative_Positive";
            this.btn_Go_Relative_Positive.Size = new System.Drawing.Size(58, 30);
            this.btn_Go_Relative_Positive.TabIndex = 9;
            this.btn_Go_Relative_Positive.Text = "Go +";
            this.btn_Go_Relative_Positive.UseVisualStyleBackColor = true;
            this.btn_Go_Relative_Positive.Click += new System.EventHandler(this.btn_Go_Relative_Positive_Click);
            // 
            // txb_RelativePos
            // 
            this.txb_RelativePos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_RelativePos.Location = new System.Drawing.Point(859, 13);
            this.txb_RelativePos.Name = "txb_RelativePos";
            this.txb_RelativePos.Size = new System.Drawing.Size(100, 25);
            this.txb_RelativePos.TabIndex = 8;
            this.txb_RelativePos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Lmt_Negative
            // 
            this.lbl_Lmt_Negative.AutoSize = true;
            this.lbl_Lmt_Negative.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_Lmt_Negative.Location = new System.Drawing.Point(1251, 7);
            this.lbl_Lmt_Negative.Name = "lbl_Lmt_Negative";
            this.lbl_Lmt_Negative.Size = new System.Drawing.Size(47, 15);
            this.lbl_Lmt_Negative.TabIndex = 10;
            this.lbl_Lmt_Negative.Text = "lmt -";
            // 
            // lbl_Lmt_Positive
            // 
            this.lbl_Lmt_Positive.AutoSize = true;
            this.lbl_Lmt_Positive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_Lmt_Positive.Location = new System.Drawing.Point(1251, 28);
            this.lbl_Lmt_Positive.Name = "lbl_Lmt_Positive";
            this.lbl_Lmt_Positive.Size = new System.Drawing.Size(47, 15);
            this.lbl_Lmt_Positive.TabIndex = 11;
            this.lbl_Lmt_Positive.Text = "lmt +";
            // 
            // ckb_Servo_Switch
            // 
            this.ckb_Servo_Switch.AutoSize = true;
            this.ckb_Servo_Switch.Location = new System.Drawing.Point(1135, 17);
            this.ckb_Servo_Switch.Name = "ckb_Servo_Switch";
            this.ckb_Servo_Switch.Size = new System.Drawing.Size(89, 19);
            this.ckb_Servo_Switch.TabIndex = 12;
            this.ckb_Servo_Switch.Text = "伺服开关";
            this.ckb_Servo_Switch.UseVisualStyleBackColor = true;
            // 
            // btn_Go_Relative_Negative
            // 
            this.btn_Go_Relative_Negative.Location = new System.Drawing.Point(1029, 10);
            this.btn_Go_Relative_Negative.Name = "btn_Go_Relative_Negative";
            this.btn_Go_Relative_Negative.Size = new System.Drawing.Size(58, 30);
            this.btn_Go_Relative_Negative.TabIndex = 13;
            this.btn_Go_Relative_Negative.Text = "Go -";
            this.btn_Go_Relative_Negative.UseVisualStyleBackColor = true;
            this.btn_Go_Relative_Negative.Click += new System.EventHandler(this.btn_Go_Relative_Negative_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Go_Absolute);
            this.panel1.Controls.Add(this.btn_Go_Relative_Negative);
            this.panel1.Controls.Add(this.lbl_Motor_Name);
            this.panel1.Controls.Add(this.ckb_Servo_Switch);
            this.panel1.Controls.Add(this.lbl_CurrentPhysicalPos);
            this.panel1.Controls.Add(this.lbl_Lmt_Positive);
            this.panel1.Controls.Add(this.btn_Jog_Positive);
            this.panel1.Controls.Add(this.lbl_Lmt_Negative);
            this.panel1.Controls.Add(this.btn_Jog_Negative);
            this.panel1.Controls.Add(this.btn_Go_Relative_Positive);
            this.panel1.Controls.Add(this.txb_AbsolutePos);
            this.panel1.Controls.Add(this.txb_RelativePos);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(6, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1317, 52);
            this.panel1.TabIndex = 14;
            // 
            // Form_Axis_Simple_Controller_Horizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1335, 67);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Axis_Simple_Controller_Horizontal";
            this.Text = "Form_Axis_Simple_Controller_Horizontal";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Motor_Name;
        private System.Windows.Forms.Label lbl_CurrentPhysicalPos;
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
    }
}