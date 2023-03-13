using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900
{
    public partial class FormWorkPieceList : UIForm
    {
        public event Action<string> SetTextNowProduct;
        public FormWorkPieceList()
        {
            InitializeComponent();
            listBoxProduct.DataSource = SerializeHelper.DeSerializeXml<ProductManage>(ParaFliePath.SystemParaPath + "ProlductManage.xml").ProgramList;
            listBoxProduct.SelectedItem = SerializeHelper.DeSerializeXml<ProductManage>(ParaFliePath.SystemParaPath + "ProlductManage.xml").NowProgramName;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btn_Notarize_Click(object sender, EventArgs e)
        {
            SetTextNowProduct(listBoxProduct.SelectedValue.ToString());
            this.Close();
        }
        
        private void ReloadFormClick()
        {
            //this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormWorkPieceList_MouseDown);
            //this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormWorkPieceList_MouseMove);
            //this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormWorkPieceList_MouseUp);
        }

        #region 无边框窗体移动
        
        private bool isMouse = false; // 鼠标是否按下
        // 原点位置
        private int originX = 0;
        private int originY = 0;
        // 鼠标按下位置
        private int mouseX = 0;
        private int mouseY = 0;
        // 鼠标按下
        private void uiGroupBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { // 判断鼠标按键
                isMouse = true;
                // 屏幕坐标位置
                originX = this.Location.X;
                originY = this.Location.Y;
                // 鼠标按下位置
                mouseX = originX + e.X;
                mouseY = originY + e.Y;
            }
        }
        // 鼠标移动
        private void uiGroupBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouse)
            {
                // 移动距离
                int moveX = (e.X + this.Location.X) - mouseX;
                int moveY = (e.Y + this.Location.Y) - mouseY;
                int targetX = originX + moveX;
                int targetY = originY + moveY;
                this.Location = new Point(targetX, targetY);
            }
        }
        // 鼠标释放
        private void uiGroupBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouse)
            {
                isMouse = false;
            }
        }
        #endregion

    }
}
