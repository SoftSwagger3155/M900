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
    public enum ProProcess
    {
        制品数据 = 1,
        单板信息,
        调试台面,
        标记,
        运行选项,
        治具操作准备,
        治具更换,
        治具数据,
        治具画像登录,
        治具偏移测量,
        搬送手臂调整,
        工件手臂调整,
        登录工件标记图像,
        数据编码登录,
        检查Z2水平,
        Adjust_Z1_Level_Offset,
        Adjuset_Zlevel
    }

    public partial class FormSetProcess : Form
    {
       
        private Type typ = typeof(ProProcess);
        public static ProProcess proProcess;
        public event Action<string> FormSwitch;
        public FormSetProcess()
        {
            InitializeComponent();
            GenProcessButton();
        }
        private void GenProcessButton()
        {
            for (int i = 0; i < (int)ProProcess.Adjuset_Zlevel; i++)
            {
                skinDataGridView1.Rows.Add(new object[1] { (i + 1) + "，" + typ.GetEnumName(i + 1) });
                skinDataGridView1.Rows[i].Height = 50;
            }
        }

        private void skinDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int formValue = int.Parse(skinDataGridView1[0, e.RowIndex].Value.ToString().Split('，')[0]);
            if (proProcess.ToString() == skinDataGridView1[0, e.RowIndex].Value.ToString().Split('，')[1].ToString())
            {
                return;
            }
            switch (formValue)
            {
                case (int)ProProcess.制品数据:
                    FormSwitch("FormProductData");
                    proProcess = ProProcess.制品数据;
                    break;
                case (int)ProProcess.单板信息:
                    FormSwitch("FormBoardMessage");
                    proProcess = ProProcess.单板信息;
                    break;
                case (int)ProProcess.调试台面:
                    FormSwitch("FormDebugTableTop");
                    proProcess = ProProcess.调试台面;
                    break;
                case (int)ProProcess.标记:
                    FormSwitch("FormCheckMarking");
                    proProcess = ProProcess.标记;
                    break;
                case (int)ProProcess.运行选项:
                    FormSwitch("FormRunOption");
                    proProcess = ProProcess.运行选项;
                    break;
                case (int)ProProcess.治具操作准备:
                    FormSwitch("FormJigOpations");
                    proProcess = ProProcess.治具操作准备;
                    break;
                case (int)ProProcess.治具更换:
                    FormSwitch("FormJipChange");
                    proProcess = ProProcess.治具更换;
                    break;
                case (int)ProProcess.治具数据:
                    FormSwitch("FormJipData");
                    proProcess = ProProcess.治具数据;
                    break;
                case (int)ProProcess.治具画像登录:
                    FormSwitch("FormJipImageLogin");
                    proProcess = ProProcess.治具画像登录;
                    break;
                case (int)ProProcess.治具偏移测量:
                    FormSwitch("FormJipOffset");
                    proProcess = ProProcess.治具偏移测量;
                    break;
                case (int)ProProcess.搬送手臂调整:
                    FormSwitch("FormCoveyHandleSet");
                    proProcess = ProProcess.搬送手臂调整;
                    break;
                case (int)ProProcess.工件手臂调整:
                    FormSwitch("FormWorkPieceDebug");
                    proProcess = ProProcess.工件手臂调整;
                    break;
                case (int)ProProcess.登录工件标记图像:
                    FormSwitch("FormLoginMarkImage");
                    proProcess = ProProcess.登录工件标记图像;
                    break;
                case (int)ProProcess.数据编码登录:
                    FormSwitch("FormDataCodeDebug");
                    proProcess = ProProcess.数据编码登录;
                    break;
                case (int)ProProcess.检查Z2水平:
                    FormSwitch("FormCheckLevelZ2");
                    proProcess = ProProcess.检查Z2水平;
                    break;
                case (int)ProProcess.Adjust_Z1_Level_Offset:
                    FormSwitch("FormAdjustOffsetZ1");
                    proProcess = ProProcess.Adjust_Z1_Level_Offset;
                    break;
                case (int)ProProcess.Adjuset_Zlevel:
                    FormSwitch("FormAdjustLevelZ");
                    proProcess = ProProcess.Adjuset_Zlevel;
                    break;
                default:
                    break;
            }
        }
    }
}
