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
            this.btn_Home_All = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gpb_Content
            // 
            this.gpb_Content.Location = new System.Drawing.Point(9, 52);
            this.gpb_Content.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gpb_Content.Name = "gpb_Content";
            this.gpb_Content.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gpb_Content.Size = new System.Drawing.Size(1209, 513);
            this.gpb_Content.TabIndex = 0;
            this.gpb_Content.TabStop = false;
            this.gpb_Content.Text = "总览";
            // 
            // btn_Home_All
            // 
            this.btn_Home_All.Location = new System.Drawing.Point(147, 10);
            this.btn_Home_All.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Home_All.Name = "btn_Home_All";
            this.btn_Home_All.Size = new System.Drawing.Size(66, 38);
            this.btn_Home_All.TabIndex = 1;
            this.btn_Home_All.Text = "全复位";
            this.btn_Home_All.UseVisualStyleBackColor = true;
            this.btn_Home_All.Click += new System.EventHandler(this.btn_Home_All_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(76, 10);
            this.btn_Stop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(66, 38);
            this.btn_Stop.TabIndex = 2;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // Form_Axis_General_Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 581);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Home_All);
            this.Controls.Add(this.gpb_Content);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form_Axis_General_Controller";
            this.Text = "全马达控制器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Axis_General_Controller_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpb_Content;
        private System.Windows.Forms.Button btn_Home_All;
        private System.Windows.Forms.Button btn_Stop;
    }
}