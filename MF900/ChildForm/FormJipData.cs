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
    public partial class FormJipData : Form
    {
        private static FormJipData form;
        public FormJipData()
        {
            InitializeComponent();
            form = this;
            ReadJipData();
        }
        
        public static void SaveJipData()
        {
            ProgramParamMange.UpJipDataPara = new JipDataModel()
            {
                JipName = "Abc",
                JipStepXY = new Point() { X = int.Parse(form.uiTextBox10.Text), Y = int.Parse(form.uiTextBox11.Text) },
                JipPos = new Pofloat() { X = float.Parse(form.uiTextBox12.Text), Y = float.Parse(form.uiTextBox13.Text) },
                JipOffsetPos1 = new Pofloat() { X = float.Parse(form.uiTextBox1.Text), Y = float.Parse(form.uiTextBox2.Text) },
                JipOffsetPos2 = new Pofloat() { X = float.Parse(form.uiTextBox3.Text), Y = float.Parse(form.uiTextBox4.Text) },
               
            };
            ProgramParamMange.DownJipDataPara = new JipDataModel()
            {
                JipName = "Abc",
                JipStepXY = new Point() { X = int.Parse(form.uiTextBox5.Text), Y = int.Parse(form.uiTextBox6.Text) },
                JipPos = new Pofloat() { X = float.Parse(form.uiTextBox7.Text), Y = float.Parse(form.uiTextBox8.Text) },
                JipOffsetPos1 = new Pofloat() { X = float.Parse(form.uiTextBox14.Text), Y = float.Parse(form.uiTextBox15.Text) },
                JipOffsetPos2 = new Pofloat() { X = float.Parse(form.uiTextBox16.Text), Y = float.Parse(form.uiTextBox17.Text) },

            };
            SerializeHelper.SerializeXml<JipDataModel>(ProgramParamMange.UpJipDataPara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\UpJipDataModel.xml");
            SerializeHelper.SerializeXml<JipDataModel>(ProgramParamMange.DownJipDataPara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\DownJipDataModel.xml");
        }
        public void ReadJipData()
        {
            uiTextBox10.Text = ProgramParamMange.UpJipDataPara.JipStepXY.X.ToString();
            uiTextBox11.Text = ProgramParamMange.UpJipDataPara.JipStepXY.Y.ToString();
            uiTextBox12.Text = ProgramParamMange.UpJipDataPara.JipPos.X.ToString();
            uiTextBox13.Text = ProgramParamMange.UpJipDataPara.JipPos.Y.ToString();
            uiTextBox1.Text = ProgramParamMange.UpJipDataPara.JipOffsetPos1.X.ToString();
            uiTextBox2.Text = ProgramParamMange.UpJipDataPara.JipOffsetPos1.Y.ToString();
            uiTextBox3.Text = ProgramParamMange.UpJipDataPara.JipOffsetPos2.X.ToString();
            uiTextBox4.Text = ProgramParamMange.UpJipDataPara.JipOffsetPos2.Y.ToString();
            uiTextBox5.Text = ProgramParamMange.DownJipDataPara.JipStepXY.X.ToString();
            uiTextBox6.Text = ProgramParamMange.DownJipDataPara.JipStepXY.Y.ToString();
            uiTextBox7.Text = ProgramParamMange.DownJipDataPara.JipPos.X.ToString();
            uiTextBox8.Text = ProgramParamMange.DownJipDataPara.JipPos.Y.ToString();
            uiTextBox14.Text = ProgramParamMange.DownJipDataPara.JipOffsetPos1.X.ToString();
            uiTextBox15.Text = ProgramParamMange.DownJipDataPara.JipOffsetPos1.Y.ToString();
            uiTextBox16.Text = ProgramParamMange.DownJipDataPara.JipOffsetPos2.X.ToString();
            uiTextBox17.Text = ProgramParamMange.DownJipDataPara.JipOffsetPos2.Y.ToString();
        }
    }
}
