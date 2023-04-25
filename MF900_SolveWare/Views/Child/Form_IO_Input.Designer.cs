namespace MF900_SolveWare.Views.Child
{
    partial class Form_IO_Input
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_Input_Name = new System.Windows.Forms.Label();
            this.txb_Status = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbl_Input_Name);
            this.panel1.Controls.Add(this.txb_Status);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 39);
            this.panel1.TabIndex = 0;
            // 
            // lbl_Input_Name
            // 
            this.lbl_Input_Name.AutoSize = true;
            this.lbl_Input_Name.Font = new System.Drawing.Font("宋体", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Input_Name.Location = new System.Drawing.Point(61, 8);
            this.lbl_Input_Name.Name = "lbl_Input_Name";
            this.lbl_Input_Name.Size = new System.Drawing.Size(179, 17);
            this.lbl_Input_Name.TabIndex = 3;
            this.lbl_Input_Name.Text = "Ip-XXXXXXXXXXXXXXXX";
            // 
            // txb_Status
            // 
            this.txb_Status.BackColor = System.Drawing.Color.White;
            this.txb_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_Status.Location = new System.Drawing.Point(28, 5);
            this.txb_Status.Name = "txb_Status";
            this.txb_Status.ReadOnly = true;
            this.txb_Status.Size = new System.Drawing.Size(27, 25);
            this.txb_Status.TabIndex = 2;
            // 
            // Form_IO_Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 42);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_IO_Input";
            this.Text = "Form_IO_Input";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_IO_Input_FormClosing);
            this.Load += new System.EventHandler(this.Form_IO_Input_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Input_Name;
        private System.Windows.Forms.TextBox txb_Status;
    }
}