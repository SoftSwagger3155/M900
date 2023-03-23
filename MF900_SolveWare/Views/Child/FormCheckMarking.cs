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
    public partial class FormCheckMarking : Form, IView
    {
        public FormCheckMarking()
        {
            InitializeComponent();
            
        }

        private void uiTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radb_MarkOffset.Checked) 
                uiTabControl2.SelectedTab = tabPage3;
            else if(radb_MarkCount.Checked) 
                uiTabControl2.SelectedTab = tabPage4;
        }

        private void radb_MarkOffset_CheckedChanged(object sender, EventArgs e)
        {
            uiTabControl2.SelectedTab = tabPage3;
        }

        private void radb_MarkCount_CheckedChanged(object sender, EventArgs e)
        {
            uiTabControl2.SelectedTab = tabPage4;
        }
       
        private void uiCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            SetMarkerParaEnbale(uiCheckBox2.Checked ? true : false);
        }
        private void SetMarkerParaEnbale(bool enable)
        {
            radb_MarkOffset.Enabled = enable;
            radb_MarkCount.Enabled = enable;
            uiTabControl2.Enabled = enable;
        }

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }
    }
}
