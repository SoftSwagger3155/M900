namespace MF900_SolveWare.Views.Child
{
    partial class Form_InspectKit_Lighting
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
            this.cmb_Selector_Kind = new System.Windows.Forms.ComboBox();
            this.cmb_Selector_Item = new System.Windows.Forms.ComboBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.gpb_Content = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_TimeSpent = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.cmb_Selector_Item);
            this.groupBox1.Controls.Add(this.cmb_Selector_Kind);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "光源物件选择";
            // 
            // cmb_Selector_Kind
            // 
            this.cmb_Selector_Kind.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Kind.FormattingEnabled = true;
            this.cmb_Selector_Kind.Location = new System.Drawing.Point(6, 43);
            this.cmb_Selector_Kind.Name = "cmb_Selector_Kind";
            this.cmb_Selector_Kind.Size = new System.Drawing.Size(121, 28);
            this.cmb_Selector_Kind.TabIndex = 0;
            // 
            // cmb_Selector_Item
            // 
            this.cmb_Selector_Item.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Item.FormattingEnabled = true;
            this.cmb_Selector_Item.Location = new System.Drawing.Point(133, 43);
            this.cmb_Selector_Item.Name = "cmb_Selector_Item";
            this.cmb_Selector_Item.Size = new System.Drawing.Size(354, 28);
            this.cmb_Selector_Item.TabIndex = 1;
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(502, 38);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 36);
            this.btn_Add.TabIndex = 2;
            this.btn_Add.Text = "加入";
            this.btn_Add.UseVisualStyleBackColor = true;
            // 
            // gpb_Content
            // 
            this.gpb_Content.Location = new System.Drawing.Point(12, 120);
            this.gpb_Content.Name = "gpb_Content";
            this.gpb_Content.Size = new System.Drawing.Size(932, 450);
            this.gpb_Content.TabIndex = 1;
            this.gpb_Content.TabStop = false;
            this.gpb_Content.Text = "工作内容";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_TimeSpent,
            this.tssl_Status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 606);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(957, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_TimeSpent
            // 
            this.tssl_TimeSpent.Name = "tssl_TimeSpent";
            this.tssl_TimeSpent.Size = new System.Drawing.Size(88, 20);
            this.tssl_TimeSpent.Text = "耗时: 0.5秒 ";
            // 
            // tssl_Status
            // 
            this.tssl_Status.ActiveLinkColor = System.Drawing.SystemColors.Control;
            this.tssl_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tssl_Status.Name = "tssl_Status";
            this.tssl_Status.Size = new System.Drawing.Size(74, 20);
            this.tssl_Status.Text = "状态: Idle";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(854, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "执行";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form_InspectKit_Lighting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 632);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gpb_Content);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_InspectKit_Lighting";
            this.Text = "光源设定";
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.ComboBox cmb_Selector_Item;
        private System.Windows.Forms.ComboBox cmb_Selector_Kind;
        private System.Windows.Forms.GroupBox gpb_Content;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_TimeSpent;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Status;
        private System.Windows.Forms.Button button1;
    }
}