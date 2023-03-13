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
    public partial class FormDebugTableTop : Form
    {
        //public static DebugTableTopModel[] debugTableTopModels = new DebugTableTopModel[8];
        private static FormDebugTableTop form;
        public FormDebugTableTop()
        {
            InitializeComponent();
            form = this;
            AddDgvRow();
            ReadRetryOffsetData();
            SetGroupBoxEnable(ProgramParamMange.DebugTableTopPara.IsRetryOffset);
        }
        private void AddDgvRow()
        {
            Dgv_RetryTable.Columns[0].ReadOnly = true;
            for (int i = 0; i < 8; i++)
            {
                Dgv_RetryTable.Rows.Add(new object[] { (i + 1), 0, 0, 0, 0, 0, 0, 0 });
                Dgv_RetryTable.Rows[i].Height = 50;
            }
            GenDgvTable.SetDgvStyle(Dgv_RetryTable);
        }
        public static void SaveRetryOffsetData()
        {
            List<RetryOffsetXYZ> retryOffsetXYZs = new List<RetryOffsetXYZ>();
            for (int i = 0; i < 8; i++)
            {
                retryOffsetXYZs.Add(new RetryOffsetXYZ()
                {
                    UpOffset = new CoordXYZ()
                    {
                        X = float.Parse(form.Dgv_RetryTable[1, i].Value.ToString()),
                        Y = float.Parse(form.Dgv_RetryTable[2, i].Value.ToString()),
                        Z = float.Parse(form.Dgv_RetryTable[3, i].Value.ToString()),
                    },
                    DownOffset = new CoordXYZ()
                    {
                        X = float.Parse(form.Dgv_RetryTable[4, i].Value.ToString()),
                        Y = float.Parse(form.Dgv_RetryTable[5, i].Value.ToString()),
                        Z = float.Parse(form.Dgv_RetryTable[6, i].Value.ToString()),
                    },
                    TestCount = int.Parse(form.Dgv_RetryTable[7, i].Value.ToString())
                });
            }
            ProgramParamMange.DebugTableTopPara = new DebugTableTopModel()
            {
                IsRetryOffset = form.uiCheckBox1.Checked,
                SetRetryCondition = new RetryCondittion()
                {
                    Open = form.uiCheckBox2.Checked,
                    Wng4 = form.uiCheckBox3.Checked,
                    C = form.uiCheckBox4.Checked,
                    Short = form.uiCheckBox5.Checked,
                    Error = form.uiCheckBox6.Checked,
                    Aux = form.uiCheckBox7.Checked
                },
                RetryOffsetData = retryOffsetXYZs
            };
            
            SerializeHelper.SerializeXml(ProgramParamMange.DebugTableTopPara, ParaFliePath.ProductPath + $"{ProgramParamMange.ProductManage.NowProgramName}\\DebugTableTopModels.xml");
        }

        private void ReadRetryOffsetData()
        {
            uiCheckBox1.Checked = ProgramParamMange.DebugTableTopPara.IsRetryOffset;
            uiCheckBox2.Checked = ProgramParamMange.DebugTableTopPara.SetRetryCondition.Open;
            uiCheckBox3.Checked = ProgramParamMange.DebugTableTopPara.SetRetryCondition.Wng4;
            uiCheckBox4.Checked = ProgramParamMange.DebugTableTopPara.SetRetryCondition.C;
            uiCheckBox5.Checked = ProgramParamMange.DebugTableTopPara.SetRetryCondition.Short;
            uiCheckBox6.Checked = ProgramParamMange.DebugTableTopPara.SetRetryCondition.Error;
            uiCheckBox7.Checked = ProgramParamMange.DebugTableTopPara.SetRetryCondition.Aux;
            for (int i = 0; i < 8; i++)
            {
                Dgv_RetryTable[1, i].Value = ProgramParamMange.DebugTableTopPara.RetryOffsetData[i].UpOffset.X;
                Dgv_RetryTable[2, i].Value = ProgramParamMange.DebugTableTopPara.RetryOffsetData[i].UpOffset.Y;
                Dgv_RetryTable[3, i].Value = ProgramParamMange.DebugTableTopPara.RetryOffsetData[i].UpOffset.Z;
                Dgv_RetryTable[4, i].Value = ProgramParamMange.DebugTableTopPara.RetryOffsetData[i].DownOffset.X;
                Dgv_RetryTable[5, i].Value = ProgramParamMange.DebugTableTopPara.RetryOffsetData[i].DownOffset.Y;
                Dgv_RetryTable[6, i].Value = ProgramParamMange.DebugTableTopPara.RetryOffsetData[i].DownOffset.Z;
                Dgv_RetryTable[7, i].Value = ProgramParamMange.DebugTableTopPara.RetryOffsetData[i].TestCount;
            }
        }
        private void uiCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            SetGroupBoxEnable(uiCheckBox1.Checked);
        }
        private void SetGroupBoxEnable(bool enable)
        {
            uiGroupBox1.Enabled = enable;
            uiGroupBox4.Enabled = enable;
        }
    }
}
