
namespace MF900
{
    partial class FormWorkPieceList
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
            this.btn_Notarize = new Sunny.UI.UIButton();
            this.btn_Cancel = new Sunny.UI.UIButton();
            this.listBoxProduct = new Sunny.UI.UIListBox();
            this.uiTableLayoutPanel1 = new Sunny.UI.UITableLayoutPanel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiTableLayoutPanel2 = new Sunny.UI.UITableLayoutPanel();
            this.uiTableLayoutPanel1.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.uiTableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Notarize
            // 
            this.btn_Notarize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Notarize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Notarize.FillColor = System.Drawing.Color.Silver;
            this.btn_Notarize.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Notarize.Location = new System.Drawing.Point(331, 3);
            this.btn_Notarize.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Notarize.Name = "btn_Notarize";
            this.btn_Notarize.RectColor = System.Drawing.Color.Silver;
            this.btn_Notarize.Size = new System.Drawing.Size(79, 48);
            this.btn_Notarize.Style = Sunny.UI.UIStyle.Custom;
            this.btn_Notarize.StyleCustomMode = true;
            this.btn_Notarize.TabIndex = 0;
            this.btn_Notarize.Text = "确认";
            this.btn_Notarize.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Notarize.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_Notarize.Click += new System.EventHandler(this.btn_Notarize_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cancel.FillColor = System.Drawing.Color.Silver;
            this.btn_Cancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Cancel.Location = new System.Drawing.Point(416, 3);
            this.btn_Cancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.RectColor = System.Drawing.Color.Silver;
            this.btn_Cancel.Size = new System.Drawing.Size(79, 48);
            this.btn_Cancel.Style = Sunny.UI.UIStyle.Custom;
            this.btn_Cancel.StyleCustomMode = true;
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Cancel.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // listBoxProduct
            // 
            this.listBoxProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxProduct.FillColor = System.Drawing.Color.White;
            this.listBoxProduct.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxProduct.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.listBoxProduct.ItemSelectBackColor = System.Drawing.Color.Silver;
            this.listBoxProduct.ItemSelectForeColor = System.Drawing.Color.White;
            this.listBoxProduct.Location = new System.Drawing.Point(4, 5);
            this.listBoxProduct.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxProduct.MinimumSize = new System.Drawing.Size(1, 1);
            this.listBoxProduct.Name = "listBoxProduct";
            this.listBoxProduct.Padding = new System.Windows.Forms.Padding(2);
            this.listBoxProduct.ShowText = false;
            this.listBoxProduct.Size = new System.Drawing.Size(504, 337);
            this.listBoxProduct.Style = Sunny.UI.UIStyle.Custom;
            this.listBoxProduct.StyleCustomMode = true;
            this.listBoxProduct.TabIndex = 1;
            this.listBoxProduct.Text = "uiListBox1";
            this.listBoxProduct.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiTableLayoutPanel1
            // 
            this.uiTableLayoutPanel1.ColumnCount = 1;
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiTableLayoutPanel1.Controls.Add(this.listBoxProduct, 0, 0);
            this.uiTableLayoutPanel1.Controls.Add(this.uiPanel1, 0, 1);
            this.uiTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTableLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.uiTableLayoutPanel1.Name = "uiTableLayoutPanel1";
            this.uiTableLayoutPanel1.RowCount = 2;
            this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.uiTableLayoutPanel1.Size = new System.Drawing.Size(512, 417);
            this.uiTableLayoutPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTableLayoutPanel1.TabIndex = 2;
            this.uiTableLayoutPanel1.TagString = null;
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiTableLayoutPanel2);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.FillColor = System.Drawing.Color.White;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(4, 352);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.uiPanel1.Size = new System.Drawing.Size(504, 60);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel1.StyleCustomMode = true;
            this.uiPanel1.TabIndex = 2;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiTableLayoutPanel2
            // 
            this.uiTableLayoutPanel2.ColumnCount = 3;
            this.uiTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.uiTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.uiTableLayoutPanel2.Controls.Add(this.btn_Notarize, 1, 0);
            this.uiTableLayoutPanel2.Controls.Add(this.btn_Cancel, 2, 0);
            this.uiTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiTableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.uiTableLayoutPanel2.Name = "uiTableLayoutPanel2";
            this.uiTableLayoutPanel2.RowCount = 1;
            this.uiTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uiTableLayoutPanel2.Size = new System.Drawing.Size(498, 54);
            this.uiTableLayoutPanel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiTableLayoutPanel2.TabIndex = 2;
            this.uiTableLayoutPanel2.TagString = null;
            // 
            // FormWorkPieceList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(512, 452);
            this.Controls.Add(this.uiTableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWorkPieceList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "工件数据";
            this.TitleColor = System.Drawing.Color.Silver;
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 512, 452);
            this.uiTableLayoutPanel1.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.uiTableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIButton btn_Notarize;
        private Sunny.UI.UIButton btn_Cancel;
        private Sunny.UI.UIListBox listBoxProduct;
        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel1;
        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel2;
        private Sunny.UI.UIPanel uiPanel1;
    }
}