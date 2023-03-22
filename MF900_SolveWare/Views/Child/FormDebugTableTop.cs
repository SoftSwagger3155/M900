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

namespace MF900
{
    public partial class FormDebugTableTop : Form, IView
    {
        //public static DebugTableTopModel[] debugTableTopModels = new DebugTableTopModel[8];
        private static FormDebugTableTop form;
        public FormDebugTableTop()
        {
            InitializeComponent();
            form = this;
            AddDgvRow();
            //ReadRetryOffsetData();
            //SetGroupBoxEnable(ProgramParamMange.DebugTableTopPara.IsRetryOffset);
        }
        private void AddDgvRow()
        {
            Dgv_RetryTable.Columns[0].ReadOnly = true;
            for (int i = 0; i < 8; i++)
            {
                Dgv_RetryTable.Rows.Add(new object[] { (i + 1), 0, 0, 0, 0, 0, 0, 0 });
                Dgv_RetryTable.Rows[i].Height = 50;
            }
            //GenDgvTable.SetDgvStyle(Dgv_RetryTable);
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

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }
    }
}
