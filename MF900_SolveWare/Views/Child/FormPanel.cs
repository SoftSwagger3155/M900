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

namespace MF900_SolveWare
{
    public delegate void ParaUpdateEventHandler();
    public partial class FormPanel : Form, IView
    {
        public event Action<string> FormMainChange;
        public event ParaUpdateEventHandler ParaUpdateEvent;
        public Dictionary<string, Form> dicProcessForm =null;
        public FormPanel()
        {
            InitializeComponent();
            dicProcessForm = new Dictionary<string, Form>()
            {
                {"FormProductData",new FormProductData() },
                {"FormBoardMessage",new FormBoardMessage() },
                {"FormDebugTableTop",new FormDebugTableTop() },
                {"FormCheckMarking",new FormCheckMarking() },
                {"FormRunOption",new FormRunOption() },
                {"FormJigOpations",new FormJigOpations() },
                {"FormJipChange",new FormJipChange() },
                {"FormJipData",new FormJipData() },
                {"FormJipImageLogin",new FormJipImageLogin() },
                {"FormJipOffset",new FormJipOffset() },
                {"FormWorkPieceDebug",new FormWorkPieceDebug() },
                {"FormLoginMarkImage",new FormLoginMarkImage() },
                {"FormDataCodeDebug",new FormDataCodeDebug() },
                {"FormCheckLevelZ2",new FormCheckLevelZ2() },
                {"FormAdjustLevelZ",new FormAdjustLevelZ() },
            };
            Showsubform(new FormProductData().Name);
        }

        public void Showsubform(string name)
        {
            foreach (KeyValuePair<string, Form> f in dicProcessForm)
            {
                if (f.Value.Name == name)
                {
                    FormSwitch.SwitchForm(f.Value, panel1);
                }
            }
        }

        private void btn_Suspend_Click(object sender, EventArgs e)
        {
            if (!UIMessageBox.Show("是否完成程序参数修改且保存完成？", "提示", UIStyle.Blue, UIMessageBoxButtons.OKCancel))
                return;

            FormMainChange("FormProgramSet");
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
