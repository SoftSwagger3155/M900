namespace MF900_SolveWare.Views.Child
{
    partial class Form_InspectKit_Lighting_Item_IO
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
            this.ckb_Selected = new System.Windows.Forms.CheckBox();
            this.lbl_Tag = new System.Windows.Forms.Label();
            this.cmb_Selector_TriggerMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckb_Selected
            // 
            this.ckb_Selected.Location = new System.Drawing.Point(15, 11);
            this.ckb_Selected.Name = "ckb_Selected";
            this.ckb_Selected.Size = new System.Drawing.Size(60, 20);
            this.ckb_Selected.TabIndex = 0;
            this.ckb_Selected.Text = "选择";
            this.ckb_Selected.UseVisualStyleBackColor = true;
            this.ckb_Selected.CheckedChanged += new System.EventHandler(this.ckb_Selected_CheckedChanged);
            // 
            // lbl_Tag
            // 
            this.lbl_Tag.Location = new System.Drawing.Point(83, 12);
            this.lbl_Tag.Name = "lbl_Tag";
            this.lbl_Tag.Size = new System.Drawing.Size(217, 20);
            this.lbl_Tag.TabIndex = 1;
            this.lbl_Tag.Text = "物件: IO";
            // 
            // cmb_Selector_TriggerMode
            // 
            this.cmb_Selector_TriggerMode.Font = new System.Drawing.Font("宋体", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Selector_TriggerMode.FormattingEnabled = true;
            this.cmb_Selector_TriggerMode.Location = new System.Drawing.Point(415, 8);
            this.cmb_Selector_TriggerMode.Name = "cmb_Selector_TriggerMode";
            this.cmb_Selector_TriggerMode.Size = new System.Drawing.Size(121, 25);
            this.cmb_Selector_TriggerMode.TabIndex = 2;
            this.cmb_Selector_TriggerMode.SelectionChangeCommitted += new System.EventHandler(this.cmb_Selector_TriggerMode_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(368, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "模式";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Execute);
            this.panel1.Controls.Add(this.lbl_Tag);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ckb_Selected);
            this.panel1.Controls.Add(this.cmb_Selector_TriggerMode);
            this.panel1.Location = new System.Drawing.Point(8, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 44);
            this.panel1.TabIndex = 4;
            // 
            // btn_Execute
            // 
            this.btn_Execute.Location = new System.Drawing.Point(675, 7);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(75, 30);
            this.btn_Execute.TabIndex = 5;
            this.btn_Execute.Text = "执行";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // Form_InspectKit_Lighting_Item_IO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 55);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_InspectKit_Lighting_Item_IO";
            this.Text = "Form_InspectKit_Lighting_Item_LightSource";
            this.Load += new System.EventHandler(this.Form_InspectKit_Lighting_Item_IO_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ckb_Selected;
        private System.Windows.Forms.Label lbl_Tag;
        private System.Windows.Forms.ComboBox cmb_Selector_TriggerMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Execute;
    }
}