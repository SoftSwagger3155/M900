namespace MF900_SolveWare.Views.Child
{
    partial class Form_IO_Output
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
            this.txb_Status = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txb_Status
            // 
            this.txb_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txb_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_Status.Location = new System.Drawing.Point(12, 6);
            this.txb_Status.Name = "txb_Status";
            this.txb_Status.ReadOnly = true;
            this.txb_Status.Size = new System.Drawing.Size(27, 25);
            this.txb_Status.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Execute);
            this.panel1.Controls.Add(this.txb_Status);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 39);
            this.panel1.TabIndex = 1;
            // 
            // btn_Execute
            // 
            this.btn_Execute.Location = new System.Drawing.Point(41, 4);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(220, 30);
            this.btn_Execute.TabIndex = 2;
            this.btn_Execute.Text = "OP-XXXXXXXXXXXXXXXX";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // Form_IO_Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 42);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_IO_Output";
            this.Text = "Form_IO_Output";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_IO_Output_FormClosing);
            this.Load += new System.EventHandler(this.Form_IO_Output_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txb_Status;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Execute;
    }
}