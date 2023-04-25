using SolveWare_Service_Core;
using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Core.Definition;
using SolveWare_Service_Tool.IO.Base.Abstract;
using SolveWare_Service_Tool.IO.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_IO_Chart : Form, IView
    {
        public Form_IO_Chart()
        {
            InitializeComponent();
            MakeOps();
            MakeIps();
        }
        List<IView> view_OPs = new List<IView>();
        List<IView> view_IPs = new List<IView>();

        private void Form_IO_Chart_Load(object sender, EventArgs e)
        {
           
        }

        private void Form_IO_Chart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(view_IPs.Count > 0) 
            { 
                view_IPs.ForEach(ip => { (ip as Form).Close(); });  
            }
            if (view_OPs.Count > 0)
            {
                view_OPs.ForEach(op => { (op as Form).Close(); });
            }
        }

        public void Setup<TObj>(TObj obj)
        {
           
        }

        private void MakeOps()
        {
            var provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.IO);
            var ops =provider.Get_All_Items().ToList().FindAll(x=> (x as IOBase).IOType == IO_Type.Output);

            ops.ForEach(item =>
            {
                IView view = new Form_IO_Output();
                view.Setup(item);
                StyleForm(ref view);
                (view as Form_IO_Output).StartListening();
                view_OPs.Add(view); 
            });
            view_OPs.Reverse();
            view_OPs.ForEach(x =>
            {
                gpb_Outputs.Controls.Add(x as Form);
            });

        }
        private void MakeIps()
        {
            var provider = SolveWare.Core.MMgr.Get_Single_Tool_Resource(Tool_Resource_Kind.IO);
            var ips = provider.Get_All_Items().ToList().FindAll(x => (x as IOBase).IOType == IO_Type.Input);

            ips.ForEach(item =>
            {
                IView view = new Form_IO_Input();
                view.Setup(item);
                StyleForm(ref view);
                (view as Form_IO_Input).StartListening();
                view_IPs.Add(view);
            });

            view_IPs.Reverse();
            view_IPs.ForEach(x =>
            {
                gpb_Inputs.Controls.Add(x as Form);
            });
        }

        private void StyleForm(ref IView form)
        {
            (form as Form).TopLevel = false;
            (form as Form).Visible = true;
            (form as Form).Width = 400;
            (form as Form).Height = 30;
            (form as Form).Dock = DockStyle.Top;
            (form as Form).FormBorderStyle = FormBorderStyle.None;
        }
    }
}
