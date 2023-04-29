namespace MF900_SolveWare.Views.Child
{
    partial class Form_InspectKit_PatternMatch
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
            this.pGrid_PatternMatch = new System.Windows.Forms.PropertyGrid();
            this.gpb_Operation = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pGrid_Blob = new System.Windows.Forms.PropertyGrid();
            this.gpb_Operation.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pGrid_PatternMatch
            // 
            this.pGrid_PatternMatch.Location = new System.Drawing.Point(23, 30);
            this.pGrid_PatternMatch.Name = "pGrid_PatternMatch";
            this.pGrid_PatternMatch.Size = new System.Drawing.Size(560, 750);
            this.pGrid_PatternMatch.TabIndex = 0;
            // 
            // gpb_Operation
            // 
            this.gpb_Operation.Controls.Add(this.pGrid_Blob);
            this.gpb_Operation.Controls.Add(this.pGrid_PatternMatch);
            this.gpb_Operation.Location = new System.Drawing.Point(27, 20);
            this.gpb_Operation.Name = "gpb_Operation";
            this.gpb_Operation.Size = new System.Drawing.Size(1232, 817);
            this.gpb_Operation.TabIndex = 4;
            this.gpb_Operation.TabStop = false;
            this.gpb_Operation.Text = "参数";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gpb_Operation);
            this.panel1.Location = new System.Drawing.Point(24, -8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1282, 867);
            this.panel1.TabIndex = 5;
            // 
            // pGrid_Blob
            // 
            this.pGrid_Blob.Location = new System.Drawing.Point(624, 30);
            this.pGrid_Blob.Name = "pGrid_Blob";
            this.pGrid_Blob.Size = new System.Drawing.Size(560, 750);
            this.pGrid_Blob.TabIndex = 1;
            // 
            // Form_InspectKit_PatternMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1353, 1055);
            this.Controls.Add(this.panel1);
            this.Name = "Form_InspectKit_PatternMatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视觉模板";
            this.gpb_Operation.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pGrid_PatternMatch;
        private System.Windows.Forms.GroupBox gpb_Operation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PropertyGrid pGrid_Blob;
    }
}