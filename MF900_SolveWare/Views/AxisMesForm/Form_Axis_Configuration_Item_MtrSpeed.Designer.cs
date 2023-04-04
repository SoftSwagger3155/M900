namespace MF900_SolveWare.Views.AxisMesForm
{
    partial class Form_Axis_Configuration_Item_MtrSpeed
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
            this.gpb_Selector = new System.Windows.Forms.GroupBox();
            this.btn_Copy = new System.Windows.Forms.Button();
            this.cmb_Selector_Copy = new System.Windows.Forms.ComboBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.cmb_Selector_SpeedSetting = new System.Windows.Forms.ComboBox();
            this.pGrid_Speed = new System.Windows.Forms.PropertyGrid();
            this.gpb_Controller = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Info_Motor = new System.Windows.Forms.Label();
            this.btn_Absolute = new System.Windows.Forms.Button();
            this.btn_Relative_Positive = new System.Windows.Forms.Button();
            this.btn_Relative_Negative = new System.Windows.Forms.Button();
            this.btn_Jog_Positive = new System.Windows.Forms.Button();
            this.btn_Jog_Negative = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_RelayCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_AverageTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.btn_Home = new System.Windows.Forms.Button();
            this.gpb_Selector.SuspendLayout();
            this.gpb_Controller.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpb_Selector
            // 
            this.gpb_Selector.Controls.Add(this.btn_Copy);
            this.gpb_Selector.Controls.Add(this.cmb_Selector_Copy);
            this.gpb_Selector.Controls.Add(this.btn_Save);
            this.gpb_Selector.Controls.Add(this.cmb_Selector_SpeedSetting);
            this.gpb_Selector.Location = new System.Drawing.Point(28, 58);
            this.gpb_Selector.Name = "gpb_Selector";
            this.gpb_Selector.Size = new System.Drawing.Size(611, 82);
            this.gpb_Selector.TabIndex = 0;
            this.gpb_Selector.TabStop = false;
            this.gpb_Selector.Text = "菜单选择";
            // 
            // btn_Copy
            // 
            this.btn_Copy.Location = new System.Drawing.Point(503, 25);
            this.btn_Copy.Name = "btn_Copy";
            this.btn_Copy.Size = new System.Drawing.Size(78, 35);
            this.btn_Copy.TabIndex = 3;
            this.btn_Copy.Text = "复制";
            this.btn_Copy.UseVisualStyleBackColor = true;
            // 
            // cmb_Selector_Copy
            // 
            this.cmb_Selector_Copy.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Copy.FormattingEnabled = true;
            this.cmb_Selector_Copy.Location = new System.Drawing.Point(310, 25);
            this.cmb_Selector_Copy.Name = "cmb_Selector_Copy";
            this.cmb_Selector_Copy.Size = new System.Drawing.Size(187, 28);
            this.cmb_Selector_Copy.TabIndex = 2;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(200, 25);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(78, 35);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "储存";
            this.btn_Save.UseVisualStyleBackColor = true;
            // 
            // cmb_Selector_SpeedSetting
            // 
            this.cmb_Selector_SpeedSetting.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_SpeedSetting.FormattingEnabled = true;
            this.cmb_Selector_SpeedSetting.Location = new System.Drawing.Point(7, 25);
            this.cmb_Selector_SpeedSetting.Name = "cmb_Selector_SpeedSetting";
            this.cmb_Selector_SpeedSetting.Size = new System.Drawing.Size(187, 28);
            this.cmb_Selector_SpeedSetting.TabIndex = 0;
            // 
            // pGrid_Speed
            // 
            this.pGrid_Speed.Location = new System.Drawing.Point(28, 157);
            this.pGrid_Speed.Name = "pGrid_Speed";
            this.pGrid_Speed.Size = new System.Drawing.Size(611, 369);
            this.pGrid_Speed.TabIndex = 2;
            // 
            // gpb_Controller
            // 
            this.gpb_Controller.Controls.Add(this.btn_Home);
            this.gpb_Controller.Controls.Add(this.textBox4);
            this.gpb_Controller.Controls.Add(this.label4);
            this.gpb_Controller.Controls.Add(this.textBox3);
            this.gpb_Controller.Controls.Add(this.label3);
            this.gpb_Controller.Controls.Add(this.button1);
            this.gpb_Controller.Controls.Add(this.textBox2);
            this.gpb_Controller.Controls.Add(this.label2);
            this.gpb_Controller.Controls.Add(this.textBox1);
            this.gpb_Controller.Controls.Add(this.label1);
            this.gpb_Controller.Controls.Add(this.lbl_Info_Motor);
            this.gpb_Controller.Controls.Add(this.btn_Absolute);
            this.gpb_Controller.Controls.Add(this.btn_Relative_Positive);
            this.gpb_Controller.Controls.Add(this.btn_Relative_Negative);
            this.gpb_Controller.Controls.Add(this.btn_Jog_Positive);
            this.gpb_Controller.Controls.Add(this.btn_Jog_Negative);
            this.gpb_Controller.Location = new System.Drawing.Point(653, 58);
            this.gpb_Controller.Name = "gpb_Controller";
            this.gpb_Controller.Size = new System.Drawing.Size(314, 468);
            this.gpb_Controller.TabIndex = 3;
            this.gpb_Controller.TabStop = false;
            this.gpb_Controller.Text = "控制器";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(169, 266);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 25);
            this.textBox2.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(28, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "绝对位置设定/ mm";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(169, 180);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(28, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "相对位置设定 / mm";
            // 
            // lbl_Info_Motor
            // 
            this.lbl_Info_Motor.AutoSize = true;
            this.lbl_Info_Motor.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Info_Motor.Location = new System.Drawing.Point(21, 37);
            this.lbl_Info_Motor.Name = "lbl_Info_Motor";
            this.lbl_Info_Motor.Size = new System.Drawing.Size(202, 23);
            this.lbl_Info_Motor.TabIndex = 5;
            this.lbl_Info_Motor.Text = "AxisName: Pos mm";
            // 
            // btn_Absolute
            // 
            this.btn_Absolute.Location = new System.Drawing.Point(23, 288);
            this.btn_Absolute.Name = "btn_Absolute";
            this.btn_Absolute.Size = new System.Drawing.Size(120, 40);
            this.btn_Absolute.TabIndex = 4;
            this.btn_Absolute.Text = "绝对位置";
            this.btn_Absolute.UseVisualStyleBackColor = true;
            // 
            // btn_Relative_Positive
            // 
            this.btn_Relative_Positive.Location = new System.Drawing.Point(149, 210);
            this.btn_Relative_Positive.Name = "btn_Relative_Positive";
            this.btn_Relative_Positive.Size = new System.Drawing.Size(120, 40);
            this.btn_Relative_Positive.TabIndex = 3;
            this.btn_Relative_Positive.Text = "相对位置 +";
            this.btn_Relative_Positive.UseVisualStyleBackColor = true;
            // 
            // btn_Relative_Negative
            // 
            this.btn_Relative_Negative.Location = new System.Drawing.Point(23, 210);
            this.btn_Relative_Negative.Name = "btn_Relative_Negative";
            this.btn_Relative_Negative.Size = new System.Drawing.Size(120, 40);
            this.btn_Relative_Negative.TabIndex = 2;
            this.btn_Relative_Negative.Text = "相对位置 -";
            this.btn_Relative_Negative.UseVisualStyleBackColor = true;
            // 
            // btn_Jog_Positive
            // 
            this.btn_Jog_Positive.Location = new System.Drawing.Point(149, 116);
            this.btn_Jog_Positive.Name = "btn_Jog_Positive";
            this.btn_Jog_Positive.Size = new System.Drawing.Size(120, 40);
            this.btn_Jog_Positive.TabIndex = 1;
            this.btn_Jog_Positive.Text = "Jog +";
            this.btn_Jog_Positive.UseVisualStyleBackColor = true;
            // 
            // btn_Jog_Negative
            // 
            this.btn_Jog_Negative.Location = new System.Drawing.Point(23, 116);
            this.btn_Jog_Negative.Name = "btn_Jog_Negative";
            this.btn_Jog_Negative.Size = new System.Drawing.Size(120, 40);
            this.btn_Jog_Negative.TabIndex = 0;
            this.btn_Jog_Negative.Text = "Jog -";
            this.btn_Jog_Negative.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 40);
            this.button1.TabIndex = 10;
            this.button1.Text = "点对点位移";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(169, 383);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 25);
            this.textBox3.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(28, 390);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "来回次数";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Location = new System.Drawing.Point(169, 354);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 25);
            this.textBox4.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(28, 361);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "距离设定 / mm";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_RelayCount,
            this.tssl_AverageTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 574);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1238, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_RelayCount
            // 
            this.tssl_RelayCount.Name = "tssl_RelayCount";
            this.tssl_RelayCount.Size = new System.Drawing.Size(101, 20);
            this.tssl_RelayCount.Text = "来回次数: 0次";
            // 
            // tssl_AverageTime
            // 
            this.tssl_AverageTime.Name = "tssl_AverageTime";
            this.tssl_AverageTime.Size = new System.Drawing.Size(90, 20);
            this.tssl_AverageTime.Text = "平均时间/秒";
            // 
            // btn_Home
            // 
            this.btn_Home.Location = new System.Drawing.Point(23, 70);
            this.btn_Home.Name = "btn_Home";
            this.btn_Home.Size = new System.Drawing.Size(120, 40);
            this.btn_Home.TabIndex = 15;
            this.btn_Home.Text = "复位";
            this.btn_Home.UseVisualStyleBackColor = true;
            // 
            // Form_Axis_Configuration_Item_MtrSpeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 600);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gpb_Controller);
            this.Controls.Add(this.pGrid_Speed);
            this.Controls.Add(this.gpb_Selector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Axis_Configuration_Item_MtrSpeed";
            this.Text = "Form_Axis_Configuration_Item_MtrSpeed";
            this.gpb_Selector.ResumeLayout(false);
            this.gpb_Controller.ResumeLayout(false);
            this.gpb_Controller.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpb_Selector;
        private System.Windows.Forms.Button btn_Copy;
        private System.Windows.Forms.ComboBox cmb_Selector_Copy;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.ComboBox cmb_Selector_SpeedSetting;
        private System.Windows.Forms.PropertyGrid pGrid_Speed;
        private System.Windows.Forms.GroupBox gpb_Controller;
        private System.Windows.Forms.Button btn_Relative_Positive;
        private System.Windows.Forms.Button btn_Relative_Negative;
        private System.Windows.Forms.Button btn_Jog_Positive;
        private System.Windows.Forms.Button btn_Jog_Negative;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Info_Motor;
        private System.Windows.Forms.Button btn_Absolute;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_RelayCount;
        private System.Windows.Forms.ToolStripStatusLabel tssl_AverageTime;
        private System.Windows.Forms.Button btn_Home;
    }
}