using SolveWare_Service_Core.Base.Interface;
using SolveWare_Service_Tool.IO.Base.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_IO_Output : Form, IView
    {
        public Form_IO_Output()
        {
            InitializeComponent();
        }

        IOBase iO;
        CancellationTokenSource cancelSource = null;
        AutoResetEvent cancelEvent= new AutoResetEvent(false);  
        public void Setup<TObj>(TObj obj)
        {
            iO = obj as IOBase;
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            try
            {
                if(iO.IsOn())
                    iO.Off();
                else
                    iO.On();

            }
            catch (Exception ex)
            {

            }
        }

        private void Form_IO_Output_Load(object sender, EventArgs e)
        {
            if(iO != null)
            {
                btn_Execute.Text = iO.Name;
            }
        }

        private void Form_IO_Output_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListening();
        }

        public void StartListening()
        {
            if (iO == null) return;
            cancelSource = new CancellationTokenSource();
            Task.Run(() =>
            {
                while (!cancelSource.IsCancellationRequested)
                {
                    if(this.txb_Status.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            Color color = this.iO.IsOn() ? Color.Green : Color.LightGray;
                            this.txb_Status.BackColor = color;
                        }));
                    }               
                    Thread.Sleep(1);
                }

            }, cancelSource.Token);
        }
        private void StopListening()
        {
            if(iO == null) return;
            if(cancelSource == null) return;
            cancelSource.Cancel();
        }
    }
}
