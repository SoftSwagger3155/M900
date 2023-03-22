using SolveWare_Service_Core.Base.Interface;
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
    public delegate void ParaUpdateEventHandler();
    public partial class FormPanel : Form, IView
    {
        private Form formCurrent;
        private FormProductData formProductData;
        public event Action<string> FormButtonChange;
        public event Action<string> FormMainChange;
        public event ParaUpdateEventHandler ParaUpdateEvent;
        public FormPanel()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 界面切换
        /// </summary>
        /// <param name="name"></param>
        public void Showsubform(string name)
        {
            //foreach (Form f in new Form[] {formProgramSet,  new FormProductData(), new FormBoardMessage(),new FormDebugTableTop(),
            //new FormCheckMarking(),new FormRunOption(),new FormJigOpations(),new FormJipChange(),new FormJipData(),
            //new FormJipImageLogin(),new FormJipOffset(),new FormCoveyHandleSet(),new FormWorkPieceDebug(),new FormLoginMarkImage(),
            //new FormDataCodeDebug(),new FormCheckLevelZ2(),new FormAdjustOffsetZ1(),new FormAdjustLevelZ()})
            //{
            //    if (f.Name as string == name)
            //    {
            //        formCurrent = f;
            //        OpenForm(f, panel1);
            //    }
            //}
        }
        public void OpenForm(Form childForm, Panel panel)
        {
            try
            {
                //判断容器中是否有其他的窗体
                foreach (Control item in panel.Controls)
                {
                    if (item is Form)
                    {
                        ((Form)item).Visible = false;
                    }
                }
                if (childForm != null)
                {
                    //嵌入新窗体
                    childForm.TopLevel = false; //将子窗体设置成非顶级控件
                    childForm.Parent = panel;
                    childForm.Dock = DockStyle.Fill;   //随着容S器大小自动调整窗体大小
                    childForm.Show();
                    childForm.BringToFront();
                    this.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体切换错误:" + ex.Message);
            }
        }

        private void btn_Suspend_Click(object sender, EventArgs e)
        {
            if (!UIMessageBox.Show("是否完成程序参数修改且保存完成？", "提示", UIStyle.Blue, UIMessageBoxButtons.OKCancel))
                return;

            FormMainChange("FormProgramSet");
            FormButtonChange("FormButtonMain");
            ParaUpdateEvent.Invoke();

        }
        private void SaveParameter()
        {
            //switch (FormSetProcess.proProcess)
            //{
            //    case ProProcess.制品数据:
            //        FormProductData.SaveProductData();
            //        break;
            //    case ProProcess.单板信息:
            //        FormBoardMessage.SaveBoardMessage();
            //        break;
            //    case ProProcess.调试台面:
            //        FormDebugTableTop.SaveRetryOffsetData();
            //        break;
            //    case ProProcess.标记:
            //        FormCheckMarking.SaveCheckMark();
            //        break;
            //    case ProProcess.运行选项:
            //        FormRunOption.SaveRunOptions();
            //        break;
            //    case ProProcess.治具操作准备:
            //        break;
            //    case ProProcess.治具更换:
            //        break;
            //    case ProProcess.治具数据:
            //        FormJipData.SaveJipData();
            //        break;
            //    case ProProcess.治具画像登录:
            //        break;
            //    case ProProcess.治具偏移测量:
            //        break;
            //    case ProProcess.搬送手臂调整:
            //        FormCoveyHandleSet.SaveCoveyHand();
            //        break;
            //    case ProProcess.工件手臂调整:

            //        break;
            //    case ProProcess.登录工件标记图像:
            //        FormLoginMarkImage.SaveLoginMarkImage();
            //        break;
            //    case ProProcess.数据编码登录:
            //        break;
            //    case ProProcess.检查Z2水平:
            //        break;
            //    case ProProcess.Adjust_Z1_Level_Offset:
            //        break;
            //    case ProProcess.Adjuset_Zlevel:
            //        break;
            //    default:
            //        break;
            //}
        }
        //保存参数
        private void btn_SaveProgramPara_Click(object sender, EventArgs e)
        {
            SaveParameter();
        }

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }
    }
}
