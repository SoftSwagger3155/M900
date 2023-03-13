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
    public partial class FormFunc : Form
    {
        FuncPara funcPara = new FuncPara();
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        public FormFunc()
        {
            InitializeComponent();
            SetControlTag();
            ReadFuncPara();
        }
        #region 控件自适应大小
        private void SetControlTag()
        {
            x = uiGroupBox6.Width;
            y = uiGroupBox6.Height;
            AutoSizeControls.SetTag(this);
        }

        private void uiGroupBox6_Resize(object sender, EventArgs e)
        {
            float newx = (uiGroupBox6.Width) / x;
            float newy = (uiGroupBox6.Height) / y;
            AutoSizeControls.SetControls(newx, newy, uiGroupBox6);
        }
        #endregion

        private void btn_SaveFunc_Click(object sender, EventArgs e)
        {
            SaveFuncPara();
            BaseTask.vecPercent = ProgramParamMange.RunFuncPara.VelPrecent / 100;
        }

        public void SaveFuncPara()
        {
            string runModel = "";
            string program = "";
            string workHold = "";
            string provideOrOut = "";

            if (uiRadioButton1.Checked)
                runModel = uiRadioButton1.Text;
            else if (uiRadioButton2.Checked)
                runModel = uiRadioButton2.Text;
            else if (uiRadioButton3.Checked)
                runModel = uiRadioButton3.Text;
            if (uiRadioButton4.Checked)
                program = uiRadioButton4.Text;
            else if (uiRadioButton5.Checked)
                program = uiRadioButton5.Text;
            if (uiRadioButton6.Checked)
                workHold = uiRadioButton6.Text;
            else if (uiRadioButton7.Checked)
                workHold = uiRadioButton7.Text;
            if (uiRadioButton12.Checked)
                provideOrOut = uiRadioButton12.Text;
            else if (uiRadioButton13.Checked)
                provideOrOut = uiRadioButton13.Text;
            else if (uiRadioButton14.Checked)
                provideOrOut = uiRadioButton14.Text;

            ProgramParamMange.RunFuncPara = new FuncPara()
            {
                RunModelSelect = runModel,
                ProgramExecution = program,
                WorkHolding = workHold,
                ChekResultMark = uiRadioButton8.Checked,
                ChekFinishMark = uiRadioButton10.Checked,
                InterleafControl = uiRadioButton15.Checked,
                ProvideOrOut = provideOrOut,
                ElectrCkeckErrorOverlook = uiCheckBox1.Checked,
                DataCodeCheckErrorOverlook = uiCheckBox2.Checked,
                ImageErrorOverlook = uiCheckBox3.Checked,
                ImageErrorNowPointsCheck = uiCheckBox4.Checked,
                VelPrecent = float.Parse(uiTextBox6.Text),
                AccPrecent = float.Parse(uiTextBox7.Text)
            };
            SerializeHelper.SerializeXml<FuncPara>(ProgramParamMange.RunFuncPara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\FuncPara.xml");
        }

        public void ReadFuncPara()
        {
            switch (ProgramParamMange.RunFuncPara.RunModelSelect)
            {
                case "检查":
                    uiRadioButton1.Checked = true;
                    break;
                case "测试":
                    uiRadioButton2.Checked = true;
                    break;
                case "测量":
                    uiRadioButton3.Checked = true;
                    break;
            }
            if (ProgramParamMange.RunFuncPara.ProgramExecution == "连续")
                uiRadioButton4.Checked = true;
            else if (ProgramParamMange.RunFuncPara.ProgramExecution == "步骤")
                uiRadioButton5.Checked = true;
            if (ProgramParamMange.RunFuncPara.WorkHolding == "自动")
                uiRadioButton6.Checked = true;
            else if (ProgramParamMange.RunFuncPara.WorkHolding == "手动")
                uiRadioButton7.Checked = true;
            if (ProgramParamMange.RunFuncPara.ChekResultMark)
                uiRadioButton8.Checked = true;
            else
                uiRadioButton9.Checked = true;
            if (ProgramParamMange.RunFuncPara.ChekFinishMark)
                uiRadioButton10.Checked = true;
            else
                uiRadioButton11.Checked = true;
            if (ProgramParamMange.RunFuncPara.InterleafControl)
                uiRadioButton15.Checked = true;
            else
                uiRadioButton16.Checked = true;
            switch (ProgramParamMange.RunFuncPara.ProvideOrOut)
            {
                case "连续":
                    uiRadioButton12.Checked = true;
                    break;
                case "一周":
                    uiRadioButton13.Checked = true;
                    break;
                case "手动":
                    uiRadioButton14.Checked = true;
                    break;
            }
            uiCheckBox1.Checked = ProgramParamMange.RunFuncPara.ElectrCkeckErrorOverlook ? true : false;
            uiCheckBox2.Checked = ProgramParamMange.RunFuncPara.DataCodeCheckErrorOverlook ? true : false;
            uiCheckBox3.Checked = ProgramParamMange.RunFuncPara.ImageErrorOverlook ? true : false;
            uiCheckBox4.Checked = ProgramParamMange.RunFuncPara.ImageErrorNowPointsCheck ? true : false;
            uiTextBox6.Text = ProgramParamMange.RunFuncPara.VelPrecent.ToString();
            uiTextBox7.Text = ProgramParamMange.RunFuncPara.AccPrecent.ToString();
        }
    }
}
