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
    public partial class FormProductData : Form
    {
        private static FormProductData form;
        public FormProductData()
        {
            InitializeComponent();
            ReadProductData();
            form = this;
        }
        public static void SaveProductData()
        {
            ProgramParamMange.ProductDataPara = new ProductDataModel()
            {
                UseNowProduct = form.uiCheckBox1.Checked,
                SubtendFaceDisop = form.uiCheckBox2.Checked,
                RegionFaceDisop = form.uiCheckBox3.Checked,
                ProductXY = new Point() { X = form.txt_CountX.IntValue, Y = form.txt_CountY.IntValue },
                CqtFaceStepXY = new Pofloat() { X = (float)form.uiTextBox7.DoubleValue, Y = (float)form.uiTextBox8.DoubleValue },
                BwdFaceSkewingXY = new Pofloat() { X = (float)form.uiTextBox9.DoubleValue, Y = (float)form.uiTextBox10.DoubleValue },
                ReferencePositionXY = new Pofloat() { X = (float)form.uiTextBox11.DoubleValue, Y = (float)form.uiTextBox12.DoubleValue },
                RegionCount = new Point() { X = form.uiTextBox13.IntValue, Y = form.uiTextBox14.IntValue },
                RegionSetp = new Pofloat() { X = (float)form.uiTextBox15.DoubleValue, Y = (float)form.uiTextBox16.DoubleValue },
                CkeckFrontRefPosXY = new Pofloat() { X = (float)form.uiTextBox3.DoubleValue, Y = (float)form.uiTextBox4.DoubleValue },
                ChekBackSideRfePosXY = new Pofloat() { X = (float)form.uiTextBox5.DoubleValue, Y = (float)form.uiTextBox6.DoubleValue },
                PosMarkTableXY1 = new Pofloat() { X = (float)form.uiTextBox17.DoubleValue, Y = (float)form.uiTextBox18.DoubleValue },
                PosMarkTableXY2 = new Pofloat() { X = (float)form.uiTextBox19.DoubleValue, Y = (float)form.uiTextBox20.DoubleValue },
                BackSideXY1 = new Pofloat() { X = (float)form.uiTextBox21.DoubleValue, Y = (float)form.uiTextBox22.DoubleValue },
                BackSideXY2 = new Pofloat() { X = (float)form.uiTextBox23.DoubleValue, Y = (float)form.uiTextBox24.DoubleValue },
            };
            SerializeHelper.SerializeXml<ProductDataModel>(ProgramParamMange.ProductDataPara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\ProductDataModel.xml");
        }
        private void ReadProductData()
        {
            uiCheckBox1.Checked = ProgramParamMange.ProductDataPara.UseNowProduct;
            uiCheckBox2.Checked = ProgramParamMange.ProductDataPara.SubtendFaceDisop;
            uiCheckBox3.Checked = ProgramParamMange.ProductDataPara.RegionFaceDisop;
            txt_CountX.Text = ProgramParamMange.ProductDataPara.ProductXY.X.ToString();
            txt_CountY.Text = ProgramParamMange.ProductDataPara.ProductXY.Y.ToString();
            uiTextBox7.Text = ProgramParamMange.ProductDataPara.CqtFaceStepXY.X.ToString();
            uiTextBox8.Text = ProgramParamMange.ProductDataPara.CqtFaceStepXY.Y.ToString();
            uiTextBox9.Text = ProgramParamMange.ProductDataPara.BwdFaceSkewingXY.X.ToString();
            uiTextBox10.Text = ProgramParamMange.ProductDataPara.BwdFaceSkewingXY.Y.ToString();
            uiTextBox11.Text = ProgramParamMange.ProductDataPara.ReferencePositionXY.X.ToString();
            uiTextBox12.Text = ProgramParamMange.ProductDataPara.ReferencePositionXY.Y.ToString();
            uiTextBox13.Text = ProgramParamMange.ProductDataPara.RegionCount.X.ToString();
            uiTextBox14.Text = ProgramParamMange.ProductDataPara.RegionCount.Y.ToString();
            uiTextBox15.Text = ProgramParamMange.ProductDataPara.RegionSetp.X.ToString();
            uiTextBox16.Text = ProgramParamMange.ProductDataPara.RegionSetp.Y.ToString();
            uiTextBox3.Text = ProgramParamMange.ProductDataPara.CkeckFrontRefPosXY.X.ToString();
            uiTextBox4.Text = ProgramParamMange.ProductDataPara.CkeckFrontRefPosXY.Y.ToString();
            uiTextBox5.Text = ProgramParamMange.ProductDataPara.ChekBackSideRfePosXY.X.ToString();
            uiTextBox6.Text = ProgramParamMange.ProductDataPara.ChekBackSideRfePosXY.Y.ToString();
            uiTextBox17.Text = ProgramParamMange.ProductDataPara.PosMarkTableXY1.X.ToString();
            uiTextBox18.Text = ProgramParamMange.ProductDataPara.PosMarkTableXY1.Y.ToString();
            uiTextBox19.Text = ProgramParamMange.ProductDataPara.PosMarkTableXY2.X.ToString();
            uiTextBox20.Text = ProgramParamMange.ProductDataPara.PosMarkTableXY2.Y.ToString();
            uiTextBox21.Text = ProgramParamMange.ProductDataPara.BackSideXY1.X.ToString();
            uiTextBox22.Text = ProgramParamMange.ProductDataPara.BackSideXY1.Y.ToString();
            uiTextBox23.Text = ProgramParamMange.ProductDataPara.BackSideXY2.X.ToString();
            uiTextBox24.Text = ProgramParamMange.ProductDataPara.BackSideXY2.Y.ToString();

        }
    }
}
