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
    public partial class FormJipData : Form, IView
    {
        public FormJipData()
        {
            InitializeComponent();
        }

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }
    }
}
