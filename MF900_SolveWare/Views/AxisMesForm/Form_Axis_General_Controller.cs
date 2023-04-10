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


        public void Setup<TObj>(TObj obj)
        {
           
        }

        private void MakeGroupBox()
        {
            var provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.Motor).Get_All_Items().ToList();

            provider.ForEach(item =>
            {
                IView view = new Form_Axis_Simple_Controller_Horizontal();
                view.Setup(item);
                StyleForm(ref view);
                gpb_Content.Controls.Add(view as Form);
            });
        }

        private void StyleForm(ref IView form)
        {
            (form as Form).TopLevel = false;
            (form as Form).Visible = true;
            (form as Form).Width = 600;
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
    }
}
