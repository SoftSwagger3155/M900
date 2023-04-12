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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_Pos_Content = new System.Windows.Forms.DataGridView();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_Pos = new System.Windows.Forms.TextBox();
            this.cmb_Selector_Operand = new System.Windows.Forms.ComboBox();
            this.cmb_Selector_Motor = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_Selector_TriggerMode = new System.Windows.Forms.ComboBox();
            this.btn_IO_Add = new System.Windows.Forms.Button();
            this.btn_Delete_IO = new System.Windows.Forms.Button();
            this.cmb_Selector_IO = new System.Windows.Forms.ComboBox();
            this.cmb_Selector_IOType = new System.Windows.Forms.ComboBox();
            this.dgv_IO_Content = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Pos_Content)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IO_Content)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Pos_Content
            // 
            this.dgv_Pos_Content.AllowUserToAddRows = false;
            this.dgv_Pos_Content.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_Pos_Content.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Pos_Content.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Pos_Content.Location = new System.Drawing.Point(12, 119);
            this.dgv_Pos_Content.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgv_Pos_Content.Name = "dgv_Pos_Content";
            this.dgv_Pos_Content.RowHeadersWidth = 51;
            this.dgv_Pos_Content.RowTemplate.Height = 27;
            this.dgv_Pos_Content.Size = new System.Drawing.Size(920, 168);
            this.dgv_Pos_Content.TabIndex = 1;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(649, 30);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(920, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "𣷹加设定";
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(541, 30);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(100, 45);
            this.btn_Add.TabIndex = 4;
            this.btn_Add.Text = "加入";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
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
            // txb_Pos
            // 
            this.txb_Pos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_Pos.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_Pos.Location = new System.Drawing.Point(319, 36);
            this.txb_Pos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_Pos.Name = "txb_Pos";
            this.txb_Pos.Size = new System.Drawing.Size(101, 30);
            this.txb_Pos.TabIndex = 2;
            // 
            // cmb_Selector_Operand
            // 
            this.cmb_Selector_Operand.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Operand.FormattingEnabled = true;
            this.cmb_Selector_Operand.Location = new System.Drawing.Point(133, 38);
            this.cmb_Selector_Operand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Selector_Operand.Name = "cmb_Selector_Operand";
            this.cmb_Selector_Operand.Size = new System.Drawing.Size(121, 28);
            this.cmb_Selector_Operand.TabIndex = 1;
            // 
            // cmb_Selector_Motor
            // 
            this.cmb_Selector_Motor.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Motor.FormattingEnabled = true;
            this.cmb_Selector_Motor.Location = new System.Drawing.Point(5, 38);
            this.cmb_Selector_Motor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Selector_Motor.Name = "cmb_Selector_Motor";
            this.cmb_Selector_Motor.Size = new System.Drawing.Size(121, 28);
            this.cmb_Selector_Motor.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_Selector_TriggerMode);
            this.groupBox2.Controls.Add(this.btn_IO_Add);
            this.groupBox2.Controls.Add(this.btn_Delete_IO);
            this.groupBox2.Controls.Add(this.cmb_Selector_IO);
            this.groupBox2.Controls.Add(this.cmb_Selector_IOType);
            this.groupBox2.Location = new System.Drawing.Point(12, 298);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(920, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "𣷹加设定";
            // 
            // cmb_Selector_TriggerMode
            // 
            this.cmb_Selector_TriggerMode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_TriggerMode.FormattingEnabled = true;
            this.cmb_Selector_TriggerMode.Location = new System.Drawing.Point(437, 38);
            this.cmb_Selector_TriggerMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Selector_TriggerMode.Name = "cmb_Selector_TriggerMode";
            this.cmb_Selector_TriggerMode.Size = new System.Drawing.Size(87, 28);
            this.cmb_Selector_TriggerMode.TabIndex = 5;
            // 
            // btn_IO_Add
            // 
            this.btn_IO_Add.Location = new System.Drawing.Point(541, 30);
            this.btn_IO_Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_IO_Add.Name = "btn_IO_Add";
            this.btn_IO_Add.Size = new System.Drawing.Size(100, 45);
            this.btn_IO_Add.TabIndex = 4;
            this.btn_IO_Add.Text = "加入";
            this.btn_IO_Add.UseVisualStyleBackColor = true;
            this.btn_IO_Add.Click += new System.EventHandler(this.btn_IO_Add_Click);
            // 
            // btn_Delete_IO
            // 
            this.btn_Delete_IO.Location = new System.Drawing.Point(649, 30);
            this.btn_Delete_IO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Delete_IO.Name = "btn_Delete_IO";
            this.btn_Delete_IO.Size = new System.Drawing.Size(100, 45);
            this.btn_Delete_IO.TabIndex = 2;
            this.btn_Delete_IO.Text = "删除选择项";
            this.btn_Delete_IO.UseVisualStyleBackColor = true;
            this.btn_Delete_IO.Click += new System.EventHandler(this.btn_Delete_IO_Click);
            // 
            // cmb_Selector_IO
            // 
            this.cmb_Selector_IO.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_IO.FormattingEnabled = true;
            this.cmb_Selector_IO.Location = new System.Drawing.Point(133, 38);
            this.cmb_Selector_IO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Selector_IO.Name = "cmb_Selector_IO";
            this.cmb_Selector_IO.Size = new System.Drawing.Size(298, 28);
            this.cmb_Selector_IO.TabIndex = 1;
            // 
            // cmb_Selector_IOType
            // 
            this.cmb_Selector_IOType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_IOType.FormattingEnabled = true;
            this.cmb_Selector_IOType.Location = new System.Drawing.Point(5, 38);
            this.cmb_Selector_IOType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Selector_IOType.Name = "cmb_Selector_IOType";
            this.cmb_Selector_IOType.Size = new System.Drawing.Size(121, 28);
            this.cmb_Selector_IOType.TabIndex = 0;
            this.cmb_Selector_IOType.SelectionChangeCommitted += new System.EventHandler(this.cmb_Selector_IOType_SelectionChangeCommitted);
            // 
            // dgv_IO_Content
            // 
            this.dgv_IO_Content.AllowUserToAddRows = false;
            this.dgv_IO_Content.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv_IO_Content.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_IO_Content.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_IO_Content.Location = new System.Drawing.Point(12, 407);
            this.dgv_IO_Content.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgv_IO_Content.Name = "dgv_IO_Content";
            this.dgv_IO_Content.RowHeadersWidth = 51;
            this.dgv_IO_Content.RowTemplate.Height = 27;
            this.dgv_IO_Content.Size = new System.Drawing.Size(920, 168);
            this.dgv_IO_Content.TabIndex = 6;
            // 
            // Form_Axis_Configuration_Item_MtrSafe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 712);
            this.Controls.Add(this.dgv_IO_Content);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_Pos_Content);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form_Axis_Configuration_Item_MtrSafe";
            this.Text = "Form_Axis_Configuration_Item_MtrSafe";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Pos_Content)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IO_Content)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_Pos_Content;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Pos;
        private System.Windows.Forms.ComboBox cmb_Selector_Operand;
        private System.Windows.Forms.ComboBox cmb_Selector_Motor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_IO_Add;
        private System.Windows.Forms.Button btn_Delete_IO;
        private System.Windows.Forms.ComboBox cmb_Selector_IO;
        private System.Windows.Forms.ComboBox cmb_Selector_IOType;
        private System.Windows.Forms.ComboBox cmb_Selector_TriggerMode;
        private System.Windows.Forms.DataGridView dgv_IO_Content;
    }
}