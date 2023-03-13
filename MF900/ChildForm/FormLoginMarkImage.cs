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
    public partial class FormLoginMarkImage : Form
    {
        private static FormLoginMarkImage form;
        public FormLoginMarkImage()
        {
            InitializeComponent();
            form = this;
            ReadLoginMaekImage();
            uiGroupBox2.Enabled = ProgramParamMange.LoginMarkImagePara.UseParaPositionMarker;
        }
        public static void SaveLoginMarkImage()
        {
            ProgramParamMange.LoginMarkImagePara = new LoginMarkImageModel()
            {
                UseParaPositionMarker = form.uiCheckBox1.Checked,
                BaseCorrection = new Pofloat() { X = (float)form.uiTextBox1.DoubleValue, Y = (float)form.uiTextBox2.DoubleValue },
                TiltCorrection = new Pofloat() { X = (float)form.uiTextBox3.DoubleValue, Y = (float)form.uiTextBox4.DoubleValue },
            };
            SerializeHelper.SerializeXml(ProgramParamMange.LoginMarkImagePara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\LoginMarkImageModel.xml");
        }
        private void ReadLoginMaekImage()
        {
            uiCheckBox1.Checked = ProgramParamMange.LoginMarkImagePara.UseParaPositionMarker;
            uiTextBox1.Text = ProgramParamMange.LoginMarkImagePara.BaseCorrection.X.ToString();
            uiTextBox2.Text = ProgramParamMange.LoginMarkImagePara.BaseCorrection.Y.ToString();
            uiTextBox3.Text = ProgramParamMange.LoginMarkImagePara.TiltCorrection.X.ToString();
            uiTextBox4.Text = ProgramParamMange.LoginMarkImagePara.TiltCorrection.Y.ToString();
        }

        private void uiCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            uiGroupBox2.Enabled = uiCheckBox1.Checked;
        }
    }
}
