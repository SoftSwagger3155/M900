namespace MF900_SolveWare.Views.Child
{
    partial class Form_Safe_Protection
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.cmb_Selector_Item = new System.Windows.Forms.ComboBox();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.gpb_Content = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_TimeSpent = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.btn_Motor_Controller = new System.Windows.Forms.Button();
            this.btn_ReArrange = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.groupBox1.Controls.Add(this.btn_ReArrange);
            this.groupBox1.Controls.Add(this.btn_Motor_Controller);
            this.groupBox1.Controls.Add(this.btn_Execute);
            this.groupBox1.Controls.Add(this.cmb_Selector_Item);
            this.groupBox1.Controls.Add(this.btn_Delete);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Location = new System.Drawing.Point(23, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(915, 81);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "𣷹加设定";
            // 
            // btn_Execute
            // 
            this.btn_Execute.Location = new System.Drawing.Point(799, 22);
            this.btn_Execute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(100, 45);
            this.btn_Execute.TabIndex = 7;
            this.btn_Execute.Text = "执行";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // cmb_Selector_Item
            // 
            this.cmb_Selector_Item.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Item.FormattingEnabled = true;
            this.cmb_Selector_Item.Location = new System.Drawing.Point(11, 29);
            this.cmb_Selector_Item.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Selector_Item.Name = "cmb_Selector_Item";
            this.cmb_Selector_Item.Size = new System.Drawing.Size(156, 28);
            this.cmb_Selector_Item.TabIndex = 6;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(311, 21);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(100, 45);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "删除选择项";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(205, 21);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(100, 45);
            this.btn_Add.TabIndex = 4;
            this.btn_Add.Text = "加入";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // gpb_Content
            // 
            this.gpb_Content.Location = new System.Drawing.Point(23, 107);
            this.gpb_Content.Name = "gpb_Content";
            this.gpb_Content.Size = new System.Drawing.Size(1358, 500);
            this.gpb_Content.TabIndex = 6;
            this.gpb_Content.TabStop = false;
            this.gpb_Content.Text = "总览";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_TimeSpent,
            this.tssl_Status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 657);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1482, 26);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_TimeSpent
            // 
            this.tssl_TimeSpent.Name = "tssl_TimeSpent";
            this.tssl_TimeSpent.Size = new System.Drawing.Size(128, 20);
            this.tssl_TimeSpent.Text = "耗时 : 000.000 秒";
            // 
            // tssl_Status
            // 
            this.tssl_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tssl_Status.Name = "tssl_Status";
            this.tssl_Status.Size = new System.Drawing.Size(81, 20);
            this.tssl_Status.Text = "状态 : 成功";
            // 
            // btn_Motor_Controller
            // 
            this.btn_Motor_Controller.Location = new System.Drawing.Point(417, 21);
            this.btn_Motor_Controller.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Motor_Controller.Name = "btn_Motor_Controller";
            this.btn_Motor_Controller.Size = new System.Drawing.Size(116, 45);
            this.btn_Motor_Controller.TabIndex = 8;
            this.btn_Motor_Controller.Text = "马达控制器";
            this.btn_Motor_Controller.UseVisualStyleBackColor = true;
            this.btn_Motor_Controller.Click += new System.EventHandler(this.btn_Motor_Controller_Click);
            // 
            // btn_ReArrange
            // 
            this.btn_ReArrange.Location = new System.Drawing.Point(539, 22);
            this.btn_ReArrange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ReArrange.Name = "btn_ReArrange";
            this.btn_ReArrange.Size = new System.Drawing.Size(116, 45);
            this.btn_ReArrange.TabIndex = 9;
            this.btn_ReArrange.Text = "重新整理";
            this.btn_ReArrange.UseVisualStyleBackColor = true;
            this.btn_ReArrange.Click += new System.EventHandler(this.btn_ReArrange_Click);
            // 
            // Form_Safe_Protection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 683);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gpb_Content);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_Safe_Protection";
            this.Text = "安全措施";
            this.Load += new System.EventHandler(this.Form_Safe_Protection_Load);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.ComboBox cmb_Selector_Item;
        private System.Windows.Forms.Button btn_Execute;
        private System.Windows.Forms.GroupBox gpb_Content;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_TimeSpent;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Status;
        private System.Windows.Forms.Button btn_Motor_Controller;
        private System.Windows.Forms.Button btn_ReArrange;
    }
}