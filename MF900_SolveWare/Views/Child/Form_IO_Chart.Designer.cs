namespace MF900_SolveWare.Views.Child
{
    partial class Form_IO_Chart
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
            this.gpb_Inputs = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gpb_Outputs = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // gpb_Inputs
            // 
            this.gpb_Inputs.Location = new System.Drawing.Point(12, 38);
            this.gpb_Inputs.Name = "gpb_Inputs";
            this.gpb_Inputs.Size = new System.Drawing.Size(500, 881);
            this.gpb_Inputs.TabIndex = 0;
            this.gpb_Inputs.TabStop = false;
            this.gpb_Inputs.Text = "输入";
            // 
            // gpb_Outputs
            // 
            this.gpb_Outputs.Location = new System.Drawing.Point(618, 38);
            this.gpb_Outputs.Name = "gpb_Outputs";
            this.gpb_Outputs.Size = new System.Drawing.Size(500, 881);
            this.gpb_Outputs.TabIndex = 1;
            this.gpb_Outputs.TabStop = false;
            this.gpb_Outputs.Text = "输出";
            // 
            // Form_IO_Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 953);
            this.Controls.Add(this.gpb_Outputs);
            this.Controls.Add(this.gpb_Inputs);
            this.Name = "Form_IO_Chart";
            this.Text = "Form_IO_Chart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_IO_Chart_FormClosing);
            this.Load += new System.EventHandler(this.Form_IO_Chart_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpb_Inputs;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox gpb_Outputs;
    }
}