using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolveWare_Service_Core.Base.Interface;
using Sunny.UI;

namespace MF900_SolveWare
{
    public partial class MainForm : Form
    {
        ProgramPanelForm ProgramPanelForm;
        ProductPanelForm ProductPanelForm;
        public MainForm()
        {
            InitializeComponent();
            ProgramPanelForm = new ProgramPanelForm();
            ProductPanelForm = new ProductPanelForm();
            SwitchForm(true);
            BindingSwitchForm();
        }

        private void SwitchForm(bool isProgramForm)
        {
            FormSwitch.SwitchForm(isProgramForm ? (Form)ProgramPanelForm : (Form)ProductPanelForm, panel1);
        }

        private void BindingSwitchForm()
        {
            ProgramSetForm form = ProgramPanelForm.ProgramSetForm as ProgramSetForm;
            form.SwitchProcessForm += SwitchForm;
            ProgramPanelForm.ProgramSetForm = form;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void uiButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

