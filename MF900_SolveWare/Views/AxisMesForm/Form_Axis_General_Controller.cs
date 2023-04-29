using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.AxisMesForm
{
    public partial class Form_Axis_General_Controller : Form, IView
    {
        public Form_Axis_General_Controller()
        {
            InitializeComponent();
            MakeGroupBox();
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

        public void Setup<TObj>(TObj obj)
        {
           
        }

        private void MakeGroupBox()
        {
            var provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor).Get_All_Items().ToList();
            List<IView> views = new List<IView>();  
            provider.ForEach(item =>
            {
                IView view = new Form_Axis_Simple_Controller_Horizontal();
                view.Setup(item);
                StyleForm(ref view);
                views.Add(view);
            });
            views.Reverse();
            views.ForEach(item =>
            {
                gpb_Content.Controls.Add(item as Form);
            });
        }

        private void StyleForm(ref IView form)
        {
            (form as Form).TopLevel = false;
            (form as Form).Visible = true;
            (form as Form).Width = 1125;
            (form as Form).Height = 50;
            (form as Form).Dock = DockStyle.Top;
            (form as Form).FormBorderStyle = FormBorderStyle.None;
        }

        private void Form_Axis_General_Controller_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(gpb_Content.Controls.Count > 0) {
                foreach (var item in gpb_Content.Controls)
                {
                    (item as Form).Close();
                }
            }
        }

        private void btn_Home_All_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Do_Homing();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            SolveWare.Core.MMgr.Stop();
        }
    }
}
