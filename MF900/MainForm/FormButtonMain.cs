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
    public partial class FormButtonMain : Form
    {
        public event Action<string> ShowsubformAction;
        private Color blue1= Color.FromArgb(80, 160, 255);
        private Color blue2 = Color.FromArgb(50, 220, 255);
        private UIButton btn;
        public FormButtonMain()
        {
            InitializeComponent();
            btn_RunForm.FillColor = blue2;
        }
        private void SetBtnFillColor(string btnName)
        {
            foreach (Control control in uiTableLayoutPanel1.Controls)
            {
                if(control is UIButton)
                {
                    btn = (UIButton)control;
                    btn.FillColor = btn.Name == btnName ? blue2 : blue1;
                }
            }
        }
        //手动操作
        private void btn_HandOperate_Click(object sender, EventArgs e)
        {
            ShowsubformAction("FormDebug");
            SetBtnFillColor("btn_HandOperate");
        }
        //运行界面
        private void btn_RunForm_Click(object sender, EventArgs e)
        {
            ShowsubformAction("FormRunUI");
            SetBtnFillColor("btn_RunForm");
        }
        //程序设定
        private void btn_ShowProSet_Click(object sender, EventArgs e)
        {
            ShowsubformAction("FormProgramSet");
            SetBtnFillColor("btn_ShowProSet");
        }
        //机器设定
        private void btn_MachineSet_Click(object sender, EventArgs e)
        {
            ShowsubformAction("FormFunc");
            SetBtnFillColor("btn_MachineSet");
        }
        //参数设定
        private void btn_ParameterSet_Click(object sender, EventArgs e)
        {
            ShowsubformAction("FormParameterSet");
            SetBtnFillColor("btn_ParameterSet");
        }
        //机器状态
        private void btn_MachineState_Click(object sender, EventArgs e)
        {
            ShowsubformAction("FormMachineState");
            SetBtnFillColor("btn_MachineState");
        }
        //设备维护
        private void btn_Maintain_Click(object sender, EventArgs e)
        {
            ShowsubformAction("FormMaintaining");
            SetBtnFillColor("btn_Maintain");
        }
    }
}
