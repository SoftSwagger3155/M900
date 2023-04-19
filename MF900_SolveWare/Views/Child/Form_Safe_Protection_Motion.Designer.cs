namespace MF900_SolveWare.Views.Child
{
    partial class Form_Safe_Protection_Motion
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
            this.btn_Go = new System.Windows.Forms.Button();
            this.btn_Update_Pos = new System.Windows.Forms.Button();
            this.txb_Pos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Selector_Motor = new System.Windows.Forms.ComboBox();
            this.txb_Priority = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckb_Selected = new System.Windows.Forms.CheckBox();
            this.btn_Manual_Update = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Manual_Update);
            this.panel1.Controls.Add(this.btn_Go);
            this.panel1.Controls.Add(this.btn_Update_Pos);
            this.panel1.Controls.Add(this.txb_Pos);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmb_Selector_Motor);
            this.panel1.Controls.Add(this.txb_Priority);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ckb_Selected);
            this.panel1.Location = new System.Drawing.Point(12, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 50);
            this.panel1.TabIndex = 4;
            // 
            // btn_Go
            // 
            this.btn_Go.Location = new System.Drawing.Point(699, 7);
            this.btn_Go.Name = "btn_Go";
            this.btn_Go.Size = new System.Drawing.Size(87, 34);
            this.btn_Go.TabIndex = 10;
            this.btn_Go.Text = "前往";
            this.btn_Go.UseVisualStyleBackColor = true;
            this.btn_Go.Click += new System.EventHandler(this.btn_Go_Click);
            // 
            // btn_Update_Pos
            // 
            this.btn_Update_Pos.Location = new System.Drawing.Point(885, 7);
            this.btn_Update_Pos.Name = "btn_Update_Pos";
            this.btn_Update_Pos.Size = new System.Drawing.Size(87, 34);
            this.btn_Update_Pos.TabIndex = 9;
            this.btn_Update_Pos.Text = "位置更新";
            this.btn_Update_Pos.UseVisualStyleBackColor = true;
            this.btn_Update_Pos.Click += new System.EventHandler(this.btn_Update_Pos_Click);
            // 
            // txb_Pos
            // 
            this.txb_Pos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_Pos.Location = new System.Drawing.Point(562, 11);
            this.txb_Pos.Name = "txb_Pos";
            this.txb_Pos.Size = new System.Drawing.Size(128, 25);
            this.txb_Pos.TabIndex = 7;
            this.txb_Pos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_Pos.TextChanged += new System.EventHandler(this.txb_Pos_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(495, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "位置/mm";
            // 
            // cmb_Selector_Motor
            // 
            this.cmb_Selector_Motor.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Motor.FormattingEnabled = true;
            this.cmb_Selector_Motor.Location = new System.Drawing.Point(219, 8);
            this.cmb_Selector_Motor.Name = "cmb_Selector_Motor";
            this.cmb_Selector_Motor.Size = new System.Drawing.Size(245, 28);
            this.cmb_Selector_Motor.TabIndex = 5;
            this.cmb_Selector_Motor.SelectionChangeCommitted += new System.EventHandler(this.cmb_Selector_Motor_SelectionChangeCommitted);
            // 
            // txb_Priority
            // 
            this.txb_Priority.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_Priority.Location = new System.Drawing.Point(129, 11);
            this.txb_Priority.Name = "txb_Priority";
            this.txb_Priority.Size = new System.Drawing.Size(68, 25);
            this.txb_Priority.TabIndex = 0;
            this.txb_Priority.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_Priority.TextChanged += new System.EventHandler(this.txb_Priority_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "顺序";
            // 
            // ckb_Selected
            // 
            this.ckb_Selected.AutoSize = true;
            this.ckb_Selected.Location = new System.Drawing.Point(15, 14);
            this.ckb_Selected.Name = "ckb_Selected";
            this.ckb_Selected.Size = new System.Drawing.Size(59, 19);
            this.ckb_Selected.TabIndex = 1;
            this.ckb_Selected.Text = "选择";
            this.ckb_Selected.UseVisualStyleBackColor = true;
            // 
            // btn_Manual_Update
            // 
            this.btn_Manual_Update.Location = new System.Drawing.Point(792, 6);
            this.btn_Manual_Update.Name = "btn_Manual_Update";
            this.btn_Manual_Update.Size = new System.Drawing.Size(87, 34);
            this.btn_Manual_Update.TabIndex = 11;
            this.btn_Manual_Update.Text = "手动更新";
            this.btn_Manual_Update.UseVisualStyleBackColor = true;
            this.btn_Manual_Update.Click += new System.EventHandler(this.btn_Manual_Update_Click);
            // 
            // Form_Safe_Protection_Motion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 60);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Safe_Protection_Motion";
            this.Text = "Form_Safe_Protection_Motion";
            this.Load += new System.EventHandler(this.Form_Safe_Protection_Motion_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txb_Pos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Selector_Motor;
        private System.Windows.Forms.TextBox txb_Priority;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckb_Selected;
        private System.Windows.Forms.Button btn_Go;
        private System.Windows.Forms.Button btn_Update_Pos;
        private System.Windows.Forms.Button btn_Manual_Update;
    }
}