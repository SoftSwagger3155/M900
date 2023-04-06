namespace MF900_SolveWare.Views.Child
{
    partial class Form_InspectKit_Lighting_Item_LightSource
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdo_On = new System.Windows.Forms.RadioButton();
            this.rdo_Off = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(21, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 20);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "选择";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(89, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "物件: IO";
            // 
            // rdo_On
            // 
            this.rdo_On.AutoSize = true;
            this.rdo_On.Location = new System.Drawing.Point(259, 8);
            this.rdo_On.Name = "rdo_On";
            this.rdo_On.Size = new System.Drawing.Size(67, 19);
            this.rdo_On.TabIndex = 2;
            this.rdo_On.TabStop = true;
            this.rdo_On.Text = "开/ON";
            this.rdo_On.UseVisualStyleBackColor = true;
            // 
            // rdo_Off
            // 
            this.rdo_Off.AutoSize = true;
            this.rdo_Off.Location = new System.Drawing.Point(389, 7);
            this.rdo_Off.Name = "rdo_Off";
            this.rdo_Off.Size = new System.Drawing.Size(75, 19);
            this.rdo_Off.TabIndex = 3;
            this.rdo_Off.TabStop = true;
            this.rdo_Off.Text = "关/OFF";
            this.rdo_Off.UseVisualStyleBackColor = true;
            // 
            // Form_InspectKit_Lighting_Item_LightSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 38);
            this.Controls.Add(this.rdo_Off);
            this.Controls.Add(this.rdo_On);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_InspectKit_Lighting_Item_LightSource";
            this.Text = "Form_InspectKit_Lighting_Item_LightSource";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdo_On;
        private System.Windows.Forms.RadioButton rdo_Off;
    }
}