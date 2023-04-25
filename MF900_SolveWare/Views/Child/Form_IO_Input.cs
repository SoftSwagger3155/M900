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
using System.Windows.Forms;

namespace MF900_SolveWare.Views.Child
{
    public partial class Form_IO_Input : Form, IView
    {
        public Form_IO_Input()
        {
            InitializeComponent();
        }

        private void Form_IO_Input_Load(object sender, EventArgs e)
        {
            if(iO != null)
            {
                this.lbl_Input_Name.Text = iO.Name; 
            }
        }

        private void Form_IO_Input_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListening();
        }

        CancellationTokenSource cancelSource = null;
        AutoResetEvent cancelEvent = new AutoResetEvent(false);
        public void StartListening()
        {
            if (iO == null) return;
            if(cancelSource == null) cancelSource = new CancellationTokenSource();  
            Task.Run(() =>
            {
                while (!cancelSource.IsCancellationRequested)
                {
                    if (txb_Status.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            Color color = iO.IsOn() ? Color.Green : Color.Red;
                            txb_Status.BackColor = color;
                        }));
                    }
                 
                    Thread.Sleep(1);
                }

            
            }, cancelSource.Token);
        }
        private void StopListening()
        {
            if (iO == null) return;
            if (cancelSource == null) return;
            cancelSource.Cancel();
        }

        IOBase iO;
        public void Setup<TObj>(TObj obj)
        {
            iO = obj as IOBase; 
        }
    }
}
