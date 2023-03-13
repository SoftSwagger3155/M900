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
    public partial class FormCheckMarking : Form
    {
        private static FormCheckMarking form;
        public FormCheckMarking()
        {
            InitializeComponent();
            form = this;
            SetMarkerParaEnbale(ProgramParamMange.MarkerPara.NgDghMark);
            ReadCheckMark();
        }

        private void uiTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radb_MarkOffset.Checked) 
                uiTabControl2.SelectedTab = tabPage3;
            else if(radb_MarkCount.Checked) 
                uiTabControl2.SelectedTab = tabPage4;
        }

        private void radb_MarkOffset_CheckedChanged(object sender, EventArgs e)
        {
            uiTabControl2.SelectedTab = tabPage3;
        }

        private void radb_MarkCount_CheckedChanged(object sender, EventArgs e)
        {
            uiTabControl2.SelectedTab = tabPage4;
        }
        public static void SaveCheckMark()
        {
            DistinguishMethod distinguishMethod = form.radb_MarkOffset.Checked ? DistinguishMethod.标记偏移 : DistinguishMethod.标记次数;
            ProgramParamMange.MarkerPara = new MarkerSetModel()
            {
                MarkerOffset = new MarkerOffset()
                {
                    Open = new Pofloat() { X = float.Parse(form.uiTextBox5.Text), Y = float.Parse(form.uiTextBox6.Text) },
                    Short = new Pofloat() { X = float.Parse(form.uiTextBox7.Text), Y = float.Parse(form.uiTextBox8.Text) },
                    OpenShort = new Pofloat() { X = float.Parse(form.uiTextBox9.Text), Y = float.Parse(form.uiTextBox10.Text) },
                    Aux = new Pofloat() { X = float.Parse(form.uiTextBox11.Text), Y = float.Parse(form.uiTextBox12.Text) },
                    Error = new Pofloat() { X = float.Parse(form.uiTextBox13.Text), Y = float.Parse(form.uiTextBox14.Text) },
                    Skip = new Pofloat() { X = float.Parse(form.uiTextBox15.Text), Y = float.Parse(form.uiTextBox16.Text) }
                },
                MarkerCount = new MarkerCount()
                {
                    Open = int.Parse(form.uiTextBox17.Text),
                    Short = int.Parse(form.uiTextBox18.Text),
                    OpenShort = int.Parse(form.uiTextBox19.Text),
                    Aux = int.Parse(form.uiTextBox20.Text),
                    Error = int.Parse(form.uiTextBox21.Text),
                    Skip = int.Parse(form.uiTextBox22.Text)
                },
                DghMethod = distinguishMethod,
                ExecuteCheckMark = form.uiCheckBox1.Checked,
                MarkObject = true,
                NgDghMark = form.uiCheckBox2.Checked,
                MarkOffsetXY = new Pofloat() { X = float.Parse(form.uiTextBox23.Text), Y = float.Parse(form.uiTextBox24.Text) }
            };
            SerializeHelper.SerializeXml(ProgramParamMange.MarkerPara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\MarkerSetModel.xml");
        }
        private void ReadCheckMark()
        {
            uiTextBox5.Text = ProgramParamMange.MarkerPara.MarkerOffset.Open.X.ToString();
            uiTextBox6.Text = ProgramParamMange.MarkerPara.MarkerOffset.Open.Y.ToString();
            uiTextBox7.Text = ProgramParamMange.MarkerPara.MarkerOffset.Short.X.ToString();
            uiTextBox8.Text = ProgramParamMange.MarkerPara.MarkerOffset.Short.Y.ToString();
            uiTextBox9.Text = ProgramParamMange.MarkerPara.MarkerOffset.OpenShort.X.ToString();
            uiTextBox10.Text = ProgramParamMange.MarkerPara.MarkerOffset.OpenShort.Y.ToString();
            uiTextBox11.Text = ProgramParamMange.MarkerPara.MarkerOffset.Aux.X.ToString();
            uiTextBox12.Text = ProgramParamMange.MarkerPara.MarkerOffset.Aux.Y.ToString();
            uiTextBox13.Text = ProgramParamMange.MarkerPara.MarkerOffset.Error.X.ToString();
            uiTextBox14.Text = ProgramParamMange.MarkerPara.MarkerOffset.Error.Y.ToString();
            uiTextBox15.Text = ProgramParamMange.MarkerPara.MarkerOffset.Skip.X.ToString();
            uiTextBox16.Text = ProgramParamMange.MarkerPara.MarkerOffset.Skip.Y.ToString();
            uiTextBox17.Text = ProgramParamMange.MarkerPara.MarkerCount.Open.ToString();
            uiTextBox18.Text = ProgramParamMange.MarkerPara.MarkerCount.Short.ToString();
            uiTextBox19.Text = ProgramParamMange.MarkerPara.MarkerCount.OpenShort.ToString();
            uiTextBox20.Text = ProgramParamMange.MarkerPara.MarkerCount.Aux.ToString();
            uiTextBox21.Text = ProgramParamMange.MarkerPara.MarkerCount.Error.ToString();
            uiTextBox22.Text = ProgramParamMange.MarkerPara.MarkerCount.Skip.ToString();
            radb_MarkOffset.Checked = ProgramParamMange.MarkerPara.DghMethod == DistinguishMethod.标记偏移 ? true : false;
            uiCheckBox2.Checked = ProgramParamMange.MarkerPara.NgDghMark;
            uiTextBox23.Text = ProgramParamMange.MarkerPara.MarkOffsetXY.X.ToString();
            uiTextBox24.Text = ProgramParamMange.MarkerPara.MarkOffsetXY.Y.ToString();
        }
        private void uiCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            SetMarkerParaEnbale(uiCheckBox2.Checked ? true : false);
        }
        private void SetMarkerParaEnbale(bool enable)
        {
            radb_MarkOffset.Enabled = enable;
            radb_MarkCount.Enabled = enable;
            uiTabControl2.Enabled = enable;
        }
    }
}
