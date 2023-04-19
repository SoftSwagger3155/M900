namespace MF900_SolveWare.Views.Child
{
    partial class Form_Safe_Protection_IO
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
            this.txb_Priority = new System.Windows.Forms.TextBox();
            this.ckb_Selected = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txb_DelayTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Selector_TriggerMode = new System.Windows.Forms.ComboBox();
            this.cmb_Selector_IOType = new System.Windows.Forms.ComboBox();
            this.cmb_Selector_IO = new System.Windows.Forms.ComboBox();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "顺序";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Execute);
            this.panel1.Controls.Add(this.txb_DelayTime);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmb_Selector_TriggerMode);
            this.panel1.Controls.Add(this.cmb_Selector_IOType);
            this.panel1.Controls.Add(this.cmb_Selector_IO);
            this.panel1.Controls.Add(this.txb_Priority);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ckb_Selected);
            this.panel1.Location = new System.Drawing.Point(12, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 50);
            this.panel1.TabIndex = 3;
            // 
            // txb_DelayTime
            // 
            this.txb_DelayTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_DelayTime.Location = new System.Drawing.Point(808, 11);
            this.txb_DelayTime.Name = "txb_DelayTime";
            this.txb_DelayTime.Size = new System.Drawing.Size(85, 25);
            this.txb_DelayTime.TabIndex = 7;
            this.txb_DelayTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txb_DelayTime.TextChanged += new System.EventHandler(this.txb_DelayTime_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(741, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "延迟/ms";
            // 
            // cmb_Selector_TriggerMode
            // 
            this.cmb_Selector_TriggerMode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_TriggerMode.FormattingEnabled = true;
            this.cmb_Selector_TriggerMode.Location = new System.Drawing.Point(603, 8);
            this.cmb_Selector_TriggerMode.Name = "cmb_Selector_TriggerMode";
            this.cmb_Selector_TriggerMode.Size = new System.Drawing.Size(121, 28);
            this.cmb_Selector_TriggerMode.TabIndex = 6;
            this.cmb_Selector_TriggerMode.SelectionChangeCommitted += new System.EventHandler(this.cmb_Selector_TriggerMode_SelectionChangeCommitted);
            // 
            // cmb_Selector_IOType
            // 
            this.cmb_Selector_IOType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_IOType.FormattingEnabled = true;
            this.cmb_Selector_IOType.Location = new System.Drawing.Point(219, 9);
            this.cmb_Selector_IOType.Name = "cmb_Selector_IOType";
            this.cmb_Selector_IOType.Size = new System.Drawing.Size(121, 28);
            this.cmb_Selector_IOType.TabIndex = 5;
            this.cmb_Selector_IOType.SelectionChangeCommitted += new System.EventHandler(this.cmb_Selector_IOType_SelectionChangeCommitted);
            // 
            // cmb_Selector_IO
            // 
            this.cmb_Selector_IO.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_IO.FormattingEnabled = true;
            this.cmb_Selector_IO.Location = new System.Drawing.Point(344, 9);
            this.cmb_Selector_IO.Name = "cmb_Selector_IO";
            this.cmb_Selector_IO.Size = new System.Drawing.Size(254, 28);
            this.cmb_Selector_IO.TabIndex = 4;
            this.cmb_Selector_IO.SelectionChangeCommitted += new System.EventHandler(this.cmb_Selector_IO_SelectionChangeCommitted);
            // 
            // btn_Execute
            // 
            this.btn_Execute.Location = new System.Drawing.Point(904, 8);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(75, 33);
            this.btn_Execute.TabIndex = 9;
            this.btn_Execute.Text = "执行";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // Form_Safe_Protection_IO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 60);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Safe_Protection_IO";
            this.Text = "Form_Safe_Protection_IO";
            this.Load += new System.EventHandler(this.Form_Safe_Protection_IO_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txb_Priority;
        private System.Windows.Forms.CheckBox ckb_Selected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txb_DelayTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Selector_TriggerMode;
        private System.Windows.Forms.ComboBox cmb_Selector_IOType;
        private System.Windows.Forms.ComboBox cmb_Selector_IO;
        private System.Windows.Forms.Button btn_Execute;
    }
}