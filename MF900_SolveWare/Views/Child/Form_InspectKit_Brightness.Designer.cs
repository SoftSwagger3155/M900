namespace MF900_SolveWare.Views.Child
{
    partial class Form_InspectKit_Brightness
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
            this.tBar_Gain = new System.Windows.Forms.TrackBar();
            this.lbl_Gain_Minimum = new System.Windows.Forms.Label();
            this.lbl_Gain_Maximum = new System.Windows.Forms.Label();
            this.gpb_Gain = new System.Windows.Forms.GroupBox();
            this.btn_Set_Gain = new System.Windows.Forms.Button();
            this.txb_Gain_Value = new System.Windows.Forms.TextBox();
            this.lbl_Gain_Value = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Set_Exposure = new System.Windows.Forms.Button();
            this.tBar_Exposure = new System.Windows.Forms.TrackBar();
            this.txb_Exposure_Value = new System.Windows.Forms.TextBox();
            this.lbl_Exposure_Value = new System.Windows.Forms.Label();
            this.lbl_Exposure_Maximum = new System.Windows.Forms.Label();
            this.lbl_Exposure_Minimum = new System.Windows.Forms.Label();
            this.gpb_Current_Brightness = new System.Windows.Forms.GroupBox();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.lbl_Current_Exposure = new System.Windows.Forms.Label();
            this.lbl_Current_Gain = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tBar_Gain)).BeginInit();
            this.gpb_Gain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBar_Exposure)).BeginInit();
            this.gpb_Current_Brightness.SuspendLayout();
            this.SuspendLayout();
            // 
            // tBar_Gain
            // 
            this.tBar_Gain.AutoSize = false;
            this.tBar_Gain.Location = new System.Drawing.Point(216, 41);
            this.tBar_Gain.Maximum = 100;
            this.tBar_Gain.Name = "tBar_Gain";
            this.tBar_Gain.Size = new System.Drawing.Size(512, 38);
            this.tBar_Gain.TabIndex = 0;
            this.tBar_Gain.ValueChanged += new System.EventHandler(this.tBar_Gain_ValueChanged);
            // 
            // lbl_Gain_Minimum
            // 
            this.lbl_Gain_Minimum.AutoSize = true;
            this.lbl_Gain_Minimum.Location = new System.Drawing.Point(223, 84);
            this.lbl_Gain_Minimum.Name = "lbl_Gain_Minimum";
            this.lbl_Gain_Minimum.Size = new System.Drawing.Size(76, 15);
            this.lbl_Gain_Minimum.TabIndex = 3;
            this.lbl_Gain_Minimum.Text = "最小值: 0";
            // 
            // lbl_Gain_Maximum
            // 
            this.lbl_Gain_Maximum.AutoSize = true;
            this.lbl_Gain_Maximum.Location = new System.Drawing.Point(626, 84);
            this.lbl_Gain_Maximum.Name = "lbl_Gain_Maximum";
            this.lbl_Gain_Maximum.Size = new System.Drawing.Size(92, 15);
            this.lbl_Gain_Maximum.TabIndex = 4;
            this.lbl_Gain_Maximum.Text = "最大值: 100";
            // 
            // gpb_Gain
            // 
            this.gpb_Gain.Controls.Add(this.label1);
            this.gpb_Gain.Controls.Add(this.btn_Set_Gain);
            this.gpb_Gain.Controls.Add(this.txb_Gain_Value);
            this.gpb_Gain.Controls.Add(this.lbl_Gain_Value);
            this.gpb_Gain.Controls.Add(this.tBar_Gain);
            this.gpb_Gain.Controls.Add(this.lbl_Gain_Maximum);
            this.gpb_Gain.Controls.Add(this.lbl_Gain_Minimum);
            this.gpb_Gain.Location = new System.Drawing.Point(52, 155);
            this.gpb_Gain.Name = "gpb_Gain";
            this.gpb_Gain.Size = new System.Drawing.Size(754, 135);
            this.gpb_Gain.TabIndex = 5;
            this.gpb_Gain.TabStop = false;
            this.gpb_Gain.Text = "增益";
            // 
            // btn_Set_Gain
            // 
            this.btn_Set_Gain.Location = new System.Drawing.Point(32, 100);
            this.btn_Set_Gain.Name = "btn_Set_Gain";
            this.btn_Set_Gain.Size = new System.Drawing.Size(75, 29);
            this.btn_Set_Gain.TabIndex = 7;
            this.btn_Set_Gain.Text = "设值";
            this.btn_Set_Gain.UseVisualStyleBackColor = true;
            this.btn_Set_Gain.Click += new System.EventHandler(this.btn_Set_Gain_Click);
            // 
            // txb_Gain_Value
            // 
            this.txb_Gain_Value.Location = new System.Drawing.Point(7, 69);
            this.txb_Gain_Value.Name = "txb_Gain_Value";
            this.txb_Gain_Value.Size = new System.Drawing.Size(100, 25);
            this.txb_Gain_Value.TabIndex = 6;
            this.txb_Gain_Value.Text = "1";
            this.txb_Gain_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Gain_Value
            // 
            this.lbl_Gain_Value.AutoSize = true;
            this.lbl_Gain_Value.Location = new System.Drawing.Point(72, 41);
            this.lbl_Gain_Value.Name = "lbl_Gain_Value";
            this.lbl_Gain_Value.Size = new System.Drawing.Size(15, 15);
            this.lbl_Gain_Value.TabIndex = 5;
            this.lbl_Gain_Value.Text = "1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_Set_Exposure);
            this.groupBox1.Controls.Add(this.tBar_Exposure);
            this.groupBox1.Controls.Add(this.txb_Exposure_Value);
            this.groupBox1.Controls.Add(this.lbl_Exposure_Value);
            this.groupBox1.Controls.Add(this.lbl_Exposure_Maximum);
            this.groupBox1.Controls.Add(this.lbl_Exposure_Minimum);
            this.groupBox1.Location = new System.Drawing.Point(52, 313);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(754, 135);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "曝光时间";
            // 
            // btn_Set_Exposure
            // 
            this.btn_Set_Exposure.Location = new System.Drawing.Point(34, 98);
            this.btn_Set_Exposure.Name = "btn_Set_Exposure";
            this.btn_Set_Exposure.Size = new System.Drawing.Size(75, 31);
            this.btn_Set_Exposure.TabIndex = 9;
            this.btn_Set_Exposure.Text = "设值";
            this.btn_Set_Exposure.UseVisualStyleBackColor = true;
            this.btn_Set_Exposure.Click += new System.EventHandler(this.btn_Set_Exposure_Click);
            // 
            // tBar_Exposure
            // 
            this.tBar_Exposure.AutoSize = false;
            this.tBar_Exposure.Location = new System.Drawing.Point(216, 32);
            this.tBar_Exposure.Maximum = 100;
            this.tBar_Exposure.Name = "tBar_Exposure";
            this.tBar_Exposure.Size = new System.Drawing.Size(512, 47);
            this.tBar_Exposure.TabIndex = 6;
            this.tBar_Exposure.ValueChanged += new System.EventHandler(this.tBar_Exposure_ValueChanged);
            // 
            // txb_Exposure_Value
            // 
            this.txb_Exposure_Value.Location = new System.Drawing.Point(9, 67);
            this.txb_Exposure_Value.Name = "txb_Exposure_Value";
            this.txb_Exposure_Value.Size = new System.Drawing.Size(100, 25);
            this.txb_Exposure_Value.TabIndex = 8;
            this.txb_Exposure_Value.Text = "1";
            this.txb_Exposure_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Exposure_Value
            // 
            this.lbl_Exposure_Value.AutoSize = true;
            this.lbl_Exposure_Value.Location = new System.Drawing.Point(72, 32);
            this.lbl_Exposure_Value.Name = "lbl_Exposure_Value";
            this.lbl_Exposure_Value.Size = new System.Drawing.Size(15, 15);
            this.lbl_Exposure_Value.TabIndex = 5;
            this.lbl_Exposure_Value.Text = "1";
            // 
            // lbl_Exposure_Maximum
            // 
            this.lbl_Exposure_Maximum.AutoSize = true;
            this.lbl_Exposure_Maximum.Location = new System.Drawing.Point(626, 82);
            this.lbl_Exposure_Maximum.Name = "lbl_Exposure_Maximum";
            this.lbl_Exposure_Maximum.Size = new System.Drawing.Size(92, 15);
            this.lbl_Exposure_Maximum.TabIndex = 4;
            this.lbl_Exposure_Maximum.Text = "最大值: 100";
            // 
            // lbl_Exposure_Minimum
            // 
            this.lbl_Exposure_Minimum.AutoSize = true;
            this.lbl_Exposure_Minimum.Location = new System.Drawing.Point(223, 82);
            this.lbl_Exposure_Minimum.Name = "lbl_Exposure_Minimum";
            this.lbl_Exposure_Minimum.Size = new System.Drawing.Size(76, 15);
            this.lbl_Exposure_Minimum.TabIndex = 3;
            this.lbl_Exposure_Minimum.Text = "最小值: 0";
            // 
            // gpb_Current_Brightness
            // 
            this.gpb_Current_Brightness.Controls.Add(this.btn_Update);
            this.gpb_Current_Brightness.Controls.Add(this.btn_Execute);
            this.gpb_Current_Brightness.Controls.Add(this.lbl_Current_Exposure);
            this.gpb_Current_Brightness.Controls.Add(this.lbl_Current_Gain);
            this.gpb_Current_Brightness.Location = new System.Drawing.Point(52, 12);
            this.gpb_Current_Brightness.Name = "gpb_Current_Brightness";
            this.gpb_Current_Brightness.Size = new System.Drawing.Size(393, 128);
            this.gpb_Current_Brightness.TabIndex = 8;
            this.gpb_Current_Brightness.TabStop = false;
            this.gpb_Current_Brightness.Text = "当前使用值";
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(286, 36);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(86, 35);
            this.btn_Update.TabIndex = 10;
            this.btn_Update.Text = "更新";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Execute
            // 
            this.btn_Execute.Location = new System.Drawing.Point(286, 77);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(86, 35);
            this.btn_Execute.TabIndex = 9;
            this.btn_Execute.Text = "执行";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // lbl_Current_Exposure
            // 
            this.lbl_Current_Exposure.AutoSize = true;
            this.lbl_Current_Exposure.Location = new System.Drawing.Point(12, 87);
            this.lbl_Current_Exposure.Name = "lbl_Current_Exposure";
            this.lbl_Current_Exposure.Size = new System.Drawing.Size(93, 15);
            this.lbl_Current_Exposure.TabIndex = 1;
            this.lbl_Current_Exposure.Text = "曝光 : 1000";
            // 
            // lbl_Current_Gain
            // 
            this.lbl_Current_Gain.AutoSize = true;
            this.lbl_Current_Gain.Location = new System.Drawing.Point(12, 44);
            this.lbl_Current_Gain.Name = "lbl_Current_Gain";
            this.lbl_Current_Gain.Size = new System.Drawing.Size(93, 15);
            this.lbl_Current_Gain.TabIndex = 0;
            this.lbl_Current_Gain.Text = "增益 : 1000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "数值: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "数值: ";
            // 
            // Form_InspectKit_Brightness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 476);
            this.Controls.Add(this.gpb_Current_Brightness);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpb_Gain);
            this.Name = "Form_InspectKit_Brightness";
            this.Text = "相机亮度设定";
            ((System.ComponentModel.ISupportInitialize)(this.tBar_Gain)).EndInit();
            this.gpb_Gain.ResumeLayout(false);
            this.gpb_Gain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBar_Exposure)).EndInit();
            this.gpb_Current_Brightness.ResumeLayout(false);
            this.gpb_Current_Brightness.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar tBar_Gain;
        private System.Windows.Forms.Label lbl_Gain_Minimum;
        private System.Windows.Forms.Label lbl_Gain_Maximum;
        private System.Windows.Forms.GroupBox gpb_Gain;
        private System.Windows.Forms.Label lbl_Gain_Value;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_Exposure_Value;
        private System.Windows.Forms.Label lbl_Exposure_Maximum;
        private System.Windows.Forms.Label lbl_Exposure_Minimum;
        private System.Windows.Forms.TrackBar tBar_Exposure;
        private System.Windows.Forms.TextBox txb_Gain_Value;
        private System.Windows.Forms.Button btn_Set_Gain;
        private System.Windows.Forms.Button btn_Set_Exposure;
        private System.Windows.Forms.TextBox txb_Exposure_Value;
        private System.Windows.Forms.GroupBox gpb_Current_Brightness;
        private System.Windows.Forms.Button btn_Execute;
        private System.Windows.Forms.Label lbl_Current_Exposure;
        private System.Windows.Forms.Label lbl_Current_Gain;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}