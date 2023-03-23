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

namespace MF900_SolveWare
{
    public partial class FormLoginMarkImage : Form,IView
    {
        public FormLoginMarkImage()
        {
            InitializeComponent();
        }

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }

        private void uiCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            uiGroupBox2.Enabled = uiCheckBox1.Checked;
        }
    }
}
