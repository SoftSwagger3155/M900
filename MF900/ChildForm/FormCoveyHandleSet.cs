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
    public partial class FormCoveyHandleSet : Form
    {
        FormCoveyHandleSet form;
        public FormCoveyHandleSet()
        {
            InitializeComponent();
            form = this;
        }
        public static void SaveCoveyHand()
        {
            ProgramParamMange.HandPosPara = new HandPosModel()
            {
                SelectEffect = IonFan.无效,
                CoveyHandDown = true,
                WorkPoeceAdsorb = true,
                CoveyHandUp = true,
                AdsorbWait = true,
                CoveyHandShake = true,
            };
            SerializeHelper.SerializeXml(ProgramParamMange.HandPosPara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\HandPosModel.xml");
        }
    }
}
