namespace MF900_SolveWare.Views.AxisMesForm
{
    partial class Form_Axis_General_Controller
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
            this.gpb_Content = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // gpb_Content
            // 
            this.gpb_Content.Location = new System.Drawing.Point(12, 12);
            this.gpb_Content.Name = "gpb_Content";
            this.gpb_Content.Size = new System.Drawing.Size(1358, 641);
            this.gpb_Content.TabIndex = 0;
            this.gpb_Content.TabStop = false;
            this.gpb_Content.Text = "总览";
            // 
            // Form_Axis_General_Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 665);
            this.Controls.Add(this.gpb_Content);
            this.Name = "Form_Axis_General_Controller";
            this.Text = "全马达控制器";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpb_Content;
    }
}