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
            this.lbl_Copy = new System.Windows.Forms.Label();
            this.lbl_Main = new System.Windows.Forms.Label();
            this.btn_Copy = new System.Windows.Forms.Button();
            this.cmb_Selector_Copy = new System.Windows.Forms.ComboBox();
            this.cmb_Selector_SpeedSetting = new System.Windows.Forms.ComboBox();
            this.pGrid_Speed = new System.Windows.Forms.PropertyGrid();
            this.gpb_Controller = new System.Windows.Forms.GroupBox();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_SetZero = new System.Windows.Forms.Button();
            this.btn_Home = new System.Windows.Forms.Button();
            this.txb_RelayGap = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txb_RelayCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Relay = new System.Windows.Forms.Button();
            this.txb_AbsolutePos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_RelativePos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Absolute = new System.Windows.Forms.Button();
            this.btn_Relative_Positive = new System.Windows.Forms.Button();
            this.btn_Relative_Negative = new System.Windows.Forms.Button();
            this.btn_Jog_Positive = new System.Windows.Forms.Button();
            this.btn_Jog_Negative = new System.Windows.Forms.Button();
            this.lbl_TestPos = new System.Windows.Forms.Label();
            this.lbl_Tag_CurrentPos = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_TimeSpent = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_RelayCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.lbl_Servo = new System.Windows.Forms.Label();
            this.btn_Enable_Servo = new System.Windows.Forms.Button();
            this.btn_Disable_Servo = new System.Windows.Forms.Button();
            this.lbl_Org = new System.Windows.Forms.Label();
            this.gpb_Selector.SuspendLayout();
            this.gpb_Controller.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpb_Selector
            // 
            this.gpb_Selector.Controls.Add(this.lbl_Copy);
            this.gpb_Selector.Controls.Add(this.lbl_Main);
            this.gpb_Selector.Controls.Add(this.btn_Copy);
            this.gpb_Selector.Controls.Add(this.cmb_Selector_Copy);
            this.gpb_Selector.Controls.Add(this.cmb_Selector_SpeedSetting);
            this.gpb_Selector.Location = new System.Drawing.Point(28, 29);
            this.gpb_Selector.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpb_Selector.Name = "gpb_Selector";
            this.gpb_Selector.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpb_Selector.Size = new System.Drawing.Size(611, 82);
            this.gpb_Selector.TabIndex = 0;
            this.gpb_Selector.TabStop = false;
            this.gpb_Selector.Text = "菜单选择";
            // 
            // lbl_Copy
            // 
            this.lbl_Copy.AutoSize = true;
            this.lbl_Copy.Location = new System.Drawing.Point(307, 21);
            this.lbl_Copy.Name = "lbl_Copy";
            this.lbl_Copy.Size = new System.Drawing.Size(52, 15);
            this.lbl_Copy.TabIndex = 5;
            this.lbl_Copy.Text = "复制项";
            // 
            // lbl_Main
            // 
            this.lbl_Main.AutoSize = true;
            this.lbl_Main.Location = new System.Drawing.Point(11, 24);
            this.lbl_Main.Name = "lbl_Main";
            this.lbl_Main.Size = new System.Drawing.Size(52, 15);
            this.lbl_Main.TabIndex = 4;
            this.lbl_Main.Text = "使用项";
            // 
            // btn_Copy
            // 
            this.btn_Copy.Location = new System.Drawing.Point(503, 39);
            this.btn_Copy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Copy.Name = "btn_Copy";
            this.btn_Copy.Size = new System.Drawing.Size(77, 35);
            this.btn_Copy.TabIndex = 3;
            this.btn_Copy.Text = "复制";
            this.btn_Copy.UseVisualStyleBackColor = true;
            this.btn_Copy.Click += new System.EventHandler(this.btn_Copy_Click);
            // 
            // cmb_Selector_Copy
            // 
            this.cmb_Selector_Copy.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_Copy.FormattingEnabled = true;
            this.cmb_Selector_Copy.Location = new System.Drawing.Point(309, 39);
            this.cmb_Selector_Copy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Selector_Copy.Name = "cmb_Selector_Copy";
            this.cmb_Selector_Copy.Size = new System.Drawing.Size(187, 28);
            this.cmb_Selector_Copy.TabIndex = 2;
            // 
            // cmb_Selector_SpeedSetting
            // 
            this.cmb_Selector_SpeedSetting.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_SpeedSetting.FormattingEnabled = true;
            this.cmb_Selector_SpeedSetting.Location = new System.Drawing.Point(13, 42);
            this.cmb_Selector_SpeedSetting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Selector_SpeedSetting.Name = "cmb_Selector_SpeedSetting";
            this.cmb_Selector_SpeedSetting.Size = new System.Drawing.Size(187, 28);
            this.cmb_Selector_SpeedSetting.TabIndex = 0;
            // 
            // pGrid_Speed
            // 
            this.pGrid_Speed.Location = new System.Drawing.Point(28, 115);
            this.pGrid_Speed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pGrid_Speed.Name = "pGrid_Speed";
            this.pGrid_Speed.Size = new System.Drawing.Size(611, 412);
            this.pGrid_Speed.TabIndex = 2;
            // 
            // gpb_Controller
            // 
            this.gpb_Controller.Controls.Add(this.btn_Disable_Servo);
            this.gpb_Controller.Controls.Add(this.btn_Enable_Servo);
            this.gpb_Controller.Controls.Add(this.btn_Stop);
            this.gpb_Controller.Controls.Add(this.btn_SetZero);
            this.gpb_Controller.Controls.Add(this.btn_Home);
            this.gpb_Controller.Controls.Add(this.txb_RelayGap);
            this.gpb_Controller.Controls.Add(this.label4);
            this.gpb_Controller.Controls.Add(this.txb_RelayCount);
            this.gpb_Controller.Controls.Add(this.label3);
            this.gpb_Controller.Controls.Add(this.btn_Relay);
            this.gpb_Controller.Controls.Add(this.txb_AbsolutePos);
            this.gpb_Controller.Controls.Add(this.label2);
            this.gpb_Controller.Controls.Add(this.txb_RelativePos);
            this.gpb_Controller.Controls.Add(this.label1);
            this.gpb_Controller.Controls.Add(this.btn_Absolute);
            this.gpb_Controller.Controls.Add(this.btn_Relative_Positive);
            this.gpb_Controller.Controls.Add(this.btn_Relative_Negative);
            this.gpb_Controller.Controls.Add(this.btn_Jog_Positive);
            this.gpb_Controller.Controls.Add(this.btn_Jog_Negative);
            this.gpb_Controller.Location = new System.Drawing.Point(653, 29);
            this.gpb_Controller.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpb_Controller.Name = "gpb_Controller";
            this.gpb_Controller.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpb_Controller.Size = new System.Drawing.Size(315, 497);
            this.gpb_Controller.TabIndex = 3;
            this.gpb_Controller.TabStop = false;
            this.gpb_Controller.Text = "控制器";
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(23, 166);
            this.btn_Stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(120, 40);
            this.btn_Stop.TabIndex = 17;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_SetZero
            // 
            this.btn_SetZero.Location = new System.Drawing.Point(149, 74);
            this.btn_SetZero.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_SetZero.Name = "btn_SetZero";
            this.btn_SetZero.Size = new System.Drawing.Size(120, 40);
            this.btn_SetZero.TabIndex = 16;
            this.btn_SetZero.Text = "标定原点";
            this.btn_SetZero.UseVisualStyleBackColor = true;
            this.btn_SetZero.Click += new System.EventHandler(this.btn_SetZero_Click);
            // 
            // btn_Home
            // 
            this.btn_Home.Location = new System.Drawing.Point(23, 74);
            this.btn_Home.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Home.Name = "btn_Home";
            this.btn_Home.Size = new System.Drawing.Size(120, 40);
            this.btn_Home.TabIndex = 15;
            this.btn_Home.Text = "复位";
            this.btn_Home.UseVisualStyleBackColor = true;
            this.btn_Home.Click += new System.EventHandler(this.btn_Home_Click);
            // 
            // txb_RelayGap
            // 
            this.txb_RelayGap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_RelayGap.Location = new System.Drawing.Point(169, 392);
            this.txb_RelayGap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_RelayGap.Name = "txb_RelayGap";
            this.txb_RelayGap.Size = new System.Drawing.Size(101, 25);
            this.txb_RelayGap.TabIndex = 14;
            this.txb_RelayGap.Text = "1";
            this.txb_RelayGap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(28, 399);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "距离设定 / mm";
            // 
            // txb_RelayCount
            // 
            this.txb_RelayCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_RelayCount.Location = new System.Drawing.Point(169, 420);
            this.txb_RelayCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_RelayCount.Name = "txb_RelayCount";
            this.txb_RelayCount.Size = new System.Drawing.Size(101, 25);
            this.txb_RelayCount.TabIndex = 12;
            this.txb_RelayCount.Text = "1";
            this.txb_RelayCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(28, 428);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "来回次数";
            // 
            // btn_Relay
            // 
            this.btn_Relay.Location = new System.Drawing.Point(23, 443);
            this.btn_Relay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Relay.Name = "btn_Relay";
            this.btn_Relay.Size = new System.Drawing.Size(120, 40);
            this.btn_Relay.TabIndex = 10;
            this.btn_Relay.Text = "点对点位移";
            this.btn_Relay.UseVisualStyleBackColor = true;
            this.btn_Relay.Click += new System.EventHandler(this.btn_Relay_Click);
            // 
            // txb_AbsolutePos
            // 
            this.txb_AbsolutePos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_AbsolutePos.Location = new System.Drawing.Point(169, 304);
            this.txb_AbsolutePos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_AbsolutePos.Name = "txb_AbsolutePos";
            this.txb_AbsolutePos.Size = new System.Drawing.Size(101, 25);
            this.txb_AbsolutePos.TabIndex = 9;
            this.txb_AbsolutePos.Text = "0";
            this.txb_AbsolutePos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(28, 310);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "绝对位置设定/ mm";
            // 
            // txb_RelativePos
            // 
            this.txb_RelativePos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_RelativePos.Location = new System.Drawing.Point(169, 218);
            this.txb_RelativePos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_RelativePos.Name = "txb_RelativePos";
            this.txb_RelativePos.Size = new System.Drawing.Size(101, 25);
            this.txb_RelativePos.TabIndex = 7;
            this.txb_RelativePos.Text = "1";
            this.txb_RelativePos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(28, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "相对位置设定 / mm";
            // 
            // btn_Absolute
            // 
            this.btn_Absolute.Location = new System.Drawing.Point(23, 326);
            this.btn_Absolute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Absolute.Name = "btn_Absolute";
            this.btn_Absolute.Size = new System.Drawing.Size(120, 40);
            this.btn_Absolute.TabIndex = 4;
            this.btn_Absolute.Text = "绝对位置";
            this.btn_Absolute.UseVisualStyleBackColor = true;
            this.btn_Absolute.Click += new System.EventHandler(this.btn_Absolute_Click);
            // 
            // btn_Relative_Positive
            // 
            this.btn_Relative_Positive.Location = new System.Drawing.Point(149, 248);
            this.btn_Relative_Positive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Relative_Positive.Name = "btn_Relative_Positive";
            this.btn_Relative_Positive.Size = new System.Drawing.Size(120, 40);
            this.btn_Relative_Positive.TabIndex = 3;
            this.btn_Relative_Positive.Text = "相对位置 +";
            this.btn_Relative_Positive.UseVisualStyleBackColor = true;
            this.btn_Relative_Positive.Click += new System.EventHandler(this.btn_Relative_Positive_Click);
            // 
            // btn_Relative_Negative
            // 
            this.btn_Relative_Negative.Location = new System.Drawing.Point(23, 248);
            this.btn_Relative_Negative.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Relative_Negative.Name = "btn_Relative_Negative";
            this.btn_Relative_Negative.Size = new System.Drawing.Size(120, 40);
            this.btn_Relative_Negative.TabIndex = 2;
            this.btn_Relative_Negative.Text = "相对位置 -";
            this.btn_Relative_Negative.UseVisualStyleBackColor = true;
            this.btn_Relative_Negative.Click += new System.EventHandler(this.btn_Relative_Negative_Click);
            // 
            // btn_Jog_Positive
            // 
            this.btn_Jog_Positive.Location = new System.Drawing.Point(149, 120);
            this.btn_Jog_Positive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Jog_Positive.Name = "btn_Jog_Positive";
            this.btn_Jog_Positive.Size = new System.Drawing.Size(120, 40);
            this.btn_Jog_Positive.TabIndex = 1;
            this.btn_Jog_Positive.Text = "Jog +";
            this.btn_Jog_Positive.UseVisualStyleBackColor = true;
            this.btn_Jog_Positive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Positive_MouseDown);
            this.btn_Jog_Positive.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Positive_MouseUp);
            // 
            // btn_Jog_Negative
            // 
            this.btn_Jog_Negative.Location = new System.Drawing.Point(23, 120);
            this.btn_Jog_Negative.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Jog_Negative.Name = "btn_Jog_Negative";
            this.btn_Jog_Negative.Size = new System.Drawing.Size(120, 40);
            this.btn_Jog_Negative.TabIndex = 0;
            this.btn_Jog_Negative.Text = "Jog -";
            this.btn_Jog_Negative.UseVisualStyleBackColor = true;
            this.btn_Jog_Negative.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Negative_MouseDown);
            this.btn_Jog_Negative.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Jog_Negative_MouseUp);
            // 
            // lbl_TestPos
            // 
            this.lbl_TestPos.AutoSize = true;
            this.lbl_TestPos.Location = new System.Drawing.Point(101, 574);
            this.lbl_TestPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TestPos.Name = "lbl_TestPos";
            this.lbl_TestPos.Size = new System.Drawing.Size(71, 15);
            this.lbl_TestPos.TabIndex = 5;
            this.lbl_TestPos.Text = "-999.999";
            // 
            // lbl_Tag_CurrentPos
            // 
            this.lbl_Tag_CurrentPos.AutoSize = true;
            this.lbl_Tag_CurrentPos.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Tag_CurrentPos.Location = new System.Drawing.Point(25, 574);
            this.lbl_Tag_CurrentPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Tag_CurrentPos.Name = "lbl_Tag_CurrentPos";
            this.lbl_Tag_CurrentPos.Size = new System.Drawing.Size(69, 15);
            this.lbl_Tag_CurrentPos.TabIndex = 6;
            this.lbl_Tag_CurrentPos.Text = "位置(mm)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(200, 574);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "耗时(秒)";
            // 
            // lbl_TimeSpent
            // 
            this.lbl_TimeSpent.AutoSize = true;
            this.lbl_TimeSpent.Location = new System.Drawing.Point(276, 574);
            this.lbl_TimeSpent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TimeSpent.Name = "lbl_TimeSpent";
            this.lbl_TimeSpent.Size = new System.Drawing.Size(47, 15);
            this.lbl_TimeSpent.TabIndex = 7;
            this.lbl_TimeSpent.Text = "0.001";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(347, 574);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "来回次数";
            // 
            // lbl_RelayCount
            // 
            this.lbl_RelayCount.AutoSize = true;
            this.lbl_RelayCount.Location = new System.Drawing.Point(425, 574);
            this.lbl_RelayCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_RelayCount.Name = "lbl_RelayCount";
            this.lbl_RelayCount.Size = new System.Drawing.Size(31, 15);
            this.lbl_RelayCount.TabIndex = 9;
            this.lbl_RelayCount.Text = "000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(487, 574);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 15);
            this.label9.TabIndex = 12;
            this.label9.Text = "状态";
            // 
            // lbl_Status
            // 
            this.lbl_Status.BackColor = System.Drawing.Color.Crimson;
            this.lbl_Status.Location = new System.Drawing.Point(527, 568);
            this.lbl_Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(53, 26);
            this.lbl_Status.TabIndex = 11;
            this.lbl_Status.Text = "成功";
            this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Servo
            // 
            this.lbl_Servo.BackColor = System.Drawing.Color.Crimson;
            this.lbl_Servo.Location = new System.Drawing.Point(637, 567);
            this.lbl_Servo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Servo.Name = "lbl_Servo";
            this.lbl_Servo.Size = new System.Drawing.Size(103, 26);
            this.lbl_Servo.TabIndex = 13;
            this.lbl_Servo.Text = "成功";
            this.lbl_Servo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Enable_Servo
            // 
            this.btn_Enable_Servo.Location = new System.Drawing.Point(23, 30);
            this.btn_Enable_Servo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Enable_Servo.Name = "btn_Enable_Servo";
            this.btn_Enable_Servo.Size = new System.Drawing.Size(120, 40);
            this.btn_Enable_Servo.TabIndex = 18;
            this.btn_Enable_Servo.Text = "使能";
            this.btn_Enable_Servo.UseVisualStyleBackColor = true;
            this.btn_Enable_Servo.Click += new System.EventHandler(this.btn_Enable_Servo_Click);
            // 
            // btn_Disable_Servo
            // 
            this.btn_Disable_Servo.Location = new System.Drawing.Point(150, 30);
            this.btn_Disable_Servo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Disable_Servo.Name = "btn_Disable_Servo";
            this.btn_Disable_Servo.Size = new System.Drawing.Size(120, 40);
            this.btn_Disable_Servo.TabIndex = 19;
            this.btn_Disable_Servo.Text = "不使能";
            this.btn_Disable_Servo.UseVisualStyleBackColor = true;
            this.btn_Disable_Servo.Click += new System.EventHandler(this.btn_Disable_Servo_Click);
            // 
            // lbl_Org
            // 
            this.lbl_Org.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbl_Org.Location = new System.Drawing.Point(758, 567);
            this.lbl_Org.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Org.Name = "lbl_Org";
            this.lbl_Org.Size = new System.Drawing.Size(103, 26);
            this.lbl_Org.TabIndex = 14;
            this.lbl_Org.Text = "成功";
            this.lbl_Org.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_Axis_Configuration_Item_MtrSpeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 600);
            this.Controls.Add(this.lbl_Org);
            this.Controls.Add(this.lbl_Servo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbl_RelayCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbl_TimeSpent);
            this.Controls.Add(this.lbl_Tag_CurrentPos);
            this.Controls.Add(this.lbl_TestPos);
            this.Controls.Add(this.gpb_Controller);
            this.Controls.Add(this.pGrid_Speed);
            this.Controls.Add(this.gpb_Selector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form_Axis_Configuration_Item_MtrSpeed";
            this.Text = "Form_Axis_Configuration_Item_MtrSpeed";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Axis_Configuration_Item_MtrSpeed_FormClosing);
            this.Load += new System.EventHandler(this.Form_Axis_Configuration_Item_MtrSpeed_Load);
            this.gpb_Selector.ResumeLayout(false);
            this.gpb_Selector.PerformLayout();
            this.gpb_Controller.ResumeLayout(false);
            this.gpb_Controller.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpb_Selector;
        private System.Windows.Forms.Button btn_Copy;
        private System.Windows.Forms.ComboBox cmb_Selector_Copy;
        private System.Windows.Forms.ComboBox cmb_Selector_SpeedSetting;
        private System.Windows.Forms.PropertyGrid pGrid_Speed;
        private System.Windows.Forms.GroupBox gpb_Controller;
        private System.Windows.Forms.Button btn_Relative_Positive;
        private System.Windows.Forms.Button btn_Relative_Negative;
        private System.Windows.Forms.Button btn_Jog_Positive;
        private System.Windows.Forms.Button btn_Jog_Negative;
        private System.Windows.Forms.TextBox txb_AbsolutePos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_RelativePos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Absolute;
        private System.Windows.Forms.TextBox txb_RelayGap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_RelayCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Relay;
        private System.Windows.Forms.Button btn_Home;
        private System.Windows.Forms.Button btn_SetZero;
        private System.Windows.Forms.Label lbl_Copy;
        private System.Windows.Forms.Label lbl_Main;
        private System.Windows.Forms.Label lbl_TestPos;
        private System.Windows.Forms.Label lbl_Tag_CurrentPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_TimeSpent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_RelayCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Label lbl_Servo;
        private System.Windows.Forms.Button btn_Disable_Servo;
        private System.Windows.Forms.Button btn_Enable_Servo;
        private System.Windows.Forms.Label lbl_Org;
    }
}