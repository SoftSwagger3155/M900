namespace MF900_SolveWare.Views.AxisMesForm
{
    partial class Form_Axis_Configuration_Item_MtrSafe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_Content = new System.Windows.Forms.DataGridView();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_Selector_Motor = new System.Windows.Forms.ComboBox();
            this.cmb_Selector_Operand = new System.Windows.Forms.ComboBox();
            this.txb_Pos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Content)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_Content
            // 
            this.dgv_Content.AllowUserToAddRows = false;
            this.dgv_Content.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_Content.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Content.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Content.Location = new System.Drawing.Point(12, 148);
            this.dgv_Content.Name = "dgv_Content";
            this.dgv_Content.RowHeadersWidth = 51;
            this.dgv_Content.RowTemplate.Height = 27;
            this.dgv_Content.Size = new System.Drawing.Size(920, 361);
            this.dgv_Content.TabIndex = 1;
            this.dgv_Content.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Content_CellMouseClick);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(649, 30);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(100, 45);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "删除选择项";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.btn_Delete);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txb_Pos);
            this.groupBox1.Controls.Add(this.cmb_Selector_Operand);
            this.groupBox1.Controls.Add(this.cmb_Selector_Motor);
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(920, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "𣷹加设定";
            // 
            // cmb_Selector_Motor
            // 
            this.cmb_Selector_Motor.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Motor.FormattingEnabled = true;
            this.cmb_Selector_Motor.Location = new System.Drawing.Point(6, 38);
            this.cmb_Selector_Motor.Name = "cmb_Selector_Motor";
            this.cmb_Selector_Motor.Size = new System.Drawing.Size(121, 28);
            this.cmb_Selector_Motor.TabIndex = 0;
            // 
            // cmb_Selector_Operand
            // 
            this.cmb_Selector_Operand.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Operand.FormattingEnabled = true;
            this.cmb_Selector_Operand.Location = new System.Drawing.Point(133, 38);
            this.cmb_Selector_Operand.Name = "cmb_Selector_Operand";
            this.cmb_Selector_Operand.Size = new System.Drawing.Size(121, 28);
            this.cmb_Selector_Operand.TabIndex = 1;
            // 
            // txb_Pos
            // 
            this.txb_Pos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_Pos.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_Pos.Location = new System.Drawing.Point(319, 36);
            this.txb_Pos.Name = "txb_Pos";
            this.txb_Pos.Size = new System.Drawing.Size(100, 30);
            this.txb_Pos.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "位置";
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(541, 30);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(100, 45);
            this.btn_Add.TabIndex = 4;
            this.btn_Add.Text = "加入";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // Form_Axis_Configuration_Item_MtrSafe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_Content);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Axis_Configuration_Item_MtrSafe";
            this.Text = "Form_Axis_Configuration_Item_MtrSafe";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Content)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_Content;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Pos;
        private System.Windows.Forms.ComboBox cmb_Selector_Operand;
        private System.Windows.Forms.ComboBox cmb_Selector_Motor;
    }
}