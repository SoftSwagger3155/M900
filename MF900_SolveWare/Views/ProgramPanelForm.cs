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
    public partial class ProgramPanelForm : Form
    {
        public IView RunForm;
        public IView ProgramSetForm;
        public IView DebugForm;
        public IView MachineSetForm;
        public IView ParameterSetForm;
        public IView MachineStateForm;
        public IView SystemLogForm;
        public IView MaintainForm;
        public ProgramPanelForm()
        {
            InitializeComponent();
            CreateForm();
        }

        private void CreateForm()
        {
            RunForm = new RunForm();
            ProgramSetForm = new ProgramSetForm();
            DebugForm = new DebugForm();
            MachineSetForm = new MachineSetForm();
            ParameterSetForm = new ParameterSetForm();
            MachineStateForm = new MachineStateForm();
            SystemLogForm = new SystemLogForm();
            MaintainForm = new MaintainingForm();

            BindingForm(this.RunForm, tabPage1);
            BindingForm(this.ProgramSetForm, tabPage2);
            BindingForm(this.DebugForm, tabPage3);
            BindingForm(this.MachineSetForm, tabPage4);
            BindingForm(this.ParameterSetForm, tabPage5);
            BindingForm(this.MachineStateForm, tabPage6);
            BindingForm(this.SystemLogForm, tabPage7);
            BindingForm(this.MaintainForm, tabPage8);
        }
        private void BindingForm(IView childForm, Control panel)
        {
            if (childForm != null)
            {
                Form form = childForm as Form;
                form.TopLevel = false; //将子窗体设置成非顶级控件
                form.Parent = panel;
                form.Dock = DockStyle.Fill;
                form.BringToFront();
                form.Show();
                this.Refresh();
            }
        }

        public void Setup<TObj>(TObj obj)
        {
            throw new NotImplementedException();
        }
    }
}
