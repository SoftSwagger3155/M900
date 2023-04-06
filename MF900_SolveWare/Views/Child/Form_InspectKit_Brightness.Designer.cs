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
            this.lbl_Gain_Value = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_Exposure_Value = new System.Windows.Forms.Label();
            this.tBar_Exposure = new System.Windows.Forms.TrackBar();
            this.lbl_Exposure_Maximum = new System.Windows.Forms.Label();
            this.lbl_Exposure_Minimum = new System.Windows.Forms.Label();
            this.btn_Confirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tBar_Gain)).BeginInit();
            this.gpb_Gain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBar_Exposure)).BeginInit();
            this.SuspendLayout();
            // 
            // tBar_Gain
            // 
            this.tBar_Gain.AutoSize = false;
            this.tBar_Gain.Location = new System.Drawing.Point(79, 34);
            this.tBar_Gain.Maximum = 100;
            this.tBar_Gain.Name = "tBar_Gain";
            this.tBar_Gain.Size = new System.Drawing.Size(512, 38);
            this.tBar_Gain.TabIndex = 0;
            // 
            // lbl_Gain_Minimum
            // 
            this.lbl_Gain_Minimum.AutoSize = true;
            this.lbl_Gain_Minimum.Location = new System.Drawing.Point(73, 77);
            this.lbl_Gain_Minimum.Name = "lbl_Gain_Minimum";
            this.lbl_Gain_Minimum.Size = new System.Drawing.Size(76, 15);
            this.lbl_Gain_Minimum.TabIndex = 3;
            this.lbl_Gain_Minimum.Text = "最小值: 0";
            // 
            // lbl_Gain_Maximum
            // 
            this.lbl_Gain_Maximum.AutoSize = true;
            this.lbl_Gain_Maximum.Location = new System.Drawing.Point(526, 77);
            this.lbl_Gain_Maximum.Name = "lbl_Gain_Maximum";
            this.lbl_Gain_Maximum.Size = new System.Drawing.Size(92, 15);
            this.lbl_Gain_Maximum.TabIndex = 4;
            this.lbl_Gain_Maximum.Text = "最大值: 100";
            // 
            // gpb_Gain
            // 
            this.gpb_Gain.Controls.Add(this.lbl_Gain_Value);
            this.gpb_Gain.Controls.Add(this.tBar_Gain);
            this.gpb_Gain.Controls.Add(this.lbl_Gain_Maximum);
            this.gpb_Gain.Controls.Add(this.lbl_Gain_Minimum);
            this.gpb_Gain.Location = new System.Drawing.Point(56, 28);
            this.gpb_Gain.Name = "gpb_Gain";
            this.gpb_Gain.Size = new System.Drawing.Size(634, 118);
            this.gpb_Gain.TabIndex = 5;
            this.gpb_Gain.TabStop = false;
            this.gpb_Gain.Text = "增益";
            // 
            // lbl_Gain_Value
            // 
            this.lbl_Gain_Value.AutoSize = true;
            this.lbl_Gain_Value.Location = new System.Drawing.Point(6, 39);
            this.lbl_Gain_Value.Name = "lbl_Gain_Value";
            this.lbl_Gain_Value.Size = new System.Drawing.Size(61, 15);
            this.lbl_Gain_Value.TabIndex = 5;
            this.lbl_Gain_Value.Text = "数值: 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_Exposure_Value);
            this.groupBox1.Controls.Add(this.tBar_Exposure);
            this.groupBox1.Controls.Add(this.lbl_Exposure_Maximum);
            this.groupBox1.Controls.Add(this.lbl_Exposure_Minimum);
            this.groupBox1.Location = new System.Drawing.Point(58, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 118);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "曝光时间";
            // 
            // lbl_Exposure_Value
            // 
            this.lbl_Exposure_Value.AutoSize = true;
            this.lbl_Exposure_Value.Location = new System.Drawing.Point(6, 39);
            this.lbl_Exposure_Value.Name = "lbl_Exposure_Value";
            this.lbl_Exposure_Value.Size = new System.Drawing.Size(61, 15);
            this.lbl_Exposure_Value.TabIndex = 5;
            this.lbl_Exposure_Value.Text = "数值: 0";
            // 
            // tBar_Exposure
            // 
            this.tBar_Exposure.AutoSize = false;
            this.tBar_Exposure.Location = new System.Drawing.Point(79, 34);
            this.tBar_Exposure.Maximum = 100;
            this.tBar_Exposure.Name = "tBar_Exposure";
            this.tBar_Exposure.Size = new System.Drawing.Size(512, 38);
            this.tBar_Exposure.TabIndex = 0;
            // 
            // lbl_Exposure_Maximum
            // 
            this.lbl_Exposure_Maximum.AutoSize = true;
            this.lbl_Exposure_Maximum.Location = new System.Drawing.Point(526, 77);
            this.lbl_Exposure_Maximum.Name = "lbl_Exposure_Maximum";
            this.lbl_Exposure_Maximum.Size = new System.Drawing.Size(92, 15);
            this.lbl_Exposure_Maximum.TabIndex = 4;
            this.lbl_Exposure_Maximum.Text = "最大值: 100";
            // 
            // lbl_Exposure_Minimum
            // 
            this.lbl_Exposure_Minimum.AutoSize = true;
            this.lbl_Exposure_Minimum.Location = new System.Drawing.Point(73, 77);
            this.lbl_Exposure_Minimum.Name = "lbl_Exposure_Minimum";
            this.lbl_Exposure_Minimum.Size = new System.Drawing.Size(76, 15);
            this.lbl_Exposure_Minimum.TabIndex = 3;
            this.lbl_Exposure_Minimum.Text = "最小值: 0";
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.Location = new System.Drawing.Point(598, 352);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(91, 44);
            this.btn_Confirm.TabIndex = 7;
            this.btn_Confirm.Text = "确定";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            this.btn_Confirm.Click += new System.EventHandler(this.btn_Confirm_Click);
            // 
            // Form_InspectKit_Item_Brightness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Confirm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpb_Gain);
            this.Name = "Form_InspectKit_Item_Brightness";
            this.Text = "相机亮度设定";
            ((System.ComponentModel.ISupportInitialize)(this.tBar_Gain)).EndInit();
            this.gpb_Gain.ResumeLayout(false);
            this.gpb_Gain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBar_Exposure)).EndInit();
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
        private System.Windows.Forms.TrackBar tBar_Exposure;
        private System.Windows.Forms.Label lbl_Exposure_Maximum;
        private System.Windows.Forms.Label lbl_Exposure_Minimum;
        private System.Windows.Forms.Button btn_Confirm;
    }
}