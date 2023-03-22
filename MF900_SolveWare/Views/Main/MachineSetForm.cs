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
    public partial class MachineSetForm : Form,IView
    {
        public MachineSetForm()
        {
            InitializeComponent();
        }

        public void Setup<TData>(TData data)
        {
            throw new NotImplementedException();
        }
    }
}
