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
    public partial class FormRunOption : Form
    {
        private static FormRunOption form;
        public FormRunOption()
        {
            InitializeComponent();
            form = this;
            ReadRunOptions();
        }
        public static void SaveRunOptions()
        {
            ProgramParamMange.RunOptionPara = new RunOptionsModel()
            {
                UseEVJigRises = form.uiCheckBox1.Checked,
                UseAidBoard = form.uiCheckBox2.Checked,
                CorrectionGetImage = form.uiCheckBox3.Checked,
                ElectricCheckNotGetImage = form.uiCheckBox4.Checked,
                ExceptAutoNgReCheck = form.uiCheckBox14.Checked,
                GetAllImageCheckElectric = form.uiCheckBox5.Checked,
                WorkpieceContactTiming = form.uiCheckBox6.Checked,
                ElectricCheckOnDownJigAsMounting = form.uiCheckBox7.Checked,
                DebugAutoRunLevel = form.uiCheckBox8.Checked,
                UseInitialElectricCheckerMarker = form.uiCheckBox9.Checked,
                EachColumnCorrection = form.uiCheckBox10.Checked,
                CommenstationAutoNgReCheck = form.uiCheckBox11.Checked,
                RegistOfUpperJig = form.uiCheckBox12.Checked,
                AdjustZLevelAutoByConduct = form.uiCheckBox13.Checked,
            };
            SerializeHelper.SerializeXml(ProgramParamMange.RunOptionPara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\RunOptionsModel.xml");
        }
        private void ReadRunOptions()
        {
            uiCheckBox1.Checked = ProgramParamMange.RunOptionPara.UseEVJigRises;
            uiCheckBox2.Checked = ProgramParamMange.RunOptionPara.UseAidBoard;
            uiCheckBox3.Checked = ProgramParamMange.RunOptionPara.CorrectionGetImage;
            uiCheckBox4.Checked = ProgramParamMange.RunOptionPara.ElectricCheckNotGetImage;
            uiCheckBox5.Checked = ProgramParamMange.RunOptionPara.GetAllImageCheckElectric;
            uiCheckBox6.Checked = ProgramParamMange.RunOptionPara.WorkpieceContactTiming;
            uiCheckBox7.Checked = ProgramParamMange.RunOptionPara.ElectricCheckOnDownJigAsMounting;
            uiCheckBox8.Checked = ProgramParamMange.RunOptionPara.DebugAutoRunLevel;
            uiCheckBox9.Checked = ProgramParamMange.RunOptionPara.UseInitialElectricCheckerMarker;
            uiCheckBox10.Checked = ProgramParamMange.RunOptionPara.EachColumnCorrection;
            uiCheckBox11.Checked = ProgramParamMange.RunOptionPara.CommenstationAutoNgReCheck;
            uiCheckBox12.Checked = ProgramParamMange.RunOptionPara.RegistOfUpperJig;
            uiCheckBox13.Checked = ProgramParamMange.RunOptionPara.AdjustZLevelAutoByConduct;
            uiCheckBox14.Checked = ProgramParamMange.RunOptionPara.ExceptAutoNgReCheck;
        }
    }
}
