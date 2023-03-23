using SolveWare_Service_Core.Base.Interface;
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
    public enum ProductProcess
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
        工件手臂调整,
        登录工件标记图像,
        数据编码登录,
        检查Z2水平,
        Adjuset_Zlevel
    }
    public partial class ProductPanelForm : Form, IView
    {
        private readonly Type typ = typeof(ProductProcess);
        public event Action<string> SwitchForm;
        public ProductProcess proProcess;
        FormPanel ProductPanel;
        public ProductPanelForm()
        {
            InitializeComponent();
            ProductPanel = new FormPanel();
            GenProcessButton();
            SwitchForm += ProductPanel.Showsubform;
            FormSwitch.SwitchForm(ProductPanel, this.panel1);
        }
        
        private void GenProcessButton()
        {
            for (int i = 0; i < (int)ProductProcess.Adjuset_Zlevel; i++)
            {
                uiDataGridView1.Rows.Add(new object[1] { (i + 1) + "，" + typ.GetEnumName(i + 1) });
                uiDataGridView1.Rows[i].Height = 50;
            }
        }

        public void Setup<TObj>(TObj obj)
        {
            
        }

        private void uiDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int formValue = int.Parse(uiDataGridView1[0, e.RowIndex].Value.ToString().Split('，')[0]);
            if (proProcess.ToString() == uiDataGridView1[0, e.RowIndex].Value.ToString().Split('，')[1].ToString())
            {
                return;
            }
            switch (formValue)
            {
                case (int)ProductProcess.制品数据:
                    SwitchForm("FormProductData");
                    proProcess = ProductProcess.制品数据;
                    break;
                case (int)ProductProcess.单板信息:
                    SwitchForm("FormBoardMessage");
                    proProcess = ProductProcess.单板信息;
                    break;
                case (int)ProductProcess.调试台面:
                    SwitchForm("FormDebugTableTop");
                    proProcess = ProductProcess.调试台面;
                    break;
                case (int)ProductProcess.标记:
                    SwitchForm("FormCheckMarking");
                    proProcess = ProductProcess.标记;
                    break;
                case (int)ProductProcess.运行选项:
                    SwitchForm("FormRunOption");
                    proProcess = ProductProcess.运行选项;
                    break;
                case (int)ProductProcess.治具操作准备:
                    SwitchForm("FormJigOpations");
                    proProcess = ProductProcess.治具操作准备;
                    break;
                case (int)ProductProcess.治具更换:
                    SwitchForm("FormJipChange");
                    proProcess = ProductProcess.治具更换;
                    break;
                case (int)ProductProcess.治具数据:
                    SwitchForm("FormJipData");
                    proProcess = ProductProcess.治具数据;
                    break;
                case (int)ProductProcess.治具画像登录:
                    SwitchForm("FormJipImageLogin");
                    proProcess = ProductProcess.治具画像登录;
                    break;
                case (int)ProductProcess.治具偏移测量:
                    SwitchForm("FormJipOffset");
                    proProcess = ProductProcess.治具偏移测量;
                    break;
                case (int)ProductProcess.工件手臂调整:
                    SwitchForm("FormWorkPieceDebug");
                    proProcess = ProductProcess.工件手臂调整;
                    break;
                case (int)ProductProcess.登录工件标记图像:
                    SwitchForm("FormLoginMarkImage");
                    proProcess = ProductProcess.登录工件标记图像;
                    break;
                case (int)ProductProcess.数据编码登录:
                    SwitchForm("FormDataCodeDebug");
                    proProcess = ProductProcess.数据编码登录;
                    break;
                case (int)ProductProcess.检查Z2水平:
                    SwitchForm("FormCheckLevelZ2");
                    proProcess = ProductProcess.检查Z2水平;
                    break;
                case (int)ProductProcess.Adjuset_Zlevel:
                    SwitchForm("FormAdjustLevelZ");
                    proProcess = ProductProcess.Adjuset_Zlevel;
                    break;
                default:
                    break;
            }

        }
    }
}
